using System;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Client.Models
{
    public class ReplyModel
    {
        public int Id { get; set; }
        
        public int PostId { get; set; }

        [Required]
        public string Body { get; set; }

        public int Score { get; set; } = 0;

        public DateTime DateReplied { get; set; }
        
        public bool Deleted { get; set; } = false;
    }
}