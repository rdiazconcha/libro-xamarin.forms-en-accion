using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;
using Surveys.Core.Views;
using Surveys.Entities;

namespace Surveys.Core
{
    public class SurveyDetailsViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private ILocalDbService localDbService = null;

        private IEnumerable<Team> localDbTeams = null;

        #region Properties

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                OnPropertyChanged();
            }
        }

        private DateTime birthdate;

        public DateTime Birthdate
        {
            get
            {
                return birthdate;
            }
            set
            {
                if (birthdate == value)
                {
                    return;
                }
                birthdate = value;
                OnPropertyChanged();
            }
        }

        private string favoriteTeam;

        public string FavoriteTeam
        {
            get
            {
                return favoriteTeam;
            }
            set
            {
                if (favoriteTeam == value)
                {
                    return;
                }
                favoriteTeam = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ICommand SelectTeamCommand { get; set; }

        public ICommand EndSurveyCommand { get; set; }

        public SurveyDetailsViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;

            SelectTeamCommand = new DelegateCommand(SelectTeamCommandExecute);
            EndSurveyCommand =
                new DelegateCommand(EndSurveyCommandExecute, EndSurveyCommandCanExecute).ObservesProperty(() => Name)
                    .ObservesProperty(() => FavoriteTeam);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            localDbTeams = await localDbService.GetAllTeamsAsync();

            if (parameters.ContainsKey("id"))
            {
                FavoriteTeam = localDbTeams.First(t => t.Id == (int)parameters["id"]).Name;
            }
        }

        private async void SelectTeamCommandExecute()
        {
            await navigationService.NavigateAsync(nameof(TeamSelectionView), useModalNavigation:true);
        }

        private async void EndSurveyCommandExecute()
        {
            var newSurvey = new Survey()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Birthdate = Birthdate,
                TeamId = localDbTeams.First(t => t.Name == FavoriteTeam).Id
            };

            var geolocationService = Xamarin.Forms.DependencyService.Get<IGeolocationService>();

            if (geolocationService != null)
            {
                try
                {
                    var currentLocation = await geolocationService.GetCurrentLocationAsync();
                    newSurvey.Lat = currentLocation.Item1;
                    newSurvey.Lon = currentLocation.Item2;
                }
                catch (Exception)
                {
                    newSurvey.Lat = 0;
                    newSurvey.Lon = 0;
                }
            }

            await localDbService.InsertSurveyAsync(newSurvey);

            await navigationService.GoBackAsync();
        }

        private bool EndSurveyCommandCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(FavoriteTeam);
        }
    }
}