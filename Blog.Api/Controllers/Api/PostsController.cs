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

            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role);
            var userId = claim.Value;
            post.UserId = userId;

            return RepositoryFacade.Posts.CreatePost(post);
        }
    }
}