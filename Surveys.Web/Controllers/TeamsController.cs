using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Surveys.Entities;
using Surveys.Web.DAL.SqlServer;

namespace Surveys.Web.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly TeamsProvider teamsProvider = new TeamsProvider();

        [Authorize]
        public async Task<IEnumerable<Team>> Get()
        {
            var allTeams = await teamsProvider.GetAllTeamsAsync();
            var result = new List<Team>(allTeams);

            return result;
        }
    }
}