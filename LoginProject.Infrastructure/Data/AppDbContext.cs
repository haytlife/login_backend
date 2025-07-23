using Microsoft.EntityFrameworkCore;
using LoginProject.Domain.Entities;

namespace LoginProject.Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext sınıfı
/// Veritabanı bağlantısını yönetir ve entity yapılandırmalarını içerir
/// Clean Architecture'da Infrastructure katmanının kalbi olan sınıftır
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Veritabanı tabloları - Her bir DbSet bir SQL tablosunu temsil eder
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }

    /// <summary>
    /// Entity yapılandırmalarını ve veritabanı şemasını tanımlar
    /// Fluent API kullanarak tablo yapılarını, ilişkileri ve kısıtlamaları belirler
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Kullanıcı tablosu yapılandırması
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique(); // Email benzersiz olmalı
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Oturum tablosu yapılandırması
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired();
            entity.HasIndex(e => e.Token).IsUnique(); // Token benzersiz olmalı
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            // Kullanıcı-Oturum ilişkisi: Bir kullanıcının birden çok oturumu olabilir
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinince oturumları da silinir
        });
    }
}
