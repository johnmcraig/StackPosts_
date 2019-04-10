using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Data;
//using PostsAPI.Models;

namespace PostsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly PostsDbContext _dbContext;
        private readonly IPostRepository _repo;

        public DataController(PostsDbContext dbContext, IPostRepository repo)
        {
            _dbContext = dbContext;
            _repo = repo;
        }

        // GET api/data
        [HttpGet("")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repo.GetPosts();

            return Ok(posts);
        }

        // GET api/data/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _repo.GetPost(id);
            
            return Ok(post);
        }

        // POST api/data
        [HttpPost("")]
        public void Post([FromBody] string value) { }

        // PUT api/data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/data/5
        [HttpDelete("{id}")]
        public void DeleteById(int id) { }
    }
}