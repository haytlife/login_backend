using System.ComponentModel.DataAnnotations;

namespace LoginProject.Application.DTOs.Auth;

public class ForgotPasswordRequestDto
{
    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    public string Email { get; set; } = string.Empty;
}
