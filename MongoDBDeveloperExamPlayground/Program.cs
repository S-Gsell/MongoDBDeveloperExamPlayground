using MongoDB.Driver;
using MongoDB.Bson;
using MongoDBDeveloperExamPlayground;
using MongoDBDeveloperExamPlayground.Create;
using MongoDBDeveloperExamPlayground.Read;
using MongoDBDeveloperExamPlayground.Update;
using MongoDBDeveloperExamPlayground.Delete;
using MongoDBDeveloperExamPlayground.Aggregation;
using MongoDBDeveloperExamPlayground.Indexes;

var client = new ConfigHelper()
    .SetConnectionString("MongoDb")
    .ConfigureMongoSettings()
    .GetMongoClient();

// Create a new client and connect to the server
var database = client.GetDatabase("sample_mflix");
var collection = database.GetCollection<BsonDocument>("movies");


var createExamples = new CreateExamples(collection);
var readExamples = new ReadExamples(collection);
var updateExamples = new UpdateExamples(collection);
var deleteExamples = new DeleteExamples(collection);
var aggregationExamples = new AggregationExamples(collection);
var indexExamples = new IndexExamples(collection);


//Create One
var createDocument = new BsonDocument
            {
                { "title", "Holly Is Great: The Trilogy" },
                { "year", 2025 },
                { "directors", new BsonArray {"Steven G" } },
                { "genres", new BsonArray { "Action", "Sci-Fi", "Thriller" } }
            };
//await createExamples.InsertOneAsyncExample(createDocument);

//Update One
var updateOne = Builders<BsonDocument>.Update.Set("cast", new BsonArray { "Stevie G", "Holly the Rock Johnson", "Mikey Boy" });
//await updateExamples.UpdateOneExample("title", "Holly Is Great: The Trilogy", updateOne);

//Read One
//readExamples.FindExample("title", "Holly Is Great: The Trilogy");

//Delete One
//await deleteExamples.DeleteOneAsyncExample("title", "Holly Is Great: The Trilogy");



//Create Many
//await createExamples.InsertManyAsyncExample(new List<BsonDocument>
//{
//    new BsonDocument
//    {
//        { "title", "We be poppin in the 90s" },
//        { "year", 1996 },
//        { "directors", new BsonArray {"Steven G"} },
//        { "genres", new BsonArray { "Action", "Adventure" } }
//    },
//    new BsonDocument
//    {
//        { "title", "Oh snap the world didn't end when the Myan calendar did" },
//        { "year", 2012 },
//        { "directors", new BsonArray {"Steven G"} },
//        { "genres", new BsonArray { "Action", "Thriller" } }
//    },
//    new BsonDocument
//    {
//        { "title", "When do flying cars become reality?" },
//        { "year", 2026 },
//        { "directors", new BsonArray {"Steven G"} },
//        { "genres", new BsonArray { "Action", "Sci-Fi" } }
//    }
//});

//Update Many
var updateMany = Builders<BsonDocument>.Update.Push("directors", "Holly G");
var updateMany2 = Builders<BsonDocument>.Update.Set("updatedAt", DateTime.UtcNow);

var combinedUpdate = Builders<BsonDocument>.Update.Combine(updateMany, updateMany2);
//await updateExamples.UpdateManyAsyncExample("directors", "Steven G", combinedUpdate);

//Read Many
//readExamples.FindAndListWithCursorExample("directors", "Steven G");


//Delete Many
//await deleteExamples.DeleteManyAsyncExample("directors", "Steven G");



//readExamples.FindAndSortExample("title", "fun");
//readExamples.FindAndLimitExample("year", 2000);
//readExamples.FindAndCountExample("year", 2000);
//readExamples.FindAndSkipExample("year", 2000);

//updateExamples.ReplaceOneExample("year", 2000);
//updateExamples.FindOneAndUpdateExample("year", 2001, "title", "I HAVE BEEN UPDATED");
//deleteExamples.FindOneAndDeleteExample("title", "I HAVE BEEN UPDATED");

//aggregationExamples.MatchExample("year", 2010);
//aggregationExamples.GroupExample("year");
//aggregationExamples.SortExample("year");
//aggregationExamples.ProjectExample("title", "year");

//indexExamples.CreateIndexExample("title", "year");
//indexExamples.GetIndexesExample();
//indexExamples.DropIndexExample("title_1_year_1");

client.Dispose();