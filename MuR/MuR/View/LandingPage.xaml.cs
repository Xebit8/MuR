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


namespace Murr.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {

        public LandingPage()
        {
            InitializeComponent();
        }
        
        public void LocalToPlay()
        {
            string[] fileTypes = null;
            if (Device.RuntimePlatform == Device.Android)
            {
                fileTypes = new string[] { "audio/mpeg" };
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                fileTypes = new string[] { "public.audio" };
            }
        }

        public async void ToPlay(object  sender, EventArgs args)
        {
            string audiourl = "https://ia800605.us.archive.org/32/items/Mp3Playlist_555/Daughtry-Homeacoustic.mp3";
            await CrossMediaManager.Current.Play(audiourl);
        }

    }
}