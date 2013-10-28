using System;
using System.Collections.Generic;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using MassTransit;
using MassTransit.Exceptions;
using MassTransit.Pipeline;

namespace Services
{
    public class WindsorConsumerFactory<T> : IConsumerFactory<T> where T : class
    {
        //readonly ILog _logger = LogManager.GetLogger(typeof(WindsorConsumerFactory<T>));
        readonly IWindsorContainer _container;

        public WindsorConsumerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public IEnumerable<Action<IConsumeContext<TMessage>>> GetConsumer<TMessage>(IConsumeContext<TMessage> context, InstanceHandlerSelector<T, TMessage> selector)
            where TMessage : class
        {
            using (_container.BeginScope())
            {
                T consumer = null;

                try
                {
                    consumer = _container.Resolve<T>();

                    if (consumer == null)
                    {
                        throw new ConfigurationException(string.Format("Unable to resolve type '{0}' from container: ", typeof(T)));
                    }

            
                    foreach (var handler in selector(consumer, context))
                    {
                        yield return handler;
                    }

            
            
                }
                finally
                {
                    _container.Release(consumer);
                }

            
            }
        }

   
    }
}