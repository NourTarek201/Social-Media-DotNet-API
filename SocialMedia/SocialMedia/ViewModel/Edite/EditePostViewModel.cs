using SocialMedia.Models.Enums;

namespace SocialMedia.ViewModel.Edite
{
    public class EditePostViewModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Visibility PostPrivacy { get; set; }
         

    }
}
