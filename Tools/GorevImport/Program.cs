using System.Globalization;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var arguments = Arguments.Parse(args);
var repoRoot = ResolveRepoRoot();
var appDir = Path.Combine(repoRoot, "PersonelTakipSistemi");
var outputDir = Path.Combine(repoRoot, "outputs", "gorev-import");
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

var importer = new GorevImporter(context);
var report = await importer.RunAsync(arguments.ExcelPath, arguments.ApplyChanges);

var reportPath = Path.Combine(outputDir, $"report-{DateTime.Now:yyyyMMdd-HHmmss}.json");
await File.WriteAllTextAsync(
    reportPath,
    JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }),
    Encoding.UTF8);

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine($"MODE={(arguments.ApplyChanges ? "APPLY" : "DRY-RUN")}");
Console.WriteLine($"ROWS={report.ExcelRowCount}");
Console.WriteLine($"CREATED_TASKS={report.CreatedTaskCount}");
Console.WriteLine($"SKIPPED_DUPLICATES={report.SkippedDuplicateCount}");
Console.WriteLine($"MATCHED_PERSONNEL={report.MatchedPersonnelCount}");
Console.WriteLine($"UNMATCHED_PERSONNEL={report.UnmatchedPersonnelCount}");
Console.WriteLine($"UNMATCHED_COORDINATORS={report.UnmatchedCoordinatorCount}");
Console.WriteLine($"CREATED_CENTRAL_COORDINATORS={report.CreatedCentralCoordinatorCount}");
Console.WriteLine($"CREATED_PROVINCIAL_COORDINATORS={report.CreatedProvincialCoordinatorCount}");
Console.WriteLine($"CREATED_COMMISSIONS={report.CreatedCommissionCount}");
Console.WriteLine($"UPDATED_PERSONNEL_ASSIGNMENTS={report.UpdatedPersonnelAssignmentCount}");
Console.WriteLine($"UNPARSED_END_DATES={report.UnparsedEndDateCount}");
Console.WriteLine($"REPORT={reportPath}");

if (report.UnmatchedPersonnel.Any())
{
    Console.WriteLine();
    Console.WriteLine("UNMATCHED_PERSONNEL");
    foreach (var row in report.UnmatchedPersonnel.Take(25))
    {
        Console.WriteLine($"- {row.Sheet} | {row.Il} | {row.Ad} {row.Soyad} | {row.TaskName}");
    }
}

if (report.UnmatchedCoordinatorRows.Any())
{
    Console.WriteLine();
    Console.WriteLine("UNMATCHED_COORDINATORS");
    foreach (var row in report.UnmatchedCoordinatorRows.Take(25))
    {
        Console.WriteLine($"- {row.Sheet} | {row.Il} | {row.Ad} {row.Soyad} | branch={row.BranchOrHint} | task={row.TaskName}");
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
    public string ExcelPath { get; set; } = @"C:\Users\vedat\Downloads\görevler.xlsx";
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

internal sealed class GorevImporter
{
    private static readonly CultureInfo TrCulture = new("tr-TR");

    private readonly TegmPersonelTakipDbContext _context;

    public GorevImporter(TegmPersonelTakipDbContext context)
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
            .FirstOrDefaultAsync()
            ?? throw new InvalidOperationException("Merkez teşkilatı bulunamadı.");

        var tasraTeskilat = await _context.Teskilatlar
            .AsTracking()
            .Where(x => x.IsActive && x.Tur == "Taşra" && x.BagliMerkezTeskilatId == merkezTeskilat.TeskilatId)
            .OrderBy(x => x.TeskilatId)
            .FirstOrDefaultAsync()
            ?? await _context.Teskilatlar
                .AsTracking()
                .Where(x => x.IsActive && x.Tur == "Taşra")
                .OrderBy(x => x.TeskilatId)
                .FirstOrDefaultAsync()
            ?? throw new InvalidOperationException("Taşra teşkilatı bulunamadı.");

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

        var cities = await _context.Iller
            .AsNoTracking()
            .OrderBy(x => x.Ad)
            .ToListAsync();

        var cityLookup = cities
            .GroupBy(x => Normalize(x.Ad))
            .ToDictionary(x => x.Key, x => x.First());

        var personnel = await _context.Personeller
            .AsTracking()
            .Include(x => x.GorevliIl)
            .Include(x => x.Brans)
            .Include(x => x.PersonelTeskilatlar)
            .Include(x => x.PersonelKoordinatorlukler)
            .Include(x => x.PersonelKomisyonlar)
                .ThenInclude(x => x.Komisyon)
                    .ThenInclude(x => x.BagliMerkezKoordinatorluk)
            .ToListAsync();

        var personnelLookup = personnel
            .GroupBy(BuildPersonnelLookupKey)
            .ToDictionary(x => x.Key, x => x.ToList(), StringComparer.OrdinalIgnoreCase);

        var personCityFallback = personnel
            .Where(x => x.GorevliIl != null)
            .GroupBy(x => NormalizeFullName(x.Ad, x.Soyad))
            .ToDictionary(
                x => x.Key,
                x => x.Select(p => p.GorevliIl!.Ad).Distinct(StringComparer.OrdinalIgnoreCase).ToList(),
                StringComparer.OrdinalIgnoreCase);

        var provinceCoordinatorByIlId = provincialCoordinators
            .Where(x => x.IlId.HasValue)
            .GroupBy(x => x.IlId!.Value)
            .ToDictionary(x => x.Key, x => x.First());

        var commissionByKey = commissions
            .Where(x => x.Koordinatorluk?.IlId != null && x.BagliMerkezKoordinatorluk != null)
            .GroupBy(x => BuildCommissionKey(x.Koordinatorluk!.IlId!.Value, x.BagliMerkezKoordinatorluk!.Ad))
            .ToDictionary(x => x.Key, x => x.First(), StringComparer.OrdinalIgnoreCase);

        var gorevDurumId = await _context.GorevDurumlari
            .AsNoTracking()
            .Where(x => x.Ad == "Atanmayı Bekliyor")
            .Select(x => x.GorevDurumId)
            .FirstOrDefaultAsync();
        if (gorevDurumId == 0)
        {
            gorevDurumId = 1;
        }

        var roleIdByName = await _context.GorevTurleri
            .AsNoTracking()
            .ToDictionaryAsync(x => Normalize(x.Ad), x => x.GorevTuruId);

        var categoryIdByName = await _context.IsNitelikleri
            .AsNoTracking()
            .ToDictionaryAsync(x => Normalize(x.Ad), x => x.IsNiteligiId);

        var existingTaskKeys = await LoadExistingTaskKeysAsync();

        foreach (var row in rows)
        {
            var resolvedCity = ResolveRowCity(row, personCityFallback);
            if (string.IsNullOrWhiteSpace(resolvedCity) || !TryGetCity(cityLookup, resolvedCity, out var il))
            {
                report.UnmatchedPersonnel.Add(row);
                continue;
            }

            var person = ResolvePersonnel(row, il.Ad, personnelLookup, personnel);
            if (person == null)
            {
                report.UnmatchedPersonnel.Add(row);
                continue;
            }

            report.MatchedPersonnelCount++;

            var coordinator = ResolveCenterCoordinator(row, person, centerCoordinators, merkezTeskilat.TeskilatId, report);
            if (coordinator == null)
            {
                report.UnmatchedCoordinatorRows.Add(row);
                continue;
            }

            if (!coordinator.TasraTeskilatiVarMi)
            {
                coordinator.TasraTeskilatiVarMi = true;
            }

            if (!provinceCoordinatorByIlId.TryGetValue(il.IlId, out var provincialCoordinator))
            {
                provincialCoordinator = new Koordinatorluk
                {
                    Ad = $"{il.Ad} İl Koordinatörlüğü",
                    TeskilatId = tasraTeskilat.TeskilatId,
                    Teskilat = tasraTeskilat,
                    IlId = il.IlId,
                    TasraTeskilatiVarMi = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Koordinatorlukler.Add(provincialCoordinator);
                provincialCoordinators.Add(provincialCoordinator);
                provinceCoordinatorByIlId[il.IlId] = provincialCoordinator;
                report.CreatedProvincialCoordinatorCount++;
            }

            var commissionKey = BuildCommissionKey(il.IlId, coordinator.Ad);
            if (!commissionByKey.TryGetValue(commissionKey, out var commission))
            {
                commission = new Komisyon
                {
                    Ad = BuildCommissionName(coordinator.Ad),
                    KoordinatorlukId = provincialCoordinator.KoordinatorlukId,
                    Koordinatorluk = provincialCoordinator,
                    BagliMerkezKoordinatorlukId = coordinator.KoordinatorlukId,
                    BagliMerkezKoordinatorluk = coordinator,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                _context.Komisyonlar.Add(commission);
                commissions.Add(commission);
                commissionByKey[commissionKey] = commission;
                report.CreatedCommissionCount++;
            }

            if (EnsurePersonnelAssignment(person, tasraTeskilat, provincialCoordinator, commission))
            {
                report.UpdatedPersonnelAssignmentCount++;
            }

            var categoryId = ResolveCategoryId(row.TaskName, categoryIdByName);
            var roleId = ResolveRoleId(row, roleIdByName);
            var taskKey = BuildTaskKey(person.PersonelId, il.IlId, commission, row.TaskName);

            if (!existingTaskKeys.Add(taskKey))
            {
                report.SkippedDuplicateCount++;
                continue;
            }

            var endDate = TryParseDate(row.EndDateText);
            if (row.EndDateText is not null && endDate == null)
            {
                report.UnparsedEndDateCount++;
            }

            var gorev = new Gorev
            {
                Ad = row.TaskName.Trim(),
                Aciklama = BuildDescription(row),
                IsNiteligiId = categoryId,
                GorevDurumId = gorevDurumId,
                BaslangicTarihi = DateTime.Today,
                BitisTarihi = endDate,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            gorev.GorevAtamaPersoneller.Add(new GorevAtamaPersonel
            {
                Personel = person,
                PersonelId = person.PersonelId,
                GorevTuruId = roleId
            });

            gorev.GorevAtamaKoordinatorlukler.Add(new GorevAtamaKoordinatorluk
            {
                Koordinatorluk = coordinator,
                KoordinatorlukId = coordinator.KoordinatorlukId,
                GorevTuruId = roleId
            });

            gorev.GorevAtamaKomisyonlar.Add(new GorevAtamaKomisyon
            {
                Komisyon = commission,
                KomisyonId = commission.KomisyonId,
                GorevTuruId = roleId
            });

            _context.Gorevler.Add(gorev);
            report.CreatedTaskCount++;
        }

        report.UnmatchedPersonnelCount = report.UnmatchedPersonnel.Count;
        report.UnmatchedCoordinatorCount = report.UnmatchedCoordinatorRows.Count;

        if (applyChanges)
        {
            await _context.SaveChangesAsync();
        }

        return report;
    }

    private async Task<HashSet<string>> LoadExistingTaskKeysAsync()
    {
        var keys = await _context.Gorevler
            .AsNoTracking()
            .Where(g => g.IsActive)
            .Select(g => new
            {
                g.GorevId,
                g.Ad,
                PersonelIds = g.GorevAtamaPersoneller.Select(x => x.PersonelId).ToList(),
                KomisyonIds = g.GorevAtamaKomisyonlar.Select(x => x.KomisyonId).ToList()
            })
            .ToListAsync();

        var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in keys)
        {
            var normalizedTask = Normalize(item.Ad);
            foreach (var personelId in item.PersonelIds.DefaultIfEmpty(0))
            {
                foreach (var komisyonId in item.KomisyonIds.DefaultIfEmpty(0))
                {
                    result.Add($"{personelId}:{komisyonId}:{normalizedTask}");
                }
            }
        }

        return result;
    }

    private static string BuildDescription(ExcelTaskRow row)
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(row.Sheet))
        {
            parts.Add($"Kaynak sayfa: {row.Sheet}");
        }

        if (!string.IsNullOrWhiteSpace(row.RoleText))
        {
            parts.Add($"Excel görevi: {row.RoleText.Trim()}");
        }

        if (!string.IsNullOrWhiteSpace(row.BranchOrHint))
        {
            parts.Add($"Komisyon branşı: {row.BranchOrHint.Trim()}");
        }

        return string.Join(" | ", parts);
    }

    private static string BuildTaskKey(int personelId, int ilId, Komisyon commission, string taskName)
    {
        var komisyonPart = commission.KomisyonId > 0 ? commission.KomisyonId : ilId;
        return $"{personelId}:{komisyonPart}:{Normalize(taskName)}";
    }

    private static string ResolveRowCity(ExcelTaskRow row, IReadOnlyDictionary<string, List<string>> personCityFallback)
    {
        if (!string.IsNullOrWhiteSpace(row.Il) &&
            !Normalize(row.Il).Contains("KOMISYONDISI", StringComparison.Ordinal))
        {
            return row.Il;
        }

        var personKey = NormalizeFullName(row.Ad, row.Soyad);
        if (personCityFallback.TryGetValue(personKey, out var fallbackCities))
        {
            return fallbackCities.FirstOrDefault() ?? string.Empty;
        }

        return row.Sheet;
    }

    private Personel? ResolvePersonnel(
        ExcelTaskRow row,
        string ilAd,
        IReadOnlyDictionary<string, List<Personel>> personnelLookup,
        IReadOnlyCollection<Personel> allPersonnel)
    {
        var exactKey = BuildPersonnelLookupKey(row.Ad, row.Soyad, ilAd);
        if (personnelLookup.TryGetValue(exactKey, out var exactMatches))
        {
            return SelectBestPersonnelMatch(exactMatches, row);
        }

        var fallbackKey = BuildPersonnelLookupKey(row.Ad, row.Soyad, row.Sheet);
        if (personnelLookup.TryGetValue(fallbackKey, out var fallbackMatches))
        {
            return SelectBestPersonnelMatch(fallbackMatches, row);
        }

        var nameOnly = NormalizeFullName(row.Ad, row.Soyad);
        var candidates = personnelLookup
            .Where(x => x.Key.StartsWith($"{nameOnly}|", StringComparison.OrdinalIgnoreCase))
            .SelectMany(x => x.Value)
            .DistinctBy(x => x.PersonelId)
            .ToList();

        var selected = SelectBestPersonnelMatch(candidates, row);
        if (selected != null)
        {
            return selected;
        }

        var cityCandidates = allPersonnel
            .Where(x => string.Equals(Normalize(x.GorevliIl?.Ad ?? string.Empty), Normalize(ilAd), StringComparison.Ordinal))
            .ToList();

        return FindClosestPersonnel(cityCandidates, row) ?? FindClosestPersonnel(allPersonnel, row);
    }

    private static Personel? SelectBestPersonnelMatch(IEnumerable<Personel> candidates, ExcelTaskRow row)
    {
        var list = candidates.ToList();
        if (list.Count == 0)
        {
            return null;
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        var normalizedBranch = Normalize(row.BranchOrHint ?? string.Empty);
        if (!string.IsNullOrWhiteSpace(normalizedBranch))
        {
            var branchMatch = list.FirstOrDefault(x =>
                x.Brans != null &&
                (Normalize(x.Brans.Ad) == normalizedBranch ||
                 Normalize(x.Brans.Ad).Contains(normalizedBranch, StringComparison.Ordinal) ||
                 normalizedBranch.Contains(Normalize(x.Brans.Ad), StringComparison.Ordinal)));

            if (branchMatch != null)
            {
                return branchMatch;
            }
        }

        return list.OrderByDescending(x => x.AktifMi).ThenBy(x => x.PersonelId).First();
    }

    private Koordinatorluk? ResolveCenterCoordinator(
        ExcelTaskRow row,
        Personel person,
        List<Koordinatorluk> existingCoordinators,
        int merkezTeskilatId,
        ImportReport report)
    {
        var exactExistingCoordinator = person.PersonelKomisyonlar
            .Select(x => x.Komisyon?.BagliMerkezKoordinatorluk?.Ad)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(name => existingCoordinators.FirstOrDefault(x => string.Equals(x.Ad, name, StringComparison.OrdinalIgnoreCase)))
            .FirstOrDefault(x => x != null);

        if (exactExistingCoordinator != null)
        {
            return exactExistingCoordinator;
        }

        var branchHint = ResolveBranchHint(row, person);
        if (string.IsNullOrWhiteSpace(branchHint))
        {
            return null;
        }

        var manualTarget = ResolveManualCoordinatorTarget(branchHint);
        if (!string.IsNullOrWhiteSpace(manualTarget))
        {
            var manualCoordinator = existingCoordinators.FirstOrDefault(x =>
                string.Equals(x.Ad, manualTarget, StringComparison.OrdinalIgnoreCase));

            if (manualCoordinator != null)
            {
                return manualCoordinator;
            }

            var created = CreateCoordinator(manualTarget, merkezTeskilatId, existingCoordinators);
            report.CreatedCentralCoordinatorCount++;
            return created;
        }

        var normalizedBranch = NormalizeBranch(branchHint);
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
            return best.Coordinator;
        }

        if (string.IsNullOrWhiteSpace(row.BranchOrHint))
        {
            return null;
        }

        var createdCoordinator = CreateCoordinator(BuildCoordinatorName(branchHint), merkezTeskilatId, existingCoordinators);
        report.CreatedCentralCoordinatorCount++;
        return createdCoordinator;
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

    private static bool EnsurePersonnelAssignment(Personel personel, Teskilat tasraTeskilat, Koordinatorluk provincialCoordinator, Komisyon commission)
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

        if (!personel.PersonelKoordinatorlukler.Any(x => x.KoordinatorlukId == provincialCoordinator.KoordinatorlukId))
        {
            personel.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk
            {
                PersonelId = personel.PersonelId,
                KoordinatorlukId = provincialCoordinator.KoordinatorlukId,
                Koordinatorluk = provincialCoordinator
            });
            changed = true;
        }

        if (!personel.PersonelKomisyonlar.Any(x => x.KomisyonId == commission.KomisyonId))
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

    private static int ResolveCategoryId(string taskName, IReadOnlyDictionary<string, int> categoryIdByName)
    {
        var normalized = Normalize(taskName);

        if (normalized.Contains("MEBIMUMTAZSAHSIYETLER", StringComparison.Ordinal) &&
            TryGetDictionaryValue(categoryIdByName, out var mebiMumtazId, "MEBI", "MEBIMUMTAZSAHSIYETLER"))
        {
            return mebiMumtazId;
        }

        if ((normalized.Contains("OGRETIMPROGRAMI", StringComparison.Ordinal) ||
             normalized.Contains("PROGRAM", StringComparison.Ordinal) ||
             normalized.Contains("OKK", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(categoryIdByName, out var programId, "OGRETIMPROGRAMI"))
        {
            return programId;
        }

        if ((normalized.Contains("EDERGI", StringComparison.Ordinal) || normalized.Contains("E DERGI", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(categoryIdByName, out var eDergiId, "EDERGI"))
        {
            return eDergiId;
        }

        if ((normalized.Contains("EBULTEN", StringComparison.Ordinal) || normalized.Contains("E BULTEN", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(categoryIdByName, out var eBultenId, "EBULTEN"))
        {
            return eBultenId;
        }

        if (normalized.Contains("DERGI", StringComparison.Ordinal) &&
            TryGetDictionaryValue(categoryIdByName, out var dergiId, "DERGI"))
        {
            return dergiId;
        }

        if (normalized.Contains("CIZGIROMAN", StringComparison.Ordinal) &&
            TryGetDictionaryValue(categoryIdByName, out var cizgiRomanId, "CIZGIROMAN"))
        {
            return cizgiRomanId;
        }

        if (normalized.Contains("HIKAYEKITABI", StringComparison.Ordinal) &&
            TryGetDictionaryValue(categoryIdByName, out var hikayeId, "HIKAYEKITABI"))
        {
            return hikayeId;
        }

        if ((normalized.Contains("ANIMASYON", StringComparison.Ordinal) ||
             normalized.Contains("VIDEO", StringComparison.Ordinal) ||
             normalized.Contains("DIJITAL", StringComparison.Ordinal) ||
             normalized.Contains("DIZGI", StringComparison.Ordinal) ||
             normalized.Contains("GORSEL", StringComparison.Ordinal) ||
             normalized.Contains("EICERIK", StringComparison.Ordinal) ||
             normalized.Contains("SOSYALMEDYA", StringComparison.Ordinal) ||
             normalized.Contains("YOUTUBE", StringComparison.Ordinal) ||
             normalized.Contains("COKLU", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(categoryIdByName, out var cokluOrtamId, "DIJITALICERIK", "COKLUORTAM"))
        {
            return cokluOrtamId;
        }

        if ((normalized.Contains("CALISMAKITABI", StringComparison.Ordinal) ||
             normalized.Contains("FARKLILASTIRMAETKINLIKKITABI", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(categoryIdByName, out var calismaId, "CALISMAKITABI", "CALISMAKITABIDESTEKMATERYAL"))
        {
            return calismaId;
        }

        if (normalized.Contains("DERSKITABI", StringComparison.Ordinal) &&
            TryGetDictionaryValue(categoryIdByName, out var dersKitabiId, "DERSKITABI"))
        {
            return dersKitabiId;
        }

        return TryGetDictionaryValue(categoryIdByName, out var fallbackId, "DIJITALICERIK", "MEBI", "DERSKITABI")
            ? fallbackId
            : categoryIdByName.Values.First();
    }

    private static int ResolveRoleId(ExcelTaskRow row, IReadOnlyDictionary<string, int> roleIdByName)
    {
        var roleText = $"{row.RoleText} {row.TaskName}";
        var normalized = Normalize(roleText);

        if (normalized.Contains("REHBERLIK", StringComparison.Ordinal) &&
            TryGetDictionaryValue(roleIdByName, out var rehberlik, "REHBERLIKUZMANI"))
        {
            return rehberlik;
        }

        if ((normalized.Contains("VIDEO", StringComparison.Ordinal) ||
             normalized.Contains("KURGU", StringComparison.Ordinal) ||
             normalized.Contains("YOUTUBE", StringComparison.Ordinal) ||
             normalized.Contains("ROPORTAJ", StringComparison.Ordinal) ||
             normalized.Contains("ANIMASYON", StringComparison.Ordinal) ||
             normalized.Contains("DIJITAL", StringComparison.Ordinal) ||
             normalized.Contains("EICERIK", StringComparison.Ordinal) ||
             normalized.Contains("COKLU", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var videoKurgu, "COKLUORTAMTASARIMCISI"))
        {
            return videoKurgu;
        }

        if ((normalized.Contains("GRAFIK", StringComparison.Ordinal) ||
             normalized.Contains("GORSEL", StringComparison.Ordinal) ||
             normalized.Contains("TASARIM", StringComparison.Ordinal) ||
             normalized.Contains("DIZGI", StringComparison.Ordinal) ||
             normalized.Contains("FOTOGRAF", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var gorselTasarim, "GORSELTASARIMCI"))
        {
            return gorselTasarim;
        }

        if ((normalized.Contains("PROGRAM", StringComparison.Ordinal) || normalized.Contains("OKK", StringComparison.Ordinal)) &&
            normalized.Contains("INCELEME", StringComparison.Ordinal) &&
            TryGetDictionaryValue(roleIdByName, out var programInceleme, "INCELEMEPROGRAMGELISTIRME"))
        {
            return programInceleme;
        }

        if ((normalized.Contains("OLCME", StringComparison.Ordinal) ||
             normalized.Contains("DEGERLENDIRME", StringComparison.Ordinal) ||
             normalized.Contains("SORU", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var olcmeInceleme, "INCELEMEOLCMEVEDEGERLENDIRMEUZMANI", "OLCMEVEDEGERLENDIRMEUZMANI"))
        {
            return olcmeInceleme;
        }

        if ((normalized.Contains("ALMANCA", StringComparison.Ordinal) ||
             normalized.Contains("INGILIZCE", StringComparison.Ordinal) ||
             normalized.Contains("TURKCE", StringComparison.Ordinal) ||
             normalized.Contains("DIL", StringComparison.Ordinal)) &&
            normalized.Contains("INCELEME", StringComparison.Ordinal) &&
            TryGetDictionaryValue(roleIdByName, out var dilInceleme, "INCELEMEDIL", "DILUZMANI"))
        {
            return dilInceleme;
        }

        if ((normalized.Contains("INCELEME", StringComparison.Ordinal) ||
             normalized.Contains("KONTROL", StringComparison.Ordinal) ||
             normalized.Contains("DEGERLENDIRME", StringComparison.Ordinal) ||
             normalized.Contains("DUZELTME", StringComparison.Ordinal) ||
             normalized.Contains("TTKB", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var alanInceleme, "INCELEMEALAN", "EDITOR"))
        {
            return alanInceleme;
        }

        if ((normalized.Contains("PROGRAM", StringComparison.Ordinal) || normalized.Contains("OKK", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var programUzmani, "PROGRAMGELISTIRMEUZMANI"))
        {
            return programUzmani;
        }

        if ((normalized.Contains("OYKU", StringComparison.Ordinal) ||
             normalized.Contains("HIKAYE", StringComparison.Ordinal) ||
             normalized.Contains("YAZIM", StringComparison.Ordinal) ||
             normalized.Contains("YAZAR", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var yazar, "YAZAR"))
        {
            return yazar;
        }

        if ((normalized.Contains("ALMANCA", StringComparison.Ordinal) ||
             normalized.Contains("INGILIZCE", StringComparison.Ordinal) ||
             normalized.Contains("TURKCE", StringComparison.Ordinal)) &&
            TryGetDictionaryValue(roleIdByName, out var dilUzmani, "DILUZMANI"))
        {
            return dilUzmani;
        }

        return TryGetDictionaryValue(roleIdByName, out var editor, "EDITOR", "INCELEMEALAN")
            ? editor
            : roleIdByName.Values.First();
    }

    private static bool TryGetDictionaryValue(IReadOnlyDictionary<string, int> source, out int value, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (source.TryGetValue(key, out value))
            {
                return true;
            }
        }

        value = 0;
        return false;
    }

    private static DateTime? TryParseDate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var text = value.Trim();
        if (DateTime.TryParse(text, TrCulture, DateTimeStyles.AllowWhiteSpaces, out var parsed))
        {
            return parsed.Date;
        }

        if (DateTime.TryParseExact(text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
        {
            return parsed.Date;
        }

        var normalized = Normalize(text);
        if (normalized.Contains("SURECLI", StringComparison.Ordinal) || normalized.Contains("DEVAM", StringComparison.Ordinal))
        {
            return null;
        }

        var monthMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["OCAK"] = 1,
            ["SUBAT"] = 2,
            ["MART"] = 3,
            ["NISAN"] = 4,
            ["MAYIS"] = 5,
            ["HAZIRAN"] = 6,
            ["TEMMUZ"] = 7,
            ["AGUSTOS"] = 8,
            ["EYLUL"] = 9,
            ["EKIM"] = 10,
            ["KASIM"] = 11,
            ["ARALIK"] = 12
        };

        foreach (var month in monthMap)
        {
            if (normalized.Contains(month.Key, StringComparison.Ordinal))
            {
                var year = ExtractFirstInt(normalized, 4);
                if (year.HasValue)
                {
                    return new DateTime(year.Value, month.Value, DateTime.DaysInMonth(year.Value, month.Value));
                }
            }
        }

        if (TryParseLooseDotDate(text, out var looseDate))
        {
            return looseDate;
        }

        return null;
    }

    private static bool TryParseLooseDotDate(string value, out DateTime date)
    {
        date = default;
        var parts = value.Split(new[] { '.', '/', '-' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length < 3)
        {
            return false;
        }

        if (!int.TryParse(parts[0], out var day) ||
            !int.TryParse(parts[1], out var month) ||
            !int.TryParse(parts[2].Take(4).Aggregate(string.Empty, (a, c) => a + c), out var year))
        {
            return false;
        }

        month = Math.Clamp(month, 1, 12);
        var maxDay = DateTime.DaysInMonth(year, month);
        day = Math.Clamp(day, 1, maxDay);
        date = new DateTime(year, month, day);
        return true;
    }

    private static int? ExtractFirstInt(string text, int minDigits)
    {
        var token = new string(text.Where(char.IsDigit).ToArray());
        if (token.Length < minDigits)
        {
            return null;
        }

        return int.TryParse(token.Substring(0, minDigits), out var value) ? value : null;
    }

    private static string ResolveBranchHint(ExcelTaskRow row, Personel person)
    {
        if (!string.IsNullOrWhiteSpace(row.BranchOrHint))
        {
            return row.BranchOrHint.Trim();
        }

        var existingCoordinatorNames = person.PersonelKomisyonlar
            .Select(x => x.Komisyon?.BagliMerkezKoordinatorluk?.Ad)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (existingCoordinatorNames.Count == 1)
        {
            return existingCoordinatorNames[0]!;
        }

        var normalizedTask = Normalize(row.TaskName);

        if (normalizedTask.Contains("BEDENEGITIMI", StringComparison.Ordinal) ||
            normalizedTask.Contains("SPORORTAOKULLARI", StringComparison.Ordinal) ||
            normalizedTask.Contains("OYUNVEOYUNETKINLIKLERI", StringComparison.Ordinal))
        {
            return "Beden Eğitimi";
        }

        if (normalizedTask.Contains("ALMANCA", StringComparison.Ordinal))
        {
            return "Almanca";
        }

        if (normalizedTask.Contains("INGILIZCE", StringComparison.Ordinal))
        {
            return "İngilizce";
        }

        if (normalizedTask.Contains("TURKCE", StringComparison.Ordinal))
        {
            return "Türkçe";
        }

        if (normalizedTask.Contains("MATEMATIK", StringComparison.Ordinal))
        {
            if (normalizedTask.Contains("1SINIF", StringComparison.Ordinal) ||
                normalizedTask.Contains("2SINIF", StringComparison.Ordinal) ||
                normalizedTask.Contains("3SINIF", StringComparison.Ordinal) ||
                normalizedTask.Contains("4SINIF", StringComparison.Ordinal) ||
                normalizedTask.Contains("ILKOKUL", StringComparison.Ordinal))
            {
                return "İlköğretim Matematik";
            }

            return "Matematik";
        }

        if (normalizedTask.Contains("HAYATBILGISI", StringComparison.Ordinal) ||
            normalizedTask.Contains("BIRLESTIRILMISSINIF", StringComparison.Ordinal))
        {
            return "Sınıf Öğretmenliği";
        }

        if (normalizedTask.Contains("CEVREEGITIMI", StringComparison.Ordinal) ||
            normalizedTask.Contains("IKLIMDEGISIKLIGI", StringComparison.Ordinal) ||
            normalizedTask.Contains("AFETBILINCI", StringComparison.Ordinal))
        {
            return "Fen Bilimleri";
        }

        if (normalizedTask.Contains("FENBILIMLERI", StringComparison.Ordinal))
        {
            return "Fen Bilimleri";
        }

        if (normalizedTask.Contains("SOSYALBILGILER", StringComparison.Ordinal) ||
            normalizedTask.Contains("INKILAP", StringComparison.Ordinal) ||
            normalizedTask.Contains("VATANDASLIK", StringComparison.Ordinal) ||
            normalizedTask.Contains("MEDYAOKURYAZARLIGI", StringComparison.Ordinal) ||
            normalizedTask.Contains("HUKUKVEADALET", StringComparison.Ordinal))
        {
            return "Sosyal Bilgiler";
        }

        if (normalizedTask.Contains("MEBI", StringComparison.Ordinal) ||
            normalizedTask.Contains("YOUTUBE", StringComparison.Ordinal) ||
            normalizedTask.Contains("SOSYALMEDYA", StringComparison.Ordinal) ||
            normalizedTask.Contains("VIDEO", StringComparison.Ordinal))
        {
            return "Radyo-Televizyon";
        }

        if (normalizedTask.Contains("MUZIK", StringComparison.Ordinal))
        {
            return "Müzik";
        }

        if (normalizedTask.Contains("GORSEL", StringComparison.Ordinal) ||
            normalizedTask.Contains("ANIMASYON", StringComparison.Ordinal) ||
            normalizedTask.Contains("GRAFIK", StringComparison.Ordinal) ||
            normalizedTask.Contains("DIZGI", StringComparison.Ordinal))
        {
            return "Grafik ve Fotoğraf / Grafik";
        }

        if (normalizedTask.Contains("OYKUKITABI", StringComparison.Ordinal) ||
            normalizedTask.Contains("YAZAR", StringComparison.Ordinal))
        {
            return "Türk Dili ve Edebiyatı";
        }

        if (!string.IsNullOrWhiteSpace(person.Brans?.Ad))
        {
            return person.Brans.Ad;
        }

        return string.Empty;
    }

    private static Personel? FindClosestPersonnel(IEnumerable<Personel> candidates, ExcelTaskRow row)
    {
        var target = NormalizeFullName(row.Ad, row.Soyad);

        var best = candidates
            .Select(x => new
            {
                Personel = x,
                Score = Similarity(target, NormalizeFullName(x.Ad, x.Soyad))
            })
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Personel.PersonelId)
            .FirstOrDefault();

        return best != null && best.Score >= 0.74m ? best.Personel : null;
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
            "BEDENEGITIMI" => "Beden Eğitimi Birim Koordinatörlüğü",
            "BEDENEGITIMIVESPORORTAOKULLARI" => "Beden Eğitimi Birim Koordinatörlüğü",
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
            "SINIF" => "İlkokul Hayat Bilgisi Birim Koordinatörlüğü",
            "SINIFOGRETMENLIGI" => "İlkokul Hayat Bilgisi Birim Koordinatörlüğü",
            "SOSYALBILGILER" => "Sosyal Bilgiler Birim Koordinatörlüğü",
            "TEKNOLOJIVETASARIM" => "Görsel Tasarım Birim Koordinatörlüğü",
            "TURKDILIVEEDEBIYATI" => "Dil İnceleme Birim Koordinatörlüğü",
            "TURKCE" => "Ortaokul Türkçe Birim Koordinatörlüğü",
            "YASAYANDILLERVELEHCELERKURTCEKURMANCI" => "Dil İnceleme Birim Koordinatörlüğü",
            "OZELEGITIM" => "Uzmanlar Birim Koordinatörlüğü",
            "MEBI" => "Mebi Dijital Birim Koordinatörlüğü",
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

    private static string BuildPersonnelLookupKey(Personel personel)
    {
        var ilAd = personel.GorevliIl?.Ad ?? string.Empty;
        return BuildPersonnelLookupKey(personel.Ad, personel.Soyad, ilAd);
    }

    private static string BuildPersonnelLookupKey(string ad, string soyad, string ilAd)
    {
        return $"{NormalizeFullName(ad, soyad)}|{Normalize(ilAd)}";
    }

    private static string NormalizeFullName(string ad, string soyad)
    {
        return Normalize($"{ad} {soyad}");
    }

    private static string NormalizeBranch(string value)
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

    private static List<ExcelTaskRow> ReadExcel(string excelPath)
    {
        using var package = new ExcelPackage(new FileInfo(excelPath));
        var rows = new List<ExcelTaskRow>();

        foreach (var sheet in package.Workbook.Worksheets)
        {
            if (sheet.Dimension == null)
            {
                continue;
            }

            for (var row = 2; row <= sheet.Dimension.End.Row; row++)
            {
                var ad = NormalizeCellText(sheet.Cells[row, 1].Text);
                var soyad = NormalizeCellText(sheet.Cells[row, 2].Text);
                var il = NormalizeCellText(sheet.Cells[row, 3].Text);
                var roleText = NormalizeCellText(sheet.Cells[row, 6].Text);
                var branch = NormalizeCellText(sheet.Cells[row, 8].Text);
                var taskName = NormalizeCellText(sheet.Cells[row, 10].Text);
                var endDate = NormalizeCellText(sheet.Cells[row, 11].Text);

                if (string.Equals(NormalizeBranch(sheet.Name), "SPOROKULLARI", StringComparison.Ordinal))
                {
                    taskName = NormalizeCellText(sheet.Cells[row, 11].Text);
                    endDate = NormalizeCellText(sheet.Cells[row, 12].Text);
                }

                if (string.IsNullOrWhiteSpace(ad) ||
                    string.IsNullOrWhiteSpace(soyad) ||
                    string.IsNullOrWhiteSpace(taskName))
                {
                    continue;
                }

                rows.Add(new ExcelTaskRow(
                    sheet.Name.Trim(),
                    TitleCase(ad),
                    TitleCase(soyad),
                    TitleCase(il),
                    roleText,
                    branch,
                    taskName,
                    endDate));
            }
        }

        return rows;
    }

    private static string NormalizeCellText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var cleaned = value
            .Replace('\u00A0', ' ')
            .Replace("\r", " ")
            .Replace("\n", " ")
            .Trim();

        while (cleaned.Contains("  ", StringComparison.Ordinal))
        {
            cleaned = cleaned.Replace("  ", " ", StringComparison.Ordinal);
        }

        return cleaned;
    }
}

internal sealed record ExcelTaskRow(
    string Sheet,
    string Ad,
    string Soyad,
    string Il,
    string RoleText,
    string BranchOrHint,
    string TaskName,
    string EndDateText);

internal sealed class ImportReport
{
    public string ExcelPath { get; set; } = string.Empty;
    public int ExcelRowCount { get; set; }
    public int MatchedPersonnelCount { get; set; }
    public int UnmatchedPersonnelCount { get; set; }
    public int UnmatchedCoordinatorCount { get; set; }
    public int CreatedCentralCoordinatorCount { get; set; }
    public int CreatedProvincialCoordinatorCount { get; set; }
    public int CreatedCommissionCount { get; set; }
    public int UpdatedPersonnelAssignmentCount { get; set; }
    public int CreatedTaskCount { get; set; }
    public int SkippedDuplicateCount { get; set; }
    public int UnparsedEndDateCount { get; set; }
    public List<ExcelTaskRow> UnmatchedPersonnel { get; set; } = new();
    public List<ExcelTaskRow> UnmatchedCoordinatorRows { get; set; } = new();
}
