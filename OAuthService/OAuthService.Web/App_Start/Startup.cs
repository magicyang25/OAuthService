using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(OAuthService.Web.Startup))]

namespace OAuthService.Web
{
    public class Startup
    {
        private bool _isProduction
        {
            get
            {
                var result = true;
                #if DEBUG
                result = false;
                #endif
                return result;
            }
        }

        public void Configuration(IAppBuilder app)
        {
            var signingCertificateFileName = ConfigurationManager.AppSettings["SigningCertificateFileName"];
            var signingCertificatePassword = ConfigurationManager.AppSettings["SigningCertificatePassword"];

            var entityFrameworkOptions = new IdentityServer3.EntityFramework.EntityFrameworkServiceOptions
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["OAuthService.IdentityServer"].ConnectionString
            };

            var factory = new IdentityServerServiceFactory().

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            /*
            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(InMemoryManager.GetUsers())
                .UseInMemoryScopes(InMemoryManager.GetScopes())
                .UseInMemoryClients(InMemoryManager.GetClients());
            */
            var ops = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(signingCertificateFileName, signingCertificatePassword),
                RequireSsl = _isProduction,//should be true on production serversss
                //Factory = factory
            };

            app.UseIdentityServer(ops);
        }
    }
}
