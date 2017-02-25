using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Surveys.Entities;
using Surveys.Web.DAL.SqlServer;

namespace Surveys.Web.Controllers
{
    public class SurveysController : ApiController
    {
        private readonly SurveysProvider surveysProvider = new SurveysProvider();

        [Authorize]
        public async Task<IEnumerable<Survey>> Get()
        {
            var allSurveys = await surveysProvider.GetAllSurveysAsync();
            return allSurveys;
        }

        [Authorize]
        public async Task Post([FromBody] IEnumerable<Survey> surveys)
        {
            if (surveys == null)
            {
                return;
            }

            foreach (var survey in surveys)
            {
                await surveysProvider.InsertSurveyAsync(survey);
            }
        }
    }
}