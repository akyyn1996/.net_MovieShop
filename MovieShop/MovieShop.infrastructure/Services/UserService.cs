using System;
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
        public UserService(IUserRepository iUserRepository, ICryptoService ICryptoService)
        {
            _userRepository = iUserRepository;
            _encryptionService = ICryptoService;
        }
        public Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new System.NotImplementedException();
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

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> FavoriteExists(int id, int movieId)
        {
            throw new System.NotImplementedException();
        }

        public Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PagedResultSet<User>> GetAllUsersByPagination(int pageSize = 20, int page = 0, string lastName = "")
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new System.NotImplementedException();
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