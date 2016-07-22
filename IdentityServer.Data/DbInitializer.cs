using System.Data.Entity;
using System.Data.Entity.Migrations;
using IdentityServer.Data.Models;

namespace IdentityServer.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            base.Seed(context);

            context.Roles.AddOrUpdate(r => r.Name, new Role[]
            {
                new Role
                {
                    Name = "Admin"
                },
                new Role
                {
                    Name = "User"
                }
            });
        }
    }
}
