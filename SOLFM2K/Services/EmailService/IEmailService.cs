using SOLFM2K.Models;

namespace SOLFM2K.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
