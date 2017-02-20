using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surveys.Core.ServiceInterfaces;
using Surveys.Entities;

namespace Surveys.Core.Services
{
    public class WebApiService : IWebApiService
    {
        private readonly HttpClient client;

        public WebApiService()
        {
            client = new HttpClient { BaseAddress = new Uri(Literals.WebApiServiceBaseAddress) };
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            IEnumerable<Team> result = null;
            var teams = await client.GetStringAsync("/api/teams");

            if (!string.IsNullOrWhiteSpace(teams))
            {
                result = JsonConvert.DeserializeObject<IEnumerable<Team>>(teams);
                return result;
            }

            return result;
        }

        public async Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys)
        {
            var content = new StringContent(JsonConvert.SerializeObject(surveys), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/surveys", content);

            return response.IsSuccessStatusCode;
        }
    }
}