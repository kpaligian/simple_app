using System;
using System.Linq;
using MassTransit;
using Core.Messages;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Simple.Web.Windsor.Owin.Data;
using Costa.Data.MongoDB;

namespace Services
{

    class ApplicantService
    {
        readonly IServiceBus _bus;
        MongoDBHandler mongo;
        private string collection;

        public ApplicantService()
        {
            _bus = Bus.Instance;
            mongo = new MongoDBHandler("mongodb://localhost","local");
            collection = "Applicants";
        }

        public void Start()
        {
            _bus.SubscribeHandler<CreateApplicantMessage>( msg => CreateApplicant(msg));
        }

        public void Stop()
        {
            Console.WriteLine("Stopping Service...");
        }

        public void CreateApplicant(CreateApplicantMessage msg)
        {
            Console.WriteLine("Creating Applicant: {0} {1} {2}", msg.Applicant.Name, msg.Applicant.Rate, msg.Applicant.Id );
            mongo.Save(msg.Applicant,this.collection);
            IMongoQuery query = Query<Applicant>.EQ(e => e.Id, msg.Applicant.Id);
            var result_list = mongo.Find(query, this.collection);
            BsonDocument result = result_list.First();
            Applicant retApplicant = BsonSerializer.Deserialize<Applicant>(result);
            Console.WriteLine("Retrieved Applicant: {0} {1} {2}", retApplicant.Name, retApplicant.Rate, retApplicant.Id);
        }

    }
}
