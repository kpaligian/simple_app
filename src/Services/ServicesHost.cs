using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Costa.Data.MongoDB;
using MassTransit;
using NHibernate.Engine.Query;
using Topshelf;

namespace Services
{
    public class ServicesHost
    {
        readonly IWindsorContainer _container;

        public ServicesHost()
        {
            IDictionary<string, string> args = new Dictionary<string, string>();
            args.Add("connectionString","mongodb://localhost:27017");
            args.Add("database", "local");
            _container = new WindsorContainer().Install(FromAssembly.This());

            Assembly assembly = Assembly.GetExecutingAssembly();

            ConfigureBus(assembly);
            _container.Register
                (

                    Component.For<IApplicantService>().ImplementedBy<ApplicantService>(),
                    Component.For<IMongoCollectionFactory>()
                        .ImplementedBy<MongoCollectionFactory>()
                        .LifeStyle.Singleton.DependsOn(Dependency.OnValue("connectionString","mongodb://localhost:27017")).DependsOn(Dependency.OnValue("database","local")),
                    Component.For<IMongoDBHandler>().ImplementedBy<MongoDbHandler>().LifeStyle.Singleton
                );

//            _container.Resolve<IMongoCollectionFactory>(
//                new {connectionString = "mongodb://localhost:27017", database = "local"});


            var cfg = HostFactory.New(c =>
            {
                c.SetServiceName("Simple.ApplicantServices");
                c.SetDisplayName("Simple.ApplicantServices");
                c.SetDescription("Applicant Services");

                c.Service<IApplicantService>(a =>
                {
                    a.ConstructUsing(service => _container.Resolve<IApplicantService>());
                    a.WhenStarted(o => o.Start());
                    a.WhenStopped(o => o.Stop());
                });
            });

            try
            {
                cfg.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        void ConfigureBus(Assembly assembly)
        {
            var register = new ServiceBusInstaller(_container, assembly);
            register.InstallBus();
        }
    }
}