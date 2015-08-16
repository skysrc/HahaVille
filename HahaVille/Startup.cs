using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HahaVille.Startup))]
namespace HahaVille
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
