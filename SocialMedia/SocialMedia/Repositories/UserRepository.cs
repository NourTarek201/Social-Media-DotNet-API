using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialDbContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
       
        public UserRepository(SocialDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<string> AddUser(registrationViewModel x)
        {
            if (_context.Users.Any(u => u.Email == x.Email))
            {
                throw new Exception("Email is already taken");
            }
            if (_context.Users.Any(u => u.UserName == x.Username))
            {
                throw new Exception("Username is already taken");
            }

            User NewUser = new User
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.Username,
                PhoneNumber = x.Phone,
            };

            var results = await _userManager.CreateAsync(NewUser, x.Password);
            string ans = "User created successfully";
            if (!results.Succeeded)
            {
                ans = string.Join(", ", results.Errors.Select(e => e.Description));
            }
            return ans;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetUserByUserName(string name) {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }
        public async Task<User> GetUserByEmail(string email) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task DeleteUser(string username)
        {
           
            await _context.Users.Where(x=>x.UserName==username).ExecuteDeleteAsync();
            _context.SaveChanges();
        }
        public async Task<User> GetByEmailandPassword(string email, string password)
        {
            var user =await  _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("Email is not found");
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new Exception("Password is incorrect");
            }
            return user;
        }
        public async Task Changepass(string email, string oldpass, string newpass)
        {
            var user = await GetByEmailandPassword(email, oldpass);
            await _userManager.ChangePasswordAsync(user, oldpass, newpass);
        }

    }
}
