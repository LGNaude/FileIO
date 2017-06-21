using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileIOProject.Startup))]
namespace FileIOProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
