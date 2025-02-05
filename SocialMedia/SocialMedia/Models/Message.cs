namespace SocialMedia.Models
{
    public class Message : BaseEntity
    {
        public string Content { get; set; }
        public Guid SenderId { get; set; }
    }
}
