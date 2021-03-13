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
using SpotifyAPI.Web;


namespace Murr.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {

        public LandingPage()
        {
            InitializeComponent();
        }
        //public async void ToPlay(object sender, EventArgs e)
        //{
            
        //}
        //public async void ToPause(object sender, EventArgs e)
        //{
            //await CrossMediaManager.Current.Pause();
        //}

    }
}