using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

    }
}
