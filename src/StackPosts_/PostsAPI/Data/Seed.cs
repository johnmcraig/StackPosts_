using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostsAPI.Data.Entities;

namespace PostsAPI.Data
{
    public class Seed
    {
        public static void SeedData(PostsDbContext dbContext)
        {
            if(!dbContext.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        Id = Guid.NewGuid(),
                        Title = "Welcome to the example Post",
                        Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 2.2 and Vue.js 2.6",
                        Score = 4,
                        Deleted = false,
                        DatePosted = DateTime.Now.AddMonths(-2),
                        Replies = new List<Reply> 
                        { 
                            new Reply
                            { 
                                Body = "Super exciting reply example here!", 
                                Score = 1 
                            }
                        }
                    }
                };

                dbContext.Posts.AddRange(posts);
                dbContext.SaveChanges();
            }
        }
    }
}