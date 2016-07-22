using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace IdentityServer.Data.Models
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new Scope[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.OfflineAccess,
                StandardScopes.Roles,
                new Scope
                {
                    Name = "superpromo",
                    DisplayName = "SuperPromo Api",
                    Claims = new List<ScopeClaim>
                    {
                        //new ScopeClaim(Constants.ClaimTypes.Role, true)
                    }
                },
                new Scope
                {
                    Name = "yzhang_email_service",
                    DisplayName = "Email Service Api",
                    Claims = new List<ScopeClaim>
                    {
                        //new ScopeClaim(Constants.ClaimTypes.Role, true)
                    }
                },
                new Scope
                {
                    Name = "auction_results",
                    DisplayName = "Auction Results Api",
                    Claims = new List<ScopeClaim>
                    {
                        //new ScopeClaim(Constants.ClaimTypes.Role, true)
                    }
                }
             };
        }
    }
}
