using System;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class CheckboxViewModel : ViewModelBase
    {

        public CheckboxViewModel()
        {
            Check2 = false;
        }

        private bool _check1;

        public bool Check1
        {
            get { return _check1; } 
            set { _check1 = value; OnPropertyChanged(); }
        }

        private bool _check2;

        public bool Check2
        {
            get { return _check2; } 
            set { _check2 = value; OnPropertyChanged(); }
        }

        private bool _otherCheck;

        public bool OtherCheck
        {
            get { return _otherCheck; } 
            set { _otherCheck = value; OnPropertyChanged(); }
        }

        private bool _toggleCheck;

        public bool ToggleCheck
        {
            get { return _toggleCheck; } 
            set { _toggleCheck = value; OnPropertyChanged(); }
        }

        private Command _toggleCheckCommand;

        public Command ToggleCheckCommand
        {
            get
            {
                return _toggleCheckCommand ??
                    (_toggleCheckCommand = new Command(
                        () =>
                        {
                            ToggleCheck = !ToggleCheck;
                        }
                    ));
            }
        }
    }
}

