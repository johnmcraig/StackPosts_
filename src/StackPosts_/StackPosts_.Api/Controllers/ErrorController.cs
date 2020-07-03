using Microsoft.AspNetCore.Mvc;
using StackPosts_.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackPosts_.Api.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public ErrorController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _dbContext.Posts.Find(42);

            if (thing == null) return NotFound();

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _dbContext.Posts.Find(42);

            if (thing == null) return NotFound();

            var thingToRetun = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestById(int id)
        {
            return Ok();
        }

    }
}
