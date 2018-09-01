using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthService _authService;
        private readonly PostsService _postsService;

        public HomeController(AuthService authService, PostsService postsService)
        {
            _authService = authService;
            _postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {
            var token = await _authService.GetToken("user", "password1");
            var r = await _postsService.GetAllPosts(token.Token);
            return View();
        }
    }
}