using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JwtAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly ILogger<JwtAuthorizationFilter> _logger;
    private readonly IConfiguration _configuration;

    public JwtAuthorizationFilter(ILogger<JwtAuthorizationFilter> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Verifica si el usuario está autenticado
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Obtiene los valores de validacion del token desde la configuración
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        // Obtiene el token JWT de la solicitud
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Configura la validación del token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            // Si la validación del token es exitosa, la solicitud se procesa normalmente
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al validar el token JWT: {ex.Message}");
            context.Result = new UnauthorizedResult();
        }
    }
}
