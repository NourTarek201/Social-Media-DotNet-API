using SocialMedia.Models;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(registrationViewModel user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserName(string name);
        Task<User> GetUserByEmail(string email);
        Task DeleteUser(string Username);
        Task<User> GetByEmailandPassword(string email, string password);
    }
}
