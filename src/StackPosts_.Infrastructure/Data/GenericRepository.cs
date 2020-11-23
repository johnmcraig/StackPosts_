using System.Collections.Generic;
using System.Linq;
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

        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = _dbContext.Set<T>().SingleOrDefault(x => x.Id == id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}