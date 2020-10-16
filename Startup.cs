using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HighSchoolManager.Startup))]
namespace HighSchoolManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
