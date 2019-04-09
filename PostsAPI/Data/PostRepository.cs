using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public class PostRepository : IPostRepository
    {
        public void AddPost(Post addPost)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Post deletePost)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPost(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Post updatePost)
        {
            throw new NotImplementedException();
        }
    }
}