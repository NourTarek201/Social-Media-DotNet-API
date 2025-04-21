using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Models.Context;

namespace SocialMedia.Repositories
{
    public class FollowerRepository :BaseRepository<UserFollower>
    {
        public FollowerRepository(SocialDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserFollower>> getFollowers(Guid userId)
        {
            User user = await _context.Users.Include(f => f.Followers).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return null;
            return user.Followers;
        }
        public async Task<IEnumerable<UserFollower>> getFollowings(Guid userId)
        {
            User user = await _context.Users.Include(f => f.Followings).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return null;
            return user.Followings;
        }

        public async Task<UserFollower?> GetUserFollowerByUsers(Guid userId, Guid followerId)
        {
            return await _context.Followers
                .Include(uf => uf.User)  
                .Include(uf => uf.Follower)  
                .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FollowerId == followerId);
        }

    }
}
