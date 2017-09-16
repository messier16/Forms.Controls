using System;
using System.ComponentModel;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Android.Controls.Native.RatingBar;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RatingBar = Messier16.Forms.Controls.RatingBar;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRenderer))]

namespace Messier16.Forms.Android.Controls
{
    public class RatingBarRenderer : ViewRenderer<RatingBar, Messier16RatingBar>
    {
        /// <summary>
        ///     Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var unused = DateTime.Now;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<RatingBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var layout = new LinearLayout(Context);

                    var ratingBar = new Messier16RatingBar(Context)
                    {
                        MaxStars = Element.MaxRating,
                        FillColor = e.NewElement.FillColor.ToAndroid(),
                        StrokeColor = e.NewElement.FillColor.ToAndroid()
                    };

                    ratingBar.RatingChanged += RatingBarOnRatingChanged;

                    // http://stackoverflow.com/questions/3858600/how-to-make-ratingbar-to-show-five-stars#comment4151898_3859248
                    ratingBar.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.MatchParent);
                    layout.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                    SetNativeControl(ratingBar);
                }

                Control.Rating = e.NewElement.Rating;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Element.Rating)))
                Control.Rating = Element.Rating;
            else
                base.OnElementPropertyChanged(sender, e);
        }

        private void RatingBarOnRatingChanged(object sender, float f)
        {
            Element.Rating = (int) f;
        }
    }
}