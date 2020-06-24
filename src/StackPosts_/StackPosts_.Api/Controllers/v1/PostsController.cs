using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StackPosts_.Api.Data;
using StackPosts_.Api.Data.Entities;
using StackPosts_.Api.Hubs;


namespace StackPosts_.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase 
    {
        private readonly IHubContext<PostHub, IPostHub> _hubContext;
        private readonly PostsDbContext _dbContext;

        public PostsController(IHubContext<PostHub, IPostHub> postHub, PostsDbContext dbContext)
        {
            _hubContext = postHub;
            _dbContext = dbContext;
        }

        // public static ConcurrentBag<Post> posts = new ConcurrentBag<Post>
        // {
        //     new Post
        //     {
        //         Id = Guid.Parse("b00c58c0-df00-49ac-ae85-0a135f75e01b"),
        //         Title = "Welcome to the example Post",
        //         Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 2.2 and Vue.js 2.6",
        //         Score = 4,
        //         Deleted = false,
        //         DatePosted = DateTime.Now.AddMonths(-2),
        //         Replies = new List<Reply>{new Reply { Body =  "Super exciting reply example here!", Score = 1 }}
        //     }
        // };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        { 
            var posts = await _dbContext.Posts.ToListAsync();
            return posts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            
            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody]Post post)
        {
            if(!ModelState.IsValid) return NotFound();

             _dbContext.Posts.Add(post);
            // post.Id = Guid.NewGuid();
            // post.Deleted = false;
            // post.Replies = new List<Reply>();
            // posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPost("{id}/reply")]
        public async Task<ActionResult> AddReplyAsync(int id, [FromBody]Reply reply)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(t => t.Id == id && !t.Deleted);
            if (post == null) return NotFound();

            // reply.Id = Guid.NewGuid();
            reply.PostId = id;
            reply.Deleted = false;
            post.Replies.Add(reply);

            await _hubContext.Clients.Group(id.ToString()).ReplyAdded(reply);
            await _hubContext.Clients.All.ReplyCountChange(post.Id, post.Replies.Count);

            return new JsonResult(reply);
        }

        [HttpPatch("{id}/upvote")]
        public async Task<ActionResult> UpvotePostAsync(int id)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(t => t.Id == id);
            if (post == null) return NotFound();

            // Warning, this is not thread-safe. Use interlocked methods.
            post.Score++;

            await _hubContext.Clients.All.PostScoreChange(post.Id, post.Score);

            return new JsonResult(post);
        }

        [HttpPatch("{id}/downvote")]
        public async Task<ActionResult> DownvotePostAsync(int id)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(t => t.Id == id);
            if (post == null) return NotFound();

            post.Score--;

            await _hubContext.Clients.All.PostScoreChange(post.Id, post.Score);

            return new JsonResult(post);
        }
    }
}