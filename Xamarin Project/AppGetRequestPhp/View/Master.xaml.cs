using AppGetRequestPhp.Models;
using AppGetRequestPhp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGetRequestPhp
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