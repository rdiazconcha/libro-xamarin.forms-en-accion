using Microsoft.Practices.Unity;
using Prism.Unity;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Services;
using Surveys.Core.Views;

namespace Surveys.Core
{
    public partial class App : PrismApplication
    {
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(LoginView)}");
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance<ILocalDbService>(new LocalDbService());
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<RootNavigationView>();
            Container.RegisterTypeForNavigation<SurveysView>();
            Container.RegisterTypeForNavigation<SurveyDetailsView>();
            Container.RegisterTypeForNavigation<LoginView>();
            Container.RegisterTypeForNavigation<MainView>();
            Container.RegisterTypeForNavigation<AboutView>();
        }
    }
}