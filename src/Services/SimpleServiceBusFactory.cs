using System;
using Castle.Windsor;
using MassTransit;
using MassTransit.BusConfigurators;

namespace Services
{
    public class SimpleServiceBusFactory
    {
        readonly IWindsorContainer _container;
        readonly string _connectionStringBuilder;

        public SimpleServiceBusFactory(IWindsorContainer container, string connectionStringBuilder)
        {
            _container = container;
            _connectionStringBuilder = connectionStringBuilder;
        }

        public IServiceBus Create(Action<ServiceBusConfigurator> configure)
        {
            return ServiceBusFactory.New(sbc =>
            {
                sbc.ReceiveFrom("rabbitmq://localhost/simple.applicantservices");
                sbc.EnableMessageTracing();
                sbc.UseRabbitMq();
                //sbc.UseLog4Net();
                sbc.Subscribe(subs => subs.CreateMasstransitConsumersFromContainer(_container));

                if (configure != null)
                {
                    configure(sbc);
                }
            });
        }
    }
}