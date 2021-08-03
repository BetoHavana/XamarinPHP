using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsAppAPI.Models;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace CarsAppAPI.View
{
    public partial class Select : ContentPage
    {
        public bool showList = false;
        public Select()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("*****OnSelect ");
            MainViewModel objMVM = new MainViewModel();
            modelo.Text = "";
            placa.Text = "";
            
            if(StaticConstants.showCarResponse)
            {
                layout.IsVisible = true;
                objMVM.ShowCarR = true;
                var json = Preferences.Get("carresponse","default_value");
                Console.WriteLine("***** JSON "+json);
                objMVM.ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
            }
            else
            {
                layout.IsVisible = false;
                objMVM.ShowCarR = false;
                Console.WriteLine("***** FALSE " );
            }
        }
    }
}