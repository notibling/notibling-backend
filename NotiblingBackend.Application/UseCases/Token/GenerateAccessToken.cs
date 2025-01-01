using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotiblingBackend.Application.Interfaces.Token;
using NotiblingBackend.Contracts.DTOs;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.Token
{
    public class GenerateAccessToken : IGenerateAccessToken
    {
        #region Dependency Injection
        private readonly RSA _privateKey;
        private readonly IConfiguration _configuration;

        public GenerateAccessToken(IConfiguration configuration)
        {
            _configuration = configuration;
            _privateKey = RSA.Create();
            _privateKey.ImportFromPem(System.IO.File.ReadAllText("C:\\Users\\monte\\rsa-private-key.pem"));
        }
        #endregion
        public string GenerateToken(UserDto user)
        {
            // Crear los claims esenciales
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Configurar el token descriptor
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "ApiUsers_Issuer",
                Audience = "EcommerceMicroservices_Audience",
                SigningCredentials = new SigningCredentials(
                    new RsaSecurityKey(_privateKey),
                    SecurityAlgorithms.RsaSha256
                )
            };

            // Generar y serializar el token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
