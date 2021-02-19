using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Authentication;
using Interfaces;

namespace Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInModel userModel,string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = _userService.LoginUser(userModel);
                if (user != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    var identity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { identity });

                    HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(5)
                    });

                    if (returnUrl != null)
                        return LocalRedirect(returnUrl);

                    else
                        ViewData["Success"] = "Udało sie zalogować";
                    return View();
                }
                ModelState.AddModelError("CustomError", "Nieprawidłowe dane logowania spróbuj jeszcze raz");
            }
            return View(userModel);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUser(user);

                return Content($"Użytkownik {user.Name} {user.Surname} został pomyślnie zarejestrowany");
            }
            return View(user);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}