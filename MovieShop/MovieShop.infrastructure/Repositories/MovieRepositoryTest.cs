using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Repositories
{
    public class MovieRepositoryTest : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepositoryTest(MovieShopDbContext dbContext) : base(dbContext)
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
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(50).ToListAsync();
            // skip and take
            // offset 10 and fetch 50 next rows
            return movies;
        }

        public Task<IEnumerable<Review>> GetMovieReviews(int id)
        {
            throw new NotImplementedException();
        }
    }
}

