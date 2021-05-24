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
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            InitializeComponent();
        }

        public static implicit operator Detail(NavigationPage v)
        {
            throw new NotImplementedException();
        }
    }
}