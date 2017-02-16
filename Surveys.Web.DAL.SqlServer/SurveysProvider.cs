using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Entities;

namespace Surveys.Web.DAL.SqlServer
{
    public class SurveysProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } = System.Configuration.ConfigurationManager.ConnectionStrings["Surveys"].ConnectionString;

        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            var result = new List<Survey>();
            var query = "SELECT * FROM Surveys";

            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetSurveyFromReader(reader));
                }
            }

            return result;
        }

        public async Task<int> InsertSurveys(Survey survey)
        {
            if (survey == null)
            {
                return 0;
            }

            var query = @"INSERT INTO Surveys (Id, Name, Birthdate, FavoriteTeam, Lat, Lon) 
                        VALUES
                        (@Id, @Name, @Birthdate, @FavoriteTeam, @Lat, @Lon)";


            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", GetDataValue(survey.Id)),
                new SqlParameter("@Name", GetDataValue(survey.Name)),
                new SqlParameter("@Birthdate", survey.Birthdate),
                new SqlParameter("@FavoriteTeam", GetDataValue(survey.FavoriteTeam)),
                new SqlParameter("@Lat", survey.Lat),
                new SqlParameter("@Lon", survey.Lon)
            };

            var result = await ExecuteNonQueryAsync(query, parameters.ToArray());

            return result;
        }

        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            return new Survey()
            {
                Id = reader[nameof(Survey.Id)].ToString(),
                Name = reader[nameof(Team.Name)].ToString(),
                Birthdate = (DateTime)reader[nameof(Survey.Birthdate)],
                FavoriteTeam = reader[nameof(Survey.FavoriteTeam)].ToString(),
                Lat = (double)reader[nameof(Survey.Lat)],
                Lon = (double)reader[nameof(Survey.Lon)]
            };
        }
    }
}
