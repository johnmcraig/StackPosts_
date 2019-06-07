using System;

namespace PostsAPI.Models
{
    public class Reply
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; } // call post id to assocaite reply to same post
        public string Body { get; set; } //aka Body text of reply to post
        public int Score { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateReplied { get; set; }
    }
}