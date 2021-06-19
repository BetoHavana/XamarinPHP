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

        public MainViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            ConsultaListaAutosGetCommand = new Command(async () => await ConsultaListaAutosGet());
            ConsultaListaAutosPostCommand = new Command(async () => await ConsultaListaAutosPost());
            ConsultaListaAutosPutCommand = new Command(async () => await ConsultaListaAutosPut());
            ConsultaListaAutosDeleteCommand = new Command(async () => await ConsultaListaAutosDelete());
            LoginCommand = new Command(async () => await Login());
            RegisterUserCommand = new Command(async () => await RegisterUser());
            GetAutoByIdCommand = new Command(async () => await GetAutoById());
        }
        public MainViewModel()
        {
            
        }


        #region BindingVariables

        private LoginModel listaEmpleados;
        public LoginModel ListaEmpleados
        {
            get { return listaEmpleados; }
            set { listaEmpleados = value; RaisePropetyChanged(); }
        }

        private RegisterModel listaRegistro;
        public RegisterModel ListaRegistro
        {
            get { return listaRegistro; }
            set { listaRegistro = value; RaisePropetyChanged(); }
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

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; RaisePropetyChanged(); }
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
                ListaEmpleados = JsonConvert.DeserializeObject<LoginModel>(json);
                //await SecureStorage.SetAsync("token", ListaEmpleados.token);
                //var myValue = await SecureStorage.GetAsync("token");

                login loginButton = new login();
                String token = ListaEmpleados.token;
                //await Application.Current.MainPage.Navigation.PushAsync(new QRCode());
                if (token != null)
                {
                    await Navigation.PushAsync(new Master());
                }
                
                //loginButton.thereIsToken();
                //Console.WriteLine("Should Open ");

            }
        }

        public ICommand RegisterUserCommand { get; set; }
        public async Task RegisterUser()
        {
            var paramsPost = new { name = Name, email = Email, password = Password, password_confirmation = PasswordConfirmation };
            string requestUri = "/api/v1/auth/create";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvcnVmZnNzdHVkaW9z" +
               "LmNvbVwvYXBpX2NhcnJvc1wvcHVibGljXC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyMjI2MDk2MywiZX" +
               "hwIjoxNjIyNDc2OTYzLCJuYmYiOjE2MjIyNjA5NjMsImp0aSI6IkM2YTRlVUlmc3NSUFEwSTkiLCJzdWIiOjEsInByd" +
               "iI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk" +
               "3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.r8KPFQo4GXZhsgk7fhGU_KDBZYOnZ0FMzDoKAcTU2LY";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.BaseAddress = urlBase;
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("content " + content);
            Console.WriteLine("Body" + jsonData.ToString());
            HttpResponseMessage response = await client.PostAsync(urlBase + requestUri, content);
            Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("POST Register " + json);
                ListaRegistro = JsonConvert.DeserializeObject<RegisterModel>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("422 Register " + json);
                ListaRegistro = JsonConvert.DeserializeObject<RegisterModel>(json);
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
            string requestUri = "/api/v1/cars";
            /*string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvcnVmZnNzdHVkaW9z" +
                "LmNvbVwvYXBpX2NhcnJvc1wvcHVibGljXC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyMjI2MDk2MywiZX" +
                "hwIjoxNjIyNDc2OTYzLCJuYmYiOjE2MjIyNjA5NjMsImp0aSI6IkM2YTRlVUlmc3NSUFEwSTkiLCJzdWIiOjEsInByd" +
                "iI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk" +
                "3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.r8KPFQo4GXZhsgk7fhGU_KDBZYOnZ0FMzDoKAcTU2LY";*/
            Console.WriteLine("Token: " + Token);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);

            var response = await client.GetAsync(urlBase + requestUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("List of cars: " + json);
                ListaAuto = JsonConvert.DeserializeObject<AutoModel>(json);
                Data = JsonConvert.DeserializeObject<AutoModel>(json);
            }
        }

        public ICommand GetAutoByIdCommand { get; set; }
        public async Task GetAutoById()
        {
            string stParamsGet = $"id={Id}";
            string requestUri = "/api/v1/cars/1?";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvcnVmZnNzdHVkaW9z" +
                "LmNvbVwvYXBpX2NhcnJvc1wvcHVibGljXC9hcGlcL3YxXC9hdXRoXC9sb2dpbiIsImlhdCI6MTYyMjI2MDk2MywiZX" +
                "hwIjoxNjIyNDc2OTYzLCJuYmYiOjE2MjIyNjA5NjMsImp0aSI6IkM2YTRlVUlmc3NSUFEwSTkiLCJzdWIiOjEsInByd" +
                "iI6Ijg3ZTBhZjFlZjlmZDE1ODEyZmRlYzk" +
                "3MTUzYTE0ZTBiMDQ3NTQ2YWEifQ.r8KPFQo4GXZhsgk7fhGU_KDBZYOnZ0FMzDoKAcTU2LY";
            Console.WriteLine("params :" + stParamsGet);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
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
