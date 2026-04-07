namespace PersonelTakipSistemi.Services
{
    public sealed record PasswordVerificationResult(bool Succeeded, bool RequiresUpgrade, string Reason);
}
