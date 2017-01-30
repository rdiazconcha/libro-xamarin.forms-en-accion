using System;

namespace Surveys.Core
{
    public class Survey
    {
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string FavoriteTeam { get; set; }

        public override string ToString()
        {
            return $"{Name} |  {Birthdate} | {FavoriteTeam}";
        }
    }
}