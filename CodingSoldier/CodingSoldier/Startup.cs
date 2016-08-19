using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingSoldier.Startup))]
namespace CodingSoldier
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
