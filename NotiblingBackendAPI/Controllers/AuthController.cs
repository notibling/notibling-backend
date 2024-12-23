using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotiblingBackend.Application.Dtos.User;
using NotiblingBackend.Application.Interfaces.UseCases.User;
using System.Security.Claims;
using System.Text;

namespace NotiblingBackendAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IValidateLogin _validateLogin;

        public AuthController(IValidateLogin validateLogin)
        {
            _validateLogin = validateLogin;
        }

        /// <summary>
        /// Login de usuario
        /// </summary>
        /// <param name="usuario">Datos para ingresar al sistema</param>
        /// <returns>Retorna el login del usario con su token</returns>
        [HttpPost("login-user")]
        //[AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserDto login)
        {

            try
            {
                if (!await _validateLogin.ValidateLogin(login.Email, login.Password))
                    return BadRequest("Error en el login.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
