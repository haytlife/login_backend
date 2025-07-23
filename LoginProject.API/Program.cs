/*
 * ═══════════════════════════════════════════════════════════════
 *                    LoginProject API
 * ═══════════════════════════════════════════════════════════════
 * 
 * Modern .NET 8 Kimlik Doğrulama Sistemi
 * Clean Architecture & JWT Authentication
 * 
 * Özellikler:
 * ✓ JWT Token Authentication
 * ✓ BCrypt Şifre Hashleme
 * ✓ Role-Based Authorization
 * ✓ Session Management
 * ✓ Password Reset System
 * ✓ Swagger/OpenAPI Documentation
 * 
 * ═══════════════════════════════════════════════════════════════
 */

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LoginProject.Application.DTOs.Auth;
using LoginProject.Application.Services;
using LoginProject.Application.Services.Interfaces;
using LoginProject.Domain.Interfaces;
using LoginProject.Infrastructure.Data;
using LoginProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Servisleri konteynıra ekle
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = " LoginProject API - Modern Authentication System",
        Version = "v1.0.0",
        Description = @"
**Modern .NET 8 Kimlik Doğrulama Sistemi**

Bu API, güvenli ve ölçeklenebilir kimlik doğrulama hizmetleri sunar.

** Özellikler:**
- JWT Token tabanlı authentication
- BCrypt ile güvenli şifre hashleme  
- Role-based authorization (User/Admin)
- Session management
- Password reset system

** Kullanım:**
1. Önce `/auth/register` ile kayıt olun
2. `/auth/login` ile giriş yapıp token alın
3. Token'ı 'Bearer {token}' formatında Authorization header'a ekleyin

** Test Kullanıcısı:**
- Email: admin@test.com
- Password: Test123!
- Role: Admin"
    });

    // JWT Bearer Token Authentication için Swagger yapılandırması
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// JWT Yapılandırması
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Veritabanı Yapılandırması
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("LoginProject.Infrastructure")));

// Repository Kayıtları
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

// Servis Kayıtları
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Kimlik Doğrulama
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings?.Issuer,
            ValidAudience = jwtSettings?.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? ""))
        };
    });

// CORS Yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Uygulama başlatıldığında otomatik veritabanı migration
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Veritabanı ve tablolar yoksa oluşturur
}

// HTTP request pipeline yapılandırması
// Hem Development hem de Production için Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
