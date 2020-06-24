using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Core.Entities
{
    public class Post : BaseEntity
    {   
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Score { get; set; } // upvote & downvote tracking 
        public bool Deleted { get; set; }
        public DateTime DatePosted { get; set; }
        public List<Reply> Replies;
    }
}