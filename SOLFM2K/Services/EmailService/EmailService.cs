using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using SOLFM2K.Models;

namespace SOLFM2K.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SendEmail(EmailDTO request)
        {
            var emailHost = _configuration.GetSection("MailConfiguration:EmailHost").Value;
            var emailUsername = _configuration.GetSection("MailConfiguration:EmailUsername").Value;
            var emailPass = _configuration.GetSection("MailConfiguration:EmailPass").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUsername));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Asunto;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Contenido };
            email.Body.Headers.Add("Content-Type", "text/html; charset=utf-8");

            using var smtp = new SmtpClient();
            smtp.Connect(emailHost, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailUsername, emailPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
