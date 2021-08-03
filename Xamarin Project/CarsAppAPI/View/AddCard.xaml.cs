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
        protected override async void OnAppearing()
        {
            name.Text = "";
            number.Text = "";
            year.Text = "";
            month.Text = "";
        }
    }
}
