using Prism.Unity;
using Surveys.Core.Views;
using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SurveysView)}").ConfigureAwait(false);
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootNavigationView>();
            Container.RegisterTypeForNavigation<SurveysView>();
            Container.RegisterTypeForNavigation<SurveyDetailsView>();
        }
    }
}