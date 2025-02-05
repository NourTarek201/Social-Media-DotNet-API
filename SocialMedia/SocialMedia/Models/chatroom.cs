namespace SocialMedia.Models
{
    public class Chatroom :BaseEntity
    {
        public virtual List<User> Users { get; set; }

        public virtual List<Message> Messages { get; set; }
    }
}
