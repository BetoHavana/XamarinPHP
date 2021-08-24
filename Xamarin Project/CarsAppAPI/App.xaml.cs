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
            MainPage = new AppShell();
            //Background color
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);

            //Title color
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
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
