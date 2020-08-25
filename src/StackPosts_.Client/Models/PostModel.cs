﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackPosts_.Client.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public int Score { get; set; }

        public bool Deleted { get; set; }

        public DateTime DatePosted { get; set; }

        public IEnumerable<ReplyModel> Replies { get; set; }
    }
}