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
            var token = Request.Cookies["token"];
            var r = await _postsService.GetAllPosts(token);
            return View(new HomeViewModel { Posts = r });
        }
    }
}