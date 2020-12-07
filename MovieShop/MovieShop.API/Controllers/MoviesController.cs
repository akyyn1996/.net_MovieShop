using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.OpenApi.Any;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    // attribute based routing

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase // more basic one
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // api/movies/toprevenue
        [HttpGet]
        [Route("toprevenue")]


        public async Task<IActionResult> GetTopRevenueMovies()
        {
            // call our service and call the method
            // var movies = _movieService.GetTopMovies();
            // http status code
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
            {
                return NotFound("no Movies Found");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {

            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())
            {
                return NotFound("no Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            if (movie == null)
            {
                return NotFound("Invalid movie Id");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMovieByGenreId(int genreId)
        {
            var movie = await _movieService.GetMoviesByGenre(genreId);
            if (movie == null)
            {
                return NotFound("Invalid movie Id");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{Id}/reviews")]
        public async Task<IActionResult> GetReviewsById(int Id)
        {
            var movie = await _movieService.GetReviewsForMovie(Id);
            if (!movie.Any())
            {
                return NotFound("Invalid movie Id");
            }

            return Ok(movie);
        }

    }
}
