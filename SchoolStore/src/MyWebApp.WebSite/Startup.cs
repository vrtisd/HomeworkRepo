using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebApp.WebSite.Startup))]
namespace MyWebApp.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
