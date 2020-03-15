using System;

namespace PostsAPI.Data.Entities
{
    public class Reply
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; } // call post id to associate reply to same post
        public string Body { get; set; } //aka Body text of reply to post
        public int Score { get; set; }
        public DateTime DateReplied { get; set; }
        public bool Deleted { get; set; }
        
    }
}