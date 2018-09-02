using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Common.Dto;
using Blog.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BlogApiControllerBase
    {
        public PostsController(RepositoryFacade repositoryFacade) : base(repositoryFacade)
        {
            //
        }

        [HttpGet("all")]
        [Authorize]
        public ActionResult<IEnumerable<PostDto>> GetAll()
        {
            return RepositoryFacade.Posts.GetAllPosts().ToArray();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<int> Create([FromBody] PostDto post)
        {
            // User ID is stored in default role claim
            // TODO : create custom ID claim in Api.AuthController

            var claimName = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name);
            var claimRole = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role);
            post.UserId = claimRole.Value;
            post.Username = claimName.Value;

            return RepositoryFacade.Posts.CreatePost(post);
        }

        [HttpGet("tag/{tag}")]
        [Authorize]
        public ActionResult<IEnumerable<PostDto>> GetByTag(string tag)
        {
            return RepositoryFacade.Posts.GetPostsByTag(tag).ToArray();
        }

        [HttpGet("text/{text}")]
        [Authorize]
        public ActionResult<IEnumerable<PostDto>> GetByText(string text)
        {
            return RepositoryFacade.Posts.GetPostsByText(text).ToArray();
        }

        [HttpGet("username/{username}")]
        [Authorize]
        public ActionResult<IEnumerable<PostDto>> GetByUsername(string username)
        {
            return RepositoryFacade.Posts.GetPostsByUsername(username).ToArray();
        }
    }
}