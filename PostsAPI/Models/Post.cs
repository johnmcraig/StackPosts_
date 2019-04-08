using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsAPI.Models
{
    public class Post
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [MaxLength(175)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } // upvote/downvote tracking 
        public DateTime DatePosted { get; set; }
        public List<Reply> Replies;
        
    }
}