using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Test.API.Controllers;

namespace Test.API.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713 and https://kenhaggerty.com/articles/article/aspnet-core-22-smtp-emailsender-implementation

    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailConfirmationAsync(string email, string link);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(configuration.GetSection("EmailService").GetValue<string>("SenderName"), configuration.GetSection("EmailService").GetValue<string>("Sender")));
                mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    // The third parameter is useSSL (true if the client should make an SSL-wrapped
                    // connection to the server; otherwise, false).
                    await client.ConnectAsync(configuration.GetSection("EmailService").GetValue<string>("MailServer"), configuration.GetSection("EmailService").GetValue<int>("MailPort"), configuration.GetSection("EmailService").GetValue<bool>("UseSSL"));

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(configuration.GetSection("EmailService").GetValue<string>("Sender"), configuration.GetSection("EmailService").GetValue<string>("Password"));

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch
            {
                throw;
            }
        }

        public Task SendEmailConfirmationAsync(string email, string link)
        {
            return SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
