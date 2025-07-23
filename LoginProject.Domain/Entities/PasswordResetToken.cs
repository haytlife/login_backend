using LoginProject.Domain.Entities.Base;

namespace LoginProject.Domain.Entities;

public class PasswordResetToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
    public string Email { get; set; } = string.Empty;
    
    // Navigasyon özelliği
    public User User { get; set; } = null!;
}
