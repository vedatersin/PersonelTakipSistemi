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
                    .Select(x => new IdNamePair
                    {
                        Id = x.TeskilatId,
                        Name = x.Teskilat != null ? x.Teskilat.Ad : "Silinmis",
                        Type = "Teskilat"
                    })
                    .ToListAsync(),
                Koordinatorlukler = await _context.GorevAtamaKoordinatorlukler
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Koordinatorluk)
                    .Select(x => new IdNamePair
                    {
                        Id = x.KoordinatorlukId,
                        Name = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : "Silinmis",
                        Type = "Koordinatorluk"
                    })
                    .ToListAsync(),
                Komisyonlar = await _context.GorevAtamaKomisyonlar
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Komisyon)
                    .ThenInclude(k => k.Koordinatorluk)
                    .ThenInclude(koord => koord.Il)
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
                        Type = "Komisyon"
                    })
                    .ToListAsync(),
                Personeller = await _context.GorevAtamaPersoneller
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Personel)
                    .Select(x => new IdNamePair
                    {
                        Id = x.PersonelId,
                        Name = x.Personel != null ? $"{x.Personel.Ad} {x.Personel.Soyad}" : "Silinmis",
                        Type = "Personel"
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

            var passivePersonels = model.PersonelIds.Any()
                ? await _context.Personeller
                    .Where(p => model.PersonelIds.Contains(p.PersonelId) && !p.AktifMi)
                    .Select(p => $"{p.Ad} {p.Soyad}")
                    .ToListAsync()
                : [];

            if (passivePersonels.Any())
            {
                return GorevCommandResult.BadRequest($"Pasif personel atanamaz: {string.Join(", ", passivePersonels)}");
            }

            var unauthorizedPersonels = model.PersonelIds.Any()
                ? await _context.Personeller
                    .Where(p => model.PersonelIds.Contains(p.PersonelId) &&
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
            foreach (var id in model.TeskilatIds)
            {
                _context.GorevAtamaTeskilatlar.Add(new GorevAtamaTeskilat { GorevId = model.GorevId, TeskilatId = id });
            }

            var existingKoordinatorluk = _context.GorevAtamaKoordinatorlukler.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKoordinatorlukler.RemoveRange(existingKoordinatorluk);
            foreach (var id in model.KoordinatorlukIds)
            {
                _context.GorevAtamaKoordinatorlukler.Add(new GorevAtamaKoordinatorluk { GorevId = model.GorevId, KoordinatorlukId = id });
            }

            var existingKomisyon = _context.GorevAtamaKomisyonlar.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKomisyonlar.RemoveRange(existingKomisyon);
            foreach (var id in model.KomisyonIds)
            {
                _context.GorevAtamaKomisyonlar.Add(new GorevAtamaKomisyon { GorevId = model.GorevId, KomisyonId = id });
            }

            var existingPersonel = _context.GorevAtamaPersoneller.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaPersoneller.RemoveRange(existingPersonel);
            foreach (var id in model.PersonelIds)
            {
                _context.GorevAtamaPersoneller.Add(new GorevAtamaPersonel { GorevId = model.GorevId, PersonelId = id });
            }

            await _context.SaveChangesAsync();

            await LogAssignmentDetailsAsync(model);
            await SendAssignmentNotificationsAsync(model, senderPersonelId);

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

            gorev.GorevDurumId = model.DurumId;
            gorev.DurumAciklamasi = model.Aciklama;
            gorev.UpdatedAt = DateTime.Now;

            _context.GorevDurumGecmisleri.Add(new GorevDurumGecmisi
            {
                GorevId = gorev.GorevId,
                GorevDurumId = model.DurumId,
                Aciklama = model.Aciklama,
                Tarih = DateTime.Now,
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
                .Include(g => g.Kategori)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevAtamaPersoneller)
                .Where(g => g.IsActive && g.GorevAtamaPersoneller.Any(p => p.PersonelId == userId))
                .OrderByDescending(g => g.BaslangicTarihi)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Gorev?> GetDetailAsync(int gorevId)
        {
            return _context.Gorevler
                .Include(g => g.Kategori)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevDurumGecmisleri).ThenInclude(gh => gh.IslemYapanPersonel)
                .Include(g => g.GorevDurumGecmisleri).ThenInclude(gh => gh.GorevDurum)
                .Include(g => g.GorevAtamaTeskilatlar).ThenInclude(t => t.Teskilat)
                .Include(g => g.GorevAtamaKoordinatorlukler).ThenInclude(k => k.Koordinatorluk)
                .Include(g => g.GorevAtamaKomisyonlar).ThenInclude(k => k.Komisyon).ThenInclude(kom => kom.Koordinatorluk).ThenInclude(koord => koord.Il)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(p => p.Personel)
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

                var assignedPersonelNames = model.PersonelIds.Any()
                    ? await _context.Personeller
                        .Where(p => model.PersonelIds.Contains(p.PersonelId))
                        .Select(p => $"{p.Ad} {p.Soyad}")
                        .ToListAsync()
                    : [];

                var assignedUnitNames = new List<string>();
                if (model.TeskilatIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Teskilatlar
                        .Where(t => model.TeskilatIds.Contains(t.TeskilatId))
                        .Select(t => t.Ad)
                        .ToListAsync());
                }

                if (model.KoordinatorlukIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Koordinatorlukler
                        .Where(k => model.KoordinatorlukIds.Contains(k.KoordinatorlukId))
                        .Select(k => k.Ad)
                        .ToListAsync());
                }

                if (model.KomisyonIds.Any())
                {
                    assignedUnitNames.AddRange(await _context.Komisyonlar
                        .Where(k => model.KomisyonIds.Contains(k.KomisyonId))
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

        private async Task SendAssignmentNotificationsAsync(GorevAtamaViewModel model, int? senderPersonelId)
        {
            if (!model.PersonelIds.Any())
            {
                return;
            }

            var taskName = await _context.Gorevler
                .Where(x => x.GorevId == model.GorevId)
                .Select(x => x.Ad)
                .FirstOrDefaultAsync() ?? "Gorev";

            foreach (var personelId in model.PersonelIds)
            {
                await _notificationService.CreateAsync(
                    personelId,
                    senderPersonelId,
                    "Yeni Gorev Atamasi",
                    $"Size '{taskName}' adli gorev atandi.",
                    "GorevAtama",
                    null,
                    null,
                    $"/Gorevler/Detay/{model.GorevId}");
            }
        }
    }
}
