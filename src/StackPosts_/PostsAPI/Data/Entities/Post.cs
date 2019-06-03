using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsAPI.Data.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } // upvote & downvote tracking 
        public bool Deleted { get; set; }
        public List<Reply> Replies;
    }
}