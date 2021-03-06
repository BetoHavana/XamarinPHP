using CarsAppAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI.View
{
    public partial class StartupPage : ContentPage
    {
        public StartupPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CheckLogin();
        }

        private async Task CheckLogin()
        {
            // should check for valid login instead
            await Task.Delay(2000);

            // only open Login page when no valid login
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
