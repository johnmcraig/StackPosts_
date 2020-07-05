using Microsoft.AspNetCore.Mvc;
using StackPosts_.Api.Controllers;
using StackPosts_.Api.Errors;

namespace Api.Controllers
{
    public class ErrorController : BaseApiController
    {
        [Route("errors/{code}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}