using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MediaManager;
using MuR.Model;

namespace MuR.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : Shell
    {
        public async void PlayerShellContentClicked(object sender, EventArgs args)
        {
            foreach (var item in await App.Database.SelectAllFromTable<MuR.Model.SQLiteObjects.Audio>())
                CrossMediaManager.Current.Queue.Add(CrossFileManipulation.GetAudio(item.UriFile));
        }
    }
}