using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostsAPI.Data.Entities;

namespace PostsAPI.Data
{
    public class PostsDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public PostsDbContext(DbContextOptions<PostsDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
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
            optionsBuilder.UseSqlServer(_config.GetConnectionString("sqlConString"));
        }
    }
}