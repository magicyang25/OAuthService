using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Threading.Tasks;

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
                Scope = "openid profile read",
                SignInAsAuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie,
                PostLogoutRedirectUri = "http://localhost:53698/",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = notification =>
                    {
                        var identity = notification.AuthenticationTicket.Identity;

                        identity.AddClaim(new System.Security.Claims.Claim("id_token", notification.ProtocolMessage.IdToken));
                        identity.AddClaim(new System.Security.Claims.Claim("access_token", notification.ProtocolMessage.AccessToken));

                        notification.AuthenticationTicket 
                            = new Microsoft.Owin.Security.AuthenticationTicket(identity, notification.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = notification =>
                    {
                        if(notification.ProtocolMessage.RequestType 
                            == Microsoft.IdentityModel.Protocols.OpenIdConnectRequestType.LogoutRequest)
                        {
                            notification.ProtocolMessage.IdTokenHint 
                                = notification.OwinContext.Authentication.User.FindFirst("id_token").Value;
                        }

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}
