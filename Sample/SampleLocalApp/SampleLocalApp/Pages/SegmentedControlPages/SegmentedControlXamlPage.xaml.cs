using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.SegmentedControlPages
{
    public partial class SegmentedControlXamlPage : ContentPage
    {


        string [] values = { "Artists", "Albums", "Songs", "Local" };

        public SegmentedControlXamlPage()
        {
            BindingContext = new SegmentedControlViewModel();
            InitializeComponent();

            foreach (var item in values)
            {
                DisabledSegmentedControl.Children.Add(new SegmentedControlOption { Text = item });
            }

            foreach (var item in values)
            {
                picker.Items.Add(item);
            }
            picker.SelectedIndex = 0;
        }
    }
}
