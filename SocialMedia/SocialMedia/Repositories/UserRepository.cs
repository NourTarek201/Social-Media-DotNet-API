using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Controllers;
using SocialMedia.Models;
using SocialMedia.Models.Context;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Servises;
using SocialMedia.ViewModel;
using SocialMedia.ViewModel.Edite;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialDbContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration; 
        private readonly IEmailService _emailService;

        public UserRepository(IEmailService emailService, SocialDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration= configuration;
            _emailService = emailService;
        }

        public async Task<string> AddUser(registerationViewModel x)
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

            if (results.Succeeded)
            {
               
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(NewUser);
                var encodedToken = WebUtility.UrlEncode(emailToken);
                var verificationUrl = $"http://localhost:5051/api/user/verify-email?userId={NewUser.Id}&token={encodedToken}";
                #region The body
                string emailBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
        .container {{ max-width: 600px; margin: 20px auto; background: #ffffff; padding: 20px; border-radius: 10px; 
                      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); text-align: center; }}
        .header {{ font-size: 24px; font-weight: bold; color: #333; margin-bottom: 10px; }}
        .content {{ font-size: 16px; color: #555; margin-bottom: 20px; }}
        .btn {{ display: inline-block; padding: 12px 20px; font-size: 18px; color: #fff; background-color: #28a745; 
                text-decoration: none; border-radius: 5px; }}
        .footer {{ margin-top: 20px; font-size: 14px; color: #777; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>Welcome to Our Project!</div>
        <div class='content'>
            Thank you for signing up. Please click the button below to verify your email address.
        </div>
        <a href='{verificationUrl}' class='btn'>Verify Email</a>
        <div class='footer'>
            If you did not create an account, please ignore this email.
        </div>
    </div>
</body>
</html>";
                #endregion 
                await SendEmail(NewUser.Email, $"Hello {NewUser.FirstName} {NewUser.LastName}", emailBody);
            }
            else
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
            return await _context.Users.Include(f=>f.Followings).Include(fo=>fo.Followers).FirstOrDefaultAsync(u => u.Id == id);
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
        public async Task <string> EditeUser(EditeUserViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if(_context.Users.FirstOrDefault(x=>x.UserName==model.Username)!= null)
            {
                throw new Exception("Username is already taken");
            }
            user.FirstName = model.firstName;
            user.LastName = model.lastName;
            user.UserName = model.Username;
            user.PhoneNumber = model.phone;
            await _context.SaveChangesAsync();
            return "User updated successfully";

        }
        public async Task<string?> Login(LoginViewModel req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == req.Email);
            if (user is null)
            {
                throw new Exception("Invalid username");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid password");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return "Email is not verified. Please check your inbox.";
            }
            string tokenString = GenerateJwtToken(user);
            user.SecurityStamp = tokenString;
            await _userManager.UpdateAsync(user);
            return tokenString;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["AppSettings:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["AppSettings:Issuer"],
                _configuration["AppSettings:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<string> SendEmail(string to, string subject = "Hello To our project", string body = "Is this really you?")
        {
            if (string.IsNullOrWhiteSpace(to) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(body))
            {
                return "Failed: All fields (To, Subject, Body) are required!";
            }
            try
            {
                await _emailService.SendEmailAsync(to, subject, body);
                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                return $"Failed to send email. Error: {ex.Message}";
            }
        }


    }
}
