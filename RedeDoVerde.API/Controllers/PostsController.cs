using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedeDoVerde.Domain.Account;
using RedeDoVerde.Domain.Post;
using RedeDoVerde.Repository.Context;

namespace RedeDoVerde.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly RedeDoVerdeContext _context;

        public PostsController(RedeDoVerdeContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> GetPosts()
        {
            List<Post> listPost = await _context.Posts.Include(x => x.Account).ToListAsync();

            List<PostResponse> listPostResponse = new List<PostResponse>();

            foreach (var item in listPost)
            {
                AccountResponse accountResponse = new AccountResponse { Email = item.Account.Email };

                PostResponse postResponse = new PostResponse { Id = item.Id, Account = accountResponse, Comments = item.Comments, Content = item.Content, ImagePost = item.ImagePost };
                listPostResponse.Add(postResponse);
            }

            return listPostResponse;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> GetPost(Guid id)
        {
            var post = await _context.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);

            var postResponse = new PostResponse { Id = post.Id, Account = null, Comments = post.Comments, Content = post.Content, ImagePost = post.ImagePost };

            if (post == null)
            {
                return NotFound();
            }

            return postResponse;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] Guid id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            post.Account = _context.Accounts.FirstOrDefault(x => x.Id == post.Account.Id);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(Guid id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
