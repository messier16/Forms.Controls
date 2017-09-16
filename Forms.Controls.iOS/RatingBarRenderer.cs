using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CoreGraphics;
using Messier16.Forms.Controls;
using Messier16.Forms.iOS.Controls.Native.RatingBar;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Messier16.Forms.iOS.Controls;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRenderer))]
namespace Messier16.Forms.iOS.Controls
{
    public class RatingBarRenderer : ViewRenderer<RatingBar, EDStarRating>
    {
        /// <summary>
        /// Used for registration with dependency service
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
                    StarImage = new UIKit.UIImage(Element.Image.File),
                    StarHighlightedImage = new UIKit.UIImage(Element.FilledImage.File),
                    DisplayMode = StarRatingDisplayMode.Full,
                    MaxRating = Element.MaxRating
                };

                SetNativeControl(ratingBar);
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources
                Control.RatingChanged -= Control_RatingChanged;
            }
            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                Control.RatingChanged += Control_RatingChanged;
            }
            base.OnElementChanged(e);
        }

        private void Control_RatingChanged(object sender, GenericEventArgs<float> args)
        {
            Element.Rating = (int)args.Value;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName.Equals(nameof(RatingBar.Rating)))
            {
                Control.Rating = Element.Rating;
            }
            else if (e.PropertyName.Equals(nameof(RatingBar.IsEnabled)))
            {
                Control.IsEditable = Element.IsEnabled;
            }
        }
    }
}
