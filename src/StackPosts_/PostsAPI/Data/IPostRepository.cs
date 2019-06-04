using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public interface IPostRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();

        void AddReply(Reply reply);
        
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(Guid id);
        
    }
}