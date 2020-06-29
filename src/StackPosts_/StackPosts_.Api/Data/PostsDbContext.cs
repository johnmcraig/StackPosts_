using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackPosts_.Api.Data.Entities;

namespace StackPosts_.Api.Data
{
    public class PostsDbContext : DbContext
    {
        //private readonly IConfiguration _config;
        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options)
        {
            //, IConfiguration config
            //_config = config;
            // Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("sqlConString"));
        }
    }
}