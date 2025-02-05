namespace SocialMedia.Models
{
    public class UserReaction :BaseEntity
    {
        
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Post Post { get; set; }
        public Guid PostId { get; set; }

        public virtual Reaction Reaction { get; set; }
        public Guid ReactionId { get; set; }
    }
}
