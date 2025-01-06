using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Comment : BaseEntity
    {
        //id from base
        public string Content { get; set; }
        //time from base
    }
}
