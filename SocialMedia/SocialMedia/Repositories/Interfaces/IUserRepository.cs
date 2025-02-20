using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModel;
using SocialMedia.ViewModel.Edite;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<string> AddUser(registrationViewModel x);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserName(string name);
        Task<User> GetUserByEmail(string email);
        Task DeleteUser(string Username);
        Task<User> GetByEmailandPassword(string email, string password);
        Task Changepass(string email, string oldpass, string newpass);
        Task<string> EditeUser(EditeUserViewModel model);
        Task<string?> Login(LoginViewModel req);
    }
}
