using NotiblingBackend.Contracts.DTOs;
using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Mapping
{
    public class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto()
            {
                Id = user.Id.ToString(),
                Role = user.UserRole.ToString()
            };
        }
    }
}
