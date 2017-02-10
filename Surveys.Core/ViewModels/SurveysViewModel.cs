using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;

namespace Surveys.Core
{
    public class SurveysViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;

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

        #endregion

        public ICommand NewSurveyCommand { get; set; }

        public SurveysViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            Surveys = new ObservableCollection<Survey>();

            NewSurveyCommand = new DelegateCommand(NewSurveyCommandExecute);
        }

        private async void NewSurveyCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(SurveyDetailsView));
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("NewSurvey"))
            {
                Surveys.Add(parameters["NewSurvey"] as Survey);
            }
        }
    }
}