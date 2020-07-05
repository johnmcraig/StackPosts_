using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;

namespace StackPosts_.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly StoreContext _dbContext;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(StoreContext dbContext, ILogger<PostRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void Add(Post post)
        {
            _logger.LogInformation($"Inserting entity");
            _dbContext.Add(post);
        }

        public void Delete(Post post)
        {
            _logger.LogInformation($"Deleting entity");
            _dbContext.Remove(post);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            _logger.LogInformation($"Getting a single post");
                
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
                
            return post;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            _logger.LogInformation($"Getting all posts");

            var posts = await _dbContext.Posts.Where(t => !t.Deleted).ToListAsync();

            return posts;
        }

        public async Task<Post[]> GetPostByTitle(string title, bool includeReplies = false)
        {
            _logger.LogInformation($"Getting posts by title");

            IQueryable<Post> query = _dbContext.Posts;

            if(includeReplies)
            {
                query = query.Include(r => r.Replies);
            }

            query = query.Where(t => t.Title == title).OrderByDescending(t => t.Title);
            
            return await query.ToArrayAsync();
        }

        public async Task<Post[]> GetByDatePosted(DateTime date)
        {
            _logger.LogInformation($"Getting all posts with date");

            IQueryable<Post> query = _dbContext.Posts.Include(p => p.Replies);

            query = query.OrderByDescending(p => p.DatePosted);

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public Task<Post> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Post>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}