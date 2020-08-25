using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackPosts_.Api.Dtos
{
    public class ReplyToReturnDto
    {
        public int PostId { get; set; }

        public string Body { get; set; }

        public int Score { get; set; }

        public DateTime DateReplied { get; set; }

        public bool Deleted { get; set; }
    }
}
