using System;
using MassTransit;

namespace Services
{
    public class ApplicantService : IApplicantService
    {
        readonly IServiceBus _bus;

        public ApplicantService(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {

            Console.WriteLine("Started Service...");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping Service...");
        }
    }
}