using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServisVozil.Startup))]
namespace ServisVozil
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
