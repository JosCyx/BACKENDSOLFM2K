using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using SOLFM2K.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SOLFM2K.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SolicitudContext _context;

        public EmailService(IConfiguration configuration, SolicitudContext context)
        {
            _configuration = configuration;
            _context = context; 
        }

        public void SendEmail(EmailDTO request)
        {
            var smtpCredentials = _context.SmtpConfs.FirstOrDefault();

            if (smtpCredentials == null)
            {
                //something
            }

            var emailHost = _configuration.GetSection("MailConfiguration:EmailHost").Value;
            var emailUsername = smtpCredentials.Username;
            var emailPass = smtpCredentials.Pass;

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
