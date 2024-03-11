using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogReplyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogReplyController(ApplicationDbContext context) => _context = context;


        [HttpPost("{commentId}/replies")]
        public async Task<IActionResult> AddReply(int commentId, [FromBody] BlogReply reply)
        {
            
            reply.CommentId = commentId;

            _context.BlogReplys.Add(reply);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddReply), reply);
        }

        // Get all replies for a specific comment
        [HttpGet("{commentId}/replies")]
        public IActionResult GetReplies(int commentId)
        {
            var query = _context.BlogReplys.AsNoTracking();

            query = query.Where(x => x.CommentId == commentId);

            if (query != null)
            {
                return Ok(query);
            }
            return null;


            //var replies = await _context.BlogReplys.FindAsync(commentId);

            //if (replies == null)
            //{
            //    return NotFound();
            //}

            //return Ok(replies);
        }
    }
}
