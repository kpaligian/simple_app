using MongoDB.Bson;
using MongoDB.Driver;

namespace Costa.Data.MongoDB
{
    public interface IMongoCollectionFactory
    {
        MongoCollection<BsonDocument> Create(string collection);
    }
}