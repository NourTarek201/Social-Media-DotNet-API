using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Models
{
    public class User : IdentityUser
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Post> CreatedPosts { get; set; }
        public List<Post> LikedPosts { get; set; }

           public List<Comment> CreatedComments { get; set; }
        //   public List<Followers> Followers { get; set; }
        public virtual List<chatroom>Chatrooms { get; set; }   


    }
}
