using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackPosts_.Core.Interfaces;
using StackPosts_.Core.Entities;
using System.Collections.Generic;

namespace StackPosts_.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;

        public PostsController(IPostRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            try
            {
                var posts = await _repo.GetPostsAsync();
                return posts;
            }
            catch (Exception ex)
            {
                //StatusCode(500, $"There was a server error: {ex}")
                return (IEnumerable<Post>)BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int id)
        {
            try
            {
                var post = await _repo.GetPostByIdAsync(id);
                return new JsonResult(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Eternal server error: {ex}");
            }
        }

        [HttpPost]
        public Post AddPost([FromBody] Post post)
        {
            _repo.Add(post);

            return post;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        { }

        [HttpDelete("{id}")]
        public void DeleteById(int id)
        { }
    }
}