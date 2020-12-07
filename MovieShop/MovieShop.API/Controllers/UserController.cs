﻿using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;
        }


        [HttpPost("purchase")]
        public async Task<ActionResult> CreatePurchase([FromBody] PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return Ok();
        }

        [HttpPost("favorite")]
        public async Task<ActionResult> CreateFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }

        [HttpPost("unfavorite")]
        public async Task<ActionResult> DeleteFavorite([FromBody] FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
        }

        [HttpGet("{id:int}/movie/{movieId}/favorite")]
        public async Task<ActionResult> IsFavoriteExists(int id, int movieId)
        {
            var favoriteExists = await _userService.FavoriteExists(id, movieId);
            return Ok(new { isFavorited = favoriteExists });
        }


        [HttpPost("review")]
        public async Task<ActionResult> CreateReview(ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok();
        }

        [HttpPut("review")]
        public async Task<ActionResult> UpdateReview(ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieReview(reviewRequest);
            return Ok();
        }

        [HttpDelete("{userId:int}/movie/{movieId:int}")]
        public async Task<ActionResult> DeleteReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return NoContent();
        }

        [HttpGet("{id:int}/purchases")]
        public async Task<ActionResult> GetUserPurchasedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllPurchasesForUser(id);
            return Ok(userMovies);
        }


        [HttpGet("{id:int}/favorites")]
        public async Task<ActionResult> GetUserFavoriteMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllFavoritesForUser(id);
            return Ok(userMovies);
        }


        [HttpGet("{id:int}/reviews")]
        public async Task<ActionResult> GetUserReviewedMoviesAsync(int id)
        {
            var userMovies = await _userService.GetAllReviewsByUser(id);
            return Ok(userMovies);
        }
    }
}