using System;
using System.Collections.Generic;
using CarsAppAPI.Models;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;

namespace CarsAppAPI.View
{
    public partial class SearchResults : ContentPage
    {
        PaidCarsViewModel objPC = new PaidCarsViewModel();
        public SearchResults()
        {
            InitializeComponent();
            BindingContext = objPC;
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            objPC.PaidCarsList = new MyPaidCarsModel();
            await objPC.GetPaidCars();
        }
    }
}
