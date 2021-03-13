﻿using Murr.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Murr
{
    public partial class App : Application
    {
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