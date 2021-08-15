using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarsAppAPI.ViewModels;
using CarsAppAPI.Models;
using Acr.UserDialogs;

namespace CarsAppAPI.View
{
    public partial class Login : ContentPage
    {
        ActivityIndicator activityIndicator;
        public Login()
        {
            InitializeComponent();

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//Login/Register");
            await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
        }
        private async void LogAsGuest(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Bienvenido");
            //Indicator.IsRunning = true;
            MainViewModel objMainVM = new MainViewModel();
            objMainVM.Email = "usertest@test.com";
            objMainVM.Password = "Usertest1234.";
            await objMainVM.LoginGuest();
            UserDialogs.Instance.HideLoading();
            //Indicator.IsRunning = false;
            await Shell.Current.GoToAsync($"//{nameof(InitialPage)}");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            StaticConstants.showBackLabel = true;
            email.Text = "";
            password.Text = "";
        }

    }
}