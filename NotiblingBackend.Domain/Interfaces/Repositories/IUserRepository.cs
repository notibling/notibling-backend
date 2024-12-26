using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> VerifyUsername(string userName);

        Task<User> ValidateLogin(string email, string password);

        Task<User> GetByEmail(string email);
    }
}
