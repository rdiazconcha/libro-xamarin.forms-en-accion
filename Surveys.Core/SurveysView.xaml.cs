using System;
using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class SurveysView : ContentPage
    {
        public SurveysView()
        {
            InitializeComponent();
        }

        private async void AddSurveyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SurveyDetailsView());
        }
    }
}