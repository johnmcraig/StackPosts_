using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
  }
}