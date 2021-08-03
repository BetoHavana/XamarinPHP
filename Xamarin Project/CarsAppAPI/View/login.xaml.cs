using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarsAppAPI.ViewModels;
using CarsAppAPI.Models;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //this.BindingContext = new  MainViewModel(Navigation);
            
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//Login/Register");
            await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
        }
        private async void LogAsGuest(object sender, EventArgs e)
        {
            MainViewModel objMainVM = new MainViewModel();
            objMainVM.Email = "usertest@test.com";
            objMainVM.Password = "Usertest1234.";
            await objMainVM.LoginGuest();
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