using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Models.Enums;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SocialMedia.Services
{
    public class FollowerService
    {
        private readonly UserManager<User> _userManager;
        FollowerRepository _followerRepository;
        IUserRepository<User> _userRepository;

        public FollowerService(UserManager<User> userManager, FollowerRepository followerRepository
            , IUserRepository<User> userRepository)
        {
            _userManager = userManager;
            _followerRepository = followerRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> FollowUser(Guid userId, Guid followerId)
        {
            var follower = await _userRepository.GetUserById(followerId);
            var user = await _userRepository.GetUserById(userId);

            if (user == null || follower == null)
            {
                return new BadRequestObjectResult("User or follower not found.");
            }

            var existingRequest = await _followerRepository.GetUserFollowerByUsers(userId, followerId);
            if (existingRequest != null)
            {
                return new BadRequestObjectResult("A follow request already exists between these users.");
            }
            if (userId == followerId)
            {
                return new BadRequestObjectResult("Cannot follow yourself");
            }

            var newFollower = new UserFollower
            {
                UserId = userId,
                FollowerId = followerId,
                User = user,
                Follower = follower,
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _followerRepository.add(newFollower);
            if (result == null)
            {
                return new StatusCodeResult(500);
            }

            return new OkObjectResult("Follow request sent successfully.");
        }

        public async Task<IActionResult> unFollowUser(Guid userId, Guid followerId)
        {
            var follower = await _userRepository.GetUserById(followerId);
            var user = await _userRepository.GetUserById(userId);
            if (userId == followerId)
            {
                return new BadRequestObjectResult("User Cant follow him self.");
            }
            if (user == null || follower == null)
            {
                return new BadRequestObjectResult("User or follower not found.");
            }

            var userFollower = follower.Followings.FirstOrDefault(u => u.UserId == userId);
            if (userFollower == null) return new StatusCodeResult(500);
                
            
            
           var result= await _followerRepository.delete(userFollower);
            if (result ==null)  return new StatusCodeResult(500);


            return new OkObjectResult("user unfollowed successfully.");
        }

       

        public async Task<UserFollower> UpdateStatusAsync(Guid userId, Guid followerId, RequestStatus status)
        {
            User u = await _userRepository.GetUserById(userId);
            UserFollower userFollower = await _followerRepository.GetUserFollowerByUsers(userId, followerId);
            if (userFollower == null)
            {
                throw new Exception("follow request not sent");
            }
            if (status == RequestStatus.Rejected)
            {
                return await _followerRepository.delete(userFollower);
            }

            userFollower.Status = status;
            return await _followerRepository.update(userFollower);
        }
    }
}
