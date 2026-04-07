using System.Security.Cryptography;
using System.Text;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Services;
using Xunit;

namespace PersonelTakipSistemi.Tests;

public class PasswordServiceTests
{
    private readonly PasswordService _service = new();

    [Fact]
    public void HashPassword_CreatesPasswordThatVerifiesWithoutUpgrade()
    {
        var personel = new Personel();
        _service.SetPassword(personel, "Secret123!");

        var result = _service.VerifyPassword("Secret123!", personel);

        Assert.True(result.Succeeded);
        Assert.False(result.RequiresUpgrade);
        Assert.Null(personel.Sifre);
        Assert.Equal(32, personel.SifreHash.Length);
        Assert.Equal(16, personel.SifreSalt.Length);
    }

    [Fact]
    public void VerifyPassword_RequestsUpgrade_WhenPlainTextResidualExists()
    {
        var hashResult = _service.HashPassword("Secret123!");
        var personel = new Personel
        {
            Sifre = "Secret123!",
            SifreHash = hashResult.Hash,
            SifreSalt = hashResult.Salt
        };

        var result = _service.VerifyPassword("Secret123!", personel);

        Assert.True(result.Succeeded);
        Assert.True(result.RequiresUpgrade);
        Assert.Equal("PlainTextResidual", result.Reason);
    }

    [Fact]
    public void VerifyPassword_AcceptsLegacyHmacHash_AndRequestsUpgrade()
    {
        const string password = "Secret123!";
        using var hmac = new HMACSHA512();
        var personel = new Personel
        {
            SifreHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            SifreSalt = hmac.Key
        };

        var result = _service.VerifyPassword(password, personel);

        Assert.True(result.Succeeded);
        Assert.True(result.RequiresUpgrade);
        Assert.Equal("LegacyHash", result.Reason);
    }

    [Fact]
    public void VerifyPassword_AcceptsPlainTextFallback_AndRequestsUpgrade()
    {
        var personel = new Personel
        {
            Sifre = "Secret123!",
            SifreHash = Array.Empty<byte>(),
            SifreSalt = Array.Empty<byte>()
        };

        var result = _service.VerifyPassword("Secret123!", personel);

        Assert.True(result.Succeeded);
        Assert.True(result.RequiresUpgrade);
        Assert.Equal("PlainTextFallback", result.Reason);
    }
}
