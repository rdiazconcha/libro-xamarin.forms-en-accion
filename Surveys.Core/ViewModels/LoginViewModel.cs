using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Views;

namespace Surveys.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private IWebApiService webApiService = null;
        private IPageDialogService pageDialogService = null;

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value)
                {
                    return;
                }
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, IWebApiService webApiService,
            IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.webApiService = webApiService;
            this.pageDialogService = pageDialogService;

            LoginCommand =
                new DelegateCommand(LoginCommandExecute, LoginCommandCanExecute).ObservesProperty(() => Username)
                    .ObservesProperty(() => Password);
        }

        private async void LoginCommandExecute()
        {
            try
            {
                var loginResult = await webApiService.LoginAsync(Username, Password);

                if (loginResult)
                {
                    await navigationService.NavigateAsync($"app:///{nameof(MainView)}/{nameof(RootNavigationView)}/{nameof(SurveysView)}");
                }
            }
            catch (Exception e)
            {
                await pageDialogService.DisplayAlertAsync("Login", e.Message, Literals.Ok);
            }
        }

        private bool LoginCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}