using SocialMedia.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class Post : BaseEntity
    {
        public string MediaLink { get; set; }
        public Visibility PostPrivacy{ get; set; } = Visibility.Followers;
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public List<UserReaction>? Reacters { get; set; }
        [JsonIgnore]
        public List<Comment>? Comments { get; set; }
    }
}
