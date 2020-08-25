using System;
using System.Collections;
using System.Threading.Tasks;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationDbContext _dbContext;
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Complete()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;
        
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>) _repositories[type];
    }
  }
}