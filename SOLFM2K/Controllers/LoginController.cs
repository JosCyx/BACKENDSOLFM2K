using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.Services;
using SOLFM2K.Services.CryptoService;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SolicitudContext _context;
        private readonly ITokenService _tokenService; // Inyecta el servicio
        private readonly ICryptoService _cryptoService;

        public LoginController(SolicitudContext context, ITokenService tokenService, ICryptoService cryptoService)
        {
            _context = context;
            _tokenService = tokenService;
            _cryptoService = cryptoService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(us => us.UsLogin == model.username);

            if (user == null)
            {
                return NotFound("El usuario no existe.");
            }

            var newPass = _cryptoService.DecryptPassword(user.UsContrasenia);

            if (newPass != model.pass)
            {
                return BadRequest("La contraseña no coincide.");
            }

            var token = _tokenService.GenerateToken(user, 720);

            var response = new
            {
                Token = token,
                Usuario = new
                {
                    //datos para realizar la autorizacion
                    usLogin = user.UsLogin,
                    usIdNomina = user.UsIdNomina,
                    usNombre = user.UsNombre
                }
            };

            return Ok(response);
        }

    }
}
