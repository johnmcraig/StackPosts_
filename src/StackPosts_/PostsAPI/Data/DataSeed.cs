using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public class DataSeed
    {
        private readonly PostsDbContext _dbContext;

        public DataSeed(PostsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedData()
        {
            
                
            // var postData = System.IO.File.ReadAllText("Data/PostsSeed.json");

            // var posts = JsonConvert.DeserializeObject<List<Post>>(postData);
            // var replies = JsonConvert.DeserializeObject<List<Reply>>(postData);
            
            // foreach (var post in posts)
            // {
            //     post.Replies = replies;
            //     _dbContext.AddRange(post);
            // }
            
            // _dbContext.SaveChanges();  
            await _dbContext.Database.EnsureCreatedAsync();

            if (_dbContext.Posts.Any())
                return;

            if(!_dbContext.Posts.Any())
            {
                SeedPosts();
                await _dbContext.SaveChangesAsync();
            }        
        }

        private void SeedPosts()
        {
            var posts = new List<Post>()
            {
                new Post
                { 
                    Id = Guid.NewGuid(),
                    Title = "Welcome to the example Post",
                    Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 2.2 and Vue.js 2.6",
                    Score = 0,
                    Deleted = false,
                    Replies = new List<Reply>{ new Reply { Body = "Awesome! Thanks."} } 
                },
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "C# Basic Question",
                    Body = "What is DI (Dependency Injection)?",
                    Score = 1,
                    Deleted = false,
                    Replies = new List<Reply>{ new Reply { Body = "It is a way to initialize an object in one place and extend/use its properties throughout a class, method, and application."} } 
                }
            };

            _dbContext.AddRange(posts);
        } 
    }
}