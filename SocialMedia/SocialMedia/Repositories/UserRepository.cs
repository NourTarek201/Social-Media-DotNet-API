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

        public UserRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(registrationViewModel x)
        {
            if (_context.Users.Any(u => u.Email == x.Email))
            {
                throw new Exception("Email is already taken");
            }
            if (_context.Users.Any(u => u.UserName == x.Username))
            {
                throw new Exception("Username is already taken");
            }
            User NewUser = new User {

               FirstName = x.FirstName,
               LastName = x.LastName,
               Email = x.Email,
               UserName = x.Username,
               PhoneNumber = x.Phone,

            };
            var Hashed = new PasswordHasher<User>();
            NewUser.PasswordHash = Hashed.HashPassword(NewUser, x.Password);
            await _context.Users.AddAsync(NewUser);
           await _context.SaveChangesAsync();
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
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            {
                throw new Exception("Password is incorrect");
            }
            return user;
        }

    }
}
