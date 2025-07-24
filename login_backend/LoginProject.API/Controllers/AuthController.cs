using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginProject.Application.DTOs.Auth;
using LoginProject.Application.Services.Interfaces;

namespace LoginProject.API.Controllers
{
    /// <summary>
    /// Kimlik doğrulama (Authentication) API Controller
    /// 
    /// Bu controller, kullanıcı kayıt, giriş, çıkış ve şifre işlemlerini yönetir.
    /// RESTful API prensiplerine uygun olarak tasarlanmış olup, 
    /// Swagger/OpenAPI dokümantasyonu ile tam entegre çalışmaktadır.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            try
            {
                var resetToken = await _authService.ForgotPasswordAsync(request);
                // Gerçek uygulamada: E-posta gönderimi yapılır. Şimdilik reset token'ı response olarak döndürüyoruz.
                return Ok(new { 
                    message = "Şifre sıfırlama token'ı oluşturuldu. E-posta adresinizi kontrol edin.", 
                    resetToken // Gerçek uygulamada bu döndürülmez
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            try
            {
                var success = await _authService.ResetPasswordAsync(request);
                if (success)
                {
                    return Ok(new { message = "Şifreniz başarıyla güncellendi." });
                }
                return BadRequest(new { message = "Şifre güncelleme işlemi başarısız oldu." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(new {
                    success = true,
                    message = "Başarıyla kayıt olundu.",
                    token = response.Token,
                    expiresAt = response.ExpiresAt,
                    user = response.User
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Hata detayını loglayalım
                Console.WriteLine($"[REGISTER ERROR] {ex.ToString()}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _authService.LogoutAsync(token);
                return Ok(new { message = "Başarıyla çıkış yapıldı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var isValid = await _authService.ValidateTokenAsync(token);
                if (isValid)
                {
                    return Ok(new { message = "Token geçerli.", valid = true });
                }
                return Unauthorized(new { message = "Token geçersiz.", valid = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                email = User.FindFirst(ClaimTypes.Email)?.Value,
                name = User.FindFirst(ClaimTypes.Name)?.Value,
                role = User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}
