using System;
using System.Linq;
using Android.Widget;
using Messier16.Forms.Android.Controls;
using Messier16.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]

namespace Messier16.Forms.Android.Controls
{
    public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, Spinner>
    {
        /// <summary>
        ///     Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var spinner = new Spinner(Context);
                    var values = e.NewElement.Children.Select(option => option.Text).ToList();
                    var adapter = new ArrayAdapter<string>(Context, global::Android.Resource.Layout.SimpleDropDownItem1Line, values);
                    spinner.Adapter = adapter;
                    SetNativeControl(spinner);
                }
                Control.ItemSelected += Control_ItemSelected;
            }
        }

        private void Control_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Element.SelectedIndex = e.Position;
        }
    }
}