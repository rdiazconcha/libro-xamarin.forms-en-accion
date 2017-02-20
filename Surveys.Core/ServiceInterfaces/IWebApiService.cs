using System.Collections.Generic;
using System.Threading.Tasks;
using Surveys.Entities;

namespace Surveys.Core.ServiceInterfaces
{
    public interface IWebApiService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();

        Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys);
    }
}