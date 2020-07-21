using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
  public class ReplyRepository : IReplyRepository
  {
    private readonly StoreContext _dbContext;
    private readonly ILogger<ReplyRepository> _logger;

    public ReplyRepository(StoreContext dbContext, ILogger<ReplyRepository> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public void Add(Reply entity)
    {
        _logger.LogInformation($"Inserting entity");
        _dbContext.Add(entity);
    }

    public void Delete(Reply entity)
    {
        _logger.LogInformation($"Deleting entity");
        _dbContext.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _dbContext.SaveChangesAsync()) > 0;
    }
  }
}