using NotiblingBackend.Application.Interfaces.Token.Refresh;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.Token.Refresh
{
    public class RevokeRefreshToken : IRevokeRefreshToken
    {
        #region Dependency Injection
        IRefreshTokenRepository _refreshTokenRepository;

        public RevokeRefreshToken(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion
        public Task RevokeAsync(string token)
        {
            return _refreshTokenRepository.RevokeAsync(token);
        }
    }
}
