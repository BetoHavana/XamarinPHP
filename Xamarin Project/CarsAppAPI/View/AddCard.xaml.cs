using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CarsAppAPI.View
{
    public partial class AddCard : ContentPage
    {
        public AddCard()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            name.Text = "";
            number.Text = "";
            year.Text = "";
            month.Text = "";
            cvv.Text = "";
        }
        private async void Back2PaymentOptions(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
        }
    }
}
