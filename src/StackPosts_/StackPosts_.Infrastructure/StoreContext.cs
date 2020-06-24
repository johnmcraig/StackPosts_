using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackPosts_.Core.Entities;

namespace StackPosts_.Infrastructure
{
    public class StoreContext : DbContext
    {
        private readonly IConfiguration _config;
        
        public StoreContext(DbContextOptions<StoreContext> options, IConfiguration config) : base(options)
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
            // optionsBuilder.UseSqlServer(_config.GetConnectionString("sqlConString"));
        }
    }
}