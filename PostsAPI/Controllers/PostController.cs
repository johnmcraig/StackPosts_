using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostsAPI.Data;
using PostsAPI.Models;

namespace PostsAPI.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class PostController : Controller {
        // private readonly PostsDbContext _dbContex;
        // private readonly List<Post> posts = new List<Post>();

        // public PostController(PostsDbContext dbContex)
        // {
        //     _dbContex = dbContex; 
        // }
        public static ConcurrentBag<Post> posts = new ConcurrentBag<Post>
        {
            new Post
            {
                Id = Guid.Parse("b00c58c0-df00-49ac-ae85-0a135f75e01b"),
                Title = "Welcome to the example Post",
                Body = "Welcome to this demonstration of making a Stack Overflow clone using ASP.Net Core 2.2 and Vue.js 2.6",
                Replies = new List<Reply>{ new Reply { Body = "Awesome! Thanks."}}
            }
        };

        // GET api/post
        [HttpGet()]
        public IEnumerable GetPosts()
        {
            return posts.Select(p => new {
                Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    Score = p.Score,
                    ReplyCount = p.Replies.Count
            });
        }

        // GET api/post/5
        [HttpGet ("{id}")]
        public ActionResult GetPost(Guid id)
        {
            var post = posts.SingleOrDefault(p => p.Id == id);
            if (post == null) return NotFound();

            return new JsonResult(post);
        }

        // POST api/post
        [HttpPost()]
        public Post AddPost([FromBody]Post post)
        {
            post.Id = Guid.NewGuid();
            post.Replies = new List<Reply>();
            posts.Add(post);

            return post;
        }

        [HttpPost ("{id}/reply")]
        public ActionResult AddReplyAsync(Guid id, [FromBody] Reply reply)
        {
            var post = posts.SingleOrDefault(t => t.Id == id);
            if (post == null) return NotFound();

            reply.Id = Guid.NewGuid();
            reply.PostId = id;
            post.Replies.Add(reply);

            return new JsonResult(reply);
        }

        // PUT api/post/5
        [HttpPut ("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/post/5
        [HttpDelete ("{id}")]
        public void DeleteById(int id)
        {

        }

        [HttpPatch ("{id}/upvote")]
        public ActionResult UpvotePostAsync(Guid id)
        {
            var post = posts.SingleOrDefault(t => t.Id == id);
            if (post == null) return NotFound();

            // Warning, this is not thread-safe. Use interlocked methods.
            post.Score++;
            return new JsonResult(post);
        }

        [HttpPatch ("{id}/downvote")]
        public ActionResult DownvotePostAsync(Guid id)
        {
            var post = posts.SingleOrDefault(t => t.Id == id);
            if (post == null) return NotFound();

            post.Score--;
            return new JsonResult(post);
        }
    }
}