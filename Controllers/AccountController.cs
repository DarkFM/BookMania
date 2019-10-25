using BookMania.Core.Entities.UserAggregate;
using BookMania.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user;

            if (model.EmailUserName.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.EmailUserName);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.EmailUserName);
            }


            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email/Password!");
                return View();
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.PersistUser, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Credentials. please try again");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user;
            user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                ModelState.AddModelError("username", "Profile Name Already Exists");
                return View(model);
            }

            user = new ApplicationUser(model.FirstName, model.LastName)
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(model);
            }
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return Json($"Email {email} is already in use.");
            }
            return Json(true);
        }
    }
}
