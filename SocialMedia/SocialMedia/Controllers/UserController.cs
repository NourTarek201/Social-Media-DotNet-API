using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      //  private readonly SocialDbContext _context;
        
        IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] registrationViewModel user)
        {
            try
            {
                await _userRepository.AddUser(user);
                return Ok();
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

        [HttpGet("All")]
        public async Task<IActionResult> getalluser()
        {
           var x = await _userRepository.GetAllUsers();
            return Ok(x);
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
        public async Task<IActionResult> deleteuser(string username)
        {
            await _userRepository.DeleteUser(username);
            return Ok();
        }

        

    }
}
