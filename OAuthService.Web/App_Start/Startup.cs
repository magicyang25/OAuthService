using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(OAuthService.Web.Startup))]
namespace OAuthService.Web
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "socialnetwork_implicit",
                Authority = "http://localhost:60375/",
                RedirectUri = "http://localhost:53698/",
                ResponseType = "token id_token",
                Scope = "openid profile",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}
