using CarsAppAPI.Models;
using CarsAppAPI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsAppAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public List<MenuPagina> MiMenu { get; set; }
        public Master()
        {
            InitializeComponent();
            
        }
        
    }
}