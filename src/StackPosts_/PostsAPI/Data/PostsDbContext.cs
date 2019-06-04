using Microsoft.EntityFrameworkCore;
using PostsAPI.Data.Entities;

namespace PostsAPI.Data
{
    public class PostsDbContext : DbContext
    {
        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }
}