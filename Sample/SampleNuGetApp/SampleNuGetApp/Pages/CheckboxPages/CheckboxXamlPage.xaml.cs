using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TestApp.ViewModels;

namespace TestApp.Pages.CheckboxPages
{
    public partial class CheckboxXamlPage : ContentPage
    {
        public CheckboxXamlPage()
        {
            BindingContext = new CheckboxViewModel();
            InitializeComponent();
        }
    }
}

