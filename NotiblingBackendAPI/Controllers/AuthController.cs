using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotiblingBackend.Application.Dtos.User;
using NotiblingBackend.Application.Interfaces.UseCases.User;
using NotiblingBackend.Contracts.DTOs;
using NotiblingBackend.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NotiblingBackendAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _validateLogin;
        //private readonly RSA _privateKey;

        public AuthController(IAuthentication validateLogin)
        {
            _validateLogin = validateLogin;
            //_privateKey = RSA.Create();
            //_privateKey.ImportFromPem(System.IO.File.ReadAllText("Keys/rsa-private-key.pem"));
        }

        [HttpPost("login-user")]
        public async Task<ActionResult> Login(AuthenticationDto auth)
        {

            try
            {
                var token = await _validateLogin.ValidateLogin(auth.Email, auth.Password);

                //if (user != null)
                //{
                //    //string tokenResponse = GenerateToken(user);

                //    return Ok(new { token = tokenResponse });
                //}
                //return BadRequest("Error en el login.");

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //private string GenerateToken(NotiblingBackend.Contracts.DTOs.UserDto user)
        //{
        //    //var userId = "12345"; // ID ficticio del usuario

        //    //// Crear los claims
        //    //var claims = new[]
        //    //{
        //    //        new Claim(JwtRegisteredClaimNames.Sub, userId),
        //    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    //        };

        //    //// Configurar el token
        //    //var tokenHandler = new JwtSecurityTokenHandler();
        //    //var tokenDescriptor = new SecurityTokenDescriptor
        //    //{
        //    //    Subject = new ClaimsIdentity(claims),
        //    //    Expires = DateTime.UtcNow.AddHours(1),
        //    //    Issuer = "ApiUsers_Issuer",
        //    //    Audience = "EcommerceMicroservices_Audience",
        //    //    SigningCredentials = new SigningCredentials(
        //    //        new RsaSecurityKey(_privateKey),
        //    //        SecurityAlgorithms.RsaSha256
        //    //    )
        //    //};

        //    //// Crear el token
        //    //var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //var tokenString = tokenHandler.WriteToken(token);


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
