using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LoginProject.Domain.Enums;

namespace LoginProject.Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır.")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    [JsonPropertyName("confirmPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Rol seçimi zorunludur.")]
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    // Temel bilgiler (opsiyonel)
    [JsonPropertyName("gender")]
    public Gender? Gender { get; set; }
    
    [JsonPropertyName("birthDate")]
    public DateTime? BirthDate { get; set; }
    
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    [JsonPropertyName("country")]
    public string? Country { get; set; } = "Türkiye";
}
