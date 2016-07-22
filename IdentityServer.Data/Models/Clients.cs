using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.Data.Models
{
    public static class Clients
    {
        public static class CustomScopes
        {
            public const string EmailService = "yzhang_email_service";
        }

        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "SuperPromo Mobile App Client",
                    ClientId = "superpromo",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.ResourceOwner,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("69B5F798-BE55-78BC-8AA8-0035B903DC9B".Sha256())
                    },
                    AllowedScopes = new List<string>
                    { 
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        CustomScopes.EmailService,
                        "superpromo"
                    },
                    AccessTokenLifetime = 3600 * 24 * 7
                },
                new Client
                {
                    ClientName = "WeDiscuz Web App Client",
                    ClientId = "wediscuz",
                    Enabled = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    Flow = Flows.ResourceOwner,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("69B5F798-BE55-78BC-8AA8-0035B9031234".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        CustomScopes.EmailService,
                        "wediscuz"
                    },
                    AccessTokenLifetime = 3600 * 24 * 7
                }
            };
        }
    }
}
