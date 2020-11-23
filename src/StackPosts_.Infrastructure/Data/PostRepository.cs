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
    public class PostRepository : IPostRepository, IEfCoreFeatures
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(ApplicationDbContext dbContext, ILogger<PostRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Add(Post entity)
        {
            _logger.LogInformation($"Inserting entity");
            await _dbContext.Posts.AddAsync(entity);
        }

        public async Task<Reply> AddReply(Reply entity)
        {
            await _dbContext.Replies.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleting entity");
            var post = await _dbContext.Posts.FindAsync(id);
            _dbContext.Remove(post);
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Getting a single post");
                
            return await _dbContext.Posts
                .Include(p => p.Replies)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> ListAllAsync()
        {
            _logger.LogInformation($"Getting all posts");

            return await _dbContext.Posts
                .Where(t => !t.Deleted)
                .Include(p => p.Replies)
                .ToListAsync();
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

        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<bool> PostExists(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);

            if (post == null)
            {
                return false;
            }

            return true;
        }

        public async Task Update(Post entity)
        {
             _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Post> UpVote(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            post.Score += 1;
            return post;
        }

        public async Task<Post> DownVote(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            post.Score -= 1;
            return post;
        }
    }
}