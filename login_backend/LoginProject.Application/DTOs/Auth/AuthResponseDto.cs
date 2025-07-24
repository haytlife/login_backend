using LoginProject.Domain.Enums;

namespace LoginProject.Application.DTOs.Auth;

public class AuthResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserInfoDto? User { get; set; }
}

public class UserInfoDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    
    // Temel bilgiler
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? Age { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    
    public string FullName => $"{FirstName} {LastName}";
}
