using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Data;
using PostsAPI.Models;

namespace PostsAPI.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;

        public PostsController(PostsDbContext dbContext, IPostRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetPosts()
        {
            try 
            {
                var posts = await _repo.GetPosts();
                return Ok(posts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"There was a server error: {ex}");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(Guid id)
        {
            try 
            {
                var post = await _repo.GetPost(id);
                return new JsonResult(post);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Eternal server error: {ex}");
            }
        }

        [HttpPost("")]
        public Post AddPost([FromBody] Post post)
        {
            _repo.Add(post);

            return post;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        [HttpDelete("{id}")]
        public void DeleteById(int id) { }
    }
}