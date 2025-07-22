using LoginProject.Domain.Entities.Base;
using LoginProject.Domain.Enums;

namespace LoginProject.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Öğrenci için ek bilgiler
    public string? StudentNumber { get; set; }
    public string? Department { get; set; }
    public string? University { get; set; }
    public int? Grade { get; set; } // Kaçıncı sınıf
    public decimal? GPA { get; set; } // Not ortalaması (4.0 üzerinden)
    
    // Şirket temsilcisi için ek bilgiler
    public string? CompanyName { get; set; }
    public string? Position { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    
    // Calculated Properties
    public string FullName => $"{FirstName} {LastName}";
}
