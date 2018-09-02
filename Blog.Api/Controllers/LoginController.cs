using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Service.ViewModels;
using Blog.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Service.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.GetToken(model.Username, model.Password);

                if (!string.IsNullOrWhiteSpace(result.Token))
                {
                    CookieOptions co = new CookieOptions();
                    co.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("token", result.Token, co);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}