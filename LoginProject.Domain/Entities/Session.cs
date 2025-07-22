using LoginProject.Domain.Entities.Base;

namespace LoginProject.Domain.Entities;

public class Session : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    
    // Navigation Properties
    public virtual User User { get; set; } = null!;
}
