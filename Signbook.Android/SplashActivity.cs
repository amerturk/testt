using Android.App;
using Android.OS;

namespace Signbook.Droid
{
    // Show the splash screen
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = false, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(MainActivity));
        }
    }
}