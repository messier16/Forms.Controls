using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;
using TestApp.Pages;

namespace TestApp
{
    public class App : Application
    {
        public static Color AppTint = Color.FromHex("3c8a3f");

        public App()
        {
            MainPage =  new NavigationPage(new HomePage());
            Navigation = (MainPage as NavigationPage).Navigation;
        }

        public static INavigation Navigation { get; private set;}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

