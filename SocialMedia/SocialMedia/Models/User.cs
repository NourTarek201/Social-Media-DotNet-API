using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class User : BaseUser
    {
        public List<Comment> Comments { get; set; } 
        public List<UserFollower> Followers { get; set; }
        public List<UserFollower> Followings { get; set; }
        public virtual List<Chatroom> Chatrooms { get; set; }
        public List<UserReaction> ReactedPosts { get; set; }
    }
}
