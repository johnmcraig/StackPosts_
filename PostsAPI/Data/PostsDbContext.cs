using Microsoft.EntityFrameworkCore;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public class PostsDbContext : DbContext
    {
        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
    }
}