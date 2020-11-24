using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Services
{
    public class GenreService : IGenreService
    {
        public Task<IEnumerable<Genre>> GetAllGenres()
        {
            throw new NotImplementedException();
        }
    }
}
