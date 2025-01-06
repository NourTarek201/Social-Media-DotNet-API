using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class BaseEntity : IdentityUser
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
