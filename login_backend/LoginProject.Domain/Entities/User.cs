using LoginProject.Domain.Entities.Base;
using LoginProject.Domain.Enums;

namespace LoginProject.Domain.Entities;

/// <summary>
/// Sistem kullanıcılarını temsil eden ana varlık sınıfı
/// Bu sınıf, kullanıcının temel bilgilerini ve authentication verilerini tutar
/// </summary>
public class User : BaseEntity
{
    // Kimlik Bilgileri - Kullanıcının temel tanımlayıcı bilgileri
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    // Güvenlik Bilgileri - Authentication için gerekli veriler
    public string PasswordHash { get; set; } = string.Empty;  // BCrypt ile hashlenmiş şifre
    public UserRole Role { get; set; }                        // Kullanıcının sistem içindeki rolü
    public bool IsActive { get; set; } = true;               // Hesap aktiflik durumu
    
    // İletişim Bilgileri
    public string? PhoneNumber { get; set; }
    
    // Kişisel Bilgiler - Kullanıcının demografik ve lokasyon verileri
    public Gender? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; } = "Türkiye";
    
    // İlişkisel Veriler - Diğer varlıklarla olan bağlantılar
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    
    // Hesaplanmış Özellikler - Mevcut verilerden türetilen bilgiler
    public string FullName => $"{FirstName} {LastName}";
    public int? Age => BirthDate.HasValue ? (int)((DateTime.Now - BirthDate.Value).TotalDays / 365.25) : null;
}
