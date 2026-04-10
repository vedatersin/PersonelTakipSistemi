namespace PersonelTakipSistemi.Services
{
    public sealed record PasswordHashResult(byte[] Hash, byte[] Salt);
}
