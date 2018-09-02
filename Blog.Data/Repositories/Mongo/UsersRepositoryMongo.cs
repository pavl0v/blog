using Blog.Common.Dto;
using Blog.Data.Interfaces;
using Blog.Data.Repositories.Mongo.Dto;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Repositories.Mongo
{
    public class UsersRepositoryMongo : RepositoryBaseMongo, IUsersRepository
    {
        private readonly IMongoCollection<UserMongoDto> _users;

        public UsersRepositoryMongo(MongoDbSettings settings) : base(settings)
        {
            //BsonClassMap.RegisterClassMap<UserDto>(cm =>
            //{
            //    cm.MapMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance);
            //    cm.MapMember(c => c.Login);
            //    cm.MapMember(c => c.Password);
            //});
            _users = Database.GetCollection<UserMongoDto>(settings.Users);
        }

        public UserDto Get(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            UserMongoDto user = null;
            try
            {
                user = _users.Find(x => x.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // TODO : log
                user = null;
                throw;
            }
            return user;
        }

        public UserDto Get(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            UserMongoDto user = null;
            try
            {
                user = _users.Find(x => x.Login == login && x.Password == password).FirstOrDefault();
            }
            catch(Exception ex)
            {
                // TODO : log
                user = null;
                throw;
            }
            return user;
        }
    }
}
