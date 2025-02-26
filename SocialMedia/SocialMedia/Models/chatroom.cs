namespace SocialMedia.Models
{
    public class Chatroom :BaseEntity
    {
        public virtual List<User> Users { get; set; } = new List<User>();

        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
