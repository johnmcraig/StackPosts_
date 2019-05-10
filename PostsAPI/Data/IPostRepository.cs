using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        void DeletePost(Post post);
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(Guid id);
        Task<bool> SaveAll();
    }
}