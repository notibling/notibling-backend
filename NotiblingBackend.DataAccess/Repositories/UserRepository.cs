using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Utilities.Exceptions;
using NotiblingBackend.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly NBContext _dbContext;

        public UserRepository(NBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ValidateLogin(string email, string password)
        {
            try
            {
                var user = await _dbContext.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync();

                if (user == null)
                    throw new CompanyException("Usuario no encontrado.");

                bool isPasswordValid = PasswordEncryptor.VerifyPassword(password, user.Password);
                if (!isPasswordValid)
                {
                    throw new Exception("Contraseña incorrecta.");
                }

                return true; 

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

        public async Task<bool> VerifyUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new CompanyException("El nombre de usuario esta vacio");

            try
            {
                return await _dbContext.Users
                    .AnyAsync(u => u.UserName == userName.Trim());

            }
            catch (DbUpdateException ex)
            {
                throw new CompanyException("Error al verificar el usuario.", ex);
            }
            catch (CompanyException ex)
            {
                throw new CompanyException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
