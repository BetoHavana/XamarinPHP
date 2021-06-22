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
    public partial class QRScan : ContentPage
    {
        public QRScan()
        {
            InitializeComponent();
        }
        private void ScanView_OnScanResult(ZXing.Result result)
        {
            Scanner();
        }

        private async void Scanner()
        {
            var scannerPage = new ZXingScannerPage();

            //scannerPage.Title = "Lector de QR";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    DisplayAlert("Resultado Scanner", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(scannerPage);
        }

        void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = result.Text + " (type: " + result.BarcodeFormat.ToString() + ")";
                DisplayAlert("Resultado Scanner", result.Text, "OK");
            });
        }


    }
}