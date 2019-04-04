using System;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Models
{
    public class Reply
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        public Guid PostId { get; set; } // call post id to assocaite reply to same post
        public string Title { get; set; }
        public string Body { get; set; } //aka Body text of reply to post
        public DateTime DatePosted { get; set; }
        
    }
}