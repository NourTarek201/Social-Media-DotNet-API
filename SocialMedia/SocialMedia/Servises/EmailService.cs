using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SocialMedia.Servises
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html") { Text = body };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["SenderPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
        public string EmailBody(string url)
        {
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
        <a href='{url}' class='btn'>Verify Email</a>
        <div class='footer'>
            If you did not create an account, please ignore this email.
        </div>
    </div>
</body>
</html>";
            return emailBody;

        }
    }
}
