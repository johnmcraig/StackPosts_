using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;
using StackPosts_.Core.Specifications;

namespace StackPosts_.Infrastructure.Data
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
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

        public Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
  }
}