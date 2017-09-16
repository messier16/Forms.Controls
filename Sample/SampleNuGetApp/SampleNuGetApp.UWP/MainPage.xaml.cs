namespace SampleLocalApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new TestApp.App());
        }
    }
}