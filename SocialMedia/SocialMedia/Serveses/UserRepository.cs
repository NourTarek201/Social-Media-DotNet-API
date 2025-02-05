using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Serveses;
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

        public void AddUser(registrationViewModel x)
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
            _context.Users.Add(NewUser);
            _context.SaveChanges();
        }
        public List<User> GetAllUsers()
        {
            
            return _context.Users.ToList();
        }
        public User GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByUserName(string name) {
            return _context.Users.FirstOrDefault(u => u.UserName == name);
        }
        public User GetUserByEmail(string email) {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public void DeleteUser(string username)
        {
           
            _context.Users.Remove(_context.Users.FirstOrDefault(x=>x.UserName==username));
            _context.SaveChanges();
        }
        public User GetByEmailandPassword(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
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
