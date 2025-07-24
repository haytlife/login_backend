using Microsoft.EntityFrameworkCore;
using LoginProject.Domain.Entities;
using LoginProject.Domain.Enums;
using LoginProject.Domain.Interfaces;
using LoginProject.Infrastructure.Data;

namespace LoginProject.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        // Kullanıcı adı olarak email kullanıyoruz
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == username);
    }

    public async Task<bool> ExistsAsync(string email, string username)
    {
        // Kullanıcı adı olarak email kullanıyoruz
        return await _dbSet.AnyAsync(u => u.Email == email || u.Email == username);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        return await _dbSet.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<List<User>> GetByRoleAsync(UserRole role)
    {
        return await _dbSet.Where(u => u.Role == role).ToListAsync();
    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Sessions)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
