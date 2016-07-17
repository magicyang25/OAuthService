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

            /* use oauth without IdentityServer3.AccessTokenValidation packge 
            var certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(System.Convert.FromBase64String(""), "password");
            app.UseJwtBearerAuthentication(new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { "http://localhost:60375/resources" },
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = "http://localhost:60375/resources",
                    ValidIssuer = "http://localhost:60375",
                    IssuerSigningKey = new System.IdentityModel.Tokens.X509SecurityKey(certificate)
                }
            });
            */

            //use IdentityServer3.AccessTokenValidation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:22710"
            });

            app.UseWebApi(config); //UseWebApi is from Microsoft.AspNet.WebApi.OwinSelfHost package
        }
    }
}
