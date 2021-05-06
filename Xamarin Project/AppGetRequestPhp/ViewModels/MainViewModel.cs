using AppGetRequestPhp.Models;
using AppGetRequestPhp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppGetRequestPhp.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
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

        public async Task ConsultaListaEmpleadosGet()
        {
            string stParamsGet = $"username={user}&password={pass}";

            ListaEmpleados = await webApi.executeRequestGet<ObservableCollection<EmpleadoModel>>(stParamsGet);
        }

        public ICommand ConsultaListaEmpleadosGetCommand { get; set; }


        public MainViewModel()
        {
            ConsultaListaEmpleadosGetCommand = new Command(async () => await ConsultaListaEmpleadosGet());
            ConsultaListaAutosGetCommand = new Command(async () => await ConsultaListaAutosGet());
        }

        public async Task ConsultaListaAutosGet()
        {
            string stParamsGet = $"placa={placa}&modelo={modelo}";

            ListaAutos = await webApi.executeRequestGetAuto<ObservableCollection<EmpleadoModel>>(stParamsGet);
        }

        public ICommand ConsultaListaAutosGetCommand { get; set; }

    }
}
