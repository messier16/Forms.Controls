using Android.App;
using Android.Content.PM;
using Android.OS;
using Messier16.Forms.Controls.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TestApp;

namespace SampleLocalApp.Droid
{
    [Activity(Label = "SampleLocalApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            PlatformTabbedPageRenderer.Init();
            LoadApplication(new App());
        }
    }
}