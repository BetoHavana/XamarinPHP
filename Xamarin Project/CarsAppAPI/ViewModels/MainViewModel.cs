using CarsAppAPI.Models;
using CarsAppAPI.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarsAppAPI.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        WebApiClientService webApi = new WebApiClientService();
         
        private ObservableCollection<EmpleadoModel> listaEmpleados;
        public ObservableCollection<EmpleadoModel> ListaEmpleados
        {
            get { return listaEmpleados; }
            set { listaEmpleados = value; RaisePropetyChanged(); }
        }
        private ObservableCollection<EmpleadoModel> listaAutos;
        public ObservableCollection<EmpleadoModel> ListaAutos
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
        private string pass;
        public string Pass
        {
            get { return pass; }
            set { pass = value; RaisePropetyChanged(); }
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

        public MainViewModel()
        {
            ConsultaListaEmpleadosGetCommand = new Command(async () => await ConsultaListaEmpleadosGet());
            ConsultaListaAutosGetCommand = new Command(async () => await ConsultaListaAutosGet());
            ConsultaListaAutosPostCommand = new Command(async () => await ConsultaListaAutosPost());
            ConsultaListaAutosPutCommand = new Command(async () => await ConsultaListaAutosPut());
            ConsultaListaAutosDeleteCommand = new Command(async () => await ConsultaListaAutosDelete());
            LoginCommand = new Command(async () => await Login());
        }

        public ICommand LoginCommand { get; set; }
        public async Task Login()
        {
            var paramsPost = new { email = Email, password = Pass };

            ListaEmpleados = await webApi.executeRequestPostLogin<ObservableCollection<EmpleadoModel>>(paramsPost);
        }

        public ICommand ConsultaListaEmpleadosGetCommand { get; set; }
        public async Task ConsultaListaEmpleadosGet()
        {
            string stParamsGet = $"username={user}&password={pass}";

            ListaEmpleados = await webApi.executeRequestGet<ObservableCollection<EmpleadoModel>>(stParamsGet);
        }

        public ICommand ConsultaListaAutosGetCommand { get; set; }
        public async Task ConsultaListaAutosGet()
        {
            string stParamsGet = $"placa={placa}&modelo={modelo}";

            ListaAutos = await webApi.executeRequestGetAuto<ObservableCollection<EmpleadoModel>>(stParamsGet);
        }
        
        public ICommand ConsultaListaAutosPostCommand { get; set; }
        public async Task ConsultaListaAutosPost()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo};

            ListaAutos = await webApi.executeRequestPost<ObservableCollection<EmpleadoModel>>(paramsPost);
        }

        public ICommand ConsultaListaAutosPutCommand { get; set; }
        public async Task ConsultaListaAutosPut()
        {
            var paramsPost = new { placa = Placa, modelo = Modelo, id= Id };

            ListaAutos = await webApi.executeRequestPut<ObservableCollection<EmpleadoModel>>(paramsPost);
        }

        public ICommand ConsultaListaAutosDeleteCommand { get; set; }
        public async Task ConsultaListaAutosDelete()
        {
            string stParamsGet = $"id={id}";

            ListaAutos = await webApi.executeRequestDelete<ObservableCollection<EmpleadoModel>>(stParamsGet);
        }

    }
}
