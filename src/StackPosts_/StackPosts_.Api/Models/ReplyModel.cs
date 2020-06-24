using System;

namespace PostsAPI.Models
{
    public class ReplyModel
    {   
        public int PostId { get; set; } // call post id to associate reply to same post
        public string Body { get; set; } //aka Body text of reply to post
        public int Score { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateReplied { get; set; }  
    }
}