using System.Globalization;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var arguments = Arguments.Parse(args);
var repoRoot = ResolveRepoRoot();
var appDir = Path.Combine(repoRoot, "PersonelTakipSistemi");
var outputDir = Path.Combine(repoRoot, "outputs", "tasra-komisyon-import");
Directory.CreateDirectory(outputDir);

var configuration = new ConfigurationBuilder()
    .SetBasePath(appDir)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var rawConnectionString = configuration.GetConnectionString("TegmPersonelTakipDB")
    ?? throw new InvalidOperationException("TegmPersonelTakipDB bağlantı bilgisi bulunamadı.");

var connectionStringBuilder = new SqlConnectionStringBuilder(rawConnectionString)
{
    Encrypt = false,
    TrustServerCertificate = true
};

var options = new DbContextOptionsBuilder<TegmPersonelTakipDbContext>()
    .UseSqlServer(connectionStringBuilder.ConnectionString)
    .Options;

await using var context = new TegmPersonelTakipDbContext(options);

var importer = new TasraKomisyonImporter(context);
var report = await importer.RunAsync(arguments.ExcelPath, arguments.ApplyChanges);

var reportPath = Path.Combine(outputDir, $"report-{DateTime.Now:yyyyMMdd-HHmmss}.json");
await File.WriteAllTextAsync(
    reportPath,
    JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }),
    Encoding.UTF8);

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine($"MODE={(arguments.ApplyChanges ? "APPLY" : "DRY-RUN")}");
Console.WriteLine($"ROWS={report.ExcelRowCount}");
Console.WriteLine($"MATCHED_PERSONNEL={report.MatchedPersonnelCount}");
Console.WriteLine($"UNMATCHED_PERSONNEL={report.UnmatchedPersonnelCount}");
Console.WriteLine($"CREATED_CENTRAL_COORDINATORS={report.CreatedCentralCoordinatorCount}");
Console.WriteLine($"ENABLED_CENTRAL_TASRA_FLAGS={report.EnabledCentralTasraFlagCount}");
Console.WriteLine($"CREATED_PROVINCIAL_COORDINATORS={report.CreatedProvincialCoordinatorCount}");
Console.WriteLine($"CREATED_COMMISSIONS={report.CreatedCommissionCount}");
Console.WriteLine($"ASSIGNED_PERSONNEL={report.AssignedPersonnelCount}");
Console.WriteLine($"REPORT={reportPath}");

Console.WriteLine();
Console.WriteLine("BRANCH_MAPPINGS");
foreach (var mapping in report.BranchMappings.OrderBy(x => x.Branch))
{
    Console.WriteLine($"- {mapping.Branch} => {mapping.CoordinatorName} [{mapping.Decision}]");
}

if (report.UnmatchedCities.Any())
{
    Console.WriteLine();
    Console.WriteLine("UNMATCHED_CITIES");
    foreach (var city in report.UnmatchedCities.OrderBy(x => x))
    {
        Console.WriteLine($"- {city}");
    }
}

if (report.UnmatchedPersonnel.Any())
{
    Console.WriteLine();
    Console.WriteLine("UNMATCHED_PERSONNEL_SAMPLE");
    foreach (var person in report.UnmatchedPersonnel.Take(25))
    {
        Console.WriteLine($"- {person.TcKimlikNo} | {person.Ad} {person.Soyad} | {person.Il} | {person.Brans}");
    }
}

return;

static string ResolveRepoRoot()
{
    var current = new DirectoryInfo(AppContext.BaseDirectory);
    while (current != null)
    {
        if (Directory.Exists(Path.Combine(current.FullName, "PersonelTakipSistemi")))
        {
            return current.FullName;
        }

        current = current.Parent;
    }

    return Directory.GetCurrentDirectory();
}

internal sealed class Arguments
{
    public string ExcelPath { get; set; } = @"C:\Users\vedat\Downloads\personel_listesi.xlsx";
    public bool ApplyChanges { get; set; }

    public static Arguments Parse(string[] args)
    {
        var result = new Arguments
        {
            ApplyChanges = args.Any(x => string.Equals(x, "--apply", StringComparison.OrdinalIgnoreCase))
        };

        for (var i = 0; i < args.Length; i++)
        {
            if (string.Equals(args[i], "--excel", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
            {
                result.ExcelPath = args[i + 1];
                i++;
            }
        }

        return result;
    }
}

internal sealed class TasraKomisyonImporter
{
    private static readonly CultureInfo TrCulture = new("tr-TR");

    private readonly TegmPersonelTakipDbContext _context;

    public TasraKomisyonImporter(TegmPersonelTakipDbContext context)
    {
        _context = context;
    }

    public async Task<ImportReport> RunAsync(string excelPath, bool applyChanges)
    {
        var rows = ReadExcel(excelPath);
        var report = new ImportReport
        {
            ExcelPath = excelPath,
            ExcelRowCount = rows.Count
        };

        var merkezTeskilat = await _context.Teskilatlar
            .AsTracking()
            .Where(x => x.IsActive && x.Tur == "Merkez")
            .OrderBy(x => x.TeskilatId)
            .FirstOrDefaultAsync();

        if (merkezTeskilat == null)
        {
            throw new InvalidOperationException("Merkez teşkilatı bulunamadı.");
        }

        var tasraTeskilat = await _context.Teskilatlar
            .AsTracking()
            .Where(x => x.IsActive && x.Tur == "Taşra" && x.BagliMerkezTeskilatId == merkezTeskilat.TeskilatId)
            .OrderBy(x => x.TeskilatId)
            .FirstOrDefaultAsync()
            ?? await _context.Teskilatlar
                .AsTracking()
                .Where(x => x.IsActive && x.Tur == "Taşra")
                .OrderBy(x => x.TeskilatId)
                .FirstOrDefaultAsync();

        if (tasraTeskilat == null)
        {
            throw new InvalidOperationException("Taşra teşkilatı bulunamadı.");
        }

        var iller = await _context.Iller
            .AsNoTracking()
            .OrderBy(x => x.Ad)
            .ToListAsync();

        var cityLookup = iller
            .GroupBy(x => Normalize(x.Ad))
            .ToDictionary(x => x.Key, x => x.First());

        var centerCoordinators = await _context.Koordinatorlukler
            .AsTracking()
            .Where(x => x.IsActive && x.TeskilatId == merkezTeskilat.TeskilatId)
            .OrderBy(x => x.Ad)
            .ToListAsync();

        var provincialCoordinators = await _context.Koordinatorlukler
            .AsTracking()
            .Where(x => x.IsActive && x.TeskilatId == tasraTeskilat.TeskilatId)
            .OrderBy(x => x.Ad)
            .ToListAsync();

        var commissions = await _context.Komisyonlar
            .AsTracking()
            .Include(x => x.Koordinatorluk)
            .Include(x => x.BagliMerkezKoordinatorluk)
            .Where(x => x.IsActive)
            .ToListAsync();

        var personnel = await _context.Personeller
            .AsTracking()
            .Include(x => x.PersonelTeskilatlar)
            .Include(x => x.PersonelKoordinatorlukler)
            .Include(x => x.PersonelKomisyonlar)
            .Where(x => !string.IsNullOrWhiteSpace(x.TcKimlikNo))
            .ToListAsync();

        var personnelByTc = personnel
            .GroupBy(x => NormalizeDigits(x.TcKimlikNo))
            .ToDictionary(x => x.Key, x => x.First());

        var mappingByBranch = new Dictionary<string, Koordinatorluk>(StringComparer.OrdinalIgnoreCase);
        var uniqueBranches = rows
            .Select(x => x.Brans.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x)
            .ToList();

        foreach (var branch in uniqueBranches)
        {
            mappingByBranch[branch] = ResolveCenterCoordinator(branch, centerCoordinators, merkezTeskilat.TeskilatId, report);
        }

        foreach (var coordinator in mappingByBranch.Values.DistinctBy(GetCoordinatorIdentity))
        {
            if (!coordinator.TasraTeskilatiVarMi)
            {
                coordinator.TasraTeskilatiVarMi = true;
                report.EnabledCentralTasraFlagCount++;
            }
        }

        var provinceCoordinatorByIlId = provincialCoordinators
            .Where(x => x.IlId.HasValue)
            .GroupBy(x => x.IlId!.Value)
            .ToDictionary(x => x.Key, x => x.First());

        foreach (var city in rows.Select(x => x.Il).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(x => x))
        {
            if (!TryGetCity(cityLookup, city, out var il))
            {
                report.UnmatchedCities.Add(city);
                continue;
            }

            if (!provinceCoordinatorByIlId.ContainsKey(il.IlId))
            {
                var provincialCoordinator = new Koordinatorluk
                {
                    Ad = $"{il.Ad} İl Koordinatörlüğü",
                    Teskilat = tasraTeskilat,
                    TeskilatId = tasraTeskilat.TeskilatId,
                    IlId = il.IlId,
                    TasraTeskilatiVarMi = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Koordinatorlukler.Add(provincialCoordinator);
                provinceCoordinatorByIlId[il.IlId] = provincialCoordinator;
                provincialCoordinators.Add(provincialCoordinator);
                report.CreatedProvincialCoordinatorCount++;
            }
        }

        var commissionByKey = commissions
            .Where(x => x.Koordinatorluk?.IlId != null && x.BagliMerkezKoordinatorluk != null)
            .GroupBy(x => BuildCommissionKey(x.Koordinatorluk!.IlId!.Value, x.BagliMerkezKoordinatorluk!.Ad))
            .ToDictionary(x => x.Key, x => x.First(), StringComparer.OrdinalIgnoreCase);

        foreach (var row in rows)
        {
            var tc = NormalizeDigits(row.TcKimlikNo);
            if (!personnelByTc.TryGetValue(tc, out var personel))
            {
                report.UnmatchedPersonnel.Add(row);
                continue;
            }

            if (!mappingByBranch.TryGetValue(row.Brans.Trim(), out var centerCoordinator))
            {
                report.UnmatchedPersonnel.Add(row);
                continue;
            }

            if (!TryGetCity(cityLookup, row.Il, out var il) || !provinceCoordinatorByIlId.TryGetValue(il.IlId, out var provincialCoordinator))
            {
                report.UnmatchedPersonnel.Add(row);
                continue;
            }

            var commissionKey = BuildCommissionKey(il.IlId, centerCoordinator.Ad);
            if (!commissionByKey.TryGetValue(commissionKey, out var commission))
            {
                commission = new Komisyon
                {
                    Ad = BuildCommissionName(centerCoordinator.Ad),
                    Koordinatorluk = provincialCoordinator,
                    BagliMerkezKoordinatorluk = centerCoordinator,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Komisyonlar.Add(commission);
                commissions.Add(commission);
                commissionByKey[commissionKey] = commission;
                report.CreatedCommissionCount++;
            }

            report.MatchedPersonnelCount++;

            if (EnsurePersonnelAssignment(personel, tasraTeskilat, provincialCoordinator, commission))
            {
                report.AssignedPersonnelCount++;
            }
        }

        if (applyChanges)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            foreach (var entry in _context.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }

        report.UnmatchedPersonnelCount = report.UnmatchedPersonnel.Count;
        report.CreatedCentralCoordinatorCount = report.BranchMappings.Count(IsCreatedDecision);
        return report;
    }

    private Koordinatorluk ResolveCenterCoordinator(
        string branch,
        List<Koordinatorluk> existingCoordinators,
        int merkezTeskilatId,
        ImportReport report)
    {
        var manualTarget = ResolveManualCoordinatorTarget(branch);
        if (!string.IsNullOrWhiteSpace(manualTarget))
        {
            var manualCoordinator = existingCoordinators.FirstOrDefault(x =>
                string.Equals(x.Ad, manualTarget, StringComparison.OrdinalIgnoreCase));

            if (manualCoordinator != null)
            {
                report.BranchMappings.Add(new BranchMapping(branch, manualCoordinator.Ad, "manual-existing"));
                return manualCoordinator;
            }

            var createdFromManual = CreateCoordinator(manualTarget, merkezTeskilatId, existingCoordinators);
            report.BranchMappings.Add(new BranchMapping(branch, createdFromManual.Ad, "manual-created"));
            return createdFromManual;
        }

        var normalizedBranch = NormalizeBranch(branch);
        var best = existingCoordinators
            .Select(x => new
            {
                Coordinator = x,
                Score = Similarity(normalizedBranch, NormalizeCoordinatorName(x.Ad))
            })
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Coordinator.Ad)
            .FirstOrDefault();

        if (best != null && best.Score >= 0.72m)
        {
            report.BranchMappings.Add(new BranchMapping(branch, best.Coordinator.Ad, $"fuzzy-existing:{best.Score:0.00}"));
            return best.Coordinator;
        }

        var created = CreateCoordinator(BuildCoordinatorName(branch), merkezTeskilatId, existingCoordinators);
        report.BranchMappings.Add(new BranchMapping(branch, created.Ad, best == null ? "created" : $"created:{best.Score:0.00}"));
        return created;
    }

    private Koordinatorluk CreateCoordinator(string name, int merkezTeskilatId, List<Koordinatorluk> existingCoordinators)
    {
        var coordinator = new Koordinatorluk
        {
            Ad = name,
            TeskilatId = merkezTeskilatId,
            IlId = 6,
            TasraTeskilatiVarMi = true,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        _context.Koordinatorlukler.Add(coordinator);
        existingCoordinators.Add(coordinator);
        return coordinator;
    }

    private static bool EnsurePersonnelAssignment(
        Personel personel,
        Teskilat tasraTeskilat,
        Koordinatorluk provincialCoordinator,
        Komisyon commission)
    {
        var changed = false;

        if (!personel.PersonelTeskilatlar.Any(x => x.TeskilatId == tasraTeskilat.TeskilatId))
        {
            personel.PersonelTeskilatlar.Add(new PersonelTeskilat
            {
                PersonelId = personel.PersonelId,
                TeskilatId = tasraTeskilat.TeskilatId,
                Teskilat = tasraTeskilat
            });
            changed = true;
        }

        if (!personel.PersonelKoordinatorlukler.Any(x =>
                (provincialCoordinator.KoordinatorlukId > 0 && x.KoordinatorlukId == provincialCoordinator.KoordinatorlukId) ||
                ReferenceEquals(x.Koordinatorluk, provincialCoordinator)))
        {
            personel.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk
            {
                PersonelId = personel.PersonelId,
                KoordinatorlukId = provincialCoordinator.KoordinatorlukId,
                Koordinatorluk = provincialCoordinator
            });
            changed = true;
        }

        if (!personel.PersonelKomisyonlar.Any(x =>
                (commission.KomisyonId > 0 && x.KomisyonId == commission.KomisyonId) ||
                ReferenceEquals(x.Komisyon, commission)))
        {
            personel.PersonelKomisyonlar.Add(new PersonelKomisyon
            {
                PersonelId = personel.PersonelId,
                KomisyonId = commission.KomisyonId,
                Komisyon = commission
            });
            changed = true;
        }

        return changed;
    }

    private static List<ExcelPersonRow> ReadExcel(string path)
    {
        using var package = new ExcelPackage(new FileInfo(path));
        var sheet = package.Workbook.Worksheets.FirstOrDefault()
            ?? throw new InvalidOperationException("Excel sayfası bulunamadı.");

        if (sheet.Dimension == null)
        {
            return new List<ExcelPersonRow>();
        }

        var headerRow = 0;
        var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (var row = 1; row <= Math.Min(sheet.Dimension.End.Row, 10); row++)
        {
            var candidates = Enumerable.Range(1, sheet.Dimension.End.Column)
                .Select(col => sheet.Cells[row, col].Text.Trim())
                .ToList();

            if (!candidates.Any(x => NormalizeHeader(x) == "KIMLIKNO"))
            {
                continue;
            }

            headerRow = row;
            for (var col = 1; col <= sheet.Dimension.End.Column; col++)
            {
                var header = sheet.Cells[row, col].Text.Trim();
                if (!string.IsNullOrWhiteSpace(header))
                {
                    headerMap[NormalizeHeader(header)] = col;
                }
            }

            break;
        }

        if (headerRow == 0)
        {
            throw new InvalidOperationException("Excel başlık satırı bulunamadı.");
        }

        var requiredHeaders = new[]
        {
            "KIMLIKNO",
            "ADI",
            "SOYADI",
            "KADROSUNUNBULUNDUGUYER",
            "BRANSI"
        };

        foreach (var header in requiredHeaders)
        {
            if (!headerMap.ContainsKey(header))
            {
                throw new InvalidOperationException($"Excel başlığı bulunamadı: {header}");
            }
        }

        var tcCol = headerMap["KIMLIKNO"];
        var adCol = headerMap["ADI"];
        var soyadCol = headerMap["SOYADI"];
        var ilCol = headerMap["KADROSUNUNBULUNDUGUYER"];
        var bransCol = headerMap["BRANSI"];

        var rows = new List<ExcelPersonRow>();
        for (var row = headerRow + 1; row <= sheet.Dimension.End.Row; row++)
        {
            var tc = sheet.Cells[row, tcCol].Text.Trim();
            var ad = sheet.Cells[row, adCol].Text.Trim();
            var soyad = sheet.Cells[row, soyadCol].Text.Trim();
            var il = sheet.Cells[row, ilCol].Text.Trim();
            var brans = sheet.Cells[row, bransCol].Text.Trim();

            if (string.IsNullOrWhiteSpace(tc) ||
                string.IsNullOrWhiteSpace(ad) ||
                string.IsNullOrWhiteSpace(soyad) ||
                string.IsNullOrWhiteSpace(il) ||
                string.IsNullOrWhiteSpace(brans))
            {
                continue;
            }

            rows.Add(new ExcelPersonRow(
                NormalizeDigits(tc),
                TitleCase(ad),
                TitleCase(soyad),
                il.Trim(),
                brans.Trim()));
        }

        return rows
            .DistinctBy(x => $"{x.TcKimlikNo}|{Normalize(x.Il)}|{Normalize(x.Brans)}")
            .ToList();
    }

    private static bool TryGetCity(IReadOnlyDictionary<string, Il> cityLookup, string city, out Il il)
    {
        return cityLookup.TryGetValue(Normalize(city), out il!);
    }

    private static string? ResolveManualCoordinatorTarget(string branch)
    {
        var normalized = NormalizeBranch(branch);

        if (normalized.StartsWith("MUZIK", StringComparison.Ordinal))
        {
            return "Müzik Birim Koordinatörlüğü";
        }

        return normalized switch
        {
            "ALMANCA" => "Almanca Birim Koordinatörlüğü",
            "BILGISAYARVEOGRETIMTEKNOLOJILERI" => "BÖTE Birim Koordinatörlüğü",
            "BILISIMTEKNOLOJILERI" => "BÖTE Birim Koordinatörlüğü",
            "COCUKGELISIMIVEEGITIMI" => "İlkokul Hayat Bilgisi Birim Koordinatörlüğü",
            "DINKULTURUVEAHLAKBILGISI" => "Sosyal Bilgiler Birim Koordinatörlüğü",
            "FELSEFE" => "Sosyal Bilgiler Birim Koordinatörlüğü",
            "FENBILIMLERI" => "Fen Bilimleri Birim Koordinatörlüğü",
            "GORSELSANATLAR" => "Görsel Tasarım Birim Koordinatörlüğü",
            "GRAFIKVEFOTOGRAFGRAFIK" => "Görsel Tasarım Birim Koordinatörlüğü",
            "GRAFIKVEFOTOGRAFFOTOGRAF" => "Görsel Tasarım Birim Koordinatörlüğü",
            "ILKOGRETIMMATEMATIK" => "İlkokul Matematik Birim Koordinatörlüğü",
            "INGILIZCE" => "İngilizce Birim Koordinatörlüğü",
            "MATEMATIK" => "Ortaokul Matematik Birim Koordinatörlüğü",
            "OKULONCESI" => "İlkokul Hayat Bilgisi Birim Koordinatörlüğü",
            "RADYOTELEVIZYON" => "Mebi Dijital Birim Koordinatörlüğü",
            "REHBERLIK" => "Uzmanlar Birim Koordinatörlüğü",
            "SINIFOGRETMENLIGI" => "İlkokul Hayat Bilgisi Birim Koordinatörlüğü",
            "SOSYALBILGILER" => "Sosyal Bilgiler Birim Koordinatörlüğü",
            "TEKNOLOJIVETASARIM" => "Görsel Tasarım Birim Koordinatörlüğü",
            "TURKDILIVEEDEBIYATI" => "Dil İnceleme Birim Koordinatörlüğü",
            "TURKCE" => "Ortaokul Türkçe Birim Koordinatörlüğü",
            "YASAYANDILLERVELEHCELERKURTCEKURMANCI" => "Dil İnceleme Birim Koordinatörlüğü",
            "OZELEGITIM" => "Uzmanlar Birim Koordinatörlüğü",
            _ => null
        };
    }

    private static string BuildCoordinatorName(string branch)
    {
        var cleaned = branch.Trim();
        if (cleaned.EndsWith("Koordinatörlüğü", StringComparison.OrdinalIgnoreCase))
        {
            return cleaned;
        }

        return $"{TitleCase(cleaned)} Birim Koordinatörlüğü";
    }

    private static string BuildCommissionName(string coordinatorName)
    {
        var baseName = coordinatorName
            .Replace("Birim Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase)
            .Replace("Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase)
            .Trim();

        return $"{baseName} Komisyonu";
    }

    private static string NormalizeCoordinatorName(string value)
    {
        return NormalizeBranch(
            value.Replace("Birim Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase)
                 .Replace("Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase));
    }

    private static string BuildCommissionKey(int ilId, string centerCoordinatorName)
    {
        return $"{ilId}:{Normalize(centerCoordinatorName)}";
    }

    private static string GetCoordinatorIdentity(Koordinatorluk coordinator)
    {
        return coordinator.KoordinatorlukId > 0
            ? coordinator.KoordinatorlukId.ToString(CultureInfo.InvariantCulture)
            : Normalize(coordinator.Ad);
    }

    private static bool IsCreatedDecision(BranchMapping mapping)
    {
        return mapping.Decision.StartsWith("created", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(mapping.Decision, "manual-created", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeBranch(string value)
    {
        return new string(Normalize(value).Where(char.IsLetterOrDigit).ToArray());
    }

    private static string NormalizeHeader(string value)
    {
        return new string(Normalize(value).Where(char.IsLetterOrDigit).ToArray());
    }

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var upper = value.Trim().ToUpper(TrCulture);
        var sb = new StringBuilder(upper.Length);

        foreach (var ch in upper.Normalize(NormalizationForm.FormD))
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (category == UnicodeCategory.NonSpacingMark)
            {
                continue;
            }

            sb.Append(ch switch
            {
                'İ' => 'I',
                'I' => 'I',
                'Ş' => 'S',
                'Ğ' => 'G',
                'Ü' => 'U',
                'Ö' => 'O',
                'Ç' => 'C',
                'Â' => 'A',
                'Ê' => 'E',
                'Î' => 'I',
                'Ô' => 'O',
                'Û' => 'U',
                _ => ch
            });
        }

        return sb.ToString();
    }

    private static string NormalizeDigits(string value)
    {
        return new string((value ?? string.Empty).Where(char.IsDigit).ToArray());
    }

    private static string TitleCase(string value)
    {
        return TrCulture.TextInfo.ToTitleCase((value ?? string.Empty).Trim().ToLower(TrCulture));
    }

    private static decimal Similarity(string left, string right)
    {
        if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
        {
            return 0m;
        }

        if (left == right)
        {
            return 1m;
        }

        var leftBigrams = BuildBigrams(left);
        var rightBigrams = BuildBigrams(right);
        if (leftBigrams.Count == 0 || rightBigrams.Count == 0)
        {
            return 0m;
        }

        var intersection = leftBigrams.Intersect(rightBigrams).Count();
        return (2m * intersection) / (leftBigrams.Count + rightBigrams.Count);
    }

    private static HashSet<string> BuildBigrams(string value)
    {
        var result = new HashSet<string>(StringComparer.Ordinal);
        if (value.Length == 1)
        {
            result.Add(value);
            return result;
        }

        for (var i = 0; i < value.Length - 1; i++)
        {
            result.Add(value.Substring(i, 2));
        }

        return result;
    }
}

internal sealed record ExcelPersonRow(string TcKimlikNo, string Ad, string Soyad, string Il, string Brans);

internal sealed class ImportReport
{
    public string ExcelPath { get; set; } = string.Empty;
    public int ExcelRowCount { get; set; }
    public int MatchedPersonnelCount { get; set; }
    public int UnmatchedPersonnelCount { get; set; }
    public int CreatedCentralCoordinatorCount { get; set; }
    public int EnabledCentralTasraFlagCount { get; set; }
    public int CreatedProvincialCoordinatorCount { get; set; }
    public int CreatedCommissionCount { get; set; }
    public int AssignedPersonnelCount { get; set; }
    public List<string> UnmatchedCities { get; set; } = new();
    public List<ExcelPersonRow> UnmatchedPersonnel { get; set; } = new();
    public List<BranchMapping> BranchMappings { get; set; } = new();
}

internal sealed record BranchMapping(string Branch, string CoordinatorName, string Decision);
