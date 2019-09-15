using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HikerTrails.Startup))]
namespace HikerTrails
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
