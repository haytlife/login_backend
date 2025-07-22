# LoginProject - .NET 8 Authentication API
# Project Dependencies and Requirements

## Framework
- .NET 8.0

## Main Packages

### API Layer (LoginProject.API)
- Microsoft.AspNetCore.Authentication.JwtBearer v8.0.10
- Microsoft.EntityFrameworkCore.Design v8.0.10
- Microsoft.EntityFrameworkCore.Sqlite v8.0.10
- Swashbuckle.AspNetCore v6.8.1

### Application Layer (LoginProject.Application)
- BCrypt.Net-Next v4.0.3
- Microsoft.Extensions.Options v8.0.0
- System.IdentityModel.Tokens.Jwt v8.2.1
- Microsoft.IdentityModel.Tokens v8.2.1
- System.ComponentModel.Annotations v5.0.0

### Infrastructure Layer (LoginProject.Infrastructure)
- Microsoft.EntityFrameworkCore v8.0.10
- Microsoft.EntityFrameworkCore.Sqlite v8.0.10
- Microsoft.EntityFrameworkCore.SqlServer v8.0.10
- Microsoft.EntityFrameworkCore.Tools v8.0.10

### Domain Layer (LoginProject.Domain)
- No external dependencies (Clean Architecture principle)

## Development Tools
- Entity Framework Core Tools (for migrations)
- Swagger/OpenAPI (for API documentation)

## Database
- SQLite (for development)
- SQL Server (production ready)

## Features
- JWT Authentication
- User Registration & Login
- Password Hashing (BCrypt)
- Clean Architecture
- Repository Pattern
- Dependency Injection

## Security
- JWT Token-based authentication
- BCrypt password hashing
- Input validation with Data Annotations

## Installation
```bash
# Clone the repository
git clone [repository-url]
cd LoginProject

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run database migrations
cd LoginProject.API
dotnet ef database update

# Run the application
dotnet run
```

## API Endpoints
- POST /api/auth/register - User registration
- POST /api/auth/login - User login
- POST /api/auth/logout - User logout
- GET /api/auth/validate - Token validation
- GET /api/auth/profile - Get user profile

## Development Environment
- Visual Studio 2022 / VS Code
- .NET 8 SDK
- SQL Server Express / SQLite

## Testing
- Swagger UI available at /swagger
- Postman collection recommended for API testing
