using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.Enums;
using SocialMedia.Repositories;
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
        private readonly FollowerRepository _followerRepository;
        IBaseRepository<UserFollower> followerRepo;
        
        UserManager<User> _userManager;
        public FollowersController(IBaseRepository<UserFollower> followerRepo, 
            UserManager<User> _userManager,
             FollowerService _followerService,
             FollowerRepository followerRepository)
            
        {
            this.followerRepo = followerRepo;
            this._userManager = _userManager;
            this._followerService = _followerService;
            this._followerRepository = followerRepository;
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

            return follow;
            
        }

        [HttpGet("UserFollowers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> getFollowers()
        {
            var userIdString = User.FindFirst("Id")?.Value;

            
           
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("Invalid user ID.");
            }
            var followers = await _followerRepository.GetFollowes(userId);

            if (followers == null)
            {
                return NotFound("No followers found.");
            }
            return Ok(followers);
        }

        [HttpGet("UserFollowing")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> getFollowing()
        {
            var userIdString = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Please sign in first.");
            }
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("Invalid user ID.");
            }
            var following = await _followerRepository.GetFollowings(userId);
            if (following == null)
            {
                return NotFound("No following found.");
            }
            return Ok(following);
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

            return unfollow;
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
