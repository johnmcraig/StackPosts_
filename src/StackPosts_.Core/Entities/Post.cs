using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Core.Entities
{
    public class Post : BaseEntity
    {   
        public string Title { get; set; }

        public string Body { get; set; }

        public int Score { get; set; }

        public bool Deleted { get; set; }

        public DateTime DatePosted { get; set; }

        public IEnumerable<Reply> Replies { get; set; }
    }
}