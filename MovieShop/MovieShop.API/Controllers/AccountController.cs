using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
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
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            if (ModelState.IsValid)
            {
                var userLogin = await _userService.ValidateUser(loginRequestModel.Email, loginRequestModel.Password);
                if (userLogin != null)
                {
                    // success, here geenrate the JWT
                    var token = GenerateJWT(userLogin);
                    return Ok(new { token });
                }
                return Unauthorized();
            }
            return BadRequest(new { message = "Invalid email or password" });
        }

        private string GenerateJWT(UserLoginResponseModel userLoginResponseModel)
        {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, userLoginResponseModel.Id.ToString()),
                new Claim( JwtRegisteredClaimNames.GivenName, userLoginResponseModel.FirstName ),
                new Claim( JwtRegisteredClaimNames.FamilyName, userLoginResponseModel.LastName ),
                new Claim( JwtRegisteredClaimNames.Email, userLoginResponseModel.Email )
            };
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<double>("TokenSettings:ExpirationHours"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Audience = _configuration["TokenSettings:Audience"],
                Issuer = _configuration["TokenSettings:Issuer"],
                SigningCredentials = credentials,
                Expires = expires
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedToken);
        }

    }
}
