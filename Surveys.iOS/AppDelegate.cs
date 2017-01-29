using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Surveys.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Xamarin.Forms.Forms.Init();
            LoadApplication(new Core.App());

            return base.FinishedLaunching(application, launchOptions);
        }
    }
}