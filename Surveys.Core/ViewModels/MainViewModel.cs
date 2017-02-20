using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Surveys.Core.Models;
using Surveys.Core.Views;

namespace Surveys.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Module> modules;

        public ObservableCollection<Module> Modules
        {
            get
            {
                return modules;
            }
            set
            {
                if (modules == value)
                {
                    return;
                }
                modules = value;
                OnPropertyChanged();
            }
        }

        private Module selectedModule;

        public Module SelectedModule
        {
            get
            {
                return selectedModule;
            }
            set
            {
                if (selectedModule == value)
                {
                    return;
                }
                selectedModule = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            Modules = new ObservableCollection<Module>
            {
                new Module()
                {
                    Icon = "survey.png",
                    Title = "Encuestas",
                    LoadModuleCommand =
                        new DelegateCommand(
                            async () =>
                                await navigationService.NavigateAsync(
                                    $"{nameof(RootNavigationView)}/{nameof(SurveysView)}"))
                },
                new Module()
                {
                    Icon = "sync.png",
                    Title = "Sincronización",
                    LoadModuleCommand =
                        new DelegateCommand(
                            async () =>
                                await navigationService.NavigateAsync($"{nameof(RootNavigationView)}/{nameof(SyncView)}"))
                },
                new Module()
                {
                    Icon = "about.png",
                    Title = "Acerca de...",
                    LoadModuleCommand =
                        new DelegateCommand(
                            async () =>
                                await navigationService.NavigateAsync(
                                    $"{nameof(RootNavigationView)}/{nameof(AboutView)}"))
                }
            };

            PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedModule))
            {
                SelectedModule?.LoadModuleCommand.Execute(null);
            }
        }
    }
}