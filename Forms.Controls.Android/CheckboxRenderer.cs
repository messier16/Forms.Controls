using System;
using Android.Content;
using Android.Content.Res;
using Android.Support.V7.Widget;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Checkbox), typeof(CheckboxRenderer))]

namespace Messier16.Forms.Android.Controls
{
    public class CheckboxRenderer : ViewRenderer<Checkbox, AppCompatCheckBox>, CompoundButton.IOnCheckedChangeListener
    {
        public CheckboxRenderer(Context context) : base(context)
        {

        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            ((IViewController) Element).SetValueFromRenderer(Checkbox.CheckedProperty, isChecked);
        }

        /// <summary>
        ///     Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var unused = DateTime.Now;
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            var sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

            if (sizeConstraint.Request.Width == 0)
            {
                var width = widthConstraint;
                if (widthConstraint <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Default values");
                    width = 100;
                }
                else if (widthConstraint <= 0)
                {
                    width = 100;
                }

                sizeConstraint = new SizeRequest(new Size(width, sizeConstraint.Request.Height),
                    new Size(width, sizeConstraint.Minimum.Height));
            }

            return sizeConstraint;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && Control != null)
            {
                if (Element != null)
                    Element.CheckedChanged -= OnElementCheckedChanged;
                Control.SetOnCheckedChangeListener(null);
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Checkbox> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
                e.OldElement.CheckedChanged -= OnElementCheckedChanged;

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var checkBox = new AppCompatCheckBox(Context);

                    if (Element.CheckboxBackgroundColor != default(Color))
                    {
                        var backgroundColor = GetBackgroundColorStateList(Element.CheckboxBackgroundColor);
                        checkBox.SupportButtonTintList = backgroundColor;
                    }
                    checkBox.SetOnCheckedChangeListener(this);
                    SetNativeControl(checkBox);
                }
                else
                {
                    UpdateEnabled();
                }

                e.NewElement.CheckedChanged += OnElementCheckedChanged;
                Control.Checked = e.NewElement.Checked;
            }
        }

        private ColorStateList GetBackgroundColorStateList(Color color)
        {
            return new ColorStateList(
                new[]
                {
                    new[] {-global::Android.Resource.Attribute.StateEnabled}, // checked
                    new[] {-global::Android.Resource.Attribute.StateChecked}, // unchecked
                    new[] {global::Android.Resource.Attribute.StateChecked} // checked
                },
                new int[]
                {
                    color.WithSaturation(0.1).ToAndroid(),
                    color.ToAndroid(),
                    color.ToAndroid()
                });
        }

        private void OnElementCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Control.Checked = Element.Checked;
        }

        private void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;
        }

        private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.Checked = e.IsChecked;
        }
    }
}