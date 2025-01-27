using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class BaseIdentityUser : IdentityUser<Guid>
    {
        // Add any common properties for all identity users here
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
