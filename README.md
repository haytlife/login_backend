# LoginProject - Authentication System

Modern ve güvenli kullanıcı kimlik doğrulama sistemi. .NET 8 Web API backend ve React frontend ile geliştirilmiştir.

## Özellikler

- Kullanıcı Kayıt & Giriş: Email/şifre ile güvenli authentication
- JWT Token Sistemi: Bearer token ile session yönetimi
- Profil Yönetimi: Kullanıcı bilgileri güncelleme
- Şifre Sıfırlama: Email ile şifre yenileme
- Modern UI: React + TailwindCSS ile responsive tasarım
- Güvenlik: BCrypt şifreleme, JWT doğrulama
- Veritabanı: SQLite ile yerel depolama

## Teknolojiler

### Backend (.NET 8)
- **Framework**: ASP.NET Core Web API
- **Authentication**: JWT Bearer Token
- **Database**: Entity Framework Core + SQLite
- **Security**: BCrypt.Net password hashing
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API)

### Frontend (React)
- **Framework**: React 19 + Vite
- **Styling**: TailwindCSS 4.0
- **Routing**: React Router DOM
- **State Management**: React Context API

## Kurulum

### Gereksinimler
- .NET 8.0 SDK veya üzeri
- Node.js 18+ ve npm
- Git

### 1. Projeyi Klonlayın
```bash
git clone <repository-url>
cd LoginProject
```

### 2. Backend Kurulumu
```bash
# Backend klasörüne gidin
cd login_backend/LoginProject.API

# NuGet paketlerini yükleyin
dotnet restore

# Veritabanını oluşturun
dotnet ef database update

# API'yi başlatın
dotnet run
```
Backend http://localhost:5000 adresinde çalışacaktır.

### 3. Frontend Kurulumu
```bash
# Yeni terminal açın ve frontend klasörüne gidin
cd vite-project

# NPM paketlerini yükleyin
npm install

# Development server'ı başlatın
npm run dev
```
Frontend http://localhost:5173 adresinde çalışacaktır.

## Kullanım

1. **Kayıt Ol**: http://localhost:5173 adresine gidip yeni hesap oluşturun
2. **Giriş Yap**: Email ve şifrenizle giriş yapın
3. **Dashboard**: Profil bilgilerinizi görüntüleyin ve güncelleyin
4. **Çıkış**: Güvenli şekilde oturumu sonlandırın

## API Endpoints

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/Auth/register` | Yeni kullanıcı kaydı |
| POST | `/api/Auth/login` | Kullanıcı girişi |
| GET | `/api/Auth/profile` | Profil bilgileri |
| PUT | `/api/Auth/profile` | Profil güncelleme |
| POST | `/api/Auth/validate-token` | Token doğrulama |
| POST | `/api/Auth/logout` | Çıkış yapma |
| POST | `/api/Auth/reset-password` | Şifre sıfırlama |

## Proje Yapısı

```
LoginProject/
├── login_backend/              # Backend (.NET 8)
│   ├── LoginProject.API/       # Web API katmanı
│   ├── LoginProject.Application/ # İş mantığı katmanı
│   ├── LoginProject.Domain/    # Domain modelleri
│   └── LoginProject.Infrastructure/ # Veri erişimi
├── vite-project/              # Frontend (React)
│   ├── src/
│   │   ├── components/        # React bileşenleri
│   │   ├── context/          # Context API
│   │   ├── services/         # API servisleri
│   │   └── utils/           # Yardımcı fonksiyonlar
│   └── public/              # Statik dosyalar
└── README.md
```

## Geliştirme

### Backend Development
```bash
cd login_backend/LoginProject.API
dotnet watch run  # Hot reload ile geliştirme
```

### Frontend Development
```bash
cd vite-project
npm run dev  # Hot reload ile geliştirme
```

### Production Build
```bash
# Backend
cd login_backend/LoginProject.API
dotnet publish -c Release

# Frontend
cd vite-project
npm run build
```

## Güvenlik

- Şifreler BCrypt ile hash'lenir
- JWT token'lar güvenli şekilde imzalanır
- CORS politikaları uygulanır
- Input validation ve sanitization

## Sorun Giderme

### Backend Çalışmıyor
1. .NET 8 SDK'nın yüklü olduğundan emin olun
2. `dotnet restore` ile paketleri güncelleyin
3. Port 5000'in boş olduğundan emin olun

### Frontend Çalışmıyor
1. Node.js 18+ yüklü olduğundan emin olun
2. `npm install` ile paketleri güncelleyin
3. Port 5173'ün boş olduğundan emin olun

### Veritabanı Sorunları
```bash
cd login_backend/LoginProject.API
dotnet ef database drop --force
dotnet ef database update
```

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Commit yapın (`git commit -m 'feat: add amazing feature'`)
4. Branch'i push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## İletişim

Sorularınız için issue açabilir veya pull request gönderebilirsiniz.
