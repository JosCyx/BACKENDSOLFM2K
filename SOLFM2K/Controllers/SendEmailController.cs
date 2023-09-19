using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOLFM2K.Models;
using SOLFM2K.Services.EmailService;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public SendEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendMail(EmailDTO request)
        {
            try
            {
                _emailService.SendEmail(request);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
