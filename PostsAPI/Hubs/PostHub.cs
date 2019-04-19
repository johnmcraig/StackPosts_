using Microsoft.AspNetCore.SignalR;
using PostsAPI.Hubs;
using PostsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAPI.Hubs
{
    public interface IPostHub
    {
        Task PostScoreChange(Guid postId, int score);
        Task ReplyCountChange(Guid postId, int replyCount);
        Task ReplyAdded(Reply reply);
    }

    public class PostHub : Hub<IPostHub>
    {
        public async Task JoinPost(Guid postId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
        }
        public async Task LeavePost(Guid postId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId.ToString());
        }
        

    }
}
