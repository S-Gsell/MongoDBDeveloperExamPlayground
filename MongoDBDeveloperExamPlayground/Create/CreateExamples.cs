using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground.Create
{
    public class CreateExamples : MongoDBExample
    {
        public CreateExamples(IMongoCollection<BsonDocument> collection) : base(collection) { }

        public async Task InsertOneAsyncExample(BsonDocument document)
        {
            try
            {
                await _collection.InsertOneAsync(document);
                Console.WriteLine("Document inserted successfully.");
                Console.WriteLine(document.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error inserting document.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task InsertManyAsyncExample(IEnumerable<BsonDocument> documents)
        {
            try
            {
                await _collection.InsertManyAsync(documents);
                Console.WriteLine($"{documents.Count()} Documents inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting many documents.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void BulkWriteExample()
        {

        }
    }
}
