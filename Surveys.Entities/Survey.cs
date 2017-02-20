using System;

namespace Surveys.Entities
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public int TeamId { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} |  {Birthdate} | {TeamId} | {Lat} | {Lon}";
        }
    }
}