using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsAppAPI.Models;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        public RegisterUser()
        {
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();
        }

        public void cleanForm()
        {
            name.Text = "";
            email.Text = "";
            password.Text = "";
            repass.Text = "";
            lastname.Text = "";
        }
        protected override void OnAppearing()
        {
            name.Text = "";
            email.Text = "";
            password.Text = "";
            repass.Text = "";
            lastname.Text = "";
        }
        private async void BackLogin(object sender, EventArgs e)
        {
            
            if (StaticConstants.showBackLabel)
            {
                await Shell.Current.GoToAsync($"//{nameof(Login)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(Select)}");
            }
        }
    }
}