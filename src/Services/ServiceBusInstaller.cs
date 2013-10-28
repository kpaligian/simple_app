using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Saga;
using MassTransit.NHibernateIntegration.Saga;

namespace Services
{
    public class ServiceBusInstaller
    {
        readonly IWindsorContainer _container;
        readonly Assembly _serviceAssembly;

        public ServiceBusInstaller(IWindsorContainer container, Assembly serviceAssembly)
        {
            _container = container;
            _serviceAssembly = serviceAssembly;
        }

        /// <summary>
        /// Install Fully Configured IServiceBus In The Container
        /// </summary>
        /// <param name="configure"></param>
        public void InstallBus(Action<ServiceBusConfigurator> configure = null)
        {
            var servicesFactory = CreateFactory();
            RegisterMessageHandlersInContainer();
            CreateBusAndRegisterInContainer(configure, servicesFactory);
        }

        /// <summary>
        /// Create IServiceBus Factory
        /// </summary>
        /// <returns></returns>
        SimpleServiceBusFactory CreateFactory()
        {
            //var settings = _container.Resolve<IRabbitMqSettings>();
    //           var application = _container.Resolve<IApplicationSettings>();
            var connectionString = "";
            var servicesFactory = new SimpleServiceBusFactory(_container, connectionString);
            return servicesFactory;
        }

        /// <summary>
        /// Scan The Service Host Assembly For All IConsumer
        /// Register Them In the Container
        /// </summary>
        void RegisterMessageHandlersInContainer()
        {
            var consumerType = typeof(IConsumer);
            _container.Register
                (
                    consumerType.ComponentsOfTypeInAssembly(_serviceAssembly).Configure(c => c.LifeStyle.Scoped()),
                    Component.For(typeof(ISagaRepository<>)).ImplementedBy(typeof(NHibernateSagaRepository<>))
                );
        }

        /// <summary>
        /// Create The Bus Using Factory To Scan The Container For Handlers
        /// Register The Bus In The Container
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="serviceBusFactory"></param>
        void CreateBusAndRegisterInContainer(Action<ServiceBusConfigurator> configure, SimpleServiceBusFactory serviceBusFactory)
        {
            _container.Register
                (
                    Component.For<IServiceBus>()
                        .UsingFactoryMethod(c => serviceBusFactory.Create(configure))
                        .LifeStyle
                        .Singleton
                );
        }
    }
}