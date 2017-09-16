using System;

using Xamarin.Forms;
using TestApp.Pages.CheckboxPages;

namespace TestApp.Pages
{
    public class CheckboxPage : TabbedPage
    {
        public CheckboxPage()
        {
            Title = "Checkbox";
			AutomationId = "page";

            Children.Add(new CheckboxCodePage { Title = "Code", Icon = "code.png"});
			Children.Add(new CheckboxXamlPage { Title = "Markup", Icon = "xaml.png" });
			Children.Add(new CheckboxListCodePage { Title = "List", Icon = "xaml.png" });
        }
    }
}


