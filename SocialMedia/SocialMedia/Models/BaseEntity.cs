using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class BaseEntity 
    {
        public Guid Id { get; set; }
        public TimestampAttribute Timestamp { get; set; }

    }
}
