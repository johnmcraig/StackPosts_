using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackPosts_.Api.Controllers.v2;
using StackPosts_.Api.Data.Entities;
using StackPosts_.Core.Interfaces;
using StackPosts_.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StackPosts_.Tests
{
    public class PostsController_Tests
    {
        [Fact]
        public async void GetPost()
        {
            #region Arrange

            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "StackPosts")
                .Options;

            using(var context = new StoreContext(options))
            {
                context.Add(new Post()
                {
                    Id = 1,
                    Title = $"Test Post",
                    Body = $"Test Content for Post",
                    Score = 1,
                    Deleted = false,
                    DatePosted = DateTime.UtcNow,
                });

                context.SaveChanges(); 
            }

            Post post_existing = null;
            Post post_notExisting = null;

            #endregion

            #region Act

            using (var context = new StoreContext(options))
            {
                var controller = new PostsController(context);
                post_existing = (await controller.GetPost(1)).Value;
                post_notExisting = (await controller.GetPost(2)).Value;
            }

            #endregion

            #region Assert

            Assert.True(post_existing != null && post_notExisting != null);

            #endregion

        }
    }
}
