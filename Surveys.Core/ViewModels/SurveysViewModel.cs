using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Surveys.Core.ServiceInterfaces;
using Surveys.Entities;

namespace Surveys.Core
{
    public class SurveysViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private IPageDialogService pageDialogService = null;
        private ILocalDbService localDbService = null;

        #region Properties

        private ObservableCollection<Survey> surveys;

        public ObservableCollection<Survey> Surveys
        {
            get
            {
                return surveys;
            }
            set
            {
                if (surveys == value)
                {
                    return;
                }
                surveys = value;
                OnPropertyChanged();
            }
        }

        private Survey selectedSurvey;

        public Survey SelectedSurvey
        {
            get
            {
                return selectedSurvey;
            }
            set
            {
                if (selectedSurvey == value)
                {
                    return;
                }
                selectedSurvey = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty => Surveys == null || !Surveys.Any();

        #endregion

        public ICommand NewSurveyCommand { get; set; }

        public ICommand DeleteSurveyCommand { get; set; }

        public SurveysViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            ILocalDbService localDbService = null)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.localDbService = localDbService;

            Surveys = new ObservableCollection<Survey>();

            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
            DeleteSurveyCommand =
                new DelegateCommand(DeleteSurveyCommandExecute, DeleteSurveyCommandCanExecute).ObservesProperty(
                    () => SelectedSurvey);
        }

        private async void NewSurveyCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(SurveyDetailsView));
        }

        private async void DeleteSurveyCommandExecute()
        {
            if (SelectedSurvey == null)
            {
                return;
            }

            var result = await pageDialogService.DisplayAlertAsync(Literals.DeleteSurveyTitle,
                Literals.DeleteSurveyConfirmation, Literals.Ok, Literals.Cancel);

            if (result)
            {
                await localDbService.DeleteSurveyAsync(SelectedSurvey);

                await LoadSurveysAsync();
            }
        }

        private bool DeleteSurveyCommandCanExecute()
        {
            return SelectedSurvey != null;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadSurveysAsync();
        }

        private async Task LoadSurveysAsync()
        {
            var allSurveys = await localDbService.GetAllSurveysAsync();
            if (allSurveys != null)
            {
                Surveys = new ObservableCollection<Survey>(allSurveys);
            }
            OnPropertyChanged(nameof(IsEmpty));
        }
    }
}