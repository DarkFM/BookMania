using BookMania.Core.Entities.UserAggregate;
using BookMania.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<NavbarViewComponent> _logger;

        public NavbarViewComponent(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<NavbarViewComponent> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var options = new Microsoft.AspNetCore.Http.CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                IsEssential = true,
                MaxAge = TimeSpan.FromDays(10),
            };

            //ViewBag.IsLoggedIn = _signInManager.IsSignedIn(UserClaimsPrincipal);
            ViewBag.IsLoggedIn = User.Identity.IsAuthenticated;
            ViewBag.UserName = User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(Request.Cookies["Test"]))
            {
                var obj = JsonConvert.DeserializeObject<TestClass>(Request.Cookies["Test"]);
                _logger.LogDebug("Cookie -> Key: {key} || String values: {value}", "Test", string.Join(", ", obj.Str));
            }

            //foreach (var cookie in Request.Cookies)
            //{
            //    _logger.LogDebug("Cookie - Key: {key} | Value: {value}", cookie.Key, cookie.Value);
            //}
            //ViewBag.CartCount = Request.Cookies.

            var cookieVal = JsonConvert.SerializeObject(new TestClass());
                _logger.LogDebug("Writing the cookie with value of" + string.Join(", ", cookieVal));
            HttpContext.Response.Cookies.Append("Test",cookieVal, options);
            return View("Navbar");
        }

        private class TestClass
        {
            public int Count { get; set; } = 5;
            public IEnumerable<string> Str { get; set; } = new List<string> { "a" }.AsReadOnly();

        }

    }
}
