using SocialMedia.Models;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(registrationViewModel user);
        List<User> GetAllUsers();
        User GetUserById(Guid id);
        User GetUserByUserName(string name);
        User GetUserByEmail(string email);
        void DeleteUser(string Username);
        User GetByEmailandPassword(string email, string password);
    }
}
