# LoginProject - Authentication API

Modern bir .NET 8 tabanlı kimlik doğrulama (authentication) sistemi. Bu proje, Clean Architecture prensiplerine uygun olarak geliştirilmiş bir JWT token tabanlı kullanıcı giriş sistemidir.

## 🏗️ Proje Yapısı

```
LoginProject/
├── LoginProject.API/              # Presentation Layer
│   ├── Controllers/
│   │   └── AuthController.cs      # Authentication endpoints
│   ├── Program.cs                 # Application entry point
│   └── appsettings.json          # Configuration
├── LoginProject.Application/      # Business Logic Layer
│   ├── DTOs/Auth/                # Data Transfer Objects
│   ├── Services/                 # Business services
│   └── Interfaces/              # Service contracts
├── LoginProject.Domain/          # Core Business Layer
│   ├── Entities/                # Domain entities
│   ├── Enums/                   # Domain enumerations
│   └── Interfaces/              # Repository contracts
└── LoginProject.Infrastructure/  # Data Access Layer
    ├── Data/                    # Database context
    └── Repositories/            # Data access implementations
```

## 🚀 Özellikler

- ✅ **JWT Authentication** - Token tabanlı kimlik doğrulama
- ✅ **User Registration** - Yeni kullanıcı kaydı
- ✅ **User Login** - Kullanıcı girişi
- ✅ **Password Hashing** - BCrypt ile güvenli şifre saklama
- ✅ **Clean Architecture** - Katmanlı mimari
- ✅ **Repository Pattern** - Veri erişim katmanı soyutlaması
- ✅ **Dependency Injection** - Bağımlılık enjeksiyonu
- ✅ **Swagger Documentation** - API dokümantasyonu
- ✅ **Entity Framework Core** - ORM ve veritabanı yönetimi

## 🛠️ Teknolojiler

- **Framework:** .NET 8.0
- **Database:** SQLite (geliştirme), SQL Server (üretim)
- **ORM:** Entity Framework Core 8.0
- **Authentication:** JWT (JSON Web Tokens)
- **Password Hashing:** BCrypt
- **API Documentation:** Swagger/OpenAPI

## 📋 Gereksinimler

- .NET 8 SDK
- Visual Studio 2022 / VS Code
- SQL Server Express veya SQLite

## ⚡ Kurulum

1. **Repository'yi klonlayın:**
```bash
git clone [repository-url]
cd LoginProject
```

2. **NuGet paketlerini yükleyin:**
```bash
dotnet restore
```

3. **Projeyi derleyin:**
```bash
dotnet build
```

4. **Veritabanı migration'larını çalıştırın:**
```bash
cd LoginProject.API
dotnet ef database update
```

5. **Uygulamayı çalıştırın:**
```bash
dotnet run
```

## 🔗 API Endpoints

### Authentication Endpoints

| Method | Endpoint | Açıklama | Auth Required |
|--------|----------|----------|---------------|
| POST | `/api/auth/register` | Yeni kullanıcı kaydı | ❌ |
| POST | `/api/auth/login` | Kullanıcı girişi | ❌ |
| POST | `/api/auth/logout` | Kullanıcı çıkışı | ✅ |
| GET | `/api/auth/validate` | Token doğrulama | ✅ |
| GET | `/api/auth/profile` | Kullanıcı profili | ✅ |

### Swagger Documentation
Uygulama çalıştırıldıktan sonra API dokümantasyonu için: `https://localhost:5001/swagger`

## 📝 Kullanım Örnekleri

### Kullanıcı Kaydı
```json
POST /api/auth/register
{
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "email": "ahmet@example.com",
  "password": "123456",
  "confirmPassword": "123456",
  "phoneNumber": "555-1234567",
  "role": "Student"
}
```

### Kullanıcı Girişi
```json
POST /api/auth/login
{
  "email": "ahmet@example.com",
  "password": "123456"
}
```

### Response Örneği
```json
{
  "success": true,
  "message": "Başarıyla giriş yapıldı.",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-07-22T12:00:00Z",
  "user": {
    "id": 1,
    "firstName": "Ahmet",
    "lastName": "Yılmaz",
    "email": "ahmet@example.com",
    "role": "Student"
  }
}
```

## 🗄️ Veritabanı

### User Entity
- Id, FirstName, LastName, Email
- PasswordHash, PhoneNumber, Role
- StudentNumber, Department, University, Grade, GPA
- CreatedAt, UpdatedAt, IsActive

### Session Entity  
- Id, UserId, Token, ExpiresAt
- IsActive, CreatedAt

## 🔒 Güvenlik

- **JWT Tokens:** Stateless authentication
- **BCrypt Hashing:** Güvenli şifre saklama
- **Input Validation:** Data Annotations ile doğrulama
- **CORS Configuration:** Cross-origin istekler için yapılandırma

## 🚀 Geliştirme

### Migration Ekleme
```bash
dotnet ef migrations add [MigrationName] --project LoginProject.Infrastructure --startup-project LoginProject.API
```

### Veritabanını Güncelleme
```bash
dotnet ef database update --project LoginProject.Infrastructure --startup-project LoginProject.API
```

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 👥 Katkıda Bulunanlar

- Staj Öğrencileri - Backend Development Team

## 📞 İletişim

Sorularınız için [issue](../../issues) açabilirsiniz.
