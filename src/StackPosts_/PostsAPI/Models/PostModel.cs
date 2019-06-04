using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsAPI.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } // upvote & downvote tracking 
        public bool Deleted { get; set; }
        public List<Reply> Replies;
    }
}