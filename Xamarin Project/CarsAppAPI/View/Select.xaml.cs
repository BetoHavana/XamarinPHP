using System;
using CarsAppAPI.Models;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CarsAppAPI.View
{
    public partial class Select : ContentPage, INotifyPropertyChanged
    {
        public bool showList = false;
        MainViewModel objMVM = new MainViewModel();
        public event PropertyChangedEventHandler PropertyChanged2;

        public void RaisePropetyChanged2([CallerMemberName] string propertyName = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private AutoModel listaAuto2;
        public AutoModel ListaAuto2
        {
            get { return listaAuto2; }
            set { listaAuto2 = value; RaisePropetyChanged2(); }
        }
        public Select()
        {
            InitializeComponent();
            BindingContext = objMVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine("*****OnSelect ");
            
            modelo.Text = "";
            placa.Text = "";
            
            if(StaticConstants.showCarResponse)
            {
                layout.IsVisible = true;
                objMVM.ShowCarR = true;
                /*var json = Preferences.Get("carresponse","default_value");
                Console.WriteLine("***** JSON "+json);
                ListaAuto2 = JsonConvert.DeserializeObject<AutoModel>(json);
                objMVM.Placa = ListaAuto2.car.license_plate;
                await objMVM.GetAutoById();*/
                StaticConstants.showCarResponse = false;
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