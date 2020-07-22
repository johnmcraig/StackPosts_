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
        // private readonly IHubContext<PostHub, IPostHub> _hubContext;
        private readonly IPostRepository _postRepo;
        private readonly IReplyRepository _replyRepo;

        public PostsController( IPostRepository postRepo, IReplyRepository replyRepo)
        {
            // IHubContext<PostHub, IPostHub> postHub, _hubContext = postHub;
            _postRepo = postRepo;
            _replyRepo = replyRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        { 
            var posts = await _postRepo.GetPostsAsync();
            return posts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);
            
            if (post == null) return NotFound(new ApiResponse(404));

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
        {
            if(!ModelState.IsValid) return NotFound(new ApiResponse(404));

            _postRepo.Add(post);

            await _postRepo.SaveChangesAsync();

            return Ok(post);

            
        }

        [HttpPost("{id}/reply")]
        public async Task<ActionResult> AddReplyAsync(int id, [FromBody] Reply reply)
        {
            var post = await _postRepo.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            //reply.Id = id;
            reply.PostId = post.Id;
            reply.Deleted = false;
            reply.DateReplied = DateTime.UtcNow;

            _replyRepo.Add(reply);

            await _replyRepo.SaveChangesAsync();

            // await _hubContext.Clients.Group(id.ToString()).ReplyAdded(reply);
            
            // await _hubContext.Clients.All.ReplyCountChange(post.Id, post.Replies.Count);

            return Ok(reply);
        }

        [HttpPatch("{id}/upvote")]
        public async Task<ActionResult> UpvotePostAsync(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            // Warning, this is not thread-safe. Use interlocked methods.
            // post.Score++;
            await _postRepo.UpVote(id);

            // await _hubContext.Clients.All.PostScoreChange(post.Id, post.Score);

            return Ok(post);
        }

        [HttpPatch("{id}/downvote")]
        public async Task<ActionResult> DownvotePostAsync(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);

            if (post == null) return NotFound();

            // post.Score--;
            await _postRepo.DownVote(id);

            // await _hubContext.Clients.All.PostScoreChange(post.Id, post.Score);

            return Ok(post);
        }
    }
}