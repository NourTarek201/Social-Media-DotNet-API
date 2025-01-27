using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Comment
    {
        //id from base
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        //time from base
    }
}
