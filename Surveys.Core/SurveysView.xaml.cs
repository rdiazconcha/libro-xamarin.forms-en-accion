using System;
using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class SurveysView : ContentPage
    {
        public SurveysView()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<ContentPage, Survey>(this, Messages.NewSurveyComplete, (sender, args) =>
            {
                SurveysPanel.Children.Add(new Label()
                {
                    Text = args.ToString()
                });
            });
        }

        private async void AddSurveyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetailsView());
        }
    }
}