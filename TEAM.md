# Takım Çalışma Rehberi

## Proje Yapısı

### Backend (Tamamlandı)
- **LoginProject.API** - REST API endpoints
- **LoginProject.Application** - Business logic
- **LoginProject.Domain** - Core entities
- **LoginProject.Infrastructure** - Data access

## API Entegrasyon Bilgileri

### Base URL
```
Development: http://localhost:5000
Production: [To be configured]
```

### Authentication Endpoints
```
POST /api/auth/register    - Kullanıcı kaydı
POST /api/auth/login       - Kullanıcı girişi  
POST /api/auth/logout      - Çıkış
GET  /api/auth/validate    - Token doğrulama
GET  /api/auth/profile     - Kullanıcı profili
POST /api/auth/forgot-password - Şifre sıfırlama
POST /api/auth/reset-password  - Şifre güncelleme
```

### JWT Token Kullanımı
```javascript
// Header'a eklenmesi gereken format
Authorization: Bearer {token}
```

## Frontend Takımı için Gerekli Bilgiler

### User Registration Payload
```json
{
  "firstName": "string",
  "lastName": "string", 
  "email": "string",
  "password": "string",
  "confirmPassword": "string",
  "phoneNumber": "string",
  "role": "User", // veya "Admin"
  "gender": 1, // 1=Male, 2=Female, 3=Other
  "birthDate": "2000-01-01T00:00:00Z",
  "city": "string",
  "country": "Türkiye"
}
```

### Login Payload
```json
{
  "email": "string",
  "password": "string"
}
```

### Successful Response Example
```json
{
  "success": true,
  "message": "Başarıyla giriş yapıldı.",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-07-23T13:00:00Z",
  "user": {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "role": "User",
    "phoneNumber": "+905551234567"
  }
}
```

## Development Setup

### Backend Çalıştırma
```bash
cd LoginProject.API
dotnet run
# API: http://localhost:5000
# Swagger: http://localhost:5000/swagger
```

### Frontend Entegrasyon Örneği
```javascript
// Axios örneği
const API_BASE = 'http://localhost:5000/api';

// Login request
const login = async (email, password) => {
  const response = await axios.post(`${API_BASE}/auth/login`, {
    email,
    password
  });
  
  // Token'ı local storage'a kaydet
  localStorage.setItem('token', response.data.token);
  return response.data;
};

// Authenticated request
const getProfile = async () => {
  const token = localStorage.getItem('token');
  const response = await axios.get(`${API_BASE}/auth/profile`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return response.data;
};
```

## API Test Örnekleri

### Register
```
POST http://localhost:5000/api/auth/register
Content-Type: application/json

{
  "firstName": "Test",
  "lastName": "User",
  "email": "test@example.com",
  "password": "Test123!",
  "confirmPassword": "Test123!",
  "role": "User"
}
```

### Login
```
POST http://localhost:5000/api/auth/login
Content-Type: application/json

{
  "email": "test@example.com",
  "password": "Test123!"
}
```

## Backend Destek

Backend ile ilgili sorularınız olduğunda:
1. Bu dosyayı kontrol edin
2. Swagger UI'ı kullanın: `http://localhost:5000/swagger`
3. Kod yorumlarına bakın

---

**Backend hazır ve çalışır durumda.**
