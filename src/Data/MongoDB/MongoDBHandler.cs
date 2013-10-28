using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Costa.Data.MongoDB
{

    public class MongoDbHandler : IMongoDBHandler
    {
        readonly IMongoCollectionFactory _dbFactory;

        public MongoDbHandler(IMongoCollectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Save(Object obj, string collection)
        {
            var retrievedCollection = _dbFactory.Create(collection);
            retrievedCollection.Insert(obj);
        }

        public MongoCursor<BsonDocument> Find(IMongoQuery query, string collection)
        {
            var retrievedCollection = _dbFactory.Create(collection);
            return retrievedCollection.Find(query);
        }
    }
}
