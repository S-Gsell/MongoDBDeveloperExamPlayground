using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground.Delete
{
    public class DeleteExamples : MongoDBExample
    {
        public DeleteExamples(IMongoCollection<BsonDocument> collection) : base(collection) { }

        //db.collection.deleteOne()
        public async Task DeleteOneAsyncExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                await _collection.DeleteOneAsync(filter);
                Console.WriteLine("Document deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the document.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.deleteMany()
        public async Task DeleteManyAsyncExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                var result = await _collection.DeleteManyAsync(filter);
                Console.WriteLine($"{result.DeletedCount} documents deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting documents.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.findOneAndDelete()
        public void FindOneAndDeleteExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                var deletedDocument = _collection.FindOneAndDelete(filter);
                if (deletedDocument != null)
                {
                    Console.WriteLine("Document deleted successfully:");
                    Console.WriteLine(deletedDocument.ToJson());
                }
                else
                {
                    Console.WriteLine("No document found matching the criteria.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the document.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.remove()
        public void RemoveExample()
        {

        }
    }
}
