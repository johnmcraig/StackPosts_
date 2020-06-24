using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Api.Data.Entities
{
    public class Post
    {   
        [Key]
        public int Id { get; set; }
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