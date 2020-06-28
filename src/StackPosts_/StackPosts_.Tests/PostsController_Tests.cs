using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackPosts_.Api.Controllers;
using StackPosts_.Api.Data;
using StackPosts_.Api.Data.Entities;
using StackPosts_.Infrastructure.Data;
using System;

using Xunit;

namespace StackPosts_.Tests
{
    public class PostsController_Tests
    {
        //[Fact]
        //public async void GetPost()
        //{
        //    #region Arrange

        //    var options = new DbContextOptionsBuilder<PostsDbContext>()
        //        .UseInMemoryDatabase(databaseName: "StackPosts")
        //        .Options;

        //    using (var context = new PostsDbContext(options))
        //    {
        //        context.Add(new Post()
        //        {
        //            Id = 1,
        //            Title = $"Test Post",
        //            Body = $"Test Content for Post",
        //            Score = 1,
        //            Deleted = false,
        //            DatePosted = DateTime.UtcNow,
        //        });

        //        context.SaveChanges();
        //    }

        //    Post post_existing = null;
        //    Post post_notExisting = null;

        //    #endregion

        //    #region Act

        //    using (var context = new PostsDbContext(options))
        //    {
        //        var controller = new PostsController(options);
        //        post_existing = controller.GetPost(1).Value;
        //        post_notExisting = controller.GetPost(2).Value;
        //    }

        //    #endregion

        //    #region Assert

        //    Assert.True(post_existing != null && post_notExisting != null);

        //    #endregion
        //}
    }
}
