using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DusColl.Startup))]

namespace DusColl
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}