using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;
        public CommentsController(CommentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _context.UserComments.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(UserComment userComment)
        {
            var values = _context.UserComments.Add(userComment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var values = _context.UserComments.Find(id);
            _context.UserComments.Remove(values);
            _context.SaveChanges();
            return Ok("Yorum başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var values = _context.UserComments.Find(id);
            return Ok(values);
        }

        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            var values = _context.UserComments.Update(userComment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla güncellendi");
        }

        [HttpGet("CommentListByProductId")]
        public IActionResult CommentListByProductId(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return Ok(new List<UserComment>());
                }

                var values = _context.UserComments.Where(x => x.ProductId == id).ToList();

                if (values == null)
                {
                    return Ok(new List<UserComment>());
                }

                return Ok(values);
            }
            catch (Exception)
            {
                return Ok(new List<UserComment>());
            }
        }
    }
}
