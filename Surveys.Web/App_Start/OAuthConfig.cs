using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Surveys.Web.Providers;

namespace Surveys.Web.App_Start
{
    public class OAuthConfig
    {
        public static string PublicClientId { get; }
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        static OAuthConfig()
        {
            PublicClientId = "LibroXamarinForms";
        }

        public static void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AuthorizeEndpointPath = new PathString("/auth"),
                Provider = new AppOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                Realm = "LibroXamarinForms"
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}