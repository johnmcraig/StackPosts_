using StackPosts_.Core.Entities;
using System.Threading.Tasks;

namespace StackPosts_.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}