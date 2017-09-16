using System;
using System.ComponentModel;
using CoreGraphics;
using Messier16.Forms.Controls;
using Messier16.Forms.iOS.Controls;
using Messier16.Forms.iOS.Controls.Native.RatingBar;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRenderer))]

namespace Messier16.Forms.iOS.Controls
{
    public class RatingBarRenderer : ViewRenderer<RatingBar, EDStarRating>
    {
        /// <summary>
        ///     Used for registration with dependency service
        /// </summary>
        public new static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RatingBar> e)
        {
            if (e.OldElement != null || Element == null) return;

            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property
                var ratingBar = new EDStarRating(new CGRect(0, 0, 1, Element.HeightRequest))
                {
                    IsEditable = Element.IsEnabled,
                    Rating = Element.Rating,
                    StarImage = new UIImage(Element.Image.File),
                    StarHighlightedImage = new UIImage(Element.FilledImage.File),
                    DisplayMode = StarRatingDisplayMode.Full,
                    MaxRating = Element.MaxRating
                };

                SetNativeControl(ratingBar);
            }

            if (e.OldElement != null)
                Control.RatingChanged -= Control_RatingChanged;
            if (e.NewElement != null)
                Control.RatingChanged += Control_RatingChanged;
            base.OnElementChanged(e);
        }

        private void Control_RatingChanged(object sender, GenericEventArgs<float> args)
        {
            Element.Rating = (int) args.Value;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName.Equals(nameof(RatingBar.Rating)))
                Control.Rating = Element.Rating;
            else if (e.PropertyName.Equals(nameof(RatingBar.IsEnabled)))
                Control.IsEditable = Element.IsEnabled;
        }
    }
}