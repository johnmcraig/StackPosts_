using Microsoft.Extensions.Configuration;
using Moq;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StackPosts_.Tests
{
    public class PostsControllerTests
    {
        [Fact]
        public async void GetPosts_WhenNoParams_ReturnAllPosts()
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
                   .Setup(repo => repo.GetPostsAsync())
                   .Returns(() => Task.FromResult(mockPosts.AsEnumerable()));

            var mockConfigurationRoot = new Mock<IConfigurationRoot>();

            mockConfigurationRoot.SetupGet(config => config[It.IsAny<string>()]).Returns("some setting");

            var postsController = new Api.Controllers.v2.PostsController(mockDataRepository.Object);

            var result = await postsController.GetPosts();

            Assert.Equal(10, result.Count());

            mockDataRepository.Verify(mock => mock.GetPostsAsync(), Times.Once());
        }
    }
}
