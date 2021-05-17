using MuR.Model;
using MuR.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MuR
{
    public partial class App : Application
    {
        private static Page _player;
        private static DatabaseRepository database = new DatabaseRepository();
        public static DatabaseRepository Database => database;
        public static Page Player // оптимизировать
        {
            get { return _player; }
            set
            {
                if (value is MuR.View.PlayerPage)
                    _player = value;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
