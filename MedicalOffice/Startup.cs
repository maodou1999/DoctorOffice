using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CherryCitySoftware.MedicalOffice.Startup))]
namespace CherryCitySoftware.MedicalOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
