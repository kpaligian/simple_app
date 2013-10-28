using System;
using System.Reflection.Emit;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Owin;
using Simple.Web.Authentication;
using Simple.Web.Http;
using Simple.Web.OwinSupport;
using Simple.Web.Windsor.Owin;
using Simple.Web.Windsor.Owin.Pipeline;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace Simple.Web.Windsor.Owin
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder builder) 
        {
            builder.Use(typeof(CustomTracer));
           // builder.Map("/resources", sw => sw.UseSimpleWeb());
            //builder.UseNancy();
            builder.UseStaticFiles("/Web", "Web");
            builder.UseStaticFiles("/Scripts", "Scripts");
            builder.UseStaticFiles("/Content", "Content");
            builder.UseStaticFiles("/partials", "partials");
            
            //var config = new HttpConfiguration();
            //config.Routes.MapHttpRoute( "DefaultApi","api/{controller}/");
            //builder.UseWebApi(config);
            
            
            IWindsorContainer container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
         

        }
    }

  
  

}
