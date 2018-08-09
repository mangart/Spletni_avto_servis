using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiseranjeVozil.Startup))]
namespace ServiseranjeVozil
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
