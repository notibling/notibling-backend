using NotiblingBackend.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Interfaces.UseCases.User
{
    public interface IAuthentication
    {
        //Task<bool> ValidateLogin(string email, string password);
        //Task<UserDto> ValidateLogin2(string email, string password);
        Task<string> ValidateLogin(string email, string password);
    }
}
