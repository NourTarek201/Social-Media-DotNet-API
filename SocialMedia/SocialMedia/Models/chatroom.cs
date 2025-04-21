namespace SocialMedia.Models
{
    public class Chatroom :BaseEntity
    {
        //public virtual User Creator { get; set; }
        //public Guid CreatorId { get; set; }
        public virtual List<User> Users { get; set; } = new List<User>();
        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
