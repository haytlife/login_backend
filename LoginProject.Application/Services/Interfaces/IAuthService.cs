using LoginProject.Application.DTOs.Auth;

namespace LoginProject.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<bool> ValidateTokenAsync(string token);
    Task LogoutAsync(string token);
    string GenerateJwtToken(Domain.Entities.User user);
}
