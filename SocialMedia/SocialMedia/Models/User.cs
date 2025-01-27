using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class User : BaseIdentityUser
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [JsonIgnore]

        public List<Post> CreatedPosts { get; set; } = new List<Post>();
        [JsonIgnore]

        public List<Post> LikedPosts { get; set; } = new List<Post>();
        [JsonIgnore]

        public List<Comment> CreatedComments { get; set; } = new List<Comment>();
        [JsonIgnore]

        // public List<Followers> Followers { get; set; }
        public virtual List<chatroom> Chatrooms { get; set; } = new List<chatroom>();
    }
}
