using LoginProject.Domain.Entities.Base;

namespace LoginProject.Domain.Entities;

/// <summary>
/// Kullanıcı oturumlarını yöneten varlık sınıfı
/// JWT token'ları ve oturum bilgilerini veritabanında takip eder
/// Güvenlik açısından token'ların geçerlilik süresini ve durumunu kontrol eder
/// </summary>
public class Session : BaseEntity
{
    // Oturum Kimlik Bilgileri
    public int UserId { get; set; }                    // Oturumu açan kullanıcının ID'si
    public string Token { get; set; } = string.Empty; // JWT token değeri
    
    // Oturum Kontrol Bilgileri
    public DateTime ExpiresAt { get; set; }   // Token'ın son kullanma tarihi
    public bool IsActive { get; set; } = true; // Oturumun aktif olup olmadığı
    
    // İzleme ve Güvenlik Bilgileri
    public string? IpAddress { get; set; }  // Oturumun başlatıldığı IP adresi
    public string? UserAgent { get; set; }  // Kullanılan tarayıcı/uygulama bilgisi
    
    // İlişkisel Veri - Oturumun sahibi olan kullanıcı
    public virtual User User { get; set; } = null!;
}
