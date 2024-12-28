using NotiblingBackend.Application.Interfaces.Token;
using NotiblingBackend.Application.Interfaces.Token.Refresh;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Application.UseCases.Token.Refresh
{
    public class AddRefreshToken : IAddRefreshToken
    {
        #region Dependency Injection
        IRefreshTokenRepository _refreshTokenRepository;

        public AddRefreshToken(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion

        public Task AddAsync(RefreshToken refreshToken)
        {
            return _refreshTokenRepository.AddAsync(refreshToken);
        }
    }
}
