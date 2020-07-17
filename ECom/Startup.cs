using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECom.Startup))]
namespace ECom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
