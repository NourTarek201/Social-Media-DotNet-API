namespace SocialMedia.Models
{
    public class Message
    {
        //id from base
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid SenderId { get; set; }
        //  public User Sender { get; set;}
    }
}
