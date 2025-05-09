using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModel;
using SocialMedia.ViewModel.Edite;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IUserRepository<T>: IBaseRepository<T>
    {
        Task<string> AddUser(registerationDTO x);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserName(string name);
        Task<User> GetUserByEmail(string email);
        Task DeleteUser(string Username);
        Task<User> GetByEmailandPassword(string email, string password);
        Task Changepass(string email, string oldpass, string newpass);
        Task<string> EditeUser(EditeUserDTO model);
        Task<string?> Login(LoginDTO req);
        Task<string> ForgotPassword(string email);
        Task<string> AutoResetPassword(Guid userId, string token);



    }
}
