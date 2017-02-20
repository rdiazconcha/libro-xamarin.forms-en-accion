using System.IO;
using Prism.Mvvm;
using Surveys.Entities;
using Xamarin.Forms;

namespace Surveys.Core.ViewModels
{
    public class TeamViewModel : BindableBase
    {
        private int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id == value)
                {
                    return;
                }
                id = value;
                OnPropertyChanged();
            }
        }

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

        private string color;

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color == value)
                {
                    return;
                }
                color = value;
                OnPropertyChanged();
            }
        }

        private ImageSource logo;

        public ImageSource Logo
        {
            get
            {
                return logo;
            }
            set
            {
                if (logo == value)
                {
                    return;
                }
                logo = value;
                OnPropertyChanged();
            }
        }

        public static TeamViewModel GetViewModelFromEntity(Team entity)
        {
            var result = new TeamViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                Logo = ImageSource.FromStream(() => new MemoryStream(entity.Logo))
            };

            return result;
        }
    }
}