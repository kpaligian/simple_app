using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Costa.Data;
using MassTransit;
using Simple.Web.Windsor.Owin.App_Start;

namespace Simple.Web.Windsor.Owin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicantContext,Migrations.Configuration>());
            CreateDatabase();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ConfigureBus();
           
        }

        void ConfigureBus()
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();
                sbc.ReceiveFrom("rabbitmq://localhost/simple.web");
            });
        }

        private void CreateDatabase() {
            var context = new ApplicantContext();
            context.Database.CreateIfNotExists();
        }
    }
}