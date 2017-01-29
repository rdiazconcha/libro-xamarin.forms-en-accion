using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Surveys.Droid
{
    [Activity(Label = "Surveys.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Core.App());
        }
    }
}