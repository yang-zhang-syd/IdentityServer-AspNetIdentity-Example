using System.Data.Entity;
using IdentityServer.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityServer.Data
{
    public class DbContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        static DbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbContext(string connString) : base(connString)
        {
        }

        public DbContext() : this("DefaultConnection")
        {
        }
    }
}
