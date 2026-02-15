using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBDeveloperExamPlayground.Aggregation
{
    public class AggregationExamples : MongoDBExample
    {
        public AggregationExamples(IMongoCollection<BsonDocument> collection) : base(collection){}

        public void MatchExample<T>(string field, T value)
        {
            var greaterThanFilter = Builders<BsonDocument>.Filter.Gt(field, value);

            var pipeline = new EmptyPipelineDefinition<BsonDocument>()
                .Match(greaterThanFilter);

            try
            {
                var result = _collection.Aggregate(pipeline).ToList();
                Console.WriteLine($"{result.Count} matched documents.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting match example");
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public void GroupExample(string field)
        {
            var pipeline = new EmptyPipelineDefinition<BsonDocument>()
                .Group(new BsonDocument
                {
                    { "_id", $"${field}" },
                    { "count", new BsonDocument("$sum", 1) }
                });

            try
            {
                var results = _collection.Aggregate(pipeline).ToList();
                foreach(var result in results)
                {
                    Console.WriteLine(result.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not perform group aggregation");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void SortExample(string field)
        {

            var projection = new BsonDocument
            {
                { field, 1 },
            };

            var pipeline = new EmptyPipelineDefinition<BsonDocument>()
                .Sort(Builders<BsonDocument>.Sort.Ascending(field)).Limit(10).Project(projection);

            try
            {
                var results = _collection.Aggregate(pipeline).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not perform sort aggregation");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //db.collection.find({}, {field: 1, field2: 1}).limit(10)
        public void ProjectExample(string field, string field2)
        {
            var projection = new BsonDocument
            {
                { field, 1 },
                { field2, 1}
            };
            var pipeline = new EmptyPipelineDefinition<BsonDocument>()
                .Project(projection).Limit(10);
            try
            {
                var results = _collection.Aggregate(pipeline).ToList();
                foreach (var result in results)
                {
                    Console.WriteLine(result.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not perform project aggregation");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
