using System;
using System.Collections.Generic;
using CarsAppAPI.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        /*protected override async void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            string source =
                args.Source != null ? args.Source.ToString() : "";

            string previous =
                args.Previous != null ? args.Previous.Location.ToString() : "";

            string current =
                args.Current != null ? args.Current.Location.ToString() : "";

            string mensaje = $"Se realizó {source} desde " +
                $"{previous} hacia {current}";

            await DisplayAlert("Navegación", mensaje, "Ok");
        }*/
        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}
