using System;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class SyncViewModel : ViewModelBase
    {
        private IWebApiService webApiService = null;
        private ILocalDbService localDbService = null;

        private string status;

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value)
                {
                    return;
                }
                status = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy == value)
                {
                    return;
                }
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand SyncCommand { get; set; }

        public SyncViewModel(IWebApiService webApiService, ILocalDbService localDbService)
        {
            this.webApiService = webApiService;
            this.localDbService = localDbService;

            SyncCommand = new DelegateCommand(SyncCommandExecute);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            Status = Application.Current.Properties.ContainsKey("lastSync")
                ? $"Última actualización: {(DateTime)Application.Current.Properties["lastSync"]}"
                : "No se han sincronizado los datos";
        }

        private async void SyncCommandExecute()
        {
            IsBusy = true;

            //Envía las encuestas
            var allSurveys = await localDbService.GetAllSurveysAsync();

            if (allSurveys != null && allSurveys.Any())
            {
                await webApiService.SaveSurveysAsync(allSurveys);
                await localDbService.DeleteAllSurveysAsync();
            }

            //Consulta los equipos
            var allTeams = await webApiService.GetTeamsAsync();

            if (allTeams != null && allTeams.Any())
            {
                await localDbService.DeleteAllTeamsAsync();
                await localDbService.InsertTeamsAsync(allTeams);
            }

            Application.Current.Properties["lastSync"] = DateTime.Now;
            await Application.Current.SavePropertiesAsync();
            Status = $"Se enviaron {allSurveys.Count()} encuestas y se obtuvieron {allTeams.Count()} equipos";
            IsBusy = false;
        }
    }
}