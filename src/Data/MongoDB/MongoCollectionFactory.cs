using System;
using System.Runtime.Remoting.Channels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Costa.Data.MongoDB
{
    public class MongoCollectionFactory : IMongoCollectionFactory
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _database;

        public MongoCollectionFactory(string connectionString, string database)
        {
            _client = new MongoClient(connectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(database);
        }

        public MongoCollection<BsonDocument> Create(string collection)
        {
            return _database.GetCollection(collection);
        }
    }
}