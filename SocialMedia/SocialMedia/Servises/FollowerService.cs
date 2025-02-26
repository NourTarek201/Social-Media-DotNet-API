using Microsoft.AspNetCore.Identity;
using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using System.Security.Claims;

namespace SocialMedia.Services
{
    public class FollowerService
    {
        private readonly UserManager<User> _userManager;
        IBaseRepository<UserFollower> _followerRepository;
        IUserRepository _userRepository;
        public FollowerService(UserManager<User> userManager, IBaseRepository<UserFollower> followerRepository
            , IUserRepository userRepository)
        {
            _userManager = userManager;
            _followerRepository = followerRepository;
            _userRepository = userRepository;
        }

        public async Task<UserFollower?> FollowUser(Guid userId, Guid followerId)
        {
            var follower = await _userRepository.GetUserById(followerId);
            var user = await _userRepository.GetUserById(userId);

            if (user == null || follower == null)
            {
                return null;
            }

            var newFollower = new UserFollower
            {
                UserId = userId,
                FollowerId = followerId,
                User=user,
                Follower = follower,
                CreatedAt = DateTime.UtcNow,
            };
            follower.Followings.Add(newFollower);
            user.Followers.Add(newFollower);
            return await _followerRepository.add(newFollower);
        }

        public async Task<UserFollower?> unFollowUser(Guid userId, Guid followerId)
        {
            var follower = await _userRepository.GetUserById(followerId);
            var user = await _userRepository.GetUserById(userId);

            if (user == null || follower == null)
            {
                return null;
            }

            foreach (UserFollower u in follower.Followings)
            {
                if (u.UserId == userId) {
                    return await _followerRepository.delete(u);
                } 
            }

            return null;
            
        }
    }
}
