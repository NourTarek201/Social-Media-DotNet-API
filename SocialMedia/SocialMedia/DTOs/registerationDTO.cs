using System.ComponentModel.DataAnnotations;

namespace SocialMedia.ViewModel
{
    public class registerationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

    }
}
