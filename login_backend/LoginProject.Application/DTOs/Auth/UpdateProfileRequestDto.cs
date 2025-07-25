using System.ComponentModel.DataAnnotations;
using LoginProject.Domain.Enums;

namespace LoginProject.Application.DTOs.Auth;

public class UpdateProfileRequestDto
{
    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [MinLength(2, ErrorMessage = "Ad en az 2 karakter olmalıdır.")]
    [MaxLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
    public string LastName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string? PhoneNumber { get; set; }

    [MaxLength(50, ErrorMessage = "Şehir en fazla 50 karakter olabilir.")]
    public string? City { get; set; }

    [MaxLength(50, ErrorMessage = "Ülke en fazla 50 karakter olabilir.")]
    public string? Country { get; set; }

    public Gender? Gender { get; set; }

    public DateTime? BirthDate { get; set; }
}
