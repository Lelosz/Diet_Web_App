using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostController(ApplicationDbContext context) => _context = context;



        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreatePost([FromBody]BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePost), blogPost);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePost(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id) return BadRequest("Blog ID mismatch");

            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("PostList")]
        public IActionResult retrieveAllPostInformation()
        {
            var Posts = _context.BlogPosts;
            return Posts == null ? NotFound() : Ok(Posts);
            
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.BlogPosts.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }
    }
}
