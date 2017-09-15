using System.Collections.Generic;
using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
    public class SegmentedControl : View, IViewContainer<SegmentedControlOption>
    {
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(SegmentedControl), 0);

        public SegmentedControl()
        {
            Children = new List<SegmentedControlOption>();
        }

        //        public static readonly BindableProperty SelectedTextProperty =
        //            BindableProperty.Create(nameof(SelectedText), typeof (string), typeof (SegmentedControl), 
        //                defaultBindingMode: BindingMode.OneWayToSource);

        public int SelectedIndex
        {
            get => (int) GetValue(SelectedIndexProperty);
            set
            {
                SetValue(SelectedIndexProperty, value);
                OnPropertyChanged(nameof(SelectedText));
            }
        }

        public string SelectedText => Children[SelectedIndex].Text;

        public SegmentedControlOption this[int index] => Children[index];

        #region IViewContainer implementation

        public IList<SegmentedControlOption> Children { get; }

        #endregion
    }
}