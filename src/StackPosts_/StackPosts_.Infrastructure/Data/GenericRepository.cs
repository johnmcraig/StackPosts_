using System.Threading.Tasks;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {
    public void Add(T entity)
    {
      throw new System.NotImplementedException();
    }

    public void Delete(T entity)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}