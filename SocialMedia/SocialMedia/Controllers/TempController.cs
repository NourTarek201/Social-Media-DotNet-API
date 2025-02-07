using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;

namespace SocialMedia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TempController : ControllerBase
    {
        private readonly IBaseRepository<Reaction> repo;

        public TempController(IBaseRepository<Reaction> repo)
        {
            this.repo = repo;

        }

        [HttpPost("addReaction")]
        public async Task<IActionResult> addReaction(Reaction reaction)
        {
            var newReaction = await repo.add(reaction);
            return Ok();
        }

        [HttpGet("getReactions")]
        public async Task<IActionResult> getReactions()
        {
            var all = await repo.getAll();
            return Ok(all);
        }
    }
}
