using System.Collections.Generic;
using System.Linq;
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

        public void Seed()
        {
            if (_dbContext.Posts.Any())
                return;
                
            var postData = System.IO.File.ReadAllText("Data/PostsSeed.json");

            var posts = JsonConvert.DeserializeObject<List<Post>>(postData);
            
            foreach (var post in posts)
            {
                var reply = post.Replies;
                _dbContext.AddRange(post);
            }
            
            _dbContext.SaveChanges();            
        }
    }
}