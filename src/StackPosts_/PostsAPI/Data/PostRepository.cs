using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsAPI.Data.Entities;

namespace PostsAPI.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly PostsDbContext _dbContext;

        public PostRepository(PostsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T: class
        {
            _dbContext.Add(entity);
        }

        public void AddReply(Reply reply)
        {
            _dbContext.Add(reply);
        }

        public void Delete<T>(T entity) where T: class
        {
            _dbContext.Remove(entity);
        }

        public async Task<Post> GetPost(Guid id)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return posts;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }
    }
}