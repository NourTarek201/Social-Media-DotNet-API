using SocialMedia.Models.Enums;

namespace SocialMedia.ViewModel
{
    public class PostViewModel
    {
        public Guid UserId { get; set; }
        public string MediaLink { get; set; }
        public Visibility PostPrivacy { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}
