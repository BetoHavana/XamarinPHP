using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarsAppAPI.ViewModels;

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
            //await Shell.Current.GoToAsync("//login/registration");
            await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
        }

    }
}