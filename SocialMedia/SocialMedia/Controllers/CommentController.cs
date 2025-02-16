using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        IBaseRepository<Comment> _commentBaseRepository;
        IBaseRepository<Post> _postRepository;
        IUserRepository _userRepository;
        public CommentController(IBaseRepository<Comment> commentRepository, IBaseRepository<Post> postRepository, IUserRepository userRepository)
        {
            _commentBaseRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddComment([FromBody] CommentViewModel comment)
        {
            try
            {
                Comment comment1 = new Comment
                {
                    Content = comment.Content,
                    UserId = comment.UserId,
                    PostId = comment.PostId
                };
                var user=await _userRepository.GetUserById(comment.UserId);
                var post = await _postRepository.getById(comment.PostId);
                if (user==null||post==null)
                {
                    return BadRequest("User or post not found");
                }
                await _commentBaseRepository.add(comment1);
                return Ok("Comment added sucsefuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetAll")]
        public async Task<IActionResult> getall()
        {
           var com= await _commentBaseRepository.getAll();
            
            return Ok(com);
        }
        [HttpPost("Edit")]
        public async Task<IActionResult>Edite([FromBody]EditeCommentViewModel Editcomment)
        {
            try
            {
                var comment = await _commentBaseRepository.getById(Editcomment.Id);
               comment.Content = Editcomment.Comment;
                comment.UpdatedAt = DateTime.Now;
                await _commentBaseRepository.update(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        
        
        

    }
}
