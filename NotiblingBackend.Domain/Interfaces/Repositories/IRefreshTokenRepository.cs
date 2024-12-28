using NotiblingBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task RevokeAsync(string token);
        Task SaveChangesAsync();
    }
}
