using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Interfaces.Token
{
    public interface IGenerateRefreshToken
    {
        string GenerateRefreshToken();
    }
}
