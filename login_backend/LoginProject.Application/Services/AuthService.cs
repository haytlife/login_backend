using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using LoginProject.Application.DTOs.Auth;
using LoginProject.Application.Services.Interfaces;
using LoginProject.Application.Utilities;
using LoginProject.Domain.Entities;
using LoginProject.Domain.Enums;
using LoginProject.Domain.Interfaces;

namespace LoginProject.Application.Services;

/// <summary>
/// Kimlik doğrulama ve kullanıcı yönetimi servis implementasyonu
/// 
/// Bu servis, modern güvenlik standartlarına uygun olarak tasarlanmıştır:
/// - JWT token tabanlı authentication
/// - BCrypt ile güvenli şifre hashleme
/// - Session yönetimi ile token takibi
/// - Role-based authorization desteği
/// </summary>
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
                
                // Temel bilgiler
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                Age = user.Age,
                City = user.City,
                Country = user.Country
            }
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Şifre güçlülük kontrolü
        if (!PasswordValidator.IsValidPassword(request.Password))
        {
            throw new InvalidOperationException(PasswordValidator.GetPasswordRequirements());
        }

        // Email kontrolü
        if (await _userRepository.IsEmailExistsAsync(request.Email))
        {
            throw new InvalidOperationException("Bu email adresi zaten kullanılıyor.");
        }

        // Şifreyi hash'le
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Kullanıcı oluştur - sadece gerekli alanlarla
        var user = new User
        {
            FirstName = request.Username, // Username'i FirstName olarak kullanıyoruz
            LastName = "", // Boş bırakıyoruz
            Email = request.Email,
            PasswordHash = passwordHash,
            PhoneNumber = request.PhoneNumber,
            Role = UserRole.User, // Varsayılan rol
            IsActive = true,
            
            // Opsiyonel alanları boş bırakıyoruz
            Gender = null,
            BirthDate = null,
            City = null,
            Country = "Türkiye"
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

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new InvalidOperationException("Bu email ile kayıtlı kullanıcı bulunamadı.");

        // Reset token oluştur
        var resetToken = GeneratePasswordResetToken();
        
        // Gerçek uygulamada burada token veritabanına kaydedilir ve e-posta gönderilir
        // Şimdilik sadece token'ı döndürüyoruz
        return resetToken;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        // Şifre güçlülük kontrolü
        if (!PasswordValidator.IsValidPassword(request.NewPassword))
        {
            throw new InvalidOperationException(PasswordValidator.GetPasswordRequirements());
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            throw new InvalidOperationException("Bu email ile kayıtlı kullanıcı bulunamadı.");

        // Gerçek uygulamada token doğrulaması yapılır
        // Şimdilik basit kontrol
        if (string.IsNullOrEmpty(request.Token))
            throw new InvalidOperationException("Geçersiz reset token.");

        // Yeni şifreyi hash'le
        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        
        // Kullanıcının şifresini güncelle
        user.PasswordHash = newPasswordHash;
        user.UpdatedAt = DateTime.UtcNow;
        
        // Değişiklikleri kaydet
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        return true;
    }

    public string GeneratePasswordResetToken()
    {
        return Guid.NewGuid().ToString("N")[..16]; // 16 karakterlik token
    }
}
