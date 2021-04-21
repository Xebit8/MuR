using System;
using System.Collections.Generic;
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

namespace Murr.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        int play_counter = 0;
        int menu_counter = 0;
        public LandingPage()
        {
            InitializeComponent();
        }
        public async void PickAudioFile(object sender, EventArgs args)
        {
            var files = (await FilePicker.PickMultipleAsync(CrossFileManipulation.optionsPicket)).ToArray();
            CrossFileManipulation.LoadToCache(files);
        }
        public async void LocalToPlay(object sender, EventArgs args)
        {
            foreach (var item in CrossFileManipulation.LoadFromExternalCache())
                CrossMediaManager.Current.Queue.Add(item);

            IMediaItem currentAudioItem = CrossMediaManager.Current.Queue.Current;
            SongData(currentAudioItem);

<<<<<<< HEAD
            
=======
            play_counter++;
>>>>>>> 99855ab85a0186c663e06fa2c9d07e1ab6ec4da9

            if (play_counter % 2 != 0)
            {
                PlayBtn.Source = "Resources/drawable/pause.png";

                await CrossMediaManager.Current.PlayPause();
            }
<<<<<<< HEAD
            else if (counter == 0)
            {
                PlayBtn.Source = "Resources/drawable/pause.png";

                await CrossMediaManager.Current.Play();
            }
            else if (counter % 2 == 0)
=======
            else if (play_counter % 2 == 0)
>>>>>>> 99855ab85a0186c663e06fa2c9d07e1ab6ec4da9
            {
                PlayBtn.Source = "Resources/drawable/play.png";

                await CrossMediaManager.Current.Pause();
            }
<<<<<<< HEAD
=======
            else if (play_counter == 0)
            {
                PlayBtn.Source = "Resources/drawable/pause.png";
>>>>>>> 99855ab85a0186c663e06fa2c9d07e1ab6ec4da9

            counter++;
        }
        public async void SkipFwd(object sender, EventArgs args)
        {
            await CrossMediaManager.Current.PlayNext();
        }
        public async void SkipBack(object sender, EventArgs args)
        {
            await CrossMediaManager.Current.PlayPrevious();
        }
        public void SongData(IMediaItem currentAudioItem)
        {

            song_label.Text = currentAudioItem.Title;
            artist_label.Text = currentAudioItem.Artist;
        }
        public void Menu(object sender, EventArgs args)
        {

            play_counter++;

            if (play_counter % 2 != 0)
            {
                BackgroundImageSource = "Resources/drawable/background2.png";
                songPic.Source = "Resources/drawable/examle2.png";
            }
            else if (play_counter % 2 == 0)
            {
                BackgroundImageSource = "Resources/drawable/background.png";
                songPic.Source = "Resources/drawable/examle.png";
            }
        } 
    }
}