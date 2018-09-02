using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.ViewModels;
using Blog.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostsService _postsService;

        public HomeController(PostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postsService.GetAllPosts(Request.Cookies["token"]);
            return View(new HomeViewModel { Posts = posts });
        }
    }
}