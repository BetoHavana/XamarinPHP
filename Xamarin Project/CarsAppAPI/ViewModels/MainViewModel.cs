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

        Uri urlBase = new Uri("https://ruffsstudios.com/api_carros/public");
        WebApiClientService webApi = new WebApiClientService();

        #region constructors
        public MainViewModel(INavigation navigation)
        {
            this.Navigation = navigation; 

        }
        public MainViewModel()
        {
            ConsultaListaAutosGetCommand = new Command(async () => await ConsultaListaAutosGet());
            ConsultaListaAutosPostCommand = new Command(async () => await ConsultaListaAutosPost());
            ConsultaListaAutosPutCommand = new Command(async () => await ConsultaListaAutosPut());
            ConsultaListaAutosDeleteCommand = new Command(async () => await ConsultaListaAutosDelete());
            LoginCommand = new Command(async () => await Login());
            GetAutoByIdCommand = new Command(async () => await GetAutoById());
        }
        #endregion

        #region BindingVariables

        StaticConstants cts = new StaticConstants();
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

        private ObservableCollection<LoginModel> listaAutos;
        public ObservableCollection<LoginModel> ListaAutos
        {
            get { return listaAutos; }
            set { listaAutos = value; RaisePropetyChanged(); }
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

        

        private AutoModel data;
        public AutoModel Data
        {
            get => data;
            set { data = value; RaisePropetyChanged(); }
        }
        #endregion

        #region PostMethods 
        public ICommand LoginCommand { get; set; }
        public async Task Login()
        {
            var paramsPost = new { email = Email, password = Password };
            //ListaEmpleados = await webApi.executeRequestPostLogin<ObservableCollection<EmpleadoModel>>(paramsPost);
            string requestUri = "/api/v1/auth/login";
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("content " + content);
            Console.WriteLine("Body" + jsonData.ToString());
            HttpResponseMessage response = await client.PostAsync(urlBase + requestUri, content);
            Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST 2 " + json);
                loginResponsetList = JsonConvert.DeserializeObject<LoginModel>(json);
                cts.Token= loginResponsetList.token;
                if (cts.Token != null)
                {
                    //await Navigation.PushAsync(new Master());
                    await App.Current.MainPage.DisplayAlert("Inicio exitoso", "", "ok");
                    await Shell.Current.GoToAsync("//InitialPage");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", "Intenta de nuevo, " +
                    "verificando tu usuario y contraseña", "Ok");
            }
        }

        

        public ICommand ConsultaListaAutosPostCommand { get; set; }
        public async Task ConsultaListaAutosPost()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo };

            ListaAutos = await webApi.executeRequestPost<ObservableCollection<LoginModel>>(paramsPost);
        }
        #endregion

        #region GetMethods
        public ICommand ConsultaListaAutosGetCommand { get; set; }
        public async Task ConsultaListaAutosGet()
        {
            string requestUri = "/api/v1/cars?page=1";
            Console.WriteLine("Token: " + cts.Token);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9." +
                "eyJpc3MiOiJodHRwOlwvXC9ydWZmc3N0dWRpb3MuY29tXC9hcGlfY2Fycm9zXC9wdWJsaWNcL2FwaVwvdjFcL2F1dGhcL2xvZ2lu" +
                "IiwiaWF0IjoxNjI1Njc2NzA4LCJleHAiOjE2MjU4OTI3MDgsIm5iZiI6MTYyNTY3NjcwOCwianRpIjoiWnZvQVMzY2VCbHR6N0VqayIs" +
                "InN1YiI6MiwicHJ2IjoiODdlMGFmMWVmOWZkMTU4MTJmZGVjOTcxNTNhMTRlMGIwNDc1NDZhYSJ9.lPYIrUEZIMO6bIXjW7MkodyE5_E5O9w7wpuggfQdzSc");

            var response = await client.GetAsync(urlBase + requestUri);

            if (response.IsSuccessStatusCode)
            {
                var res = await App.Current.MainPage.DisplayAlert("Auto localizado", "Para continuar al pago presione Pagar", "Pagar","no pagar");

                if(res)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("List of cars: " + json);
                    ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                    //Data = JsonConvert.DeserializeObject<AutoModel>(json);
                }
            }
        }

        public ICommand GetAutoByIdCommand { get; set; }
        public async Task GetAutoById()
        {
            string stParamsGet = $"id={Id}";
            string requestUri = "/api/v1/cars/1?";
            Console.WriteLine("params :" + stParamsGet);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            var response = await client.GetAsync(urlBase+requestUri + stParamsGet);
            Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Requested Car: " + json);
                ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
            }
        }
        #endregion

        #region PutMethods
        public ICommand ConsultaListaAutosPutCommand { get; set; }
        public async Task ConsultaListaAutosPut()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo, id = Id };

            ListaAutos = await webApi.executeRequestPut<ObservableCollection<LoginModel>>(paramsPost);
        }
        #endregion

        #region DeleteMethods
        public ICommand ConsultaListaAutosDeleteCommand { get; set; }
        public async Task ConsultaListaAutosDelete()
        {
            string stParamsGet = $"id={id}";

            ListaAutos = await webApi.executeRequestDelete<ObservableCollection<LoginModel>>(stParamsGet);
        }
        #endregion

    }
}
