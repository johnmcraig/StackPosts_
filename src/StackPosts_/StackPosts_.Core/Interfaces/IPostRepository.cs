using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackPosts_.Core.Entities;

namespace StackPosts_.Core.Interfaces
{
    public interface IPostRepository : IGenericRepository
    {
        void AddReply(Reply reply);
        
        Task<Post[]> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post[]> GetPostByTitle(string title, bool includeReplies = false);
        Task<Post[]> GetByDatePosted(DateTime date);
        
    }
}