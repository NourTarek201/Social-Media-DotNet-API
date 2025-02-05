using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

using SocialMedia.Repositories;
using SocialMedia.Serveses;
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
        public ActionResult AddUser([FromBody] registrationViewModel user)
        {
            try
            {
                _userRepository.AddUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByEmailAndpass")]
        public IActionResult GetByEmailandPassword(string email, string password)
        {
            try
            {
               var user= _userRepository.GetByEmailandPassword(email, password);
                return Ok(user);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public ActionResult getalluser()
        {
           var x = _userRepository.GetAllUsers();
            return Ok(x);
        }

        [HttpGet("byId")]
        public IActionResult getuser(Guid id) 
        {
           var x= _userRepository.GetUserById(id);
            return Ok(x);
        }

        [HttpGet("Username")]
        public IActionResult getuserbyusername(string name)
        {
            var x = _userRepository.GetUserByUserName(name);
            return Ok(x);
        }

        [HttpGet("byEmail")]
        public IActionResult getuserbyemail(string email)
        {
            var x = _userRepository.GetUserByEmail(email);
            return Ok(x);
        }

        [HttpDelete]
        public IActionResult deleteuser(string username)
        {
            _userRepository.DeleteUser(username);
            return Ok();
        }

        

    }
}
