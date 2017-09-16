using System;

using Xamarin.Forms;

namespace TestApp.Pages
{
    public class HomePage : ContentPage
    {

        private Button GoToRatingBarButton;
        private Button GoToCheckboxButton;
        private Button GoToSegmentedControl;

        public HomePage()
        {
            Title = "Messier16 Controls";

            GoToRatingBarButton = new Button();
			GoToRatingBarButton.AutomationId = nameof(GoToRatingBarButton);
            GoToRatingBarButton.Text = "RatingBar";
            GoToRatingBarButton.Clicked += GoToButton_Clicked;


            GoToCheckboxButton = new Button();
			GoToCheckboxButton.AutomationId = nameof(GoToCheckboxButton);
            GoToCheckboxButton.Text = "Checkbox";
            GoToCheckboxButton.Clicked += GoToButton_Clicked;

            GoToSegmentedControl = new Button();
			GoToSegmentedControl.AutomationId = nameof(GoToSegmentedControl);
            GoToSegmentedControl.Text = "SegmentedControl";
            GoToSegmentedControl.Clicked += GoToButton_Clicked;

            Content = new StackLayout
            {
                Children =
                {
                    GoToRatingBarButton,
                            GoToCheckboxButton,
                            GoToSegmentedControl
                }
            };
        }

        async void GoToButton_Clicked(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b == GoToRatingBarButton)
                await App.Navigation.PushAsync(new RatingBarPage());
            if (b == GoToCheckboxButton)
                await App.Navigation.PushAsync(new CheckboxPage());
            if (b == GoToSegmentedControl)
                await App.Navigation.PushAsync(new SegmentedControlPage());
        }
    }
}


