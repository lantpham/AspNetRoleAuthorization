using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetRoleAuthorization.Startup))]
namespace AspNetRoleAuthorization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
