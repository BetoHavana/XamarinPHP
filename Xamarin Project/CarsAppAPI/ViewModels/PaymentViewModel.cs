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
using Xamarin.Essentials;
using System.Collections.ObjectModel;

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

        private PaymentModel currentcardlist;
        public PaymentModel CurrentCardList
        {
            get { return currentcardlist; }
            set { currentcardlist = value; RaisePropetyChanged(); }
        }


        public ICommand GetCardsCommand { get; set; }
        public Command BackCommand { get; }
        public Command Back2SelectCommand { get; }
        public Command AddCardCommand { get; }
        public ICommand PostPaymentCommand { get; set; }
        public ICommand PostAddCardCommand { get; set; }
        public PaymentViewModel()
        {
            
            BackCommand = new Command(Back);
            Back2SelectCommand = new Command(Back2Select);
            AddCardCommand = new Command(GoAddCard);
            DeleteCardsCommand = new Command(async (e) =>
            {
                string id = (e as string);
                await DeleteCards(id);
            });
            GetCardsCommand = new Command(async () =>
            {
                await GetCards();
            });
            PostPaymentCommand = new Command(async (e) =>
            {
                string id = (e as string);
                await PostPayment(id);
            });
            PostAddCardCommand = new Command(async () =>
            {
                await PostAddCard();
            });
        }

        private async void Back(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");
        }
        private async void Back2Select(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(Select)}");
        }
        private async void GoAddCard(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(AddCard)}");
        }
        public async Task GetCards()
        {
            CardsList = new PaymentModel();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            var response = await client.GetAsync(urlBase + StaticConstants.GetCardsEndpoint);
            if (response.IsSuccessStatusCode)
            {
                IsTitleVisible = true;
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("cards: " + json);
                CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                StaticConstants.Cards = CardsList;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Ocurrió un error " + json);
                CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                await App.Current.MainPage.DisplayAlert("Ocurrió un error", "", "Cerrar");
            }
        }

        public async Task PostPayment(String ids)
        {
            UserDialogs.Instance.ShowLoading("Realizando Pago");
            var paramsPost = new { source_id = ids, device_session_id = "a",license_plate = StaticConstants.currentPlate };
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
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert("Pago exitoso", "", "Cerrar");

                StaticConstants.showCarResponse = true;
                await Shell.Current.GoToAsync($"//{nameof(Select)}");
                
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Payment to do " + json);
                //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.DisplayAlert("Ocurrió un error", "", "Cerrar");
            }
        }

        public async Task PostAddCard()
        {
            bool letsAddCard = true;
            Console.WriteLine("estatus " + string.IsNullOrWhiteSpace(StaticConstants.Cards.cards.ToString()));
            
            try
            {
                /*Console.WriteLine("añadir");
                CardsList = new PaymentModel();
                Console.WriteLine("estatus "+ string.IsNullOrWhiteSpace(CardsList.ToString()));
                Console.WriteLine("LISTA  " + CardsList.ToString());
                var property = CardsList.GetType().GetProperty("cards");
                Console.WriteLine("cards  " + property);*/
                
                foreach (var obj in StaticConstants.Cards.cards)
                {
                    if (obj.card_number.ToString() == CardNumber)
                    {
                        letsAddCard = false;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("error "+e);
            }
            

            if (letsAddCard)
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
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("Tarjeta añadida", "", "Cerrar");
                    await Shell.Current.GoToAsync($"//{nameof(PaymentOptions)}");

                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Payment to do " + json);
                    //CardsList = JsonConvert.DeserializeObject<PaymentModel>(json);
                    UserDialogs.Instance.HideLoading();

                    await App.Current.MainPage.DisplayAlert("Ocurrió un error", "", "Cerrar");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Tarjeta no registrada", "Esta tarjeta ya ha sido registrada anteriormente, prueba con otra diferente", "Cerrar");
            }
            
        }

        #region DeleteMethods
        public ICommand DeleteCardsCommand { get; set; }
        public async Task DeleteCards(string idc)
        {
            UserDialogs.Instance.ShowLoading("Borrando tarjeta");
           
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + StaticConstants.Token);
            HttpResponseMessage response = await client.DeleteAsync(urlBase + StaticConstants.DeleteCardsEndpoint + idc);
            if (response.IsSuccessStatusCode)
            {
                IsTitleVisible = true;
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(" payment: " + json);
                CurrentCardList = CardsList;
                CardsList = new PaymentModel();
                Console.WriteLine(" lista  " + CurrentCardList);
                if (CurrentCardList != null)
                {
                    foreach (var obj in CurrentCardList.cards)
                    {
                        if (obj.id.ToString() == idc)
                        {
                            Console.WriteLine(" card id  " + idc);
                            Console.WriteLine(" card found" + obj.id.ToString());
                            CurrentCardList.cards.Remove(obj);
                            CardsList = CurrentCardList;
                            /*PaymentOptions objpayment = new PaymentOptions();
                            objpayment.UpdateList();*/
                            break;
                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert("Tarjeta borrada", "", "Cerrar");

            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Payment to do " + json);
                
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.DisplayAlert("Ocurrió un error", ""+json, "Cerrar");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropetyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

