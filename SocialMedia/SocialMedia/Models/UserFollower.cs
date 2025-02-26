using SocialMedia.Models.Enums;

namespace SocialMedia.Models
{
    public class UserFollower : BaseEntity
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }
        public virtual User Follower { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending; //default value as pending request
    }
}
