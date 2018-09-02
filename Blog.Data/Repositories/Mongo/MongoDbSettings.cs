using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Repositories.Mongo
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; }
        public string Database { get; }
        public string Posts { get; }
        public string Users { get; }

        public MongoDbSettings()
        {
            ConnectionString = "mongodb://localhost:27017";
            Database = "blog";
            Posts = "posts";
            Users = "users";
        }
    }
}
