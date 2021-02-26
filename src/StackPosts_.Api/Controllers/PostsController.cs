using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StackPosts_.Api.Hubs;
using StackPosts_.Core.Interfaces;
using StackPosts_.Core.Entities;
using StackPosts_.Api.Errors;

namespace StackPosts_.Api.Controllers
{
    public class PostsController : BaseApiController 
    {
        private readonly IPostRepository _postRepo;

        public PostsController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postRepo.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepo.GetByIdAsync(id);
            
            if (post == null) return NotFound(new ApiResponse(404));

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            if (post == null) return BadRequest(ModelState);

            if(!ModelState.IsValid) return NotFound(new ApiResponse(404));

            await _postRepo.Add(post);

            return Created("AddPost", new { post });
   
        }

        [HttpPost("/reply")]
        public async Task<IActionResult> AddReplyAsync(Reply addNewReply)
        {
            var postExists = await _postRepo.PostExists(addNewReply.PostId);

            if(!postExists)
            {
                return NotFound();
            }

            var savedReply = await _postRepo.AddReply(addNewReply);

            return Created("AddReplyAsync", new { savedReply });
        }
    }
}