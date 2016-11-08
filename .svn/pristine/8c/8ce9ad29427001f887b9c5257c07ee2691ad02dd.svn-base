using CherryCitySoftware.MedicalOffice.Context;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CherryCitySoftware.MedicalOffice
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EnableDbInitialization"]))
            {
                Database.SetInitializer<ApplicationDbContext>(new ApplicationDbContext.DropCreateAlwaysInitializer());
            }

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            var db = new ApplicationDbContext();
            db.LoggingMessages.Add(
                new Models.LoggingMessage()
                {
                    Category = "Error",
                    EntryDate = DateTime.Now,
                    UserName = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty,
                    Message = ex.ToString()
                });
            db.SaveChanges();
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}
