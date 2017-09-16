using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Messier16.Forms.Controls;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp
{
    public class CheckboxListCodePage : ContentPage
    {
        ObservableCollection<ControlWrapper> controls;
        Random r = new Random();

        public List<ControlWrapper> GetControls(int n)
        {
            return System.Linq.Enumerable.Range(0, n)
                         .Select(i => new ControlWrapper() { IsVisible = i % 2 == 0 })
                  .ToList();
        }

        public CheckboxListCodePage()
        {

            ToolbarItem reload = new ToolbarItem() { Text = "Reload" };
            reload.Clicked += (sender, e) =>
            {
                controls.Clear();
                var c = GetControls(30);
                foreach (var item in c)
                {
                    controls.Add(item);
                }
            };

            ToolbarItems.Add(reload);

            BindingContext = new CheckboxViewModel();
            var list = new ListView();

            //list.SetBinding(ListView.ItemsSourceProperty, "Checkboxes");
            var template = new DataTemplate(typeof(ControlCell<Checkbox, Switch>));
            list.ItemTemplate = template;

            controls = new ObservableCollection<ControlWrapper>(GetControls(30));
            list.ItemsSource = controls;
            list.ItemSelected += (sender, e) =>
            {

                if (list.SelectedItem != null)
                {
                    var c = list.SelectedItem as ControlWrapper;
                    c.IsVisible = !c.IsVisible;
                    list.SelectedItem = null;
                }
            };

            Content = list;
        }
    }
}
