using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Entities;
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

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> ValidateLogin(string email, string password)
        {
            try
            {
                var user = await _dbContext.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync() ?? throw new CompanyException("Usuario no encontrado.");

                bool isPasswordValid = PasswordEncryptor.VerifyPassword(password, user.Password);
                if (!isPasswordValid)
                {
                    throw new CompanyException("Los datos ingresados son incorrectos.");
                }

                return user; 

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
