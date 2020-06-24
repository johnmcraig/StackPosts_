using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PostsAPI.Hubs
{
    public class PostHub : Hub<IPostHub>
    {
        private readonly ILogger _logger;

        public PostHub(ILogger<PostHub> logger)
        {
            _logger = logger;
        }

        public async Task JoinPostGroup(int postId)
        {
            _logger.LogInformation($"Client {Context.ConnectionId} is viewing {postId}");

            await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
        }

        public async Task LeavePostGroup(int postId)
        {
            _logger.LogInformation($"Client {Context.ConnectionId} is no longer viewing {postId}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId.ToString());
        }
        
    }
}
