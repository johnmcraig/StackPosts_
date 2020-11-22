using StackPosts_.Core.Entities;
using StackPosts_.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackPosts_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> ListAllAsync();
        
        void Add(T entity);
        
        void Delete(T entity);
    }
}