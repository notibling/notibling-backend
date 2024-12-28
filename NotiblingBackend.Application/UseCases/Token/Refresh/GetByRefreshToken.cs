using NotiblingBackend.Application.Interfaces.Token.Refresh;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.Token.Refresh
{
    public class GetByRefreshToken : IGetByRefreshToken
    {
        #region Dependency Injection
        IRefreshTokenRepository _refreshTokenRepository;

        public GetByRefreshToken(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion
        public Task<RefreshToken> GetByTokenAsync(string token)
        {
            return _refreshTokenRepository.GetByTokenAsync(token);
        }
    }
}
