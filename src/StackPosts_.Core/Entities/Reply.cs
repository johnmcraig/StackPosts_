using System;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Core.Entities
{
    public class Reply : BaseEntity
    {
        public int PostId { get; set; }

        [Required]
        public string Body { get; set; }

        public int Score { get; set; }

        public DateTime DateReplied { get; set; }
        
        public bool Deleted { get; set; }
    }
}