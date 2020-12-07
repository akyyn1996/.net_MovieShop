using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.ServiceInterfaces;
using System.Threading.Tasks;
using MovieShop.Core.RepositoryInterfaces;

namespace MovieShop.infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _encryptionService;
        private readonly IMovieService _movieService;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IAsyncRepository<Review> _reviewRepository;
        public UserService(IUserRepository iUserRepository, ICryptoService ICryptoService, IMovieService movieService, IPurchaseRepository purchaseRepository, IAsyncRepository<Favorite> favoriteRepository, IAsyncRepository<Review> reviewRepository)
        {
            _userRepository = iUserRepository;
            _encryptionService = ICryptoService;
            _movieService = movieService;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };

            await _favoriteRepository.AddAsync(favorite);

        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review();
            review.UserId = reviewRequest.UserId;
            review.MovieId = reviewRequest.MovieId;
            review.Rating = reviewRequest.Rating;
            review.ReviewText = reviewRequest.ReviewText;
            

            await _reviewRepository.AddAsync(review);
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            // send to email to user repository, see if the data exists for the email.
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            //First create a unique random salt
            var salt = _encryptionService.CreateSalt();

            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;

            throw new System.NotImplementedException();
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {

            var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistAsync(f => f.MovieId == movieId &&
                                                                 f.UserId == id);
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {

            var favoriteMovies = await _favoriteRepository.ListAllWithIncludesAsync(
                p => p.UserId == id,
                p => p.Movie);
            var response = new FavoriteResponseModel();
            foreach (var favoriteMovie in favoriteMovies)
            {
                response.FavoriteMovies.Add(new FavoriteResponseModel.FavoriteMovieResponseModel
                    {
                        PosterUrl = favoriteMovie.Movie.PosterUrl,
                        ReleaseDate = (DateTime)favoriteMovie.Movie.ReleaseDate,
                        Title = favoriteMovie.Movie.Title
                    }
                    
                    );
            }

            response.UserId = id;

            return response;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {

            var purchasedMovies = await _purchaseRepository.ListAllWithIncludesAsync(
                p => p.UserId == id,
                p => p.Movie);


            var response = new PurchaseResponseModel();
            response.UserId = id;
            foreach (var purchasedMovie in purchasedMovies)
            {
                response.PurchasedMovies.Add(new PurchaseResponseModel.PurchasedMovieResponseModel
                {
                    Title = purchasedMovie.Movie.Title,
                    PosterUrl = purchasedMovie.Movie.PosterUrl,
                    ReleaseDate = (DateTime)purchasedMovie.Movie.ReleaseDate,
                    

                });
            }

            return response;
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var userReviews = await _reviewRepository.ListAllWithIncludesAsync(r => r.UserId == id, r => r.Movie);
            var response = new ReviewResponseModel();
            response.UserId = id;
            foreach (var userReview in userReviews)
            {
                response.MovieReviews.Add(new ReviewMovieResponseModel
                {
                    MovieId = userReview.MovieId,
                    Name = userReview.Movie.Title,
                    Rating = userReview.Rating,
                    ReviewText = userReview.ReviewText,
                    UserId =  userReview.UserId
                });
            }

            return response;
        }

        public Task<PagedResultSet<User>> GetAllUsersByPagination(int pageSize = 20, int page = 0, string lastName = "")
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User Not Found");
            var responseModel = new UserRegisterResponseModel();
            responseModel.FirstName = user.FirstName;
            responseModel.LastName = user.LastName;
            responseModel.Email = user.Email;
            responseModel.Id = user.Id;

            return responseModel;

        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExistAsync(p =>
                p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movie.Price;
            var purchase = new Purchase();
            purchase.UserId = purchaseRequest.UserId;
            purchase.PurchaseNumber = (Guid)purchaseRequest.PurchaseNumber;
            purchase.TotalPrice = (decimal)purchaseRequest.TotalPrice;
            purchase.PurchaseDateTime = (DateTime)purchaseRequest.PurchaseDateTime;
            purchase.MovieId = purchaseRequest.MovieId;

            //var purchase = _mapper.Map<Purchase>(purchaseRequest);
            await _purchaseRepository.AddAsync(purchase);

            
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbFavorite =
                await _favoriteRepository.ListAsync(r => r.UserId == favoriteRequest.UserId &&
                                                         r.MovieId == favoriteRequest.MovieId);
            await _favoriteRepository.DeleteAsync(dbFavorite.First());
            throw new System.NotImplementedException();
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review();
            review.MovieId = reviewRequest.MovieId;
            review.Rating = reviewRequest.Rating;
            review.UserId = reviewRequest.UserId;
            review.ReviewText = reviewRequest.ReviewText;

            await _reviewRepository.UpdateAsync(review);
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {

            // we are gonna check if the email exists in the database
            var user = await _userRepository.GetUserByEmail(email);
            
            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;


            var response = new UserLoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            //var response = _mapper.Map<UserLoginResponseModel>(user);
            //var userRoles = roles.ToList();
            //if (userRoles.Any())
            //{
            //    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
            //}
            if (isSuccess)
                return response;
            else
                return null;
        }
    }
}