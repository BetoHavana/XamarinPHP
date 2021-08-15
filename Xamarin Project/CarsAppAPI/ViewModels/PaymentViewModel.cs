using CarsAppAPI.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using Newtonsoft.Json;
using CarsAppAPI.View;
using System.Text;
using Acr.UserDialogs;

namespace CarsAppAPI.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
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

        private PaymentModel cardslist;
        public PaymentModel CardsList
        {
            get { return cardslist; }
            set { cardslist = value; RaisePropetyChanged(); }
        }
        private bool _isTitleVisible;
        public bool IsTitleVisible
        {
            get { return _isTitleVisible; }
            set { _isTitleVisible = value; RaisePropetyChanged();}
        }

        private string cardNumber;
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; RaisePropetyChanged(); }
        }
        private string cvv;
        public string Cvv
        {
            get { return cvv; }
            set { cvv = value; RaisePropetyChanged(); }
        }
        private string expMonth;
        public string ExpMonth
        {
            get { return expMonth; }
            set { expMonth = value; RaisePropetyChanged(); }
        }
        private string expYear;
        public string ExpYear
        {
            get { return expYear; }
            set { expYear = value; RaisePropetyChanged(); }
        }

        public ICommand GetCardsCommand { get; set; }
        public Command BackCommand { get; }
        public Command AddCardCommand { get; }
        public ICommand PostPaymentCommand { get; set; }
        public ICommand PostAddCardCommand { get; set; }
        public PaymentViewModel()
        {
            
            BackCommand = new Command(Back);
            AddCardCommand = new Command(GoAddCard);
            GetCardsCommand = new Command(async () =>
            {
                await GetCards();
            });
            PostPaymentCommand = new Command(async (e) =>
            {
                String id = (e as String);
                await PostPayment(id);
            });
            PostAddCardCommand = new Command(async () =>
            {
                await PostAddCard();
            });
        }

        private async void Back(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(Select)}");
        }
        private async void GoAddCard(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCard)}");
        }
        public async Task GetCards()
        {
            Console.WriteLine("TOKEN: "+StaticConstants.Token);
            Console.WriteLine("urlBase: " + urlBase);
            Console.WriteLine("StaticConstants.GetCardsEndpoint: " + StaticConstants.GetCardsEndpoint);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            var response = await client.GetAsync(urlBase + StaticConstants.GetCardsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                IsTitleVisible = true;
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("cards: " + json);
                CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("422 Register " + json);
                CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                await App.Current.MainPage.DisplayAlert("An error ocurred", "", "ok");
            }
        }

        public async Task PostPayment(String ids)
        {
            UserDialogs.Instance.ShowLoading("Realizando Pago");
            var paramsPost = new { source_id = ids, device_session_id = "a"};
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            client.BaseAddress = urlBase;
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(urlBase + StaticConstants.ChargeEndpoint,content);
            if (response.IsSuccessStatusCode)
            {
                IsTitleVisible = true;
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(" payment: " + json);
                //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                await App.Current.MainPage.DisplayAlert("Pagado", "", "ok");
                StaticConstants.showCarResponse = true;
                await Shell.Current.GoToAsync($"//{nameof(Select)}");
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Payment to do " + json);
                //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.DisplayAlert("An error ocurred on payment", "", "ok");
            }
        }

        public async Task PostAddCard()
        {
            UserDialogs.Instance.ShowLoading("Añadiendo tarjeta");
            var paramsPost = new { holder_name = Name, card_number = CardNumber, cvv = Cvv, expiration_month = ExpMonth, expiration_year = ExpYear };
            string jsonData = JsonConvert.SerializeObject(paramsPost);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            client.BaseAddress = urlBase;
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(urlBase + StaticConstants.AddCardEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                IsTitleVisible = true;
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(" payment: " + json);
                //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                await App.Current.MainPage.DisplayAlert("Added", json, "ok");
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Payment to do " + json);
                //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.DisplayAlert("An error ocurred on adding card", "", "ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

    }
}

