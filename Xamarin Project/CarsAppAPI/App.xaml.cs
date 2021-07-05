using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarsAppAPI.View;

namespace CarsAppAPI
{
    
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new CarsAppAPI.Master());
            /*NavigationPage navPage = new NavigationPage(new CarsAppAPI.View.login());
            MainPage = navPage;*/
            MainPage = new AppShell();
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
