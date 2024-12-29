using System.Linq;
using System.Security.Claims;
using Data;
using Data.Constants;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductShoppingCartMvcUI.Repositories;
namespace e_commerce.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IUserRepository _userRepository;


        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        [HttpGet]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }


        public IActionResult AccessDenied(string returnUrl = null)
        {
            // You can pass the ReturnUrl to the view to allow the user to potentially retry or redirect
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            string Role = "";
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            var existingUser = await _userRepository.FindByNameAsync(userName);

            if (existingUser == null)
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            if (existingUser.Role== (int)Roles.User)
            {
                Role=nameof(Roles.User);
            }
            if (existingUser.Role == (int)Roles.Admin)
            {
                Role = nameof(Roles.Admin);
            }
            if (userName == existingUser.UserName && password == existingUser.Password)
            {
                // Create the identity for the user
                identity = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, Role),
        }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }
          
            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {

            var roles = Enum.GetValues(typeof(Roles)).Cast<Roles>()
                         .Select(r => new { Id = (int)r, Name = r.ToString() })
                         .ToList();
            ViewData["RoleList"] = new SelectList(roles, "Id", "Name"); 
            return View();

            


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            var roles = Enum.GetValues(typeof(Roles)).Cast<Roles>()
                    .Select(r => new { Id = (int)r, Name = r.ToString() })
                    .ToList();
            ViewData["RoleList"] = new SelectList(roles, "Id", "Name");

            var existingUser= await _userRepository.FindByNameAsync(user?.UserName);

            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(user);
            }

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                await _userRepository.AddUser(user);
                TempData["successMessage"] = "user is added successfully";
                return RedirectToAction(nameof(Login));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(user);
            }
        }
    }
}
