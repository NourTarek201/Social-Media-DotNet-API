namespace SocialMedia.Models
{
    public class Post : BaseEntity
    {
        //id from base
        public string MediaLink { get; set; }
        public string Visibility { get; set; } = "public";
        // time from base
     //   public List<User> Likers { get; set; }
     //   public List<Comment> Comments { get; set; }
    }
}
