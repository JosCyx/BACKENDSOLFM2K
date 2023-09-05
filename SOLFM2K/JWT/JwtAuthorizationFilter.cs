//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Configuration;
//using System.Text;

//public class JwtAuthorizationFilter : IAsyncAuthorizationFilter
//{
//    private readonly ILogger<JwtAuthorizationFilter> _logger;
//    private readonly IConfiguration _configuration;
//    //private readonly string _secretKey;

//    public JwtAuthorizationFilter(ILogger<JwtAuthorizationFilter> logger, IConfiguration configuration)
//    {
//        _logger = logger;
//        _configuration = configuration;
//        //_secretKey = secretKey;
//    }

//    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
//    {
//        // Verifica si el usuario está autenticado
//        if (!context.HttpContext.User.Identity.IsAuthenticated)
//        {
//            context.Result = new UnauthorizedResult();
//            return;
//        }

//        var secretKey = _configuration["SecKey"];

//        // Recupera el token JWT de la solicitud
//        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//        //if (string.IsNullOrEmpty(token))
//        //{
//        //    context.Result = new UnauthorizedResult();
//        //    return;
//        //}

//        if (token == null)
//        {
//            context.Result = new UnauthorizedResult();
//            return;
//        }

//        try
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(secretKey);

//            // Configura la validación del token
//            tokenHandler.ValidateToken(token, new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidIssuer = _configuration["JwtIssuer"],
//                ValidAudience = _configuration["JwtAudience"],
//                IssuerSigningKey = new SymmetricSecurityKey(key)
//            }, out SecurityToken validatedToken);

//            // Si la validación del token es exitosa, la solicitud se procesa normalmente
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError($"Error al validar el token JWT: {ex.Message}");
//            context.Result = new UnauthorizedResult();
//        }
//    }
//}

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

        // Obtiene la clave secreta desde la configuración
        var secretKey = _configuration["SecKey"];
        var issuer = _configuration["JwtIssuer"];
        var audience = _configuration["JwtAudience"];

        // Obtiene el token JWT de la solicitud
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        //REVISAR FILTRO PUES NO DEJA ACCEDER A NINGUNA PETICION
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

