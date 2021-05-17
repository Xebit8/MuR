using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using MuR.Model.SQLiteObjects;

namespace MuR.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        //public const string PlayCircleOutline = "\U000f040d";
        //public const string PauseCircleOutline = "\U000f03e6";
        public MuR.ViewModel.AudioListViewModel viewModel { get; set; }

        public LandingPage()
        {
            // отображатся будут все аудио
            viewModel = new ViewModel.AudioListViewModel(App.Database.SelectAllFromTable<Audio>()) { Navigation = this.Navigation };

            if(viewModel != null)
                BindingContext = viewModel;

            InitializeComponent();
        }
    }
}   