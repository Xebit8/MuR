using System;
using System.Linq;
using MediaManager;
using MediaManager.Media;
using MuR.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        public PlayerPage()
        {
            InitializeComponent();

            PlayItem();
            CrossMediaManager.Current.PositionChanged += (object sender, PositionChangedEventArgs e) =>
            {
                viewModel.Duration = e.Position;
            };
            CrossMediaManager.Current.MediaItemChanged += (object sender, MediaItemEventArgs e) =>
            {
                var media = e.MediaItem;
                viewModel = 
                new MuR.ViewModel.AudioPlayerViewModel(
                    App.Database.DBConnection.FindWithQueryAsync<MuR.Model.SQLiteObjects.Audio>("SELECT * FROM audio WHERE uri_file = ?", media.FileName).Result, media.Duration);

                this.BindingContext = viewModel;
                if (!CrossMediaManager.Current.IsPlaying())
                    CrossMediaManager.Current.Play();
            };
        }
        public void PlayItem()
        {
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
        // может быть понадобится
        //public async void LocalToPlay(object sender, EventArgs args)
        //{
        //    // удалить после использования
        //    foreach (var item in CrossFileManipulation.LoadFromExternalCache())
        //    {
        //        MuR.Model.SQLiteObjects.Audio audio = await App.Database.DBConnection.FindWithQueryAsync<MuR.Model.SQLiteObjects.Audio>("SELECT * FROM audio WHERE uri_file = ?", item.FileName);
        //        if ( audio == null)
        //           await App.Database.InsertIntoTable<MuR.Model.SQLiteObjects.Audio>(new MuR.Model.SQLiteObjects.Audio() { NameAudio = item.DisplayTitle, UriFile = item.FileName, UriImage = "Resources/drawable/examle2.png" });
        //    }
        //}
        private async void ToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}