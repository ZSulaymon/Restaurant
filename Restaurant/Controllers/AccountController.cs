﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models.Account;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly HomeController _homeController;

        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            HomeController homeController)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _homeController = homeController;
        }
        public void CallGetCountItems()
        {
            var count = _homeController.GetCountItems();
            ViewBag.Count = count;
        }
        [HttpGet]
        public IActionResult Login()
        {
            CallGetCountItems();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("LoginFail", "Username or password is wrong.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            CallGetCountItems();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.CreateAsync(new User
            {
                UserName = model.UserName,
                Email = model.Email,
            }, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
