using NotiblingBackend.Application.Interfaces.UseCases.User;
using NotiblingBackend.Domain.Interfaces.Repositories;
using NotiblingBackend.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.User
{
    public class ValidateLogin : IValidateLogin
    {
        #region Dependency Injection
        IUserRepository _userRepository;

        public ValidateLogin(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        #endregion
        Task<bool> IValidateLogin.ValidateLogin(string email, string password)
        {
            return _userRepository.ValidateLogin(email, password);
        }
    }
}
