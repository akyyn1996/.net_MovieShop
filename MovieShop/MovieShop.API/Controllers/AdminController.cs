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
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public AdminController(IMovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpPut("movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok(createdMovie);
        }

        [HttpPost("movie")]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequest movieCreateRequest)
        {
            var createdMovie = await _movieService.CreateMovie(movieCreateRequest);
            return CreatedAtRoute("GetMovie", new { id = createdMovie.Id }, createdMovie);
        }

        [HttpGet("purchases")]
        public async Task<IActionResult> GetAllPurchases([FromQuery] int pageSize = 30, [FromQuery] int page = 1)
        {
            var movies = await _movieService.GetAllMoviePurchasesByPagination(pageSize, page);
            return Ok(movies);
        }

        [HttpGet("top")]
        public IActionResult GetTopMovies()
        {
            var movies = _movieService.GetTopRevenueMovies();                            //
            return Ok(movies);
        }

    }
}
