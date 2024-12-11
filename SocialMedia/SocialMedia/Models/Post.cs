namespace SocialMedia.Models
{
    public class Post : BaseEntity
    {
        public string MediaLink { get; set; }
        public string Visibility { get; set; } = "public";
        public List<User> Likers { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
