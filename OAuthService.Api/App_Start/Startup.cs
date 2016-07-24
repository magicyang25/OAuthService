using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(OAuthService.Client.App_Start.Startup))]
namespace OAuthService.Client.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var config = GlobalConfiguration.Configuration;

            //use IdentityServer3.AccessTokenValidation package
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:60375"
            });

            app.UseWebApi(config); //UseWebApi is from Microsoft.AspNet.WebApi.OwinSelfHost package
        }
    }
}
