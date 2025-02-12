using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;
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
       
        public UserController(IUserRepository userRepository, UserManager<User> userManager,SignInManager<User>_signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
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
        [HttpPost]
        //public async Task<IActionResult> Signin(string email, string password)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(email);

        //        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        //        {
        //            return BadRequest("Invalid email or password.");
        //        }

        //        // var token = GenerateToken(user); // Generate token for the signed-in user

        //        //  await _signInManager.PasswordSignInAsync(user, password, false, false);
        //      //  await _userManager.GenerateUserTokenAsync(user);
        //        return Ok(user);
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

       


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
        

        

    }
}
