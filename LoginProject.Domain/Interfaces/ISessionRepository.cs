using LoginProject.Domain.Entities;

namespace LoginProject.Domain.Interfaces;

public interface ISessionRepository
{
    Task<Session?> GetByTokenAsync(string token);
    Task<Session?> GetByIdAsync(int id);
    Task<IEnumerable<Session>> GetActiveSessionsByUserIdAsync(int userId);
    Task AddAsync(Session session);
    void Update(Session session);
    void Delete(Session session);
    Task SaveChangesAsync();
}
