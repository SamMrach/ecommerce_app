using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projet1.Startup))]
namespace projet1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
