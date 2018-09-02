using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Client.Services;
using Blog.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class PostController : Controller
    {
        private readonly PostsService _postsService;

        public PostController(PostsService postsService)
        {
            _postsService = postsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string message)
        {
            var post = new PostDto
            {
                Id = Guid.NewGuid().ToString(),
                Message = message,
                UserId = string.Empty
            };

            await _postsService.CreatePost(post, Request.Cookies["token"]);

            return RedirectToAction("Index", "Home");
        }
    }
}