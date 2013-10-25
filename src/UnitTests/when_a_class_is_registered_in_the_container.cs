using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NUnit.Framework;

namespace UnitTests
{
    public interface IMyService
    {
        MyData Data { get; set; }
    }

    public class MyService : IMyService
    {
        public MyService(MyData data)
        {
            Data = data;
        }

        public MyData Data { get; set; }
    }

    public class MyData
    {
        public MyData()
        {
            
        }

    }


    public static class MyServiceExtensions
    {
        public static void Clean(this IMyService service)
        {
            
        }
    }


    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register
            (
                Component.For<IMyService>().ImplementedBy<MyService>().LifestyleTransient(),
                Component.For<MyData>().LifestyleSingleton()

                //Classes.FromThisAssembly()
                //       .InSameNamespaceAs<MyService>()
                //       .WithService.DefaultInterfaces()
                //       .LifestyleTransient()
            );
        }
    }


    [TestFixture]
    public class when_a_class_is_registered_in_the_container
    {
        public IWindsorContainer container;
        [SetUp]
        public void Init() 
        { 
            container = new WindsorContainer();
            container.Install(new ServicesInstaller());
        }
        [Test]
        public void it_should_resolve_the_class()
        {
            IMyService service = container.Resolve<IMyService>();
            IMyService service2 = container.Resolve<IMyService>();
            service.Clean();

            Assert.NotNull(service);

//            Assert.Equals(service.Data, service2.Data);
        }
    }
}