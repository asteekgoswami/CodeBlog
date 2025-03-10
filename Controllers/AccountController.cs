﻿using CodeBlog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace CodeBlog.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
				var identityUser = new IdentityUser
				{
					UserName = registerViewModel.Username,
					Email = registerViewModel.Email
				};
				var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);
				if (identityResult.Succeeded)
				{
					//assign the user user role
					var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
					if (roleIdentityResult.Succeeded)
					{
						//show success notification
						return RedirectToAction("Login");
					}
				}
			}

            //show error notification
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl )
        {

            var model = new LoginViewModel { ReturnUrl = returnUrl };
			return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

           if(ModelState.IsValid)
            {
				var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
				if (signInResult != null && signInResult.Succeeded)
				{
					if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
					{
						/*return  new RedirectResult(loginViewModel.ReturnUrl);*/
                        return RedirectPermanent(loginViewModel.ReturnUrl);
					}
                   
                    return RedirectToAction("Index", "Home");
				}
				else
				{

					//show errors
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(loginViewModel);
				}
			}

           return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
