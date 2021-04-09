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

namespace Murr.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
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

            await CrossMediaManager.Current.Play();
        }
    }
}