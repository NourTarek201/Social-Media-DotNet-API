using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class User : BaseUser
    {
        [JsonIgnore]
        public virtual List<Comment> Comments { get; set; }
        //[JsonIgnore]
        public virtual List<UserFollower> Followers { get; set; } = new List<UserFollower>();
        //[JsonIgnore]
        public virtual List<UserFollower> Followings { get; set; } = new List<UserFollower>();
        //[JsonIgnore]
        public virtual List<Chatroom> Chatrooms { get; set; } = new List<Chatroom>();
        //[JsonIgnore]
        public virtual List<UserReaction> ReactedPosts { get; set; } = new List<UserReaction>();
    }
}
