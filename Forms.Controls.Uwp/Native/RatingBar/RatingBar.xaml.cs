using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Messier16.Forms.Uwp.Controls.Native.RatingBar
{
    public sealed partial class RatingBar : UserControl
    {
        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register(
            "RatingValue",
            typeof(int),
            typeof(RatingBar),
            new PropertyMetadata(5, InnerRatingValueChanged));

        public static readonly DependencyProperty NumberOfStarsProperty = DependencyProperty.Register(
            "NumberOfStars",
            typeof(int),
            typeof(RatingBar),
            new PropertyMetadata(0, NumberOfStarsValueChanged));

        public static readonly DependencyProperty RatingValueHoverProperty = DependencyProperty.Register(
            "RatingValueHover",
            typeof(int),
            typeof(RatingBar),
            new PropertyMetadata(0, RatingValueHoverChanged));

        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(SolidColorBrush),
                typeof(RatingBar),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    OnStarForegroundColorChanged));


        public static readonly DependencyProperty HeightValueProperty = DependencyProperty.Register(
            "HeightValue",
            typeof(int),
            typeof(RatingBar),
            new PropertyMetadata(30, HeightValueChanged));

        public RatingBar()
        {
            InitializeComponent();
        }

        public int RatingValue
        {
            get => (int) GetValue(RatingValueProperty);
            set
            {
                if (value < 0)
                    SetValue(RatingValueProperty, 0);
                else if (value > NumberOfStars)
                    SetValue(RatingValueProperty, NumberOfStars);
                else
                    SetValue(RatingValueProperty, value);

                RatingValueChanged?.Invoke(this, new RatingValueChangedArgs(value));
            }
        }

        public int NumberOfStars
        {
            get => (int) GetValue(NumberOfStarsProperty);
            set => SetValue(NumberOfStarsProperty, value);
        }

        public int RatingValueHover
        {
            get => (int) GetValue(RatingValueHoverProperty);
            set
            {
                if (value < 0)
                    SetValue(RatingValueHoverProperty, 0);
                else if (value > NumberOfStars)
                    SetValue(RatingValueHoverProperty, NumberOfStars);
                else
                    SetValue(RatingValueHoverProperty, value);
            }
        }

        public SolidColorBrush StarForegroundColor
        {
            get => (SolidColorBrush) GetValue(StarForegroundColorProperty);
            set => SetValue(StarForegroundColorProperty, value);
        }

        public int HeightValue
        {
            get => (int) GetValue(HeightValueProperty);
            set => SetValue(HeightValueProperty, value);
        }

        public event RatingValueChanged RatingValueChanged;

        private static void InnerRatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var parent = sender as RatingBar;
            var ratingValue = (int) e.NewValue;
            var children = (parent.Content as StackPanel).Children;

            ToggleButton button = null;
            if (!children.Any())
                return;

            for (var i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = true;
            }

            for (var i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = false;
            }
        }

        private static void NumberOfStarsValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var parent = sender as RatingBar;
            var NumberOfStarsValue = (int) e.NewValue;

            for (var i = 1; i <= NumberOfStarsValue; i++)
            {
                var btn = new ToggleButton
                {
                    Tag = i,
                    Height = parent.HeightValue,
                    Style = parent.Resources["StarButton"] as Style,
                    Foreground = parent.StarForegroundColor
                };
                btn.Click += parent.RatingButtonClickEventHandler;
                btn.PointerEntered += parent.ToggleButton_DragEnter;
                parent.StackRootPanel.Children.Add(btn);
            }
        }

        private static void RatingValueHoverChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var parent = sender as RatingBar;
            var ratingValue = (int) e.NewValue;
            var children = (parent.Content as StackPanel).Children;

            ToggleButton button = null;
            for (var i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = true;
            }

            for (var i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = false;
            }

            Debug.WriteLine($"Rating value: {ratingValue}");
        }

        private static void OnStarForegroundColorChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (RatingBar) d;
            foreach (ToggleButton star in control.StackRootPanel.Children)
                star.Foreground = (SolidColorBrush) e.NewValue;
        }

        private static void HeightValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (RatingBar) sender;
            foreach (ToggleButton star in control.StackRootPanel.Children)
                star.Height = (int) e.NewValue;
        }

        public void RatingButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            RatingValue = (int) button.Tag;
            button.IsChecked = true;
        }

        public void ToggleButton_DragEnter(object sender, PointerRoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            RatingValueHover = (int) button.Tag;
        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            RatingValueHover = RatingValue;
        }
    }

    public delegate void RatingValueChanged(object sender, RatingValueChangedArgs args);

    public class RatingValueChangedArgs : EventArgs
    {
        public RatingValueChangedArgs(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}