namespace SocialMedia.Servises
{
    public interface IEmailService
    {
        string EmailBody(string url);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
