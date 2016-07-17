using System;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

[assembly: OwinStartup(typeof(OAuthService.Web.Startup))]

namespace OAuthService.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            
            var signingCertificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            var signingCertificatePassword = ConfigurationManager.AppSettings["SigningCertificatePassword"];

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(InMemoryManager.GetUsers())
                .UseInMemoryScopes(InMemoryManager.GetScopes())
                .UseInMemoryClients(InMemoryManager.GetClient());

            var ops = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(signingCertificate, signingCertificatePassword),
                RequireSsl = false,//should be true on production serversss
                Factory = factory
            };

            app.UseIdentityServer(ops);
        }
    }
}
