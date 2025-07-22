using LoginProject.Domain.Entities;
using LoginProject.Domain.Enums;

namespace LoginProject.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsAsync(string email, string username);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsStudentNumberExistsAsync(string studentNumber);
        Task<List<User>> GetByRoleAsync(UserRole role);
        Task<User?> GetUserWithDetailsAsync(int id);
    }
}
