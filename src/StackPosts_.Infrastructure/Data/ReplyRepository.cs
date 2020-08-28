using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
  public class ReplyRepository : IReplyRepository
  {
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ReplyRepository> _logger;

    public ReplyRepository(ApplicationDbContext dbContext, ILogger<ReplyRepository> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public Task<Reply> GetByIdAsync(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Reply>> ListAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public void Add(Reply entity)
    {
        _logger.LogInformation($"Inserting entity");
        _dbContext.Add(entity);
        _dbContext.SaveChangesAsync();
    }

    public void Delete(Reply entity)
    {
        _logger.LogInformation($"Deleting entity");
        _dbContext.Remove(entity);
        _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Save()
    {
        return (await _dbContext.SaveChangesAsync()) > 0;
    }
  }
}