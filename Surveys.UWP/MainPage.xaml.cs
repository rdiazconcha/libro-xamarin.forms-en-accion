using Xamarin.Forms.Platform.UWP;

namespace Surveys.UWP
{
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new Core.App());
        }
    }
}