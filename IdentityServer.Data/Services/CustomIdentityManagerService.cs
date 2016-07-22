using IdentityManager;
using IdentityManager.AspNetIdentity;
using IdentityManager.Configuration;
using IdentityServer.Data.Models;

namespace IdentityServer.Data.Services
{
    public static class SimpleIdentityManagerServiceExtensions
    {
        public static void ConfigureIdentityManagerService(this IdentityManagerServiceFactory factory, string connectionString)
        {
            factory.Register(new Registration<DbContext>(resolver => new DbContext(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<RoleStore>());
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<RoleManager>());
            factory.IdentityManagerService = new Registration<IIdentityManagerService, CustomIdentityManagerService>();
        }
    }

    public class CustomIdentityManagerService : AspNetIdentityManagerService<User, string, Role, string>
    {
        public CustomIdentityManagerService(UserManager userMgr, RoleManager roleMgr)
            : base(userMgr, roleMgr)
        {
        }
    }
}
