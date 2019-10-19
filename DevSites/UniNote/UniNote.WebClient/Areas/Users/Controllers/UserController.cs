﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniOrm;
using UniOrm.Application;
using UniOrm.Startup.Web;

namespace UniNote.WebClient.Controllers
{
    [Area("Users")]

    public class UserController : Controller
    {
        readonly IDbFactory dbFactory;
        public UserController()
        {
            // dbFactory = _dbFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllCon()
        {
            return View();
        }
        public IActionResult urlmng()
        {
            return View();
        }
        public IActionResult welcome()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        public async Task<RedirectResult> SignOut()
        {
            await HttpContext.SignOutAsync(AdminAuthorizeAttribute.CustomerAuthenticationScheme);
            return Redirect("~/");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login(string userName, string password)
        {
            var user = APP.LoginAdmin(userName, password);
            // var user = _userService.Login(userName, password);
            if (user != null)
            {
                var authenticationType = AdminAuthorizeAttribute.CustomerAuthenticationScheme;
                var identity = new ClaimsIdentity(authenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(authenticationType, new ClaimsPrincipal(identity));
                return new { isok = true, msg = "" };

            }
            return new { isok = false, msg = "登录失败，用户名密码不正确" };

        }


    }
}
