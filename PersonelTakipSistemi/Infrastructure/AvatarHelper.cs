namespace PersonelTakipSistemi.Infrastructure;

public static class AvatarHelper
{
    private static readonly string[] ThemeAvatarMarkers =
    {
        "/sevilay-tema/assets/img/avatar",
        "/sevilay-tema/assets/img/avatars/",
        "\\sevilay-tema\\assets\\img\\avatar",
        "\\sevilay-tema\\assets\\img\\avatars\\"
    };

    public static bool HasCustomPhoto(string? photoUrl)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
        {
            return false;
        }

        return !ThemeAvatarMarkers.Any(marker =>
            photoUrl.Contains(marker, StringComparison.OrdinalIgnoreCase));
    }

    public static string? NormalizePhoto(string? photoUrl)
    {
        return HasCustomPhoto(photoUrl) ? photoUrl : null;
    }

    public static string GetInitials(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return "?";
        }

        var parts = fullName
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length == 0)
        {
            return "?";
        }

        if (parts.Length == 1)
        {
            return parts[0][0].ToString().ToUpperInvariant();
        }

        return string.Concat(parts[0][0], parts[^1][0]).ToUpperInvariant();
    }

    public static string GetInitials(string? firstName, string? lastName)
    {
        return GetInitials($"{firstName} {lastName}");
    }
}
