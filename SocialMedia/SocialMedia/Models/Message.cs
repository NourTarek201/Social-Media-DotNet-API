namespace SocialMedia.Models
{
    public class Message : BaseEntity
    {
        //id from base
        public string Content { get; set; }
        public Guid SenderId { get; set; }
      //  public User Sender { get; set;}
    }
}
