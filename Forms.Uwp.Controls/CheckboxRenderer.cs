using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Messier16.Forms.Controls;
using Messier16.Forms.Uwp.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]

namespace Messier16.Forms.Uwp.Controls
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, CheckBox>
    {
        /// <summary>
        ///     Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
                if (Control != null)
                {
                    Control.Checked -= CheckBoxChecked;
                    Control.Unchecked -= CheckBoxUnchecked;
                }

            if (e.NewElement != null)
            {
                // Configure the control and subscribe to event handlers
                if (Control == null)
                {
                    var checkBox = new CheckBox();
                    checkBox.IsThreeState = false;
                    SetNativeControl(checkBox);
                }

                // You should only create the control once (if Control is null), 
                // and then you should apply the values of NewElement to that control every time that NewElement is not null. Something like this:
                // from: https://forums.xamarin.com/discussion/comment/107477/#Comment_107477
                SetSize(e.NewElement.WidthRequest);
                Control.IsChecked = e.NewElement.Checked;
                Control.IsEnabled = e.NewElement.IsEnabled;

                Control.Checked += CheckBoxChecked;
                Control.Unchecked += CheckBoxUnchecked;
            }
        }

        private void SetSize(double size)
        {
            if (size >= 0)
            {
                Control.Width = size;
                Control.Height = size;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Element == null || Control == null) return;

            if (e.PropertyName.Equals(nameof(Checkbox.Checked)))
                Control.IsChecked = Element.Checked;
            else
                base.OnElementPropertyChanged(sender, e);
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            Element.Checked = true;
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            Element.Checked = false;
        }
    }
}