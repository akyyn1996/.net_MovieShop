using MovieShop.Core.Models;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;

namespace MovieShop.infrastructure.Services
{
    public class CastService : ICastService
    {

        private readonly ICastRepository _castRepository;
        

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
            
        }

        public Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<CastResponseModel> GetCastById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castResponseModel = new CastResponseModel();
            castResponseModel.Id = cast.Id;
            castResponseModel.Gender = cast.Gender;
            castResponseModel.TmdbUrl = cast.TmdbUrl;
            castResponseModel.ProfilePath = cast.ProfilePath;
            castResponseModel.Name = cast.Name;

            return castResponseModel;
        }
    }
}
