using MovieShop.Core.Models;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Core.Models.Response;

namespace MovieShop.infrastructure.Services
{
    public class CastService : ICastService
    {
        public Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            throw new NotImplementedException();
        }
    }
}
