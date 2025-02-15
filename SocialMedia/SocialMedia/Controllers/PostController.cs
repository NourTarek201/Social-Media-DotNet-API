using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IBaseRepository<Post> _postRepository;
        IUserRepository _userRepository;
        ICommentRepository _commentRepository;
        public PostController(IBaseRepository<Post> postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddPost([FromBody] PostViewModel post)
        {
            try
            {
                Post post1 = new Post
                {
                    UserId = post.UserId,
                    MediaLink = post.MediaLink,
                    PostPrivacy = post.PostPrivacy,
                    UpdatedAt = post.UpdatedAt
                };
                await _postRepository.add(post1);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var posts = await _postRepository.getAll();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("User")]
        public async Task<IActionResult> ForUser(string Username)
        {
            try
            {
                var user = await _userRepository.GetUserByUserName(Username);
                var posts = await _postRepository.getAll();
                var userPosts = posts.Where(p => p.UserId == user.Id);
                return Ok(userPosts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            try
            {
                var post = await _postRepository.getById(id);
                await _postRepository.delete(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("PostComments")]
        public async Task<IActionResult> UserComments(Guid Postid)
        {
            try
            {
                var comments = await _commentRepository.AllpostComments(Postid);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
