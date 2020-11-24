using MovieShop.Core.Helpers;
using MovieShop.Core.Models;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMoviesCount(string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
