using StackPosts_.Core.Entities;
using System;
using System.Threading.Tasks;

namespace StackPosts_.Api.Hubs
{
    public interface IPostHub
    {
        Task PostScoreChange(int postId, int score);
        Task ReplyCountChange(int postId, int replyCount);
        Task ReplyAdded(Reply reply);
    }
}
