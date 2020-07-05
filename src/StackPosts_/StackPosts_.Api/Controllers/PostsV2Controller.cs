using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackPosts_.Core.Interfaces;
using StackPosts_.Core.Entities;
using System.Collections.Generic;

namespace StackPosts_.Api.Controllers
{
    public class PostsV2Controller : BaseApiController
    {
        private readonly IPostRepository _postRepo;

        public PostsV2Controller(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            try
            {
                var posts = await _postRepo.GetPostsAsync();
                return posts;
            }
            catch (Exception ex)
            {
                //StatusCode(500, $"There was a server error: {ex}")
                return BadRequest(ex.Message) as IEnumerable<Post>;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postRepo.GetPostByIdAsync(id);
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
            _postRepo.Add(post);

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