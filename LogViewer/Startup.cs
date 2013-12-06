
using LogViewer;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace LogViewer
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}