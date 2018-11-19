using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPanel.Startup))]
namespace CPanel
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
