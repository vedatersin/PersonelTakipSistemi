using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public class GorevWorkflowService : IGorevWorkflowService
    {
        private static readonly int[] HighLevelRoleIds = [7, 8, 9, 10];
        private static readonly int[] ResponsibleRoleIds = [2, 3, 5];

        private readonly TegmPersonelTakipDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILogService _logService;

        public GorevWorkflowService(
            TegmPersonelTakipDbContext context,
            INotificationService notificationService,
            ILogService logService)
        {
            _context = context;
            _notificationService = notificationService;
            _logService = logService;
        }

        public async Task<GorevAtamaResultViewModel?> GetAssignmentDataAsync(int gorevId)
        {
            var gorev = await _context.Gorevler.FindAsync(gorevId);
            if (gorev == null)
            {
                return null;
            }

            return new GorevAtamaResultViewModel
            {
                GorevId = gorevId,
                GorevDurumId = gorev.GorevDurumId,
                DurumAciklamasi = gorev.DurumAciklamasi,
                Teskilatlar = await _context.GorevAtamaTeskilatlar
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Teskilat)
                    .Include(x => x.GorevTuru)
                    .Select(x => new IdNamePair
                    {
                        Id = x.TeskilatId,
                        Name = x.Teskilat != null ? x.Teskilat.Ad : "Silinmis",
                        Type = "Teskilat",
                        GorevTuruId = x.GorevTuruId,
                        GorevTuruAd = x.GorevTuru != null ? x.GorevTuru.Ad : null
                    })
                    .ToListAsync(),
                Koordinatorlukler = await _context.GorevAtamaKoordinatorlukler
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Koordinatorluk)
                    .Include(x => x.GorevTuru)
                    .Select(x => new IdNamePair
                    {
                        Id = x.KoordinatorlukId,
                        Name = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : "Silinmis",
                        Type = "Koordinatorluk",
                        GorevTuruId = x.GorevTuruId,
                        GorevTuruAd = x.GorevTuru != null ? x.GorevTuru.Ad : null
                    })
                    .ToListAsync(),
                Komisyonlar = await _context.GorevAtamaKomisyonlar
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Komisyon)
                    .ThenInclude(k => k.Koordinatorluk)
                    .ThenInclude(koord => koord.Il)
                    .Include(x => x.GorevTuru)
                    .Select(x => new IdNamePair
                    {
                        Id = x.KomisyonId,
                        Name = x.Komisyon != null
                            ? (x.Komisyon.BagliMerkezKoordinatorlukId != null &&
                               x.Komisyon.Koordinatorluk != null &&
                               x.Komisyon.Koordinatorluk.Il != null
                                ? $"{x.Komisyon.Koordinatorluk.Il.Ad} Komisyonu"
                                : x.Komisyon.Ad)
                            : "Silinmis",
                        Type = "Komisyon",
                        GorevTuruId = x.GorevTuruId,
                        GorevTuruAd = x.GorevTuru != null ? x.GorevTuru.Ad : null
                    })
                    .ToListAsync(),
                Personeller = await _context.GorevAtamaPersoneller
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Personel)
                    .Include(x => x.GorevTuru)
                    .Select(x => new IdNamePair
                    {
                        Id = x.PersonelId,
                        Name = x.Personel != null ? $"{x.Personel.Ad} {x.Personel.Soyad}" : "Silinmis",
                        Type = "Personel",
                        GorevTuruId = x.GorevTuruId,
                        GorevTuruAd = x.GorevTuru != null ? x.GorevTuru.Ad : null
                    })
                    .ToListAsync()
            };
        }

        public async Task<GorevCommandResult> SaveAssignmentsAsync(GorevAtamaViewModel model, int? senderPersonelId)
        {
            var gorevExists = await _context.Gorevler.AnyAsync(x => x.GorevId == model.GorevId);
            if (!gorevExists)
            {
                return GorevCommandResult.NotFound("Gorev bulunamadi.");
            }

            var allItems = model.Teskilatlar.Concat(model.Koordinatorlukler).Concat(model.Komisyonlar).Concat(model.Personeller).ToList();
            if (allItems.Any(x => x.GorevTuruId <= 0))
            {
                return GorevCommandResult.BadRequest("Lütfen atama yapılacak kişi/birim için Görev Rolü seçiniz.");
            }

            var previousKoordinatorlukIds = await _context.GorevAtamaKoordinatorlukler
                .AsNoTracking()
                .Where(x => x.GorevId == model.GorevId)
                .Select(x => x.KoordinatorlukId)
                .ToListAsync();

            var previousKomisyonIds = await _context.GorevAtamaKomisyonlar
                .AsNoTracking()
                .Where(x => x.GorevId == model.GorevId)
                .Select(x => x.KomisyonId)
                .ToListAsync();

            var previousPersonelIds = await _context.GorevAtamaPersoneller
                .AsNoTracking()
                .Where(x => x.GorevId == model.GorevId)
                .Select(x => x.PersonelId)
                .ToListAsync();

            var personelIds = model.Personeller.Select(x => x.Id).Distinct().ToList();

            var passivePersonels = personelIds.Any()
                ? await _context.Personeller
                    .Where(p => personelIds.Contains(p.PersonelId) && !p.AktifMi)
                    .Select(p => $"{p.Ad} {p.Soyad}")
                    .ToListAsync()
                : [];

            if (passivePersonels.Any())
            {
                return GorevCommandResult.BadRequest($"Pasif personel atanamaz: {string.Join(", ", passivePersonels)}");
            }

            var unauthorizedPersonels = personelIds.Any()
                ? await _context.Personeller
                    .Where(p => personelIds.Contains(p.PersonelId) &&
                                p.PersonelKurumsalRolAtamalari.Any(r => HighLevelRoleIds.Contains(r.KurumsalRolId)))
                    .Select(p => $"{p.Ad} {p.Soyad}")
                    .ToListAsync()
                : [];

            if (unauthorizedPersonels.Any())
            {
                return GorevCommandResult.BadRequest(
                    $"Genel Mudur, Daire Baskani, Sube Muduru ve Sef rollerindeki personeller gorevlere atanamaz: {string.Join(", ", unauthorizedPersonels)}");
            }

            var existingTeskilat = _context.GorevAtamaTeskilatlar.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaTeskilatlar.RemoveRange(existingTeskilat);
            foreach (var item in model.Teskilatlar.DistinctBy(x => x.Id))
            {
                _context.GorevAtamaTeskilatlar.Add(new GorevAtamaTeskilat { GorevId = model.GorevId, TeskilatId = item.Id, GorevTuruId = item.GorevTuruId });
            }

            var existingKoordinatorluk = _context.GorevAtamaKoordinatorlukler.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKoordinatorlukler.RemoveRange(existingKoordinatorluk);
            foreach (var item in model.Koordinatorlukler.DistinctBy(x => x.Id))
            {
                _context.GorevAtamaKoordinatorlukler.Add(new GorevAtamaKoordinatorluk { GorevId = model.GorevId, KoordinatorlukId = item.Id, GorevTuruId = item.GorevTuruId });
            }

            var existingKomisyon = _context.GorevAtamaKomisyonlar.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKomisyonlar.RemoveRange(existingKomisyon);
            foreach (var item in model.Komisyonlar.DistinctBy(x => x.Id))
            {
                _context.GorevAtamaKomisyonlar.Add(new GorevAtamaKomisyon { GorevId = model.GorevId, KomisyonId = item.Id, GorevTuruId = item.GorevTuruId });
            }

            var existingPersonel = _context.GorevAtamaPersoneller.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaPersoneller.RemoveRange(existingPersonel);
            foreach (var item in model.Personeller.DistinctBy(x => x.Id))
            {
                _context.GorevAtamaPersoneller.Add(new GorevAtamaPersonel { GorevId = model.GorevId, PersonelId = item.Id, GorevTuruId = item.GorevTuruId });
            }

            await _context.SaveChangesAsync();

            await LogAssignmentDetailsAsync(model);

            var addedPersonelIds = personelIds.Except(previousPersonelIds).ToList();
            var koordinatorlukIds = model.Koordinatorlukler.Select(x => x.Id).Distinct().ToList();
            var komisyonIds = model.Komisyonlar.Select(x => x.Id).Distinct().ToList();

            var addedKoordinatorlukIds = koordinatorlukIds.Except(previousKoordinatorlukIds).ToList();
            var addedKomisyonIds = komisyonIds.Except(previousKomisyonIds).ToList();

            var recipients = new HashSet<int>(addedPersonelIds);

            if (addedKoordinatorlukIds.Any())
            {
                var koordinatorlukHeads = await _context.Koordinatorlukler
                    .AsNoTracking()
                    .Where(k => addedKoordinatorlukIds.Contains(k.KoordinatorlukId) && k.BaskanPersonelId != null)
                    .Select(k => k.BaskanPersonelId!.Value)
                    .ToListAsync();

                foreach (var id in koordinatorlukHeads)
                {
                    recipients.Add(id);
                }
            }

            if (addedKomisyonIds.Any())
            {
                var komisyonHeads = await _context.Komisyonlar
                    .AsNoTracking()
                    .Where(k => addedKomisyonIds.Contains(k.KomisyonId) && k.BaskanPersonelId != null)
                    .Select(k => k.BaskanPersonelId!.Value)
                    .ToListAsync();

                foreach (var id in komisyonHeads)
                {
                    recipients.Add(id);
                }
            }

            await SendAssignmentNotificationsAsync(model.GorevId, recipients, senderPersonelId);

            return GorevCommandResult.SuccessResult();
        }

        public async Task<GorevCommandResult> UpdateStatusAsync(GorevDurumUpdateViewModel model, int currentUserId, bool isManager)
        {
            var gorev = await _context.Gorevler
                .Include(x => x.GorevAtamaKomisyonlar)
                .Include(x => x.GorevAtamaKoordinatorlukler)
                .FirstOrDefaultAsync(x => x.GorevId == model.GorevId);

            if (gorev == null)
            {
                return GorevCommandResult.NotFound();
            }

            var now = DateTime.Now;
            var normalizedNote = string.IsNullOrWhiteSpace(model.Aciklama) ? null : model.Aciklama.Trim();

            if (isManager)
            {
                var isResponsible = await _context.PersonelKurumsalRolAtamalari.AnyAsync(a =>
                    a.PersonelId == currentUserId &&
                    ResponsibleRoleIds.Contains(a.KurumsalRolId) &&
                    ((a.KomisyonId != null && gorev.GorevAtamaKomisyonlar.Any(t => t.KomisyonId == a.KomisyonId)) ||
                     (a.KoordinatorlukId != null && gorev.GorevAtamaKoordinatorlukler.Any(t => t.KoordinatorlukId == a.KoordinatorlukId))));

                if (!isResponsible)
                {
                    return GorevCommandResult.ApplicationFailure(
                        "Yetki Hatasi: Sadece ilgili birimin Komisyon Baskani veya Koordinatoru durum guncelleyebilir.");
                }
            }

            var lastHistory = await _context.GorevDurumGecmisleri
                .AsNoTracking()
                .Where(x => x.GorevId == model.GorevId)
                .OrderByDescending(x => x.Tarih)
                .FirstOrDefaultAsync();

            if (lastHistory != null)
            {
                var minInterval = TimeSpan.FromMinutes(5);
                var elapsed = now - lastHistory.Tarih;
                if (elapsed < minInterval)
                {
                    var remainingMinutes = (int)Math.Ceiling((minInterval - elapsed).TotalMinutes);
                    remainingMinutes = Math.Max(1, remainingMinutes);
                    return GorevCommandResult.ApplicationFailure(
                        $"Bu görevde çok sık durum güncellenemez. Lütfen {remainingMinutes} dakika sonra tekrar deneyin.");
                }

                var lastNote = string.IsNullOrWhiteSpace(lastHistory.Aciklama) ? null : lastHistory.Aciklama.Trim();
                if (lastHistory.GorevDurumId == model.DurumId &&
                    string.Equals(lastNote, normalizedNote, StringComparison.Ordinal))
                {
                    return GorevCommandResult.ApplicationFailure("Aynı durum bilgisi zaten son kayıtta mevcut. Mükerrer kayıt oluşturulmadı.");
                }
            }

            var currentNote = string.IsNullOrWhiteSpace(gorev.DurumAciklamasi) ? null : gorev.DurumAciklamasi.Trim();
            if (gorev.GorevDurumId == model.DurumId && string.Equals(currentNote, normalizedNote, StringComparison.Ordinal))
            {
                return GorevCommandResult.ApplicationFailure("Durum bilgisi değişmedi. Mükerrer kayıt oluşturulmadı.");
            }

            gorev.GorevDurumId = model.DurumId;
            gorev.DurumAciklamasi = normalizedNote;
            gorev.UpdatedAt = now;

            _context.GorevDurumGecmisleri.Add(new GorevDurumGecmisi
            {
                GorevId = gorev.GorevId,
                GorevDurumId = model.DurumId,
                Aciklama = normalizedNote,
                Tarih = now,
                IslemYapanPersonelId = currentUserId
            });

            await _context.SaveChangesAsync();

            try
            {
                var statusName = await _context.GorevDurumlari
                    .Where(d => d.GorevDurumId == model.DurumId)
                    .Select(d => d.Ad)
                    .FirstOrDefaultAsync() ?? "Bilinmiyor";

                await _logService.LogAsync(
                    "Durum",
                    $"Gorev durumu degistirildi: {gorev.Ad}",
                    null,
                    $"Yeni Durum: {statusName}, Aciklama: {model.Aciklama}");
            }
            catch
            {
            }

            return GorevCommandResult.SuccessResult();
        }

        public async Task<List<GorevSearchEntityResult>> SearchEntitiesAsync(string type, string query)
        {
            query = query.ToLower();

            return type switch
            {
                "Teskilat" => await _context.Teskilatlar
                    .Where(x => x.Ad.ToLower().Contains(query))
                    .Select(x => new GorevSearchEntityResult { Id = x.TeskilatId, Text = x.Ad })
                    .Take(20)
                    .ToListAsync(),
                "Koordinatorluk" => await _context.Koordinatorlukler
                    .Where(x => x.Ad.ToLower().Contains(query))
                    .Select(x => new GorevSearchEntityResult { Id = x.KoordinatorlukId, Text = x.Ad })
                    .Take(20)
                    .ToListAsync(),
                "Komisyon" => await SearchKomisyonlarAsync(query),
                "Personel" => await _context.Personeller
                    .Where(x => x.Ad.ToLower().Contains(query) || x.Soyad.ToLower().Contains(query))
                    .Where(x => !x.PersonelKurumsalRolAtamalari.Any(r => HighLevelRoleIds.Contains(r.KurumsalRolId)))
                    .Select(x => new GorevSearchEntityResult
                    {
                        Id = x.PersonelId,
                        Text = $"{x.Ad} {x.Soyad}{(!x.AktifMi ? " (Pasif)" : string.Empty)}",
                        Disabled = !x.AktifMi
                    })
                    .Take(20)
                    .ToListAsync(),
                _ => []
            };
        }

        public async Task<List<GorevStatusHistoryResult>> GetStatusHistoryAsync(int gorevId)
        {
            return await _context.GorevDurumGecmisleri
                .Include(h => h.IslemYapanPersonel)
                .Include(h => h.GorevDurum)
                .Where(h => h.GorevId == gorevId)
                .OrderByDescending(h => h.Tarih)
                .Select(h => new GorevStatusHistoryResult
                {
                    Id = h.Id,
                    Tarih = h.Tarih.ToString("dd.MM.yyyy HH:mm"),
                    Personel = h.IslemYapanPersonel != null ? $"{h.IslemYapanPersonel.Ad} {h.IslemYapanPersonel.Soyad}" : "Sistem",
                    PersonelAvatar = h.IslemYapanPersonel != null && !string.IsNullOrEmpty(h.IslemYapanPersonel.Ad)
                        ? h.IslemYapanPersonel.Ad.Substring(0, 1) + (h.IslemYapanPersonel.Soyad != null ? h.IslemYapanPersonel.Soyad.Substring(0, 1) : string.Empty)
                        : "-",
                    Durum = h.GorevDurum != null ? h.GorevDurum.Ad : "Bilinmiyor",
                    DurumRenk = h.GorevDurum != null ? (h.GorevDurum.RenkSinifi ?? "bg-secondary") : "bg-secondary",
                    Aciklama = h.Aciklama
                })
                .ToListAsync();
        }

        public async Task<List<Gorev>> GetUserTasksAsync(int userId)
        {
            return await _context.Gorevler
                .Include(g => g.IsNiteligi)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(ap => ap.GorevTuru)
                .Where(g => g.IsActive && g.GorevAtamaPersoneller.Any(p => p.PersonelId == userId))
                .OrderByDescending(g => g.BaslangicTarihi)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Gorev?> GetDetailAsync(int gorevId)
        {
            return _context.Gorevler
                .Include(g => g.IsNiteligi)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevDurumGecmisleri).ThenInclude(gh => gh.IslemYapanPersonel)
                .Include(g => g.GorevDurumGecmisleri).ThenInclude(gh => gh.GorevDurum)
                .Include(g => g.GorevAtamaTeskilatlar).ThenInclude(t => t.Teskilat)
                .Include(g => g.GorevAtamaTeskilatlar).ThenInclude(t => t.GorevTuru)
                .Include(g => g.GorevAtamaKoordinatorlukler).ThenInclude(k => k.Koordinatorluk)
                .Include(g => g.GorevAtamaKoordinatorlukler).ThenInclude(k => k.GorevTuru)
                .Include(g => g.GorevAtamaKomisyonlar).ThenInclude(k => k.Komisyon).ThenInclude(kom => kom.Koordinatorluk).ThenInclude(koord => koord.Il)
                .Include(g => g.GorevAtamaKomisyonlar).ThenInclude(k => k.GorevTuru)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(p => p.Personel)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(p => p.GorevTuru)
                .FirstOrDefaultAsync(x => x.GorevId == gorevId);
        }

        private async Task<List<GorevSearchEntityResult>> SearchKomisyonlarAsync(string query)
        {
            var komisyonlar = await _context.Komisyonlar
                .Include(x => x.Koordinatorluk)
                .ThenInclude(koord => koord.Il)
                .Where(x => x.Ad.ToLower().Contains(query) ||
                            (x.BagliMerkezKoordinatorlukId != null &&
                             x.Koordinatorluk != null &&
                             x.Koordinatorluk.Il != null &&
                             x.Koordinatorluk.Il.Ad.ToLower().Contains(query)))
                .ToListAsync();

            return komisyonlar
                .Select(x => new GorevSearchEntityResult
                {
                    Id = x.KomisyonId,
                    Text = x.BagliMerkezKoordinatorlukId != null && x.Koordinatorluk != null && x.Koordinatorluk.Il != null
                        ? $"{x.Koordinatorluk.Il.Ad} Komisyonu"
                        : x.Ad
                })
                .Take(20)
                .ToList();
        }

        private async Task LogAssignmentDetailsAsync(GorevAtamaViewModel model)
        {
            try
            {
                var gorevName = await _context.Gorevler
                    .Where(x => x.GorevId == model.GorevId)
                    .Select(x => x.Ad)
                    .FirstOrDefaultAsync() ?? "Bilinmeyen Gorev";

                var personelIds = model.Personeller.Select(x => x.Id).Distinct().ToList();
                var assignedPersonelNames = personelIds.Any()
                    ? await _context.Personeller
                        .Where(p => personelIds.Contains(p.PersonelId))
                        .Select(p => $"{p.Ad} {p.Soyad}")
                        .ToListAsync()
                    : [];

                var assignedUnitNames = new List<string>();
                var teskilatIds = model.Teskilatlar.Select(x => x.Id).Distinct().ToList();
                if (teskilatIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Teskilatlar
                        .Where(t => teskilatIds.Contains(t.TeskilatId))
                        .Select(t => t.Ad)
                        .ToListAsync());
                }

                var koordinatorlukIds = model.Koordinatorlukler.Select(x => x.Id).Distinct().ToList();
                if (koordinatorlukIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Koordinatorlukler
                        .Where(k => koordinatorlukIds.Contains(k.KoordinatorlukId))
                        .Select(k => k.Ad)
                        .ToListAsync());
                }

                var komisyonIds = model.Komisyonlar.Select(x => x.Id).Distinct().ToList();
                if (komisyonIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Komisyonlar
                        .Where(k => komisyonIds.Contains(k.KomisyonId))
                        .Select(k => k.Ad)
                        .ToListAsync());
                }

                var logDetail = $"Gorev: {gorevName}. ";
                if (assignedPersonelNames.Any())
                {
                    logDetail += $"Atanan Kisiler: {string.Join(", ", assignedPersonelNames)}. ";
                }

                if (assignedUnitNames.Any())
                {
                    logDetail += $"Atanan Birimler: {string.Join(", ", assignedUnitNames)}. ";
                }

                if (!assignedPersonelNames.Any() && !assignedUnitNames.Any())
                {
                    logDetail += "Tum atamalar kaldirildi.";
                }

                await _logService.LogAsync("Gorev Atama", "Gorev atamalari guncellendi", null, logDetail);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    "Gorev Atama",
                    $"Gorev atamalari guncellendi (GorevID: {model.GorevId}) - Detay olusturulurken hata.",
                    null,
                    $"Hata: {ex.Message}");
            }
        }

        private async Task SendAssignmentNotificationsAsync(int gorevId, IEnumerable<int> recipientIds, int? senderPersonelId)
        {
            var recipients = recipientIds?.Distinct().ToList() ?? new List<int>();
            if (!recipients.Any())
            {
                return;
            }

            var taskName = await _context.Gorevler
                .Where(x => x.GorevId == gorevId)
                .Select(x => x.Ad)
                .FirstOrDefaultAsync() ?? "Gorev";

            foreach (var personelId in recipients)
            {
                await _notificationService.CreateAsync(
                    personelId,
                    senderPersonelId,
                    "Yeni Gorev Atamasi",
                    $"Size '{taskName}' adli gorev atandi.",
                    "GorevAtama",
                    "Gorev",
                    gorevId,
                    $"/Gorevler/Detay/{gorevId}");
            }
        }
    }
}

