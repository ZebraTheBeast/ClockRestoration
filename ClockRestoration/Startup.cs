using Microsoft.Owin;
using Owin;
using System.Web.ModelBinding;

[assembly: OwinStartup(typeof(ClockRestoration.Startup))]
namespace ClockRestoration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}