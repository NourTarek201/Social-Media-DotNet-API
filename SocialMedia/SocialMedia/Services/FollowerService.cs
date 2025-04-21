using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.Enums;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
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
                User = user,
                Follower = follower,
                CreatedAt = DateTime.UtcNow,
            };
            //follower.Followings.Add(newFollower);
            //user.Followers.Add(newFollower);
            //await _userRepository.update(user);
            var added = await _followerRepository.add(newFollower);
            Console.Write(user.Followers.First().Id + "btatesssssssss");

            return added;
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
                if (u.UserId == userId)
                {
                    return await _followerRepository.delete(u);
                }
            }

            return null;

        }

        public async Task<UserFollower> UpdateStatusAsync(Guid userId, Guid followerId, RequestStatus status)
        {
            User u = await _userRepository.GetUserById(userId);
            Console.WriteLine(userId + "\n\n\n\nbsssssssssssssssss" + followerId);
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
