using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Models
{
    public class BaseUser : IdentityUser<Guid>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Post> CreatedPosts { get; set; }
    }
}
