using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;
using Xunit;

namespace StackPosts_.Tests
{
    public class PostControllerTest
    {
        [Fact]
        public async void GetAllPosts_WithNoParams_ReturnAllPosts()
        {
            var mockPosts = new List<Post>();

            for (int i = 1; i <= 10; i++)
            {
                mockPosts.Add(new Post
                {
                    Id = 1,
                    Title = $"Test Post {i}",
                    Body = $"Test Content for Post {i}",
                    Score = 1,
                    Deleted = false,
                    DatePosted = DateTime.UtcNow,
                    Replies = new List<Reply>()
                });
            }

            var mockDataRepository = new Mock<IPostRepository>();

            mockDataRepository
                   .Setup(repo => repo.ListAllAsync())
                   .Returns(() => Task.FromResult(mockPosts.AsEnumerable()));

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();

            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var postsController = new Api.Controllers.PostsController(mockDataRepository.Object, null);

            var result = await postsController.GetPosts();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.IsType<List<Post>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            mockDataRepository.Verify(mock => mock.ListAllAsync(), Times.Once());
        }
    }
}