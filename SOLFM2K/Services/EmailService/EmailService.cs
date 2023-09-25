using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using SOLFM2K.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SOLFM2K.Services.CryptoService;

namespace SOLFM2K.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SolicitudContext _context;
        //private readonly ICryptoService _cryptoService;

        public EmailService(IConfiguration configuration, SolicitudContext context//, ICryptoService cryptoService
                                                                                   )
        {
            _configuration = configuration;
            _context = context;
            //inyeccion de dependencia del servicio de encriptacion
            //_cryptoService = cryptoService;
        }

        //desencripta una contraseña que le ingrese como parametro
        /*public string GetPass(string passDB)
        {
            return _cryptoService.DecryptPassword(passDB);
        }*/

        public void SendEmail(EmailDTO request)
        {
            //recupera las credenciales para enviar los correos
            var smtpCredentials = _context.ParamsConfs.FirstOrDefault(cr => cr.Identify == "SMTP");

            var emailHost = _configuration.GetSection("MailConfiguration:EmailHost").Value;
            var emailUsername = smtpCredentials.Content;
            var emailPass = smtpCredentials.Pass;
            //var emailPass = GetPass(smtpCredentials.Pass);
            


            //recorre la lista de destinatarios y envia un correo a cada uno

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUsername));
            email.To.Add(MailboxAddress.Parse(request.Destinatario));
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
