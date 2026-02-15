using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground
{
    public abstract class MongoDBExample
    {
        protected readonly IMongoCollection<BsonDocument> _collection;

        protected MongoDBExample(IMongoCollection<BsonDocument> collection) { 
            _collection = collection;
        }
    }
}
