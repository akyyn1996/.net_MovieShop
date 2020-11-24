using MovieShop.Core.Entities;
using MovieShop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            throw new NotImplementedException();
        }

    }
}
