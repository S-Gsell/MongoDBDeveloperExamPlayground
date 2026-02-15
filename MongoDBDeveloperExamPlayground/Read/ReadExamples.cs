using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBDeveloperExamPlayground.Read
{
    public class ReadExamples : MongoDBExample
    {
        public ReadExamples(IMongoCollection<BsonDocument> collection) : base(collection) { }

        //db.collection.find(query, projection, options)
        public void FindExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            //var filter = Builders<BsonDocument>.Filter.Empty; // to find all documents
            var findOptions = new FindOptions<BsonDocument> { Limit = 1 };
            try
            {
                var result = _collection.Find(filter).FirstOrDefault();
                Console.WriteLine("Document Found:");
                Console.WriteLine(result.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("No document found");
                Console.WriteLine(ex.Message);
            }
        }

        public void FindAndListWithCursorExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);

            try
            {
                using var cursor = _collection.Find(filter).Limit(10).ToCursor();
                foreach (var document in cursor.ToEnumerable())
                {
                    var formattedDocument = document.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true });
                    Console.WriteLine(formattedDocument);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No documents found");
                Console.WriteLine(ex.Message);
            }
        }

        //db.collection.find(query).sort(sort)
        public void FindAndSortExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Regex(field, $".*{value}.*");
            var sort = Builders<BsonDocument>.Sort.Ascending("title");

            try
            {
                var results = _collection.Find(filter).Sort(sort).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
                }
                Console.WriteLine($"{results.Count} Documents Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No documents found");
                Console.WriteLine(ex.Message);
            }
        }

        //db.collection.find(query).limit(n)
        public void FindAndLimitExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);

            var projection = new BsonDocument
            {
                { "_id", 0 },
                { "title", 1 },
                { $"{field}", 1 }
            };

            try
            {
                var results = _collection.Find(filter).Limit(5).Project(projection).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
                }
                Console.WriteLine($"{results.Count} Documents Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No documents found");
                Console.WriteLine(ex.Message);
            }
        }

        //db.collection.find(query).count()
        public void FindAndCountExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                var count = _collection.CountDocuments(filter);
                Console.WriteLine($"Count found: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error returning count");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.find(query).skip(n)
        public void FindAndSkipExample<T>(string field, T value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(field, value);
            try
            {
                var projection = new BsonDocument
                {
                    { "_id", 0 },
                    { "title", 1 },
                    { $"{field}", 1 }
                };

                var results = _collection.Find(filter).Skip(2).Limit(5).Project(projection).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToJson(new MongoDB.Bson.IO.JsonWriterSettings { Indent = true }));
                }
                Console.WriteLine($"{results.Count} Documents Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No documents found");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
