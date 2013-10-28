using System;
using System.Linq;
using Castle.Windsor.Diagnostics;
using Core.Messages;
using Costa.Data.MongoDB;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Simple.Web.Windsor.Owin.Data;

namespace Services
{
    public class ApplicantEmailSender : Consumes<CreateApplicantMessage>.All
    {
        readonly IMongoCollectionFactory _factory;

        public ApplicantEmailSender(IMongoCollectionFactory factory)
        {
            _factory = factory;
        }

        public void Consume(CreateApplicantMessage message)
        {
            
        }
    }

    /// <summary>
    /// Denormalize Applicant Stuff/Events Into A MongoDB View
    /// 
    /// </summary>
    public class ApplicantViewHandler : 
        Consumes<CreateApplicantMessage>.All
    {
        readonly IServiceBus _bus;
        readonly IMongoDBHandler _mongo;
        private string collection;

        public ApplicantViewHandler(IServiceBus bus, IMongoDBHandler mongo)
        {
            _bus = bus;
            _mongo = mongo;
            collection = "Applicants";
        }

        public void Consume(CreateApplicantMessage msg)
        {
            Console.WriteLine("Creating Applicant: {0} {1} {2}", msg.Applicant.Name, msg.Applicant.Rate, msg.Applicant.Id);
            _mongo.Save(msg.Applicant, this.collection);
            IMongoQuery query = Query<Applicant>.EQ(e => e.Id, msg.Applicant.Id);
            var result_list = _mongo.Find(query, this.collection);
            BsonDocument result = result_list.First();
            Applicant retApplicant = BsonSerializer.Deserialize<Applicant>(result);
            Console.WriteLine("Retrieved Applicant: {0} {1} {2}", retApplicant.Name, retApplicant.Rate, retApplicant.Id);
        }

    }
}