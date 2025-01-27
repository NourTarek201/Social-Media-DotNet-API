using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
    public class Post
    {
        //id from base
        public Guid Id { get; set; }
        public string MediaLink { get; set; }
        public string Visibility { get; set; } = "public";
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        // List of users who liked this post
        public List<User> Likers { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
