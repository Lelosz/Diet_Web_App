using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogCommentController(ApplicationDbContext context) => _context = context;


        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment(int postId, [FromBody] BlogComment comment)
        {
            
            comment.BlogPostId = postId;

            _context.BlogComments.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddComment), comment);
        }


        [HttpGet("{postId}/comments")]
        public IActionResult GetComments(int postId)
        {
            var query = _context.BlogComments.AsNoTracking();

            query = query.Where(x => x.BlogPostId == postId);

            if (query != null)
            {
                return Ok(query);
            }
            return null;



            //var issue = await _context.BlogComments.FindAsync(postId);
            //return issue == null ? NotFound() : Ok(issue);
            

            //var comments = await _context.BlogComments.FindAsync(postId);

            //if (query != null)
            //{
            //    return Ok(query);
            //}
            //return null;
        }













        [HttpPost("{commentId}/like")]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            var comment = await _context.BlogComments.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.DislikedByUser)
            {
                return Ok();
            }
                
            if (comment.LikedByUser)
            {
                comment.TotalLikes--;
                comment.LikedByUser = false;
                await _context.SaveChangesAsync();

                return Ok();
            }

            comment.TotalLikes++;
            comment.LikedByUser = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

            
        [HttpPost("{commentId}/dislike")]
        public async Task<IActionResult> DislikeComment(int commentId)
        {
            var comment = await _context.BlogComments.FindAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.LikedByUser)
            {
                return Ok();
            }

            if (comment.DislikedByUser)
            {
                comment.TotalDislikes--;
                comment.DislikedByUser = false;

                await _context.SaveChangesAsync();

                return Ok();
            }

            comment.TotalDislikes++;
            comment.DislikedByUser = true;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
