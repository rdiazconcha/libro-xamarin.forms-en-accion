using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Surveys.Core
{
    public class Data : NotificationObject
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

        public Data()
        {
            Surveys = new ObservableCollection<Survey>();

            NewSurveyCommand = new Command(NewSurveyCommandExecute);

            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyComplete,
                (sender, args) => { Surveys.Add(args); });
        }

        private void NewSurveyCommandExecute()
        {
            MessagingCenter.Send(this, Messages.NewSurvey);
        }
    }
}