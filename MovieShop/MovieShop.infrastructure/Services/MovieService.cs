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
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;

namespace MovieShop.infrastructure.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;
        private readonly IAsyncRepository<MovieGenre> _genresRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        // Constructor Injection
        // DI is pattern that enables us th write lossly coupled code so that the code is more maintainable and testable
        public MovieService(IMovieRepository movieRepository, IAsyncRepository<MovieGenre> genresRepository, IPurchaseRepository purchaseRepository)
        {

            // newing up is very convineint but we need to avoid it as much as we can
            // make sure you dont break any existing code....
            // always go back do the regression testing...

            // create MovieRepo instance in every method in my service class
            //  _repository = new MovieRepository(new MovieShopDbContext(null));

            _movieRepository = movieRepository;
            _genresRepository = genresRepository;
            _purchaseRepository = purchaseRepository;

        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {


            var movies = await _movieRepository.GetHighestRevenueMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                if (movie.ReleaseDate != null)
                {
                    movieResponseModel.Add(new MovieResponseModel
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        ReleaseDate = movie.ReleaseDate.Value,
                        Title = movie.Title
                    });
                }
                else
                {
                    movieResponseModel.Add(new MovieResponseModel
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        Title = movie.Title
                    });
                }

            }
            return movieResponseModel;
        }

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var movie = new Movie();
            movie.Revenue = movieCreateRequest.Revenue;
            movie.Title = movieCreateRequest.Title;
            movie.Overview = movieCreateRequest.Overview;
            movie.Tagline = movieCreateRequest.Tagline;
            movie.Revenue = movieCreateRequest.Revenue;
            movie.Budget = movieCreateRequest.Budget;
            movie.ImdbUrl = movieCreateRequest.ImdbUrl;
            movie.TmdbUrl = movieCreateRequest.TmdbUrl;
            movie.PosterUrl = movieCreateRequest.PosterUrl;
            movie.BackdropUrl = movieCreateRequest.BackdropUrl;
            movie.OriginalLanguage = movieCreateRequest.OriginalLanguage;
            movie.ReleaseDate = movieCreateRequest.ReleaseDate;
            movie.RunTime = movieCreateRequest.RunTime;
            movie.Price = movieCreateRequest.Price;
            var createdMovie = await _movieRepository.AddAsync(movie);
            // var movieGenres = new List<MovieGenre>();
            foreach (var genre in movieCreateRequest.Genres)
            {
                var movieGenre = new MovieGenre { MovieId = createdMovie.Id, GenreId = genre.Id };
                await _genresRepository.AddAsync(movieGenre);
            }

            var response = new MovieDetailsResponseModel();
            // not assign value here
            return response;

        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            var movie = new Movie();
            movie.Revenue = movieCreateRequest.Revenue;
            movie.Title = movieCreateRequest.Title;
            movie.Overview = movieCreateRequest.Overview;
            movie.Tagline = movieCreateRequest.Tagline;
            movie.Revenue = movieCreateRequest.Revenue;
            movie.Budget = movieCreateRequest.Budget;
            movie.ImdbUrl = movieCreateRequest.ImdbUrl;
            movie.TmdbUrl = movieCreateRequest.TmdbUrl;
            movie.PosterUrl = movieCreateRequest.PosterUrl;
            movie.BackdropUrl = movieCreateRequest.BackdropUrl;
            movie.OriginalLanguage = movieCreateRequest.OriginalLanguage;
            movie.ReleaseDate = movieCreateRequest.ReleaseDate;
            movie.RunTime = movieCreateRequest.RunTime;
            movie.Price = movieCreateRequest.Price;


            var createdMovie = await _movieRepository.UpdateAsync(movie);
            foreach (var genre in movieCreateRequest.Genres)
            {
                var movieGenre = new MovieGenre { MovieId = createdMovie.Id, GenreId = genre.Id };
                await _genresRepository.UpdateAsync(movieGenre);
            }

            var response = new MovieDetailsResponseModel();
            // not assign value here
            return response;

        }


        public async Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
            var totalPurchases = await _purchaseRepository.GetCountAsync();
            var purchases = await _purchaseRepository.GetAllPurchases(pageSize, page);
            var response = new List<MovieResponseModel>();
            foreach (var purchase in purchases)
            {
                response.Add(new MovieResponseModel
                {
                    Id = purchase.Id,
                    PosterUrl = purchase.Movie.PosterUrl,
                    ReleaseDate = purchase.PurchaseDateTime,
                    Title = purchase.Movie.Title
                });
            }
            

            
            var purchasedMovies = new PagedResultSet<MovieResponseModel>(response, page, pageSize, totalPurchases);
            return purchasedMovies;
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
            var movies = await _movieRepository.GetByIdAsync(id);
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

        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                movieResponseModel.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = (DateTime)movie.ReleaseDate,
                    Title = movie.Title
                });
            }

            return movieResponseModel;
            
        }

        public Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMoviesCount(string title = "")
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);
            var reviewMovieResponseModel = new List<ReviewMovieResponseModel>();
            foreach (var review in reviews)
            {
                 reviewMovieResponseModel.Add(
                     new ReviewMovieResponseModel{
                         UserId = review.UserId,MovieId = review.MovieId, 
                         Name = review.User.FirstName + " " + review.User.LastName,
                         ReviewText = review.ReviewText,
                         Rating = review.Rating
                         }
                     );               
            }

            return reviewMovieResponseModel;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {

            var movies = await _movieRepository.GetTopRatedMovies();
            // Map our Movie Entity to MovieResponseModel
            var movieResponseModel = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                if (movie.ReleaseDate != null)
                {
                    movieResponseModel.Add(new MovieResponseModel
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        ReleaseDate = movie.ReleaseDate.Value,
                        Title = movie.Title
                    });
                }
                else
                {
                    movieResponseModel.Add(new MovieResponseModel
                    {
                        Id = movie.Id,
                        PosterUrl = movie.PosterUrl,
                        Title = movie.Title
                    });
                }

            }
            return movieResponseModel;
        }







    }
}
