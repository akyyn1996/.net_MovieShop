using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel userRegisterRequest)
        {
            if (ModelState.IsValid)
            {
                // call the user service
                var response = await _userService.CreateUser(userRegisterRequest);

                return Ok(response);
            }

            return BadRequest(new {message = "please correct the input"});
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user =await _userService.GetUserDetails(id);
            if (user == null)
            {
                return NotFound("Id not Found");
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginRequestModel requestModel)
        {
            var user = await _userService.ValidateUser(requestModel.Email,requestModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok("login success.");
        }
    }
}
