using Microsoft.EntityFrameworkCore;
using NotiblingBackend.Domain.Entities;
using NotiblingBackend.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.DataAccess.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        readonly NBContext _dbContext;

        public RefreshTokenRepository(NBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            try
            {
                return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task RevokeAsync(string token)
        {
            var refreshToken = await GetByTokenAsync(token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                _dbContext.RefreshTokens.Update(refreshToken);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
