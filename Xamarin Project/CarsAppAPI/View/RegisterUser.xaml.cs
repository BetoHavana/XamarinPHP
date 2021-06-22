using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsAppAPI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        MainViewModel page;
        public RegisterUser()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(Navigation);
            page = new MainViewModel(this);
        }
    }
}