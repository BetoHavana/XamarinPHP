using System;
using CarsAppAPI.View;
using Xamarin.Forms;

namespace CarsAppAPI.ViewModels
{
    public class RegisterViewModel : ContentPage
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}

