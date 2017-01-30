using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SurveysView());
        }
    }
}