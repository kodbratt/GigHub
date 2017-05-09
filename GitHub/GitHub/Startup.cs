using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GitHub.Startup))]
namespace GitHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
