using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using LoginProject.Application.DTOs.Auth;
using LoginProject.Application.Services.Interfaces;
using LoginProject.Domain.Entities;
using LoginProject.Domain.Enums;
using LoginProject.Domain.Interfaces;

namespace LoginProject.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        ISessionRepository sessionRepository,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        // Kullanıcıyı email ile bul
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !user.IsActive)
        {
            throw new UnauthorizedAccessException("Geçersiz email veya şifre.");
        }

        // Şifre kontrolü
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Geçersiz email veya şifre.");
        }

        // JWT token oluştur
        var token = GenerateJwtToken(user);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

        // Session oluştur ve kaydet
        var session = new Session
        {
            UserId = user.Id,
            Token = token,
            ExpiresAt = expiresAt,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _sessionRepository.AddAsync(session);
        await _sessionRepository.SaveChangesAsync();

        return new AuthResponseDto
        {
            Success = true,
            Message = "Başarıyla giriş yapıldı.",
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserInfoDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role.ToString(),
                PhoneNumber = user.PhoneNumber,
                StudentNumber = user.StudentNumber,
                Department = user.Department,
                University = user.University
            }
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Email kontrolü
        if (await _userRepository.IsEmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("Bu email adresi zaten kullanılıyor.");
        }

        // Öğrenci numarası kontrolü (eğer verilmişse)
        if (!string.IsNullOrEmpty(request.StudentNumber) && 
            await _userRepository.IsStudentNumberExistsAsync(request.StudentNumber))
        {
            throw new InvalidOperationException("Bu öğrenci numarası zaten kullanılıyor.");
        }

        // Şifreyi hash'le
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Kullanıcı oluştur
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PhoneNumber = request.PhoneNumber,
            Role = Enum.Parse<UserRole>(request.Role),
            IsActive = true,
            StudentNumber = request.StudentNumber,
            Department = request.Department,
            University = request.University,
            Grade = request.Grade,
            GPA = request.GPA
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // Login işlemi yap
        return await LoginAsync(new LoginRequestDto
        {
            Email = request.Email,
            Password = request.Password
        });
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var session = await _sessionRepository.GetByTokenAsync(token);
        return session != null && session.IsActive && session.ExpiresAt > DateTime.UtcNow;
    }

    public async Task LogoutAsync(string token)
    {
        var session = await _sessionRepository.GetByTokenAsync(token);
        if (session != null)
        {
            session.IsActive = false;
            _sessionRepository.Update(session);
            await _sessionRepository.SaveChangesAsync();
        }
    }
}
