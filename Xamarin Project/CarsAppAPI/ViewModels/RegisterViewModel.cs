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

namespace CarsAppAPI.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        Uri urlBase = new Uri(StaticConstants.UrlBase);
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

        private RegisterModel listaRegistro;
        public RegisterModel ListaRegistro
        {
            get { return listaRegistro; }
            set { listaRegistro = value; RaisePropetyChanged(); }
        }

        public RegisterViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterUserCommand = new Command(async () => await RegisterUserMethod());
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

        public ICommand RegisterUserCommand { get; set; }
        public async Task RegisterUserMethod()
        {
            UserDialogs.Instance.ShowLoading("Registrando");
            if (Password != "" && Password != null &&
                PasswordConfirmation != "" && PasswordConfirmation != null &&
                Name != "" && Name != null &&
                Email != "" && Email != null )
            {
                if (Password == PasswordConfirmation)
                {
                    var paramsPost = new { name = Name, email = Email, password = Password, password_confirmation = PasswordConfirmation };

                    HttpClient client = new HttpClient();
                    Console.WriteLine("My Token " + StaticConstants.Token);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
                    client.BaseAddress = urlBase;
                    string jsonData = JsonConvert.SerializeObject(paramsPost);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    Console.WriteLine("content " + content);
                    Console.WriteLine("Body" + jsonData.ToString());
                    HttpResponseMessage response = await client.PostAsync(urlBase + StaticConstants.RegisterEndpoint, content);
                    Console.WriteLine("Codigo: " + response.IsSuccessStatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("POST Register " + json);
                        ListaRegistro = JsonConvert.DeserializeObject<RegisterModel>(json);
                        RegisterUser RU = new RegisterUser();
                        RU.cleanForm();
                        await App.Current.MainPage.DisplayAlert("Registered", "", "ok");
                        if (StaticConstants.isLogged == false)
                        {
                            MainViewModel objMVM = new MainViewModel();
                            objMVM.Email = ListaRegistro.user.email;
                            objMVM.Password = Password;
                            StaticConstants.isRegisteredFromGuest = true;
                            await objMVM.Login();
                            
                            await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        var json = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("422 Register " + json);
                        ListaRegistro = JsonConvert.DeserializeObject<RegisterModel>(json);
                        await App.Current.MainPage.DisplayAlert("An error ocurred", "", "ok");
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Las contraseñas no coinciden", "", "ok");
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert("Llena todos los campos", "", "ok");
            }
        }
    }
}

