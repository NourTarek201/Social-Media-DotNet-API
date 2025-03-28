namespace SocialMedia.Repositories.Interfaces
{
    public interface IEmailService
    {
        string EmailBody(string url);
        Task SendEmailAsync(string toEmail, string subject, string body);
        string ForgotPasswordBody(string url);
        string PasswordRestBody(string newPassword);
    }
}
