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

namespace CarsAppAPI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Uri urlBase = new Uri(StaticConstants.UrlBase);
        #region constructors
        public MainViewModel()
        {
            
            //ListaAuto = new AutoModel();
            //ConsultaListaAutosGetCommand = new Command(async () => { await ConsultaListaAutosGet(); });
            /*ConsultaListaAutosPostCommand = new Command(async () => await ConsultaListaAutosPost());
            ConsultaListaAutosPutCommand = new Command(async () => await ConsultaListaAutosPut());
            ConsultaListaAutosDeleteCommand = new Command(async () => await ConsultaListaAutosDelete());*/
            LoginCommand = new Command(async () => { await Login(); });
            LoginGuestCommand = new Command(async () => { await LoginGuest(); });
            GetAutoByIdCommand = new Command(async () => await GetAutoById());
        }
        #endregion

        #region BindingVariables
        private LoginModel loginResponsetList;
        public LoginModel LoginRequestList
        {
            get { return loginResponsetList; }
            set { loginResponsetList = value; RaisePropetyChanged(); }
        }

        private AutoModel listaAuto;
        public AutoModel ListaAuto
        {
            get { return listaAuto; }
            set { listaAuto = value; RaisePropetyChanged(); }
        }

        private string user;
        public string User
        {
            get { return user; }
            set { user = value; RaisePropetyChanged(); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropetyChanged(); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropetyChanged(); }
        }

        private string passwordConfirmation;
        public string PasswordConfirmation
        {
            get { return passwordConfirmation; }
            set { passwordConfirmation = value; RaisePropetyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; RaisePropetyChanged(); }
        }
        private string placa;
        public string Placa
        {
            get { return placa; }
            set { placa = value; RaisePropetyChanged(); }
        }
        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; RaisePropetyChanged(); }
        }
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropetyChanged(); }
        }
        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; RaisePropetyChanged(); }
        }
        
        private bool showCarR;
        public bool ShowCarR
        {
            get { return showCarR; }
            set { showCarR = value; RaisePropetyChanged(); }
        }

        #endregion

        #region PostMethods 
        public ICommand LoginCommand { get; set; }
        public async Task Login()
        {
            UserDialogs.Instance.ShowLoading("Bienvenido");
            var paramsPost = new { email = Email, password = Password };
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(urlBase + StaticConstants.LoginUrl, content);
            Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Login" + json);
                loginResponsetList = JsonConvert.DeserializeObject<LoginModel>(json);
                StaticConstants.Token = loginResponsetList.token;
                if (loginResponsetList.token != null)
                {
                    //await App.Current.MainPage.DisplayAlert("Bienvenido", "", "ok");
                    StaticConstants.isLogged = true;
                    if (StaticConstants.isRegisteredFromGuest == false)
                    {
                        await Shell.Current.GoToAsync("//Select");
                    }
                    
                }
                UserDialogs.Instance.HideLoading();

            }
            else
            {
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", "verifica" +
                    " tu usuario y contraseña", "Aceptar");
            }
        }

        public ICommand LoginGuestCommand { get; set; }
        public async Task LoginGuest()
        {
            var paramsPost = new { email = Email, password = Password };
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(urlBase + StaticConstants.LoginUrl, content);
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Login Guest " + json);
                loginResponsetList = JsonConvert.DeserializeObject<LoginModel>(json);
                StaticConstants.Token = loginResponsetList.token;
                if (loginResponsetList.token != null)
                {
                    StaticConstants.isLogged = false;
                    StaticConstants.showBackLabel = false;
                    await Shell.Current.GoToAsync("//Select");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", "Verifica tu usuario " +
                    "y contraseña", "Aceptar");
            }
        }
        #endregion

        #region GetMethods

        /*public ICommand ConsultaListaAutosGetCommand { get; set; }
        public async Task ConsultaListaAutosGet()
        {
            UserDialogs.Instance.ShowLoading("Busqueda");
            Console.WriteLine("Token: " + StaticConstants.Token);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            var response = await client.GetAsync(urlBase + StaticConstants.GetCarsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                UserDialogs.Instance.HideLoading();
                bool res = await App.Current.MainPage.DisplayAlert("Auto localizado",
                    "Para continuar al pago presione Pagar", "Pagar","no pagar");

                if(res && StaticConstants.isLogged)
                {
                    await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
                    var json = await response.Content.ReadAsStringAsync();
                    Preferences.Set("carresponse",json);
                    Console.WriteLine("List of cars: " + json);
                    //ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                } else if(res && !StaticConstants.isLogged)
                {
                    await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
                }

            } else
            {
                UserDialogs.Instance.HideLoading();
            }
        }*/

        public ICommand GetAutoByIdCommand { get; set; }
        public async Task GetAutoById()
        {
            if (Placa != null && Placa != "")
            {
                PaidCarsViewModel objPCVM = new PaidCarsViewModel();
                await objPCVM.carWasFound();
                MyPaidCarsModel customObj = StaticConstants.objCarsPaid;
                Console.WriteLine(" lista  " + customObj);
                bool plateIsPaid = false;
                if (customObj != null)
                {
                    foreach (var obj in customObj.payments)
                    {
                        if (obj.car_payment_info.license_plate.ToString() == Placa)
                        {
                            Console.WriteLine(" Plate  " + obj.car_payment_info.license_plate.ToString());
                            Console.WriteLine(" current plate  " + placa);
                            plateIsPaid = true;
                            break;
                        }
                    }
                }
                if (plateIsPaid == false)
                {
                    UserDialogs.Instance.ShowLoading("Buscando");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
                    var response = await client.GetAsync(StaticConstants.UrlBase +
                    StaticConstants.GetCarsByIdEndpoint + Placa);
                    Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        UserDialogs.Instance.HideLoading();
                        bool res = await App.Current.MainPage.DisplayAlert("Auto localizado",
                           "¿Desea continuar? costo de búsqueda $100 MXN", "Aceptar", "Cancelar");

                        if (res && StaticConstants.isLogged)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            Preferences.Set("carresponse", json);
                            Console.WriteLine("List of cars: " + json);
                            ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                            StaticConstants.currentPlate = ListaAuto.car.license_plate;
                            await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
                        }
                        else if (res && !StaticConstants.isLogged)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            Preferences.Set("carresponse", json);
                            ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                            StaticConstants.currentPlate = ListaAuto.car.license_plate;
                            await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await App.Current.MainPage.DisplayAlert("Auto no localizado",
                            "revisa que la información sea correcta", "Aceptar");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Ya haz buscado este auto, ve a mis búsquedas", "", "Aceptar");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Llena todos los campos para la búsqueda", "", "Aceptar");
            }


        }
        #endregion

        #region PutMethods
        /*public ICommand ConsultaListaAutosPutCommand { get; set; }
        public async Task ConsultaListaAutosPut()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo, id = Id };

            ListaAutos = await webApi.executeRequestPut<ObservableCollection<LoginModel>>(paramsPost);
        }*/
        #endregion

        #region DeleteMethods
        /*public ICommand ConsultaListaAutosDeleteCommand { get; set; }
        public async Task ConsultaListaAutosDelete()
        {
            string stParamsGet = $"id={id}";

            ListaAutos = await webApi.executeRequestDelete<ObservableCollection<LoginModel>>(stParamsGet);
        }*/
        #endregion

    }
}
