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

        public async Task<IEnumerable<Team>> Get()
        {
            var allTeams = await teamsProvider.GetAllTeamsAsync();
            var result = new List<Team>(allTeams);
            return result;

            //var result = new List<Team>
            //{
            //    new Team() { Id = 1, Name = "Alianza Lima", Color = "#0000FF" },
            //    new Team() { Id = 2, Name = "América", Color = "#FFFF35" },
            //    new Team() { Id = 3, Name = "Boca Juniors", Color = "#0000FF" },
            //    new Team() { Id = 4, Name = "Caracas FC", Color = "#7C0029" },
            //    new Team() { Id = 5, Name = "Colo-Colo", Color = "#0000FF" },
            //    new Team() { Id = 6, Name = "Peñarol", Color = "#FFFF35" },
            //    new Team() { Id = 7, Name = "Real Madrid", Color = "#E612E3" },
            //    new Team() { Id = 8, Name = "Saprissa", Color = "#7C0029" }
            //};
        }
    }
}