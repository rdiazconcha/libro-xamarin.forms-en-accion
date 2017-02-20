using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Surveys.Core.ServiceInterfaces;

namespace Surveys.Core.ViewModels
{
    public class TeamSelectionViewModel : ViewModelBase
    {
        private INavigationService navigationService = null;
        private ILocalDbService localDbService = null;

        private ObservableCollection<TeamViewModel> teams;

        public ObservableCollection<TeamViewModel> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                if (teams == value)
                {
                    return;
                }
                teams = value;
                OnPropertyChanged();
            }
        }

        private TeamViewModel selectedTeam;

        public TeamViewModel SelectedTeam
        {
            get
            {
                return selectedTeam;
            }
            set
            {
                if (selectedTeam == value)
                {
                    return;
                }
                selectedTeam = value;
                OnPropertyChanged();
            }
        }

        public TeamSelectionViewModel(INavigationService navigationService, ILocalDbService localDbService)
        {
            this.navigationService = navigationService;
            this.localDbService = localDbService;

            PropertyChanged += TeamSelectionViewModel_PropertyChanged;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var allTeams = await localDbService.GetAllTeamsAsync();

            if (allTeams != null)
            {
                Teams = new ObservableCollection<TeamViewModel>(allTeams.Select(TeamViewModel.GetViewModelFromEntity));
            }
        }

        private async void TeamSelectionViewModel_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedTeam))
            {
                if (SelectedTeam == null)
                {
                    return;
                }

                var param = new NavigationParameters { { "id", SelectedTeam.Id } };
                await navigationService.GoBackAsync(param, true);
            }
        }
    }
}