using System;

using Xamarin.Forms;
using TestApp.Pages.RatingBarPages;
using TestApp.Pages.SegmentedControlPages;

namespace TestApp.Pages
{
    public class SegmentedControlPage : TabbedPage
    {
        public SegmentedControlPage()
        {
            Title = "SegmentedControl";
			AutomationId = "page";

            Children.Add(new SegmentedControlCodePage { Title = "Code", Icon="code.png" });
            Children.Add(new SegmentedControlXamlPage { Title = "Markup", Icon = "xaml.png" });
        }
    }
}


