using StackPosts_.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackPosts_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}