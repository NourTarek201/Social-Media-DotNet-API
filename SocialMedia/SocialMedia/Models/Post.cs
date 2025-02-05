using SocialMedia.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
    public class Post : BaseEntity
    {
        public string MediaLink { get; set; }
        public Visibility PostPrivacy{ get; set; } = Visibility.Followers;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public List<UserReaction> Reacters { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
