using Blog.Common.Dto;
using Blog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Data.Mocks
{
    public class PostsRepositoryMock : IPostsRepository
    {
        private readonly Dictionary<string, PostDto> _posts;

        public PostsRepositoryMock()
        {
            _posts = new Dictionary<string, PostDto>();

            _posts.Add("1", new PostDto
            {
                Id = "1",
                Message = "Welcome post of user1",
                Tags = new List<string> { "tag1" },
                UserId = "1",
            });
            _posts.Add("2", new PostDto
            {
                Id = "2",
                Message = "Welcome post of user2",
                Tags = new List<string> { "tag1", "tag2" },
                UserId = "2"
            });
            _posts.Add("3", new PostDto
            {
                Id = "3",
                Message = "Welcome post of user3",
                Tags = null,
                UserId = "3"
            });
        }

        public int CreatePost(PostDto post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            int res = 0;

            try
            {
                _posts.Add(post.Id, post);
                res = 1;
            }
            catch(Exception ex)
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
                _posts.Remove(postId);
                res = 1;
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
            return _posts.Values;
        }

        public PostDto GetByPostId(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                throw new ArgumentNullException(nameof(postId));

            PostDto res = null;

            try
            {
                res = _posts[postId];
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

            return _posts.Values.Where(x => x.UserId == userId);
        }
    }
}
