using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground.Indexes
{
    public class IndexExamples : MongoDBExample
    {
        public IndexExamples(IMongoCollection<BsonDocument> collection) : base(collection) { }

        public void CreateIndexExample(string field, string field2)
        {
            var indexModel = new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending(field).Ascending(field2)
            );

            try
            {
                var indexName = _collection.Indexes.CreateOne(indexModel);
                Console.WriteLine($"Index created successfully: {indexName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating index");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void GetIndexesExample()
        {
            try
            {
                var indexes = _collection.Indexes.List().ToList();
                Console.WriteLine("Indexes in the collection:");
                foreach (var index in indexes)
                {
                    Console.WriteLine(index.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving indexes");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DropIndexExample(string indexName)
        {
            try
            {
                _collection.Indexes.DropOne(indexName);
                Console.WriteLine($"Index '{indexName}' dropped successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error dropping index");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
