using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using Surveys.Entities;

namespace Surveys.Core.ViewModels
{
    public class SurveyViewModel : BindableBase
    {
        public string Id { get; set; }

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

        private TeamViewModel team;

        public TeamViewModel Team
        {
            get
            {
                return team;
            }
            set
            {
                if (team == value)
                {
                    return;
                }
                team = value;
                OnPropertyChanged();
            }
        }

        private double lat;

        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                if (lat == value)
                {
                    return;
                }
                lat = value;
                OnPropertyChanged();
            }
        }

        private double lon;

        public double Lon
        {
            get
            {
                return lon;
            }
            set
            {
                if (lon == value)
                {
                    return;
                }
                lon = value;
                OnPropertyChanged();
            }
        }

        public static SurveyViewModel GetViewModelFromEntity(Survey entity, IEnumerable<Team> teams)
        {
            var result = new SurveyViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Birthdate = entity.Birthdate,
                Team = TeamViewModel.GetViewModelFromEntity(teams.First(t => t.Id == entity.TeamId)),
                Lat = entity.Lat,
                Lon = entity.Lon
            };

            return result;
        }

        public static Survey GetEntityFromViewModel(SurveyViewModel viewModel)
        {
            var result = new Survey
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Birthdate = viewModel.Birthdate,
                TeamId = viewModel.Team.Id,
                Lat = viewModel.Lat,
                Lon = viewModel.Lon
            };

            return result;
        }
    }
}