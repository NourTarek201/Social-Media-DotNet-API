using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public TimestampAttribute Timestamp { get; set; }
    }
}
