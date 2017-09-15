using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
    public class Checkbox : View
    {
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(nameof(Checked), typeof(bool), typeof(Checkbox), false, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Checkbox)bindable).CheckedChanged?.Invoke(bindable, new CheckedChangedEventArgs((bool)newValue));
                });


        public static readonly BindableProperty TickColorProperty =
            BindableProperty.Create(nameof(TickColor), typeof(Color), typeof(Checkbox), Color.Default, BindingMode.TwoWay);

        public static readonly BindableProperty CheckboxBackgroundColorProperty =
            BindableProperty.Create(nameof(CheckboxBackgroundColor), typeof(Color), typeof(Checkbox), Color.Default, BindingMode.TwoWay);

        public Color TickColor
        {
            get { return (Color)GetValue(TickColorProperty); }
            set { SetValue(TickColorProperty, value); }
        }

        public Color CheckboxBackgroundColor
        {
            get { return (Color)GetValue(CheckboxBackgroundColorProperty); }
            set { SetValue(CheckboxBackgroundColorProperty, value); }
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { if (Checked != value) SetValue(CheckedProperty, value); }
        }

        public EventHandler<CheckedChangedEventArgs> CheckedChanged;
    }

    public class CheckedChangedEventArgs : EventArgs
    {
        public bool Checked { get; private set; }
        public CheckedChangedEventArgs(bool value)
        {
            Checked = value;
        }
    }
}
