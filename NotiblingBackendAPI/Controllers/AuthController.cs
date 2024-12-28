using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotiblingBackend.Application.Dtos.User;
using NotiblingBackend.Application.Interfaces.Token;
using NotiblingBackend.Application.Interfaces.Token.Refresh;
using NotiblingBackend.Application.Interfaces.UseCases.User;
using NotiblingBackend.Contracts.DTOs;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
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
        private readonly IGenerateAccessToken _generateAccessToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;
        private readonly IAddRefreshToken _addRefreshToken;
        private readonly ISaveChangesRefreshToken _saveChangesRefreshToken;
        private readonly IGetByRefreshToken _getByRefreshToken;
        private readonly IRevokeRefreshToken _revokeRefreshToken;
        //private readonly RSA _privateKey;

        public AuthController(
            IAuthentication validateLogin,
            IGenerateAccessToken generateAccessToken,
            IGenerateRefreshToken generateRefreshToken,
            IAddRefreshToken addRefreshToken,
            ISaveChangesRefreshToken saveChangesRefreshToken,
            IGetByRefreshToken getByRefreshToken,
            IRevokeRefreshToken revokeRefreshToken)
        {
            _validateLogin = validateLogin;
            _generateAccessToken = generateAccessToken;
            _generateRefreshToken = generateRefreshToken;
            _addRefreshToken = addRefreshToken;
            _saveChangesRefreshToken = saveChangesRefreshToken;
            _getByRefreshToken = getByRefreshToken;
            _revokeRefreshToken = revokeRefreshToken;
        }

        [HttpPost("login-user")]
        public async Task<ActionResult> Login(AuthenticationDto auth)
        {

            try
            {
                //var token = await _validateLogin.ValidateLogin(auth.Email, auth.Password);

                //return Ok(new { token });

                // Validar credenciales y generar el Access Token
                var token = await _validateLogin.ValidateLogin(auth.Email, auth.Password);

                // Generar el Refresh Token
                var refreshToken = _generateRefreshToken.GenerateRefreshToken();

                // Guardar el Refresh Token en la base de datos
                await _addRefreshToken.AddAsync(new RefreshToken
                {
                    Token = refreshToken,
                    UserId = auth.Email, // O el ID del usuario según corresponda
                    UserRole = "Company",
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false
                });
                await _saveChangesRefreshToken.SaveChangesAsync();

                // Configurar la cookie para el Refresh Token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // Solo se enviará por HTTPS
                    SameSite = SameSiteMode.Strict, // Evitar envío en contextos cruzados
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

                return Ok(new { AccessToken = token, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto request)
        {
            //var storedToken = await _getByRefreshToken.GetByTokenAsync(request.RefreshToken);

            //if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiryDate <= DateTime.UtcNow)
            //    return Unauthorized("Invalid or expired refresh token.");

            //var newJwtToken = _generateAccessToken.GenerateToken(new NotiblingBackend.Contracts.DTOs.UserDto { Id = storedToken.UserId, Role = "Company" });
            //var newRefreshToken = _generateRefreshToken.GenerateRefreshToken();

            //storedToken.Token = newRefreshToken;
            //storedToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
            //await _saveChangesRefreshToken.SaveChangesAsync();

            //return Ok(new { AccessToken = newJwtToken, RefreshToken = newRefreshToken });

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("No refresh token found.");

            var storedToken = await _getByRefreshToken.GetByTokenAsync(refreshToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiryDate <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            var newJwtToken = _generateAccessToken.GenerateToken(new NotiblingBackend.Contracts.DTOs.UserDto
            {
                Id = storedToken.UserId,
                Role = "Company"
            });
            var newRefreshToken = _generateRefreshToken.GenerateRefreshToken();

            storedToken.Token = newRefreshToken;
            storedToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
            await _saveChangesRefreshToken.SaveChangesAsync();

            // Actualizar la cookie con el nuevo Refresh Token
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

            return Ok(new { AccessToken = newJwtToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _revokeRefreshToken.RevokeAsync(refreshToken);
                await _saveChangesRefreshToken.SaveChangesAsync();
                Response.Cookies.Delete("refreshToken");
            }

            return Ok("Sesión cerrada.");
        }
    }
}
