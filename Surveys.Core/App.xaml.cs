using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new ContentPage()
            {
                Content =
                    new Label()
                    {
                        Text = "Prueba",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
            };
        }
    }
}