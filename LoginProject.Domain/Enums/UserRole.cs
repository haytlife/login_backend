namespace LoginProject.Domain.Enums;

public enum UserRole
{
    Student = 1,      // Öğrenci
    Supervisor = 2,   // Staj sorumlusu/danışmanı
    Company = 3,      // Şirket temsilcisi
    Admin = 4         // Sistem yöneticisi
}

public enum InternshipType
{
    Summer = 1,       // Yaz stajı
    Winter = 2,       // Kış stajı
    Project = 3       // Proje bazlı staj
}

public enum InternshipStatus
{
    Applied = 1,      // Başvuru yapıldı
    Accepted = 2,     // Kabul edildi
    Rejected = 3,     // Reddedildi
    InProgress = 4,   // Devam ediyor
    Completed = 5,    // Tamamlandı
    Cancelled = 6     // İptal edildi
}

public enum Department
{
    ComputerEngineering = 1,     // Bilgisayar Mühendisliği
    SoftwareEngineering = 2,     // Yazılım Mühendisliği
    ElectricalEngineering = 3,   // Elektrik Mühendisliği
    MechanicalEngineering = 4,   // Makine Mühendisliği
    IndustrialEngineering = 5,   // Endüstri Mühendisliği
    BusinessAdministration = 6,  // İşletme
    Economics = 7,               // İktisat
    Psychology = 8,              // Psikoloji
    GraphicDesign = 9,          // Grafik Tasarım
    Marketing = 10              // Pazarlama
}

public enum WorkModel
{
    OnSite = 1,          // Ofiste çalışma
    Remote = 2,          // Uzaktan çalışma
    Hybrid = 3           // Hibrit çalışma
}

public enum ApplicationStatus
{
    Open = 1,            // Başvuru açık
    Closed = 2,          // Başvuru kapalı
    Suspended = 3        // Başvuru durduruldu
}

