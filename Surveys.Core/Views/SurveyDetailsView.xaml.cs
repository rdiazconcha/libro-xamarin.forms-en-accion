using Surveys.Core.ViewModels;
using Xamarin.Forms;

namespace Surveys.Core
{
    public partial class SurveyDetailsView : ContentPage
    {
        public SurveyDetailsView()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<SurveyDetailsViewModel, string[]>(this, Messages.SelectTeam,
                async (sender, args) =>
                {
                    var favoriteTeam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, args);

                    if (!string.IsNullOrWhiteSpace(favoriteTeam))
                    {
                        MessagingCenter.Send((ContentPage)this, Messages.TeamSelected, favoriteTeam);
                    }
                });

            MessagingCenter.Subscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete,
                async (sender, args) => { await Navigation.PopAsync(); });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<SurveyDetailsViewModel, string[]>(this, Messages.SelectTeam);
            MessagingCenter.Unsubscribe<SurveyDetailsViewModel, Survey>(this, Messages.NewSurveyComplete);
        }
    }
}