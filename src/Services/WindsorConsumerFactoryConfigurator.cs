using System;
using Castle.Windsor;
using Magnum.Reflection;
using MassTransit;
using MassTransit.SubscriptionConfigurators;

namespace Services
{
    public class WindsorConsumerFactoryConfigurator
    {
        readonly SubscriptionBusServiceConfigurator _configurator;
        readonly IWindsorContainer _container;

        public WindsorConsumerFactoryConfigurator(SubscriptionBusServiceConfigurator configurator, IWindsorContainer container)
        {
            _container = container;
            _configurator = configurator;
        }

        public void ConfigureConsumer(Type messageType)
        {
            this.FastInvoke(new[] { messageType }, "Configure");
        }


        public void Configure<T>() where T : class, IConsumer
        {
            _configurator.Consumer(new WindsorConsumerFactory<T>(_container));
        }
    }
}