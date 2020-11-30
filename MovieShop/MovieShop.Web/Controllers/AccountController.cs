using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.Web.Controllers
{
    public class AccountController : Controller
    {

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
        //it also work with parameter not only model!!!!!!!!!!!!!!! not case sensitive!!!!!!!!!!!
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            return View();
        }

        public async Task<IActionResult> Login()

        {

            return View();
        }

        //public async Task<IActionResult> Login(LoginRequestModel loginRequest)

        //{

        //    return View();
        //}
    }
}
