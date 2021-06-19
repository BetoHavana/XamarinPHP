
using System;
using Xamarin.Forms;

namespace CarsAppAPI
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        public String myToken { get; set; }
        public void  AuthLogin()
        {
        
            Console.WriteLine("TOKEN " + myToken);
            if (myToken.Length > 0)
            {
                Navigation.PushAsync(new Master());
                Console.WriteLine("Should Open ");
            }
            
        }
    }
}
