using System;
using Xamarin.Forms;

namespace TestApp
{
	public class ControlCell<T> : ViewCell where T : View, new()
	{
		public T Control
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
				}
			};

			var control = new T();
			Grid.SetColumn(control, 1);
			grid.Children.Add(control);

			control.SetBinding(View.IsVisibleProperty, "IsVisible");

			View = grid;
		}
	}
}
