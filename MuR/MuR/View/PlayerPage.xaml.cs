using System;
using System.Linq;
using MediaManager;
using MediaManager.Media;
using MuR.Model;
using Xamarin.Essentials;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MuR.Model.SQLiteObjects;
using PositionChangedEventArgs = MediaManager.Playback.PositionChangedEventArgs;

namespace MuR.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        /// <summary>
        /// объект вида плеера
        /// </summary>
        public MuR.ViewModel.AudioPlayerViewModel viewModel { get; protected set; }
        public PlayerPage(IList<Audio> audios)
        {
            InitializeComponent();

            PlayItems(audios);
            CrossMediaManager.Current.PositionChanged += (object sender, PositionChangedEventArgs e) =>
            {
                viewModel.Duration = e.Position;
            };
            CrossMediaManager.Current.MediaItemChanged += (object sender, MediaItemEventArgs e) =>
            {
                var media = e.MediaItem;
                viewModel = 
                new MuR.ViewModel.AudioPlayerViewModel(
                    App.Database.SelectAllFromTable<Audio>().First(audio => audio.UriFile == media.FileName), media.Duration);
                
                this.BindingContext = viewModel;
                if (!CrossMediaManager.Current.IsPlaying())
                    CrossMediaManager.Current.Play();
            };
        }
        private void PlayItems(IList<Audio> audios)
        {
            foreach (Audio audio in audios)
            {
                CrossMediaManager.Current.Queue.Add(CrossFileManipulation.GetAudio(audio.UriFile));
            }
            CrossMediaManager.Current.Play();
        }
        /// <summary>
        /// выбрать файлы из файлововй системы
        /// </summary>
        /// <param name="sender">аргументы передаваемые с вызовом метода</param>
        /// <param name="args">объект который вызывает метод</param>
        public async void PickAudioFile(object sender, EventArgs args)
        {
            var files = (await FilePicker.PickMultipleAsync(CrossFileManipulation.optionsPicket)).ToArray();
            CrossFileManipulation.LoadToCache(files);
        }
        /// <summary>
        /// на предыдущую страницу
        /// </summary>
        private async void ToBack(object sender, EventArgs e)
        {
            App.Player = await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            App.Player = this;
        }
    }
}