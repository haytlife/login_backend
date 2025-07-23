namespace LoginProject.Domain.Enums;

/// <summary>
/// Sistem içindeki kullanıcı rollerini tanımlayan enum
/// Her rolün farklı yetkileri ve erişim seviyeleri vardır
/// </summary>
public enum UserRole
{
    User = 1,           // Standart kullanıcı - Temel özelliklere erişim
    Admin = 2           // Sistem yöneticisi - Tüm özelliklere erişim
}

/// <summary>
/// Kullanıcıların cinsiyet bilgilerini tutan enum
/// Kişisel verilerin doğru kategorize edilmesi için kullanılır
/// </summary>
public enum Gender
{
    Male = 1,           // Erkek
    Female = 2,         // Kadın  
    Other = 3           // Diğer - Alternatif cinsiyet kimliği
}

