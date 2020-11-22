using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackPosts_.Core.Entities;

namespace StackPosts_.Core.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>, IEfCoreFeatures
    {
        void AddReply(Reply entity);
        Task<bool> PostExists(int id);
    }
}