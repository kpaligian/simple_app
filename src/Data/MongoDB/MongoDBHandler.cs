using System;
using FluentNHibernate.MappingModel.Collections;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Simple.Web.Windsor.Owin.Data;

namespace Costa.Data.MongoDB
{
    public class MongoDBHandler
    {
        private string _connectionString { get; set; }
        private string _database { get; set; }
        MongoClient client;
        MongoServer server;
        MongoDatabase db_reference;

        public MongoDBHandler(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
            client = new MongoClient(connectionString);
            server = client.GetServer();
            db_reference = server.GetDatabase(database);
        }

        public void Save(Object obj, string Collection)
        {
            var collection = db_reference.GetCollection(Collection);
            collection.Insert(obj);
        }

        public MongoCursor<BsonDocument> Find(IMongoQuery query, string Collection)
        {
            var collection = db_reference.GetCollection(Collection);
            return collection.Find(query);
        }
    }
}
