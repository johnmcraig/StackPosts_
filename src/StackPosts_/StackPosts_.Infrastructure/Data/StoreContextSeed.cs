using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;

namespace StackPosts_.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext, ILoggerFactory loggerFactory)
        {
            if(!dbContext.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Welcome to the primary example post",
                        Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 3.1 and Vue.js",
                        Score = 4,
                        Deleted = false,
                        DatePosted = DateTime.UtcNow.AddMonths(-2)
                    },
                    new Post
                    {
                        Title = "Welcome to another example post",
                        Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 3.1 and Vue.js",
                        Score = 10,
                        Deleted = false,
                        DatePosted = DateTime.UtcNow.AddMonths(-6)
                    },
                    new Post
                    {
                        Title = "Welcome to yet another example post",
                        Body = "Welcome to this demonstration of making posts using ASP.Net Core 3.1 and Vue.js",
                        Score = -10,
                        Deleted = false,
                        DatePosted = DateTime.UtcNow.AddMonths(-1)
                    },

                };

                await dbContext.Posts.AddRangeAsync(posts);
            }

            if(!dbContext.Replies.Any())
            {
                var replies = new List<Reply>
                {
                    new Reply
                    {
                        PostId = 1,
                        Body = "Super exciting reply example here!", 
                        Score = 1 
                    },
                    new Reply
                    {
                        PostId = 1,
                        Body = "Another exciting reply example here!",
                        Score = 5
                    },
                    new Reply
                    {
                        PostId = 2,
                        Body = "Glad to see all is working well!",
                        Score = -3
                    }
                };

                await dbContext.Replies.AddRangeAsync(replies);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}