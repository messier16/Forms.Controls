namespace TestApp.ViewModels
{
    public class SegmentedControlViewModel : ViewModelBase
    {
        public SegmentedControlViewModel()
        {
            SelectedIx = 0;
        }
        private int _selectedIx;
        public int SelectedIx
        {
            get { return _selectedIx; }
            set { _selectedIx = value; OnPropertyChanged(); }
        }

        private string _sportText;
        public string SportText
        {
            get { return _sportText; }
            set { _sportText = value; OnPropertyChanged(); }
        }
    }
}
