using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Data.Services
{
    public class UserStore : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(DbContext ctx) : base(ctx)
        {
        }
    }

    public class UserManager : UserManager<User, string>
    {
        public UserManager(UserStore store) : base(store)
        {
            ClaimsIdentityFactory = new ClaimsFactory();
        }
    }

    public class ClaimsFactory : ClaimsIdentityFactory<User, string>
    {
        public ClaimsFactory()
        {
            UserIdClaimType = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            UserNameClaimType = IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName;
            RoleClaimType = IdentityServer3.Core.Constants.ClaimTypes.Role;
        }

        public override async Task<ClaimsIdentity> CreateAsync(UserManager<User, string> manager, User user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);
            if (!String.IsNullOrWhiteSpace(user.FirstName))
            {
                ci.AddClaim(new Claim("given_name", user.FirstName));
            }
            if (!String.IsNullOrWhiteSpace(user.LastName))
            {
                ci.AddClaim(new Claim("family_name", user.LastName));
            }
            return ci;
        }
    }

    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(DbContext ctx) : base(ctx)
        {
        }
    }

    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore store) : base(store)
        {
        }
    }
}
