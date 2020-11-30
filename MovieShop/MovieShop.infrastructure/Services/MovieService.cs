using MovieShop.Core.Helpers;
using MovieShop.Core.Models;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.infrastructure.Data;
using MovieShop.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;

namespace MovieShop.infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _repository;

        // Constructor Injection
        // DI is pattern that enables us th write lossly coupled code so that the code is more maintainable and testable
        public MovieService(IMovieRepository repository)
        {

            // newing up is very convineint but we need to avoid it as much as we can
            // make sure you dont break any existing code....
            // always go back do the regression testing...

            // create MovieRepo instance in every method in my service class
            //  _repository = new MovieRepository(new MovieShopDbContext(null));

            _repository = repository;

        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {


            var movies = await _repository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title
                });
            }
            return movieResponseModel;
        }

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

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movies = await _repository.GetByIdAsync(id);
            // Map our Movie Entity to MovieResponseModel
            var movieDetailsResponseModel = new MovieDetailsResponseModel();

            movieDetailsResponseModel.Id = movies.Id;
            movieDetailsResponseModel.Revenue = movies.Revenue;
            movieDetailsResponseModel.BackdropUrl = movies.BackdropUrl;
            movieDetailsResponseModel.Budget = movies.Budget;
            movieDetailsResponseModel.Title = movies.Title;
            movieDetailsResponseModel.Overview = movies.Overview;
            movieDetailsResponseModel.PosterUrl = movies.PosterUrl;
            movieDetailsResponseModel.Tagline = movies.Tagline;
            movieDetailsResponseModel.ImdbUrl = movies.ImdbUrl;
            movieDetailsResponseModel.TmdbUrl = movies.TmdbUrl;
            movieDetailsResponseModel.Price = movies.Price;


            return movieDetailsResponseModel;



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


        //public override async Task<Movie> GetByIdAsync(int id)
        //{
        //    var movie = await _dbContext.Movies
        //                                .Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.MovieGenres)
        //                                .ThenInclude(m => m.Genre)
        //                                .FirstOrDefaultAsync(m => m.Id == id);
        //    if (movie == null) return null;
        //    var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
        //                                      .AverageAsync(r => r == null ? 0 : r.Rating);
        //    if (movieRating > 0) movie.Rating = movieRating;
        //    return movie;
        //}
    }
}
