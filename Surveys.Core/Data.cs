using System.Collections.ObjectModel;
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

        public Data()
        {
            Surveys = new ObservableCollection<Survey>();

            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyComplete,
                (sender, args) => { Surveys.Add(args); });
        }
    }
}