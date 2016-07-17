
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace OAuthService.Web
{
    public static class InMemoryManager
    {
        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject ="test@test.com",
                    Username = "test@test.com",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(Constants.ClaimTypes.Name, "Tester")
                    }
                }
            };
        }

        public static IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "read",
                    DisplayName ="Read user data"
                }
            };
        }

        public static IEnumerable<Client> GetClient()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Flow = Flows.ResourceOwner,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        "read"
                    },
                    Enabled = true
                }
            };
        }

    }
}
