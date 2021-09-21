using System;
using System.Threading.Tasks;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentOptions : ContentPage
    {
        PaymentViewModel payment = new PaymentViewModel();
        public PaymentOptions()
        {
            InitializeComponent();
            BindingContext = payment;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await payment.GetCards();
        }

        private async void Back2Select(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Select)}");
        }
    }
}
