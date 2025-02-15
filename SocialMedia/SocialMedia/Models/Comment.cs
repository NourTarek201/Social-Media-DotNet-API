using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMedia.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public Guid PostId { get; set; }
        [JsonIgnore]
        public virtual Post Post { get; set; }
    }
}
