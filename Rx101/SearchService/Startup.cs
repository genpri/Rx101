using Microsoft.Owin;
using Owin;
using SearchService;

[assembly: OwinStartup(typeof(Startup))]

namespace SearchService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app: app);
        }
    }
}