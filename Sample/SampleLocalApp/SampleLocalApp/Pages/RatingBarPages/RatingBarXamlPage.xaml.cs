using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Pages.RatingBarPages
{
    public partial class RatingBarXamlPage : ContentPage
    {
        public RatingBarXamlPage()
        {
            BindingContext = new RatingBarViewModel();
            InitializeComponent();
        }
    }
}
