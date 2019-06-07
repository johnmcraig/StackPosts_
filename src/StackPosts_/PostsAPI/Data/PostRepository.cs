using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostsAPI.Data.Entities;

namespace PostsAPI.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly PostsDbContext _dbContext;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(PostsDbContext dbContext, ILogger<PostRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T: class
        {
            _logger.LogInformation($"Inserting entity");
            _dbContext.Add(entity);
        }

        public void AddReply(Reply reply)
        {
            _dbContext.Add(reply);
        }

        public void Delete<T>(T entity) where T: class
        {
            _logger.LogInformation($"Deleting entity");
            _dbContext.Remove(entity);
        }

        public async Task<Post> GetPost(Guid id)
        {
            _logger.LogInformation($"Getting a single post");
            var post = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
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

            query = query.Where(p => p.DatePosted == date).OrderByDescending(p => p.DatePosted);

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}