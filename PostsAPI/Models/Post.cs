using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsAPI.Models
{
    public class Post
    {
        
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Score { get; set; } // upvote & downvote tracking 
        public DateTime DatePosted { get; set; }
        public bool Deleted { get; set; }
        public List<Reply> Replies;
    }
}