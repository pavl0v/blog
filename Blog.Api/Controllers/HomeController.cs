using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Client;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersService _usersService;

        public HomeController(UsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var r = await _usersService.Get("1");
            return View();
        }
    }
}