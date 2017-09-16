using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Messier16.Forms.iOS.Controls.Native.Checkbox;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsCheckbox = Messier16.Forms.Controls.Checkbox;
using FormsCheckboxCheckedChangedEventArgs = Messier16.Forms.Controls.CheckedChangedEventArgs;
using Messier16.Forms.iOS.Controls;

[assembly: ExportRenderer(typeof(FormsCheckbox), typeof(CheckboxRenderer))]
namespace Messier16.Forms.iOS.Controls
{
	public class CheckboxRenderer : ViewRenderer<FormsCheckbox, Checkbox>
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public new static void Init()
		{
			var temp = DateTime.Now;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				Control.ValueChanged -= ControlValueChanged;

			base.Dispose(disposing);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<FormsCheckbox> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null)
				e.OldElement.CheckedChanged -= OnElementCheckedChanged;

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					// Instantiate the native control and assign it to the Control property
					var width = 140d;
					if (Element.WidthRequest > 0)
					{
						width = Element.WidthRequest;
					}
					var checkBox = new Native.Checkbox.Checkbox(new CGRect(0, 0, width, width));
				    if (e.NewElement.CheckboxBackgroundColor != Color.Default)
				        checkBox.CheckboxBackgroundColor = e.NewElement.CheckboxBackgroundColor.ToUIColor();

					if (e.NewElement.TickColor != Color.Default)
						checkBox.CheckColor = e.NewElement.TickColor.ToUIColor();

					SetNativeControl(checkBox);
					Control.ValueChanged += ControlValueChanged;
				}
				Control.Checked = Element.Checked;
				e.NewElement.CheckedChanged += OnElementCheckedChanged;
			}
		}

		private void OnElementCheckedChanged(object sender, FormsCheckboxCheckedChangedEventArgs e)
		{
			Control.Checked = Element.Checked;
		}

		private void ControlValueChanged(object sender, EventArgs e)
		{
			((IElementController)Element).SetValueFromRenderer(FormsCheckbox.CheckedProperty, Control.Checked);
		}

		//protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		//{
		//    if (Element == null || Control == null) return;

		//    if (e.PropertyName == nameof(Element.IsEnabled))
		//    {
		//        Control.Enabled = Element.IsEnabled;
		//    }
		//    else if (e.PropertyName == nameof(Element.Checked))
		//    {
		//        Control.Checked = Element.Checked;
		//    }
		//    else if (e.PropertyName == nameof(Element.TickColor))
		//    {
		//        Control.TickColor = Element.TickColor.ToUIColor();
		//    }
		//    else if (e.PropertyName == nameof(Element.BackgroundColor))
		//    {
		//        Control.CheckboxBackgroundColor = Element.BackgroundColor.ToUIColor();
		//    }
		//    else if (e.PropertyName == nameof(Element.IsVisible))
		//    {
		//        Control.Hidden = !Element.IsVisible;
		//        Control.SetNeedsDisplay();
		//    }
		//    else
		//    {
		//        base.OnElementPropertyChanged(sender, e);
		//    }
		//    //else if (e.PropertyName == nameof(Element.IsVisible))
		//    //{
		//    //	Control.Hidden = !Element.IsVisible;
		//    //}
		//}
	}
}
