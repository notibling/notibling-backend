using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Interfaces.UseCases.User
{
    public interface IValidateLogin
    {
        Task<bool> ValidateLogin(string email, string password);
    }
}
