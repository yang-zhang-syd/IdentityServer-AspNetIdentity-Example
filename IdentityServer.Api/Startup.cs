using System.Web.Http;
using IdentityManager.Configuration;
using IdentityServer.Api;
using IdentityServer.Data.Models;
using IdentityServer.Data.Services;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServer.Api
{
    public class Startup
    {
        private const string ConnectionString = "DefaultConnection";

        public void Configuration(IAppBuilder app)
        {
            // config identity server
            var factory = new IdentityServerServiceFactory
            {
                CorsPolicyService = new IdentityServer3.Core.Configuration.Registration<ICorsPolicyService>(new DefaultCorsPolicyService {AllowAll = true}),
                ScopeStore = new IdentityServer3.Core.Configuration.Registration<IScopeStore>(new InMemoryScopeStore(Scopes.Get())),
                ClientStore = new IdentityServer3.Core.Configuration.Registration<IClientStore>(new InMemoryClientStore(Clients.Get()))
            };
            factory.ConfigureUserService(ConnectionString);

            app.Map("/identity", idServer => idServer.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "Identity Server for 8sApp",
                RequireSsl = false,
                Factory = factory,
                SigningCertificate = Certificate.Certificate.Get(),
                AuthenticationOptions = new AuthenticationOptions()
                {
                    LoginPageLinks = new[]
                    {
                        new LoginPageLink
                        {
                            Text = "Register",
                            Href = "register"
                        }
                    }
                }
            }));

            // config identity manager
            app.Map("/admin", adminApp =>
            {
                var identityManagerServiceFactory = new IdentityManagerServiceFactory();
                identityManagerServiceFactory.ConfigureIdentityManagerService(ConnectionString);
                var options = new IdentityManagerOptions
                {
                    Factory = identityManagerServiceFactory,
                    SecurityConfiguration = {RequireSsl = false}
                };
                adminApp.UseIdentityManager(options);
            });

            // config web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
