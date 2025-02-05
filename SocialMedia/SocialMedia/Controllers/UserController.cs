using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModel;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SocialDbContext _context;
        public UserController(SocialDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult AddCategore([FromBody] UserviewModel catigure)
        {
            User x = new User();
            x.FirstName= catigure.firstName;
            x.LastName = catigure.lastName;
            x.Email = catigure.email;
            x.UserName = catigure.password;
            x.PhoneNumber = catigure.phone;
              x.PasswordHash = catigure.password;   
            _context.Users.Add(x);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public ActionResult getalluser()
        {
           var x = _context.Users.ToList();
            return Ok(x);
        }
    }
}
