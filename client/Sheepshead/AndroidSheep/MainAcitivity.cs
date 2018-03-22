using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace AndroidSheep
{
    [Activity(Label = "AndroidSheep"
        , MainLauncher = true
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]

    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new Table2D();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
        
    }

}

