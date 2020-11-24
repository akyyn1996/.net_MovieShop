using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext _dbContext;

        

        public EfRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(); 
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
            

        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _dbContext.Set<T>().Where(filter).CountAsync();
            }

            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<bool> GetExistAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null && await _dbContext.Set<T>().Where(filter).AnyAsync())
                return true;
            return false;
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

// readonly can be initilize it in creating or constructor.
//why return????