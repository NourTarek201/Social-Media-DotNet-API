namespace SocialMedia.Models
{
    public class chatroom
    {
        public Guid Id { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
