using SocialMedia.Models.Enums;

namespace SocialMedia.ViewModel
{
    public class PostDTO
    {
        public Guid UserId { get; set; }
        public string MediaLink { get; set; }
        public string? Description { get; set; }
        public Visibility PostPrivacy { get; set; }
    }
}
