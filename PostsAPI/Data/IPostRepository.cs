using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public interface IPostRepository
    {
        void AddPost(Post addPost);
        void UpdatePost(Post updatePost);
        void DeletePost(Post deletePost);
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(Guid id);
    }
}