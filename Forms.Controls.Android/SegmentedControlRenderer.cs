using System;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using Messier16.Forms.Controls;
using Android.Widget;
using Android.Graphics;
using Android.Content;
using Android.Util;
using Xamarin.Forms;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Android.Controls.Native.SegmentedControl;


[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace Messier16.Forms.Android.Controls
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, RadioGroup>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            var layoutInflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);

            var g = new RadioGroup(Context);
            g.Orientation = Orientation.Horizontal;
            g.CheckedChange += (sender, eventArgs) => {
                var rg = (RadioGroup)sender;
                if (rg.CheckedRadioButtonId != -1)
                {
                    var id = rg.CheckedRadioButtonId;
                    var radioButton = rg.FindViewById(id);
                    var radioId = rg.IndexOfChild(radioButton);
                    //                    var btn = (RadioButton)rg.GetChildAt (radioId);
                    //                    var selection = (String)btn.Text;
                    e.NewElement.SelectedIndex = radioId;
                }
            };

            for (var i = 0; i < e.NewElement.Children.Count; i++)
            {
                var o = e.NewElement.Children[i];
                var v = (SegmentedControlButton)layoutInflater.Inflate(Resource.Layout.SegmentedControl, null);
                v.Text = o.Text;
                if (i == 0)
                    v.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
                else if (i == e.NewElement.Children.Count - 1)
                    v.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);
                g.AddView(v);
            }

            SetNativeControl(g);
        }
    }
}

