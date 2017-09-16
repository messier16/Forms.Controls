using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.CheckboxPages
{
    public class CheckboxCodePage : ContentPage
    {
        public CheckboxCodePage()
        {
			var cb1 = new Checkbox() { CheckboxBackgroundColor = Color.Yellow, TickColor = Color.Red, WidthRequest = 55, HeightRequest = 55 };
            var cb2 = new Checkbox()
            {
                CheckboxBackgroundColor = Color.FromHex("5480BE"),
                TickColor = Color.FromHex("73A6DA"),
                WidthRequest = 45,
                IsEnabled = cb1.Checked
            };
            var cb3 = new Checkbox() { WidthRequest = 35, Checked = true };
            var cb4 = new Checkbox()
            {
				#if DEBUG
                AutomationId = "cb4",
				#endif
                TickColor = Color.Blue,
                CheckboxBackgroundColor = Color.Maroon,
                IsVisible = cb2.Checked
            };

            cb1.PropertyChanged +=
                (sender, e) =>
                {
                    if (e.PropertyName.Equals("Checked"))
                    {
                        cb2.IsEnabled = cb1.Checked;
                        cb3.Checked = !cb1.Checked;
                    }
                };

            cb2.PropertyChanged +=
                (sender, e) =>
                {
                    if (e.PropertyName.Equals("Checked"))
                    {
                        cb4.IsVisible = cb2.Checked;
                    }
                };

            var grid = new Grid
            {
                RowDefinitions =
                    {
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                            new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                    },
                ColumnDefinitions =
                    {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)},
                            new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star)},
                    }
            };

            var toggableCheck = new Checkbox() { WidthRequest = 45 };
            var buttonToggle = new Button { Text = "Toggle check" };

            buttonToggle.Clicked += (sender, e) => { toggableCheck.Checked = !toggableCheck.Checked; };

            var otherCheck = new Checkbox() { WidthRequest = 55 };
            var otherCheckLabel = new Label
            {
                Text = otherCheck.Checked ? "Checked" : "Unchecked",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            otherCheck.PropertyChanged += (sender, e) =>
            {
                otherCheckLabel.Text = otherCheck.Checked ? "Checked" : "Unchecked";
            };

            grid.Children.Add(toggableCheck, 0, 0);
            grid.Children.Add(buttonToggle, 1, 0);

            grid.Children.Add(otherCheck, 0, 1);
            grid.Children.Add(otherCheckLabel, 1, 1);

            var checkboxes = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    cb1,
                    cb2,
                    cb3,
                    cb4
                }
            };

            Content = new StackLayout
            {
                Children =
                {
                    checkboxes,
                    //grid
                }
            };
        }
    }
}


