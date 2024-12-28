using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.Interfaces.Token.Refresh
{
    public interface IRevokeRefreshToken
    {
        Task RevokeAsync(string token);
    }
}
