using System.Collections.ObjectModel;
using System.Windows.Input;
using Surveys.Core.ViewModels;
using Xamarin.Forms;

namespace Surveys.Core
{
    public class SurveysViewModel : NotificationObject
    {
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

        public ICommand NewSurveyCommand { get; set; }

        public SurveysViewModel()
        {
            Surveys = new ObservableCollection<Survey>();

            NewSurveyCommand = new Command(NewSurveyCommandExecute);

            MessagingCenter.Subscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete,
                (sender, args) => { Surveys.Add(args); });
        }

        private void NewSurveyCommandExecute()
        {
            MessagingCenter.Send(this, Messages.NewSurvey);
        }
    }
}