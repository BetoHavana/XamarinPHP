using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new CarsAppAPI.Master());
            NavigationPage navPage = new NavigationPage(new CarsAppAPI.View.login());
            MainPage = navPage;
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
