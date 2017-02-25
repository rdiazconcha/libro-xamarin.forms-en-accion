using System.Web.Http;
using Owin;
using Surveys.Web.App_Start;

namespace Surveys.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            OAuthConfig.ConfigureOAuth(app, config);

            app.UseWebApi(config);

            WebApiConfig.Register(config);
        }
    }
}