namespace SocialMedia.Models
{
    public class Followers : BaseEntity
    {
        public virtual List<User>User {  get; set; }
        public virtual List<User> Follower { get; set; }
        public virtual string RequestStatus { get; set; }
    }
}
