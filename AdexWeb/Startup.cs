using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdexWeb.Startup))]
namespace AdexWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
