using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Application.Interfaces.UseCases.User;
using NotiblingBackend.Contracts.DTOs;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Mapping;
using NotiblingBackend.Utilities.Exceptions;
using NotiblingBackend.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using NotiblingBackend.Application.Interfaces.Token;

namespace NotiblingBackend.Application.UseCases.User
{
    public class Authentication : IAuthentication
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;
        private readonly IGenerateAccessToken _generateAccessToken;
        private readonly RSA _privateKey;

        public Authentication(IUserRepository userRepository, IGenerateAccessToken generateAccessToken)
        {
            _userRepository = userRepository;
            _privateKey = RSA.Create();
            _privateKey.ImportFromPem(System.IO.File.ReadAllText("Keys/rsa-private-key.pem"));
            _generateAccessToken = generateAccessToken;
        }
        #endregion

        public async Task<string> ValidateLogin(string email, string password)
        {
            try
            {
                var user = await _userRepository.ValidateLogin(email, password);

                bool isPasswordValid = PasswordEncryptor.VerifyPassword(password, user.Password);
                if (!isPasswordValid)
                {
                    throw new CompanyException("Los datos ingresados son incorrectos.");
                }

                var userDto = user switch
                {
                    Domain.Entities.Company company => new UserDto
                    {
                        Id = company.CompanyId.ToString(),
                        Role = company.UserRole.ToString(),
                    },
                    Customer customer => new UserDto
                    {
                        Id = customer.CustomerId.ToString(),
                        Role = customer.UserRole.ToString(),
                    },
                    _ => throw new Exception("Tipo de usuario desconocido.")
                };

                return _generateAccessToken.GenerateToken(userDto);

            }
            catch (CompanyException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private string GenerateToken(UserDto user)
        //{
        //    // Crear los claims esenciales
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        //        new Claim(ClaimTypes.Role, user.Role),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        //    };

        //    // Configurar el token descriptor
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        Issuer = "ApiUsers_Issuer",
        //        Audience = "EcommerceMicroservices_Audience",
        //        SigningCredentials = new SigningCredentials(
        //            new RsaSecurityKey(_privateKey),
        //            SecurityAlgorithms.RsaSha256
        //        )
        //    };

        //    // Generar y serializar el token
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}
    }
}
