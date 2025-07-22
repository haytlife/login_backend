using System.ComponentModel.DataAnnotations;

namespace LoginProject.Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Rol seçimi zorunludur.")]
    public string Role { get; set; } = string.Empty;

    // Öğrenci bilgileri (opsiyonel)
    public string? StudentNumber { get; set; }
    public string? Department { get; set; }
    public string? University { get; set; }
    public int? Grade { get; set; }
    public decimal? GPA { get; set; }
}
