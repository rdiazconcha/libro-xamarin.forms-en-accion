using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Surveys.Core.Models;
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

        public async Task<bool> LoginAsync(string username, string password)
        {
            var encodedUsername = WebUtility.UrlEncode(username);
            var encodedPassword = WebUtility.UrlEncode(password);

            var content = new StringContent(
                $"grant_type=password&username={encodedUsername}&password={encodedPassword}", Encoding.UTF8,
                "application/x-www-form-urlencoded");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("keep-alive", "1");
                var uri = new Uri($"{Literals.WebApiServiceBaseAddress}Token");

                using (var response = httpClient.PostAsync(uri.ToString(), content).Result)
                {
                    var value = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var token = JsonConvert.DeserializeObject<TokenResponseModel>(value);

                        var tokenString = token.AccessToken;

                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                        return true;
                    }

                    if (value.Contains("access_denied"))
                    {
                        throw new Exception("Acceso denegado");
                    }

                    throw new Exception();
                }
            }
        }
    }
}