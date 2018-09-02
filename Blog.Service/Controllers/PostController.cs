using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Service.ViewModels;
using Blog.Client.Services;
using Blog.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.Controllers
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
        public async Task<IActionResult> Index(PostViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Message))
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrWhiteSpace(model.Tags))
                model.Tags = string.Empty;

            var post = new PostDto
            {
                PostId = Guid.NewGuid().ToString(),
                Message = model.Message,
                Tags = model.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList(),
                UserId = string.Empty
            };

            await _postsService.CreatePost(post, Request.Cookies["token"]);

            return RedirectToAction("Index", "Home");
        }
    }
}