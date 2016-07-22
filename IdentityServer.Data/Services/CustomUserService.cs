using IdentityServer.Data.Models;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

namespace IdentityServer.Data.Services
{
    public static class UserServiceExtensions
    {
        public static void ConfigureUserService(this IdentityServerServiceFactory factory, string connString)
        {
            factory.UserService = new Registration<IUserService, CustomUserService>();
            factory.Register(new Registration<UserManager>());
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<DbContext>(resolver => new DbContext(connString)));
        }
    }

    public class CustomUserService : AspNetIdentityUserService<User, string>
    {
        public CustomUserService(UserManager userMgr)
            : base(userMgr)
        {
        }
    }
}
