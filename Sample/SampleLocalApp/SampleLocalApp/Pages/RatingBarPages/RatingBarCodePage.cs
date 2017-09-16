using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.RatingBarPages
{
    public class RatingBarCodePage : ContentPage
    {
        RatingBar SetRating;
        public RatingBarCodePage()
        {

            var ratingBar1 = new RatingBar();
            ratingBar1.FillColor = Color.Green; 
            ratingBar1.FilledImage = "star_filled.png";
            ratingBar1.Image = "star.png";
            ratingBar1.MaxRating = 10;
            ratingBar1.HeightRequest = 50;

            var ratingBar2 = new RatingBar();
            ratingBar2.FillColor = Color.Maroon;
            ratingBar2.FilledImage = "star_filled.png";
            ratingBar2.Image = "star.png";
            ratingBar2.MaxRating = 6;
            ratingBar2.HeightRequest = 50;

            var ratingBeer = new RatingBar();
            ratingBeer.FillColor = Color.Navy;
            ratingBeer.FilledImage = "beer_filled.png";
            ratingBeer.Image = "beer.png";
            ratingBeer.MaxRating = 5;
            ratingBeer.HeightRequest = 50;

            SetRating = new RatingBar();
            SetRating.FillColor = Color.Navy;
            SetRating.FilledImage = "beer_filled.png";
            SetRating.Image = "beer.png";
            SetRating.MaxRating = 5;
            SetRating.HeightRequest = 50;

            var retingTextBox = new Entry
            {
                Keyboard = Keyboard.Numeric
            };
            retingTextBox.TextChanged += RetingTextBox_TextChanged;



            var rating2Label = new Label
            {
                Text = ratingBar2.Rating.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };


            var ratingBeerLabel = new Label
            {
                Text = ratingBeer.Rating + " beer",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            ratingBar2.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Rating"))
                    rating2Label.Text = ratingBar2.Rating.ToString();
            };

            ratingBeer.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Rating"))
                    ratingBeerLabel.Text = ratingBeer.Rating + " beer";
            };

            var grid = new Grid
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = 
                {
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Auto)},
                }
            };
            

            grid.Children.Add(ratingBar2, 0, 0);
            grid.Children.Add(rating2Label, 1, 0);
            grid.Children.Add(ratingBeer, 0, 1);
            grid.Children.Add(ratingBeerLabel, 0, 2);
            Grid.SetColumnSpan(ratingBeer, 2);
            Grid.SetColumnSpan(ratingBeerLabel, 2);


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    SetRating,
                    retingTextBox,
                    ratingBar1,
                    grid
                }
            };
        }

        private void RetingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int rating = 0;
            Int32.TryParse(e.NewTextValue, out rating);
            SetRating.Rating = rating;
        }
    }
}


