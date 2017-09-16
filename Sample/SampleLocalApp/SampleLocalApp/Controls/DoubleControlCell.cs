using System;
using Xamarin.Forms;

namespace TestApp
{
	public class ControlCell<T1,T2> : ViewCell where T1 : View, new() where T2 : View, new()
    {
        public T1 Control1
        {
            get;
            private set;
        }

        public T2 Control2
        {
            get;
            private set;
        }

        public ControlCell()
		{
			var grid = new Grid()
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
			};

            var control1 = new T1();
            Grid.SetColumn(control1, 1);
            grid.Children.Add(control1);

            var control2 = new T2();
            Grid.SetColumn(control2, 2);
            grid.Children.Add(control2);

            control1.SetBinding(View.IsVisibleProperty, "IsVisible");
            control2.SetBinding(View.IsVisibleProperty, "IsVisible");

            View = grid;
		}
	}
}
