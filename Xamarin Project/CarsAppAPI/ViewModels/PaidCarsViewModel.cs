using CarsAppAPI.Models;
using CarsAppAPI.Service;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using Xamarin.Essentials;
using Newtonsoft.Json;
using CarsAppAPI.View;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Dynamic;

namespace CarsAppAPI.ViewModels
{
    public class PaidCarsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Uri urlBase = new Uri(StaticConstants.UrlBase);
        #region constructors
        public PaidCarsViewModel()
        {
            GetPaidCarsCommand = new Command(async () =>
             {
                 await GetPaidCars();
             });
            carWasFoundCommand = new Command(async () =>
            {
                await carWasFound();
            });


        }
        #endregion

        #region BindingVariables
        private MyPaidCarsModel paidcarslist;
        public MyPaidCarsModel PaidCarsList
        {
            get { return paidcarslist; }
            set { paidcarslist = value; RaisePropetyChanged(); }
        }

        private List<MyPaidCarsModel> paidcarslist2;
        public List<MyPaidCarsModel> PaidCarsList2
        {
            get { return paidcarslist2; }
            set { paidcarslist2 = value; RaisePropetyChanged(); }
        }


        #endregion


        #region GetMethods

        public ICommand GetPaidCarsCommand { get; set; }
        public async Task GetPaidCars()
        {
            if (StaticConstants.isLogged)
            {
                UserDialogs.Instance.ShowLoading("Buscando información");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
                var response = await client.GetAsync(urlBase + StaticConstants.GetPaidCars);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    UserDialogs.Instance.HideLoading(); Console.WriteLine("List of cars: " + json);
                    PaidCarsList = JsonConvert.DeserializeObject<MyPaidCarsModel>(json);
                    Console.WriteLine("List of cars: " + PaidCarsList);
                    StaticConstants.objCarsPaid = PaidCarsList;
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Registrate", "para ver esta sección, debes estar registrado", "Aceptar");
            }

        }


        public ICommand carWasFoundCommand { get; set; }
        public async Task carWasFound()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            var response = await client.GetAsync(urlBase + StaticConstants.GetPaidCars);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("List of cars: " + json);
                PaidCarsList = JsonConvert.DeserializeObject<MyPaidCarsModel>(json);
                Console.WriteLine("List of cars: " + PaidCarsList);
                StaticConstants.objCarsPaid = PaidCarsList;

            }
        }

        #endregion



    }
}
