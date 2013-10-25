using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Costa.Data;
using Simple.Web.Windsor.Owin.Controllers;
using Simple.Web.Windsor.Owin.Data;
using Simple.Web.Windsor.Owin.Resources;

namespace Simple.Web.Windsor.Owin.Components
{
    public class ComponentsInstaller : IWindsorInstaller
    {
        public ComponentsInstaller() { }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register
            (
                Classes.FromThisAssembly().BasedOn<IGet>().LifestyleScoped(),
                Classes.FromThisAssembly().BasedOn<IPost>().LifestyleScoped(),
                Classes.FromThisAssembly().BasedOn(typeof(IPost<>)).LifestyleScoped()
            );

            container.Register
            (
                Component.For<DataService>().LifestyleSingleton(),
                Component.For<System.Object>().LifestyleTransient(),
                Component.For<HomeController>().LifestylePerWebRequest(),
                Component.For<ApplicantContext>().LifestyleSingleton(),
                Component.For<IApplicantsRepository>().ImplementedBy<ApplicantsRepository>().LifestylePerWebRequest()
            );
        }
    }
}