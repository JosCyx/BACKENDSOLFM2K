using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOLFM2K.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOLFM2K.Services
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user, double expirationMinutes);
        void metodo();
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario user, double expirationMinutes)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UsLogin),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Puedes agregar otros reclamos personalizados aquí
            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes), // Establece la expiración del token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void metodo()
        {
            // Accede a los valores de la configuración
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var secretKey = _configuration["JwtSettings:SecretKey"];

            // Hacer algo con los valores, por ejemplo, imprimirlos
            Console.WriteLine($"Issuer: {issuer}");
            Console.WriteLine($"Audience: {audience}");
            Console.WriteLine($"SecretKey: {secretKey}");
        }
    }
}
