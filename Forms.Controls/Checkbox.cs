using System;
using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
    public class Checkbox : View
    {
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(nameof(Checked), typeof(bool), typeof(Checkbox), false, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((Checkbox) bindable).CheckedChanged?.Invoke(bindable,
                        new CheckedChangedEventArgs((bool) newValue));
                });


        public static readonly BindableProperty TickColorProperty =
            BindableProperty.Create(nameof(TickColor), typeof(Color), typeof(Checkbox), Color.Default,
                BindingMode.TwoWay);

        public static readonly BindableProperty CheckboxBackgroundColorProperty =
            BindableProperty.Create(nameof(CheckboxBackgroundColor), typeof(Color), typeof(Checkbox), Color.Default,
                BindingMode.TwoWay);

        public EventHandler<CheckedChangedEventArgs> CheckedChanged;

        public Color TickColor
        {
            get => (Color) GetValue(TickColorProperty);
            set => SetValue(TickColorProperty, value);
        }

        public Color CheckboxBackgroundColor
        {
            get => (Color) GetValue(CheckboxBackgroundColorProperty);
            set => SetValue(CheckboxBackgroundColorProperty, value);
        }

        public bool Checked
        {
            get => (bool) GetValue(CheckedProperty);
            set
            {
                if (Checked != value) SetValue(CheckedProperty, value);
            }
        }
    }

    public class CheckedChangedEventArgs : EventArgs
    {
        public CheckedChangedEventArgs(bool value)
        {
            Checked = value;
        }

        public bool Checked { get; }
    }
}