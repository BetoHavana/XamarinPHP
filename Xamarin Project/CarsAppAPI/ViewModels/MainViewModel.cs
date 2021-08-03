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

        Uri urlBase = new Uri(StaticConstants.UrlBase);

        #region constructors
        /*public MainViewModel(INavigation navigation)
        {
            this.Navigation = navigation; 

        }*/
        public MainViewModel()
        {
            ListaAuto = new AutoModel();
            ConsultaListaAutosGetCommand = new Command(async () => { await ConsultaListaAutosGet(); });
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
                    //await Navigation.PushAsync(new Master());
                    await App.Current.MainPage.DisplayAlert("Bienvenido", "", "ok");
                    StaticConstants.isLogged = true;
                    if (StaticConstants.isRegisteredFromGuest == false)
                    {
                        await Shell.Current.GoToAsync("//InitialPage");
                    }
                   
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", "verifica" +
                    " tu usuario y contraseña", "Ok");
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

                    await Shell.Current.GoToAsync("//InitialPage");

                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Usuario no encontrado", "Verifica tu usuario " +
                    "y contraseña", "Ok");
            }
        }


        /*public ICommand ConsultaListaAutosPostCommand { get; set; }
        public async Task ConsultaListaAutosPost()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo };

            ListaAutos = await webApi.executeRequestPost<ObservableCollection<LoginModel>>(paramsPost);
        }*/
        #endregion

        #region GetMethods
        public ICommand ConsultaListaAutosGetCommand { get; set; }
        public async Task ConsultaListaAutosGet()
        {
            Console.WriteLine("Token: " + StaticConstants.Token);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            var response = await client.GetAsync(urlBase + StaticConstants.GetCarsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                bool res = await App.Current.MainPage.DisplayAlert("Auto localizado",
                    "Para continuar al pago presione Pagar", "Pagar","no pagar");
                if(res && StaticConstants.isLogged)
                {
                    await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
                    var json = await response.Content.ReadAsStringAsync();
                    Preferences.Set("carresponse",json);
                    Console.WriteLine("List of cars: " + json);
                    ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                } else if(res && !StaticConstants.isLogged)
                {
                    await Shell.Current.GoToAsync($"//{nameof(RegisterUser)}");
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
        /*
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
        */
    }
}
