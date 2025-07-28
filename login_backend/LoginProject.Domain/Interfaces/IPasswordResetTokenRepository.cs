using LoginProject.Domain.Entities;

namespace LoginProject.Domain.Interfaces
{
    public interface IPasswordResetTokenRepository : IRepository<PasswordResetToken>
    {
        Task<PasswordResetToken?> GetValidTokenAsync(string email, string token);
        Task DeleteAllTokensByEmailAsync(string email);
        Task InvalidateTokenAsync(PasswordResetToken tokenEntity);
    }
}
