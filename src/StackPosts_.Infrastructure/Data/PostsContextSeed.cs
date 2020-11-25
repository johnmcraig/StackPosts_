using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;

namespace StackPosts_.Infrastructure.Data
{
    public class PostsContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
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
                        Body = "This is another mock demonstration of making a Stack Overflow clone using ASP.Net Core 3.1 and Vue.js",
                        Score = 10,
                        Deleted = false,
                        DatePosted = DateTime.UtcNow.AddMonths(-6)
                    },
                    new Post
                    {
                        Title = "Welcome to yet another example post",
                        Body = "Yet another mock demonstration of making posts using ASP.Net Core 3.1 and Vue.js",
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
                        Score = 1,
                        Deleted = false,
                        DateReplied = DateTime.UtcNow
                    },
                    new Reply
                    {
                        PostId = 1,
                        Body = "Another exciting reply example here!",
                        Score = 5,
                        Deleted = false,
                        DateReplied = DateTime.UtcNow
                    },
                    new Reply
                    {
                        PostId = 2,
                        Body = "Glad to see all is working well!",
                        Score = -3,
                        Deleted = false,
                        DateReplied = DateTime.UtcNow
                    },
                    new Reply
                    {
                        PostId = 3,
                        Body = "Hey! This is basically a repeat post!",
                        Score = 0,
                        Deleted = false,
                        DateReplied = DateTime.UtcNow
                    }
                };

                await dbContext.Replies.AddRangeAsync(replies);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}