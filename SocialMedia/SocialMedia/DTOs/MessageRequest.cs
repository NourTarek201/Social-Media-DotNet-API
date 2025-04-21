namespace SocialMedia.DTOs
{
    public class MessageRequest
    {
        public Guid TargetUserId { get; set; }
        public string Msg { get; set; }
    }

}
