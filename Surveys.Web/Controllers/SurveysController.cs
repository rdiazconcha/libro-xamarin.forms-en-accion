using System;
using System.Collections.Generic;
using System.Web.Http;
using Surveys.Entities;

namespace Surveys.Web.Controllers
{
    public class SurveysController : ApiController
    {
        // GET: api/Surveys
        public IEnumerable<Survey> Get()
        {
            var result = new List<Survey>();

            for (int i = 0; i < 5; i++)
            {
                result.Add(new Survey()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Persona" + i,
                    Birthdate = new DateTime(1977, 1, 1).AddMonths(i),
                    FavoriteTeam = Guid.NewGuid().ToString(),
                    Lat = i * 3.14,
                    Lon = i * 3.14 * 100
                });
            }

            return result;
        }

        // GET: api/Surveys/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Surveys
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Surveys/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Surveys/5
        public void Delete(int id)
        {
        }
    }
}