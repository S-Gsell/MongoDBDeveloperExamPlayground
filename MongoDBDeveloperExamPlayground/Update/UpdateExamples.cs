using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground.Update
{

    //update operatiors: $set, $inc, $unset, $max
    public class UpdateExamples : MongoDBExample
    {
        public UpdateExamples(IMongoCollection<BsonDocument> collection) : base(collection) { }

        //db.collection.updateOne(filter, update, options)
        public async Task UpdateOneExample<T>(string field, T value, UpdateDefinition<BsonDocument> update)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);

            try
            {
                await _collection.UpdateOneAsync(filter, update);
                Console.WriteLine("Document updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating document.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.updateMany(filter, update, options)
        public async Task UpdateManyAsyncExample<T>(string field, T value, UpdateDefinition<BsonDocument> update)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                var updated = await _collection.UpdateManyAsync(filter, update);
                Console.WriteLine($"{updated.ModifiedCount} documents updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating many documents");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.replaceOne(filter, update, options)
        public void ReplaceOneExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            var newDocument = new BsonDocument
            {
                {"title", "Replaced Document"},
                {field, BsonValue.Create(value)  }
            };

            try
            {
                _collection.Find(filter).First();
                var replaced = _collection.ReplaceOne(filter, newDocument);
                Console.WriteLine($"{replaced.ModifiedCount} documents replaced.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to replace one");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.findOneAndReplace()
        public void FindOneAndReplaceExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            var newDocument = new BsonDocument
            {
                {"title", "FindOneAndReplaced Document"},
                {field, BsonValue.Create(value)  }
            };
            try
            {
                var replaced = _collection.FindOneAndReplace(filter, newDocument);
                Console.WriteLine($"Document replaced successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to find one and replace");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.findOneAndUpdate()
        public void FindOneAndUpdateExample<T, K>(string field, T value, string field2, K value2)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            var update = Builders<BsonDocument>.Update.Set(field2, value2);

            try
            {
                var updatedDocument = _collection.FindOneAndUpdate(filter, update);
                Console.WriteLine("Document updated successfully:");
                Console.WriteLine(updatedDocument.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to find one and update");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
