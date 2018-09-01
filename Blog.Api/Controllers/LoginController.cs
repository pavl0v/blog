﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.ViewModels;
using Blog.Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = "")
        {
            var loginViewModel = new LoginViewModel { Username = "4"};
            return View(loginViewModel);
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