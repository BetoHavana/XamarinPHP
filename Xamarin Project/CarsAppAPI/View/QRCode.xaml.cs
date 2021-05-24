using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCode : ContentPage
    {
        public QRCode()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Scanner();
        }

        private async void Scanner()
        {
            var scannerPage = new ZXingScannerPage();

            scannerPage.Title = "Lector de QR";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    DisplayAlert("Valor Obtenido", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(scannerPage);
        }
    }
}