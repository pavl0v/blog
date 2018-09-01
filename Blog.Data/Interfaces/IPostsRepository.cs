using Blog.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Interfaces
{
    public interface IPostsRepository
    {
        int CreatePost(PostDto post);
        int DeletePost(string postId);
        PostDto GetByPostId(string postId);
        IEnumerable<PostDto> GetAllPosts();
        IEnumerable<PostDto> GetPostsByUserId(string userId);
    }
}
