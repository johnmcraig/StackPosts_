using System.Collections;
using System.Collections.Generic;

namespace StackPosts_.Api.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }

    }
}