using System.Text;
using Xunit;

namespace PersonelTakipSistemi.Tests;

public class TurkishEncodingGuardTests
{
    [Fact]
    public void TurkishText_ShouldNotContainMojibakePatterns_InViewsAndViewModels()
    {
        var solutionRoot = FindSolutionRoot();
        var appRoot = Path.Combine(solutionRoot, "PersonelTakipSistemi");

        var rootsToScan = new[]
        {
            Path.Combine(appRoot, "Views"),
            Path.Combine(appRoot, "ViewModels")
        };

        var extensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".cshtml",
            ".cs"
        };

        var mojibakeTokens = new[]
        {
            "Ã",
            "Ä",
            "Å",
            "�"
        };

        var offenders = new List<string>();

        foreach (var root in rootsToScan.Where(Directory.Exists))
        {
            foreach (var file in Directory.EnumerateFiles(root, "*.*", SearchOption.AllDirectories))
            {
                if (!extensions.Contains(Path.GetExtension(file)))
                {
                    continue;
                }

                var content = File.ReadAllText(file, Encoding.UTF8);
                if (mojibakeTokens.Any(content.Contains))
                {
                    offenders.Add(Path.GetRelativePath(solutionRoot, file));
                }
            }
        }

        Assert.True(
            offenders.Count == 0,
            "Mojibake (bozuk Türkçe karakter) bulundu: " + string.Join(", ", offenders));
    }

    private static string FindSolutionRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "PersonelTakipSistemi.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Çözüm klasörü bulunamadı.");
    }
}
