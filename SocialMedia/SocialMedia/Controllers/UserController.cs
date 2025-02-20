using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;
using SocialMedia.ViewModel.Edite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      //  private readonly SocialDbContext _context;
        
        IUserRepository _userRepository;
        SignInManager<User> _signInManager;
        UserManager<User> _userManager;
        ICommentRepository _commentRepository;
        IPostRepository _postRepository;
       
        public UserController(IUserRepository userRepository, UserManager<User> userManager,SignInManager<User>_signInManager,ICommentRepository commentRepository,IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _commentRepository = commentRepository; 
            _postRepository = postRepository;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] registrationViewModel user)
        {
            try
            {
                var result = await _userRepository.AddUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
  

        [HttpGet("ByEmailAndpass")]
        public async Task<IActionResult> GetByEmailandPassword(string email, string password)
        {
            try
            {
               var user= await _userRepository.GetByEmailandPassword(email, password);
                return Ok(user);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> getalluser()
        {
            try
            {
                var x = await _userRepository.GetAllUsers();
                return Ok(x);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("You are not authorized to access this resource.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> getuser(Guid id) 
        {
            var x = await _userRepository.GetUserById(id);
            return Ok(x);
        }

        [HttpGet("Username")]
        public async Task<IActionResult> getuserbyusername(string name)
        {
            var x =await _userRepository.GetUserByUserName(name);
            return Ok(x);
        }

        [HttpGet("byEmail")]
        public async Task<IActionResult> getuserbyemail(string email)
        {
            var x = await _userRepository.GetUserByEmail(email);
            return Ok(x);
        }

        [HttpDelete]
        public async Task<IActionResult> Deleteuser(string username)
        {
            await _userRepository.DeleteUser(username);
            return Ok();
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> Changepass(string email,string oldpass,string newpass)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, oldpass))
            {
                return BadRequest("Invalid email or password.");
            }

            await _userRepository.Changepass(email, oldpass, newpass);
            return Ok();
        }
        [HttpGet("UserComments")]
        public async Task<IActionResult> UserComments(Guid userid)
        {
            try
            {
                var comments = await _commentRepository.AlluserComments(userid);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("UserPosts")]
        public async Task<IActionResult> UserPosts(Guid userid)
        {
            try
            {
                var posts = await _postRepository.AlluserPosts(userid);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("EditeUser")]
        public async Task<IActionResult> EditeUser([FromBody] EditeUserViewModel model)
        {
            try
            {
                var result = await _userRepository.EditeUser(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel req)
        {
            try
            {
                var result = await _userRepository.Login(req);
             

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
