using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly PostsDbContext _dbContext;

        public PostRepository(PostsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddPost(Post post)
        {
            _dbContext.Add(post);
        }

        public void DeletePost(Post post)
        {
            _dbContext.Remove(post);
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

        public async Task<bool> SaveAll()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}