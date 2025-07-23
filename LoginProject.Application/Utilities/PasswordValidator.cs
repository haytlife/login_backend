using System.Text.RegularExpressions;

namespace LoginProject.Application.Utilities;

/// <summary>
/// Şifre güvenliği kontrolleri için yardımcı sınıf
/// Modern güvenlik standartlarına uygun şifre doğrulama işlemlerini gerçekleştirir
/// OWASP şifre güvenliği rehberlerine uygun olarak tasarlanmıştır
/// </summary>
public static class PasswordValidator
{
    /// <summary>
    /// Verilen şifrenin güvenlik kriterlerini karşılayıp karşılamadığını kontrol eder
    /// </summary>
    /// <param name="password">Kontrol edilecek şifre</param>
    /// <returns>Şifre geçerliyse true, değilse false</returns>
    public static bool IsValidPassword(string password)
    {
        // Minimum uzunluk kontrolü
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;

        // Büyük harf kontrolü - En az bir büyük harf olmalı
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        // Küçük harf kontrolü - En az bir küçük harf olmalı
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        // Sayı kontrolü - En az bir rakam olmalı
        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;

        // Özel karakter kontrolü - Güvenliği artırmak için gerekli
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            return false;

        return true;
    }

    /// <summary>
    /// Şifre gereksinimlerini açıklayan kullanıcı dostu bir mesaj döndürür
    /// </summary>
    /// <returns>Şifre kriterlerini açıklayan metin</returns>
    public static string GetPasswordRequirements()
    {
        return "Güvenli bir şifre oluşturmak için: " +
               "En az 8 karakter uzunluğunda olmalı, " +
               "en az bir büyük harf (A-Z), " +
               "en az bir küçük harf (a-z), " +
               "en az bir rakam (0-9) ve " +
               "en az bir özel karakter (!@#$%^&* vb.) içermelidir.";
    }
}
