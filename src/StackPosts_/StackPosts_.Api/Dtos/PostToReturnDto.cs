using StackPosts_.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackPosts_.Api.Dtos
{
    public class PostToReturnDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int Score { get; set; }

        public bool Deleted { get; set; }

        public DateTime DatePosted { get; set; }

        public List<Reply> Replies;
    }
}
