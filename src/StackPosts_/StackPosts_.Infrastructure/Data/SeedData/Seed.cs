using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackPosts_.Core.Entities;

namespace StackPosts_.Infrastructure.Data.SeedData
{
    public class Seed
    {
        public static void SeedData(StoreContext dbContext)
        {
            if(!dbContext.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        Id = 1,
                        Title = "Welcome to the example Post",
                        Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 2.2 and Vue.js 2.6",
                        Score = 4,
                        Deleted = false,
                        DatePosted = DateTime.Now.AddMonths(-2)
                    }
                    
                };

                dbContext.Posts.AddRange(posts);
            }

            if(!dbContext.Replies.Any())
            {
                var replies = new List<Reply>
                {
                    new Reply
                    { 
                        Id = 1,
                        PostId = 1,
                        Body = "Super exciting reply example here!", 
                        Score = 1 
                    }
                };

                dbContext.Replies.AddRange(replies);
            }

            dbContext.SaveChanges();
        }
    }
}