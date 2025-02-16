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
        IBaseRepository<Post> _postBaserpository;
        IUserRepository _userRepository;
        ICommentRepository _commentRepository;
        IPostRepository _postRepository;
        public PostController(IBaseRepository<Post> PostCRUDRepository, IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _postBaserpository = PostCRUDRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
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
                    description=post.Description
                };
                await _postBaserpository.add(post1);
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
                var posts = await _postBaserpository.getAll();
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
                var userPosts = await _postRepository.AlluserPosts(user.Id);
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
                var post = await _postBaserpository.getById(id);
                await _postBaserpository.delete(post);
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
        [HttpPost("Edit")]
        public async Task<IActionResult> EditPost([FromBody] EditePostViewModel post)
        {
            try
            {
                var post1 = await _postBaserpository.getById(post.Id);
                post1.description = post.Description;
                post1.PostPrivacy = post.PostPrivacy;
                post1.UpdatedAt = DateTime.Now;
                await _postBaserpository.update(post1);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        

    }
}
