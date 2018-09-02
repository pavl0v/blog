using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Repositories.Mongo
{
    public abstract class RepositoryBaseMongo
    {
        private readonly MongoDbSettings _settings;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        protected IMongoDatabase Database
        {
            get { return _database; }
        }

        protected MongoDbSettings Settings
        {
            get { return _settings; }
        }

        public RepositoryBaseMongo(MongoDbSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.Database);
        }
    }
}
