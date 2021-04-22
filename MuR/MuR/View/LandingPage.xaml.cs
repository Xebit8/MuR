using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Murr.View;
using MediaManager;
using Xamarin.Essentials;
using MuR.Model;
using MediaManager.Library;
using System.Windows.Input;

namespace Murr.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public const string PlayCircleOutline = "\U000f040d";
        public const string PauseCircleOutline = "\U000f03e6";

        public LandingPage()
        {
            InitializeComponent();
        }
        public async void PickAudioFile(object sender, EventArgs args)
        {
            var files = (await FilePicker.PickMultipleAsync(CrossFileManipulation.optionsPicket)).ToArray();
            CrossFileManipulation.LoadToCache(files);
        }
        private async void ButtonPlaySong_Clicked(object sender, EventArgs e)
        {
            var selectedAudioItem = (((Button)sender).BindingContext as AudioItem);
            if (selectedAudioItem == null)
                return;

            var result = await CrossMediaManager.Current.PlayQueueItem(selectedAudioItem.Number - 1);

            SetupCurrentItemDetails();

            if (!CrossMediaManager.Current.IsPlaying())
                await CrossMediaManager.Current.Play();
        }
        private void SetupPlaylist(bool isListRefresh)
        {
            List<IMediaItem> queueList = CrossMediaManager.Current.Queue.MediaItems.ToList();

            ObservableCollection<AudioItem> list = new ObservableCollection<AudioItem>();
            for (int i = 0; i < queueList.Count; i++)
            {
                IMediaItem item = queueList[i];

                string title = "-"; string artist = "-";

                if (!string.IsNullOrEmpty(item.DisplayTitle))
                    title = $"{item.DisplayTitle.ToUpper()}";

                if (!string.IsNullOrEmpty(item.Artist))
                    artist = $"{item.Artist.ToUpper()}";

                list.Add(new AudioItem()
                {
                    Title = title,
                    Artist = artist,
                    Number = i + 1
                });
            }

            CollectionView.ItemsSource = list;

            if (isListRefresh)
                CollectionView.ScrollTo(list.Last());
        }

        private void SetupCurrentItemDetails()
        {
            IMediaItem currentAudioItem = CrossMediaManager.Current.Queue.Current;

            // Media item details
            var displayDetails = string.Empty;
            if (!string.IsNullOrEmpty(currentAudioItem.DisplayTitle))
                displayDetails = currentAudioItem.DisplayTitle;

            if (!string.IsNullOrEmpty(currentAudioItem.Artist))
                displayDetails = $"{displayDetails} - {currentAudioItem.Artist}";

            LabelCurrentTrackTitle.Text = displayDetails.ToUpper();

            LabelCurrentTrackIndex.Text = $"CURRENT TRACK: {CrossMediaManager.Current.Queue.CurrentIndex + 1}/{CrossMediaManager.Current.Queue.Count}";
        }

        public class AudioItem
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public int Number { get; set; }
        }
        public async void ToPlayerPage(object sender, EventArgs args)
        {
                await Navigation.PushAsync(new PlayerPage());
        }

    }
}   