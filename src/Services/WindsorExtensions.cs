using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Magnum.Extensions;
using MassTransit;
using MassTransit.Saga;
using MassTransit.SubscriptionConfigurators;
using MassTransit.WindsorIntegration;

public static class WindsorExtensions
{

    public static void CreateMasstransitConsumersFromContainer(this SubscriptionBusServiceConfigurator configurator, IWindsorContainer container)
    {
        if (configurator == null)
        {
            throw new ArgumentNullException("configurator");
        }
        if (container == null)
        {
            throw new ArgumentNullException("container");
        }

        var consumerTypes = container.FindTypes<IConsumer>(x => x.Implements<ISaga>() == false);
        if (consumerTypes.Count > 0)
        {
            var consumerConfigurator = new WindsorConsumerFactoryConfigurator(configurator, container);
            foreach (var type in consumerTypes)
            {
                consumerConfigurator.ConfigureConsumer(type);
            }
        }

        var sagaTypes = container.FindTypes<ISaga>(x => true);
        if (sagaTypes.Count > 0)
        {
            var sagaConfigurator = new WindsorSagaFactoryConfigurator(configurator, container);

            foreach (Type type in sagaTypes)
            {
                sagaConfigurator.ConfigureSaga(type);
            }
        }
    }

    public static BasedOnDescriptor ComponentsOfTypeInAssembly(this Type type, Assembly assembly)
    {
        return Classes.FromAssembly(assembly).BasedOn(type);
    }

    public static void AddTypesOf<T>(this IWindsorContainer container, Assembly assembly)
    {
        container.Register(typeof(T).ComponentsOfTypeInAssembly(assembly));
    }

    public static void AddTypesOf<T>(this IWindsorContainer container, Assembly assembly, Action<ComponentRegistration> configurer)
    {
        container.Register(typeof(T).ComponentsOfTypeInAssembly(assembly).Configure(configurer));
    }

    public static IList<Type> FindTypes<T>(this IWindsorContainer container, Func<Type, bool> filter)
    {
        return container.Kernel.GetAssignableHandlers(typeof(T)).Select(h => h.ComponentModel.Implementation).Where(filter).ToList();
    }

    public static object ResolveOrRegister(this IWindsorContainer container, Type type)
    {
        if (type.IsClass && !container.Kernel.HasComponent(type))
            container.Kernel.AddComponent(type.FullName, type, LifestyleType.Transient);

        return container.Resolve(type);
    }

    public static T ResolveOrRegister<T>(this IWindsorContainer container)
    {
        return (T)ResolveOrRegister(container, typeof(T));
    }

    public static void InjectProperties(this IWindsorContainer container, object target)
    {
        container.Kernel.InjectProperties(target);
    }

    public static void InjectProperties(this IKernel kernel, object target)
    {
        var type = target.GetType();

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (property.CanWrite && kernel.HasComponent(property.PropertyType))
            {
                var value = kernel.Resolve(property.PropertyType);

                try
                {
                    property.SetValue(target, value, null);
                }

                catch (Exception ex)
                {
                    var message = string.Format("Error setting property {0} on type {1}, See inner exception for more information.", property.Name, type.FullName);

                    throw new Exception(message, ex);
                }
            }
        }
    }

    public static BasedOnDescriptor Except<T>(this BasedOnDescriptor descriptor)
    {
        return descriptor.Unless(t => t == typeof(T));
    }

    public static T[] ResolveAllExcept<T>(this IKernel kernel, Type exception)
    {
        var resolved = new Dictionary<IHandler, object>();
        foreach (var handler in kernel.GetAssignableHandlers(typeof(T)))
        {
            if (!resolved.ContainsKey(handler) && handler.ComponentModel.Implementation != exception)
            {
                var component = kernel.Resolve<T>(handler.ComponentModel.Name);
                resolved.Add(handler, component);
            }
        }

        return resolved.Select(kv => kv.Value).Cast<T>().ToArray();
    }
  
}