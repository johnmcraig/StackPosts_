using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackPosts_.Core.Entities;

namespace StackPosts_.Core.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {   
        Task<IReadOnlyList<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<Post[]> GetPostByTitle(string title, bool includeReplies = false);
        Task<Post[]> GetByDatePosted(DateTime date);
    }
}