using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using SocialMedia.Repositories.Interfaces;

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
        public async Task<string> SendEmail(string to, string subject = "Hello To our project", string body = "Is this really you?")
        {
            if (string.IsNullOrWhiteSpace(to) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(body))
            {
                return "Failed: All fields (To, Subject, Body) are required!";
            }
            try
            {
                await SendEmailAsync(to, subject, body);
                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                return $"Failed to send email. Error: {ex.Message}";
            }
        }
        public string EmailBody(string url)
        {
            string emailBody = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Verify Your Email</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }}
        .header {{
            font-size: 24px;
            font-weight: bold;
            color: #333333;
            margin-bottom: 20px;
        }}
        .content {{
            font-size: 16px;
            color: #555555;
            margin-bottom: 30px;
        }}
        .btn {{
            display: inline-block;
            padding: 12px 24px;
            font-size: 16px;
            color: #ffffff;
            background-color: #28a745;
            text-decoration: none;
            border-radius: 5px;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 12px;
            color: #888888;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>Verify Your Email Address</div>
        <div class='content'>
            Thank you for signing up! To complete your registration, please click the button below to verify your email address.
        </div>
        <a href='{url}' class='btn'>Verify Email</a>
        <div class='footer'>
            If you did not request this, you can safely ignore this email.<br />
            &copy; {DateTime.UtcNow.Year} Social. All rights reserved.
        </div>
    </div>
</body>
</html>";
            return emailBody;
        }

        public string ForgotPasswordBody(string url)
        {
            string emailBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 20px;
            }}
            .container {{
                max-width: 500px;
                margin: auto;
                background: white;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            h2 {{
                color: #333;
            }}
            p {{
                color: #555;
                font-size: 16px;
            }}
            .button {{
                display: inline-block;
                padding: 12px 24px;
                background-color: #28a745;
                color: white;
                font-size: 16px;
                text-decoration: none;
                border-radius: 5px;
                margin-top: 20px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Password Reset Request</h2>
            <p>We received a request to reset your password. Click the button below to proceed:</p>
            <a href='{url}' class='button'>Reset Password</a>
            <p>If you did not request this, please ignore this email.</p>
            <p class='footer'>This link will expire soon. Please reset your password promptly.</p>
        </div>
    </body>
    </html>";

            return emailBody;
        }

        public string PasswordRestBody(string newPassword)
        {
            string emailBody = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }}
            .container {{
                max-width: 600px;
                margin: 20px auto;
                background: #ffffff;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            h2 {{
                color: #333;
            }}
            p {{
                color: #555;
                font-size: 16px;
            }}
            .password-box {{
                display: inline-block;
                padding: 10px;
                background-color: #f8f9fa;
                border: 1px solid #ddd;
                border-radius: 5px;
                font-size: 18px;
                font-weight: bold;
                margin-top: 10px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>Password Reset Successful</h2>
            <p>Your password has been reset successfully. Here is your new temporary password:</p>
            <div class='password-box'>{newPassword}</div>
            <p>Please log in and change your password immediately to keep your account secure.</p>
            <p class='footer'>If you did not request this change, please contact support immediately.</p>
        </div>
    </body>
    </html>";

            return emailBody;
        }
    }
}
