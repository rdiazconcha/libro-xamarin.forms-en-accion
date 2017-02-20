using System.Collections.Generic;
using System.Threading.Tasks;
using Surveys.Entities;

namespace Surveys.Core.ServiceInterfaces
{
    public interface ILocalDbService
    {
        Task<IEnumerable<Survey>> GetAllSurveysAsync();

        Task InsertSurveyAsync(Survey survey);

        Task DeleteSurveyAsync(Survey survey);

        Task DeleteAllSurveysAsync();

        Task DeleteAllTeamsAsync();

        Task InsertTeamsAsync(IEnumerable<Team> teams);

        Task<IEnumerable<Team>> GetAllTeamsAsync();
    }
}