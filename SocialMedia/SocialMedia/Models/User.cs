using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class User : BaseUser
    {
        [JsonIgnore]
        public List<Comment> Comments { get; set; }
        [JsonIgnore]
        public List<UserFollower>? Followers { get; set; }
        [JsonIgnore]
        public List<UserFollower>? Followings { get; set; }
        [JsonIgnore]
        public virtual List<Chatroom>? Chatrooms { get; set; }
        [JsonIgnore]
        public List<UserReaction>? ReactedPosts { get; set; }
    }
}
