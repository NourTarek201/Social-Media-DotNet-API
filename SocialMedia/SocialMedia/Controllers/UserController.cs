using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SoocialDbContext _context;
        public UserController(SoocialDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult AddCategore([FromBody] User catigure)
        {
            _context.Users.Add(catigure);
            _context.SaveChanges();
            return Ok();
        }
    }
}
