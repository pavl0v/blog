using Blog.Common.Dto;
using Blog.Data.Interfaces;
using Blog.Data.Repositories.Mongo.Dto;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Repositories.Mongo
{
    public class PostsRepositoryMongo : RepositoryBaseMongo, IPostsRepository
    {
        private readonly IMongoCollection<PostMongoDto> _posts;

        public PostsRepositoryMongo(MongoDbSettings settings) : base(settings)
        {
            _posts = Database.GetCollection<PostMongoDto>(settings.Posts);
        }

        public int CreatePost(PostDto post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            int res = 0;

            try
            {
                _posts.InsertOne(new PostMongoDto(post));
                res = 1;
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public int DeletePost(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                throw new ArgumentNullException(nameof(postId));

            int res = 0;

            try
            {
                var r = _posts.DeleteOne(x => x.PostId == postId);
                res = r.DeletedCount == 1 ? 1 : 0;
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            IEnumerable<PostDto> res = null;

            try
            {
                res = _posts.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public PostDto GetByPostId(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                throw new ArgumentNullException(nameof(postId));

            PostDto res = null;

            try
            {
                res = _posts.Find(x => x.PostId == postId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public IEnumerable<PostDto> GetPostsByTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            IEnumerable<PostDto> res = null;

            try
            {
                res = _posts.Find(x => x.Tags != null && x.Tags.Contains(tag)).ToList();
            }
            catch(Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public IEnumerable<PostDto> GetPostsByText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            IEnumerable<PostDto> res = null;

            try
            {
                res = _posts.Find(x => x.Message != null && x.Message.Contains(text)).ToList();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public IEnumerable<PostDto> GetPostsByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            IEnumerable<PostDto> res = null;

            try
            {
                res = _posts.Find(x => x.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }

        public IEnumerable<PostDto> GetPostsByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            IEnumerable<PostDto> res = null;

            try
            {
                res = _posts.Find(x => x.Username == username).ToList();
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            return res;
        }
    }
}
