using System;
using MassTransit;
using Topshelf;

namespace Services
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.UseRabbitMqRouting();
                sbc.ReceiveFrom("rabbitmq://localhost/simple.applicantservices");
            });

            var cfg = HostFactory.New(c =>
            {
                c.SetServiceName("Simple.ApplicantServices");
                c.SetDisplayName("Simple.ApplicantServices");
                c.SetDescription("Applicant Services");

                c.Service<ApplicantService>(a =>
                {
                    a.ConstructUsing(service=>new ApplicantService());
                    a.WhenStarted(o=> o.Start());
                    a.WhenStopped(o=>o.Stop());
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
    }
}
