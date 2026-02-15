using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongoDBDeveloperExamPlayground
{
    public class ConfigHelper
    {
        private readonly IConfigurationRoot _configuration;
        private string? currentConnectionString;
        private MongoClientSettings? mongoClientSettings;

        public ConfigHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _configuration = configuration;
        }

        public ConfigHelper SetConnectionString(string name)
        {
            this.currentConnectionString = _configuration[$"ConnectionStrings:{name}"];
            return this;
        }

        public ConfigHelper ConfigureMongoSettings()
        {
            this.mongoClientSettings = MongoClientSettings.FromConnectionString(this.currentConnectionString);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            this.mongoClientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return this;
        }

        public MongoClient GetMongoClient()
        {
            return new MongoClient(this.mongoClientSettings);
        }
    }
}
