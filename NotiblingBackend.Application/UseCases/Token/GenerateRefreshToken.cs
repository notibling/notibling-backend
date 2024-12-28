using NotiblingBackend.Application.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.Token
{
    public class GenerateRefreshToken : IGenerateRefreshToken
    {
        string IGenerateRefreshToken.GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
