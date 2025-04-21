using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.Enums;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private FollowerService _followerService;
        IBaseRepository<UserFollower> followerRepo;
        UserManager<User> _userManager;
        public FollowersController(IBaseRepository<UserFollower> followerRepo, 
            UserManager<User> _userManager,
             FollowerService _followerService)
        {
            this.followerRepo = followerRepo;
            this._userManager = _userManager;
            this._followerService = _followerService;
        }
        // GET: api/<FollowersController>
        [HttpGet]
        public async Task<IActionResult> getAllFollowers()
        {
            var all = await followerRepo.getAll();
            return Ok(all);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("FollowUser")]
        public async Task<IActionResult> FollowUser(Guid followingUserId)
        {
            var userIdString = User.FindFirst("Id")?.Value;

            //Console.WriteLine($"Extracted User ID: {userIdString}");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }
            var follow = await _followerService.FollowUser(followingUserId, userGuid);
            
            if(follow == null)
            {
                return BadRequest("Invalid request.");
            }
            return Ok("Follow request successful.");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("unFollowUser")]
        public async Task<IActionResult> unFollowUser(Guid followingUserId)
        {
            var userIdString = User.FindFirst("Id")?.Value;

            //Console.WriteLine($"Extracted User ID: {userIdString}");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            var unfollow = await _followerService.unFollowUser(followingUserId, userGuid);

            if (unfollow == null)
            {
                return BadRequest("Invalid request.");
            }
            return Ok("UnFollow request successful.");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRequest(Guid followerID)
        {
            var userIdString = User.FindFirst("Id")?.Value;

            //Console.WriteLine($"Extracted User ID: {userIdString}");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            var result = await _followerService.UpdateStatusAsync(userGuid, followerID, RequestStatus.Accepted);
            return result != null ? Ok("Status Updated") : BadRequest("Failed to update status");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("block")]
        //needed logic to block content of this user 
        public async Task<IActionResult> BlockRequest(Guid followerID)
        {
            var userIdString = User.FindFirst("Id")?.Value;

            //Console.WriteLine($"Extracted User ID: {userIdString}");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            var result = await _followerService.UpdateStatusAsync(userGuid, followerID, RequestStatus.Blocked);
            return result != null ? Ok("Status Updated") : BadRequest("Failed to update status");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("reject")]
        public async Task<IActionResult> RejectRequest(Guid followerID)
        {
            var userIdString = User.FindFirst("Id")?.Value;

            //Console.WriteLine($"Extracted User ID: {userIdString}");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            var result = await _followerService.UpdateStatusAsync(userGuid, followerID, RequestStatus.Rejected);
            return result != null ? Ok("Status Updated") : BadRequest("Failed to update status");
        }
    }
}
