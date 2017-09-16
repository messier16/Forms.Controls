using System;
using System.ComponentModel;

namespace TestApp
{
	public class ControlWrapper : INotifyPropertyChanged
	{
		private bool isVisible;
		public bool IsVisible
		{
			get { return isVisible;}
			set { isVisible = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
