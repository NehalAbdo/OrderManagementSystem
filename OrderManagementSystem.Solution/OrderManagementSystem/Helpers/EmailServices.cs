using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using OrderManagementSystem.Setting;
using System;
using System.Threading.Tasks;
using OrderManagementSystem.Helpers;

namespace Order.Services
{
    public class EmailServices : IMailService
    {
        private readonly MailSetting _emailSettings;

        public EmailServices(IOptions<MailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));
                emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlMessage;
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly (logging, etc.)
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}