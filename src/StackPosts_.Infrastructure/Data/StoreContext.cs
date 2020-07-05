using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using StackPosts_.Core.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace StackPosts_.Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        private readonly IConfiguration _config;
        
        public StoreContext(DbContextOptions<StoreContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ///<summary>
            /// Converts decimal to double since it is not supported in SqLite 
            /// </summary>
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(_config.GetConnectionString("sqlConString"));
            // optionsBuilder.UseInMemoryDatabase(databaseName: "StackPosts");
            optionsBuilder.UseSqlite(_config.GetConnectionString("DefaultConnection"));
        }
    }
}