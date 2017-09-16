using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Messier16.Forms.Controls;
using Messier16.Forms.iOS.Controls;
using Messier16.Forms.iOS.Controls.Native.Checkbox;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace Messier16.Forms.iOS.Controls
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, UISegmentedControl>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public new static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);


            if (Control == null)
            {

                var segmentedControl = new UISegmentedControl();
                SetNativeControl(segmentedControl);
            }
            
            if (e.OldElement != null)
            {
                Control.ValueChanged -= Control_ValueChanged;
            }

            if (e.NewElement != null)
            {

                for (var i = 0; i < e.NewElement.Children.Count; i++)
                {
                    Control.InsertSegment(e.NewElement.Children[i].Text, i, false);
                }

                Control.SelectedSegment = e.NewElement.SelectedIndex;

                Control.ValueChanged += Control_ValueChanged;
            }
        }

        void Control_ValueChanged(object sender, EventArgs e)
        {
            Element.SelectedIndex = (int)Control.SelectedSegment;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Element.SelectedIndex)))
            {
                Control.SelectedSegment = Element.SelectedIndex;
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }
    }
}
