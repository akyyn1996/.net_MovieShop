using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        // http:localhost/account/register GET

        [HttpGet]
        // we need to show empty register page
        public async Task<IActionResult> Register()
        {
            return View();
        }

        // http:localhost/account/register POST
        //when user hits submit button, post information to this method
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            // only when each and every validation in our is true we need to proceed further
            if (ModelState.IsValid)
            {
                // we need to send the userRegisterRequestModel to our service
                await _userService.CreateUser(userRegisterRequestModel);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // when user login, un/pw
        // localhost/movie/details/22/purchase
        // check: whether the user is loged in
        // no: redirect to login
        // use: purchase page

        // 1 purchase page... after 5 minutes, he wanna add a movie as his favorite
        // 2 after 2 minute my account page
        // 


        // COOKIE BASED AUTHENTICATION
        // un/pw: YES : MVC create a auth-coolie that has some expiration time
        // 
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest, string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");
            if (!ModelState.IsValid) return View();


            var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname,  user.LastName),
                new Claim(ClaimTypes.NameIdentifier,  user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect(returnUrl);

            
        }



        [HttpGet]
        [Authorize] // check cookie expire or not : filter MVC: before a method execute
        public async Task<IActionResult> MyAccount()
        {

            return View();
        }
    }
}
