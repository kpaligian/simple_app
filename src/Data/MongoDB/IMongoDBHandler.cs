using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Costa.Data.MongoDB
{
    public interface IMongoDBHandler
    {
        void Save(Object obj, string collection);
        MongoCursor<BsonDocument> Find(IMongoQuery query, string collection);
    }
}