using Microsoft.EntityFrameworkCore;
using SocialMedia.DTOs;
using SocialMedia.Models;
using SocialMedia.Models.Context;

namespace SocialMedia.Repositories
{
    public class FollowerRepository :BaseRepository<UserFollower>
    {
        public FollowerRepository(SocialDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FollowerDTO>>GetFollowes(Guid UserId)
        {
            var user = await _context.Users
                .Include(f => f.Followers)
                .ThenInclude(f => f.Follower)
               .FirstOrDefaultAsync(x => x.Id == UserId);
            return user.Followers.Select(f => new FollowerDTO
            {
                FollowerId = f.FollowerId,
                FollowerName = f.Follower.UserName,
               
            });

        }

        public async Task<IEnumerable<FollowerDTO>> GetFollowings(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Followings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Followings == null)
                return Enumerable.Empty<FollowerDTO>();

            var followings = user.Followings.Select(f => new FollowerDTO
            {
                FollowerId = f.Id,
                FollowerName = f.Follower.UserName,
            });

            return followings;
        }






        public async Task<UserFollower?> GetUserFollowerByUsers(Guid userId, Guid followerId)
        {
            var result = await _context.Followers
                .Include(uf => uf.User)
                .Include(uf => uf.Follower)
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FollowerId == followerId);
            if (result == null) return null;
            return result;
        }

    }
}
