using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CollegeAdministration.Startup))]
namespace CollegeAdministration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
