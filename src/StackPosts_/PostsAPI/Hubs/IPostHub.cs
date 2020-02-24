using PostsAPI.Data.Entities;
using System;
using System.Threading.Tasks;

namespace PostsAPI.Hubs
{
    public interface IPostHub
    {
        Task PostScoreChange(Guid postId, int score);
        Task ReplyCountChange(Guid postId, int replyCount);
        Task ReplyAdded(Reply reply);
    }
}
