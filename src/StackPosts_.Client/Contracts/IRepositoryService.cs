using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackPosts.Client.Contracts
{
    public interface IRepositoryService<T> where T : class
    {
        Task<IList<T>> GetAll(string url);
        Task<T> GetSingle(string url, int id);
        Task<T> Create(string url, T entity);
        Task<T> Update(string url, T entity, int id);
        Task<bool> Delete(string url, int id);
    }
}