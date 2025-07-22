using Microsoft.EntityFrameworkCore;
using LoginProject.Domain.Entities;
using LoginProject.Domain.Interfaces;
using LoginProject.Infrastructure.Data;

namespace LoginProject.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByTokenAsync(string token)
    {
        return await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Token == token);
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Session>> GetActiveSessionsByUserIdAsync(int userId)
    {
        return await _context.Sessions
            .Where(s => s.UserId == userId && s.IsActive && s.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task AddAsync(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }

    public void Update(Session session)
    {
        _context.Sessions.Update(session);
    }

    public void Delete(Session session)
    {
        _context.Sessions.Remove(session);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
