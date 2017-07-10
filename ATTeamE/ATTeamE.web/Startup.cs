using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATTeamE.web.Startup))]
namespace ATTeamE.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
