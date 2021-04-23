using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MuR.View;
using MediaManager;
using Xamarin.Essentials;
using MuR.Model;
using MediaManager.Library;
using MediaManager.Media;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using PositionChangedEventArgs = MediaManager.Playback.PositionChangedEventArgs;

namespace MuR.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        int play_counter = 0;

        public PlayerPage()
        {
            InitializeComponent();

            CrossMediaManager.Current.StateChanged += Current_OnStateChanged;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
        }
        public async void PickAudioFile(object sender, EventArgs args)
        {
            var files = (await FilePicker.PickMultipleAsync(CrossFileManipulation.optionsPicket)).ToArray();
            CrossFileManipulation.LoadToCache(files);
        }
        public async void LocalToPlay(object sender, EventArgs args)
        {
            // удалить после использования
            foreach (var item in CrossFileManipulation.LoadFromExternalCache())
            {
                MuR.Model.SQLiteObjects.Audio audio = await App.Database.DBConnection.FindWithQueryAsync<MuR.Model.SQLiteObjects.Audio>("SELECT * FROM audio WHERE uri_file = ?", item.FileName);
                if (audio == null)
                    await App.Database.InsertIntoTable<MuR.Model.SQLiteObjects.Audio>(new MuR.Model.SQLiteObjects.Audio() { NameAudio = item.DisplayTitle, UriFile = item.FileName, UriImage = "Resources/drawable/examle2.png" });
            }

            foreach (var item in await App.Database.SelectAllFromTable<MuR.Model.SQLiteObjects.Audio>())
                CrossMediaManager.Current.Queue.Add(CrossFileManipulation.GetAudio(item.UriFile));

            play_counter++;

            if (play_counter == 0)
            {
                PlayBtn.Source = "Resources/drawable/pause.png";

                await CrossMediaManager.Current.Play();
            }
            else if (play_counter % 2 != 0)
            {
                PlayBtn.Source = "Resources/drawable/pause.png";

                await CrossMediaManager.Current.PlayPause();
            }
            else if (play_counter % 2 == 0)
            {
                PlayBtn.Source = "Resources/drawable/play.png";

                await CrossMediaManager.Current.Pause();
            }
        }
        private async void SkipFwd(object sender, EventArgs args)
        {
            await CrossMediaManager.Current.PlayNext();
        }
        private async void SkipBack(object sender, EventArgs args)
        {
            await CrossMediaManager.Current.PlayPrevious();
        }
        private void Shuffle(object sender, EventArgs args)
        {
            if(CrossMediaManager.Current.ShuffleMode == ShuffleMode.All)
                CrossMediaManager.Current.ShuffleMode = ShuffleMode.Off;
            else
                CrossMediaManager.Current.ShuffleMode = ShuffleMode.All;
        }
        private void Repeat(object sender, EventArgs args)
        {
            if (CrossMediaManager.Current.RepeatMode == RepeatMode.All)
            { CrossMediaManager.Current.RepeatMode = RepeatMode.Off; }
            else if (CrossMediaManager.Current.RepeatMode == RepeatMode.Off)
            { CrossMediaManager.Current.RepeatMode = RepeatMode.All; }
        }
        //private async void ClearAll(object sender, EventArgs args)
        //{
            //CrossMediaManager.Current.Queue.Clear;
        //} 
        private void SetupCurrentMediaDetails(IMediaItem currentMediaItem)
        {
            // Set up Media item details in UI
            var songDetails = string.Empty;
            if (!string.IsNullOrEmpty(currentMediaItem.DisplayTitle))
                songDetails = currentMediaItem.DisplayTitle;

            if (!string.IsNullOrEmpty(currentMediaItem.Artist))
                songDetails = $"{songDetails} - {currentMediaItem.Artist}";

            song_label.Text = songDetails.ToUpper();
        }
        private void SetupCurrentMediaPlayerState(MediaPlayerState currentPlayerState)
        {
            if (currentPlayerState == MediaManager.Player.MediaPlayerState.Loading)
            {
                SongSlider.Value = 0;
            }
            else if (currentPlayerState == MediaManager.Player.MediaPlayerState.Playing
                    && CrossMediaManager.Current.Duration.Ticks > 0)
            {
                SongSlider.Maximum = CrossMediaManager.Current.Duration.Ticks;
            }
        }
        private void SetupCurrentMediaPositionData(TimeSpan currentPlaybackPosition)
        {
            var formattingPattern = @"hh\:mm\:ss";
            if (CrossMediaManager.Current.Duration.Hours <= 0)
                formattingPattern = @"mm\:ss";

            var fullLengthString = CrossMediaManager.Current.Duration.ToString(formattingPattern);
            PositionLabel.Text = $"{currentPlaybackPosition.ToString(formattingPattern)}/{fullLengthString}";

            SongSlider.Value = currentPlaybackPosition.Ticks;

            
        }
        private void Current_MediaItemChanged(object sender, MediaItemEventArgs e)
        {
            SetupCurrentMediaDetails(e.MediaItem);
        }
        private void Current_PositionChanged(object sender, PositionChangedEventArgs args)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPositionData(args.Position);
            });
        }

        private void Current_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SetupCurrentMediaPlayerState(e.State);
            });
        }
        private async void ToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void SongSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {

        }
    }
}