using StackPosts_.Core.Entities;
using StackPosts_.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackPosts_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Task<T> GetByIdAsync(int id);

        // Task<IReadOnlyList<T>> ListAllAsync();

        // Task<T> GetEntityWithSpec(ISpecification<T> spec);
        
        // Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        
        void Add(T entity);
        
        void Delete(T entity);
        
        Task<bool> SaveChangesAsync();
    }
}