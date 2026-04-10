using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class PersonelAssignmentService : IPersonelAssignmentService
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly ILogService _logService;

        public PersonelAssignmentService(
            TegmPersonelTakipDbContext context,
            INotificationService notificationService,
            ILogService logService)
        {
            _context = context;
            _notificationService = notificationService;
            _logService = logService;
        }

        public async Task<PersonelAssignmentResult> AddTeskilatAsync(int personelId, int teskilatId, int currentUserId, string? currentUserName)
        {
            if (await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == teskilatId))
            {
                return PersonelAssignmentResult.BadRequest("Personel zaten bu teşkilata ekli.");
            }

            _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = teskilatId });
            await _context.SaveChangesAsync();

            var teskilat = await _context.Teskilatlar.FindAsync(teskilatId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: currentUserId,
                baslik: "Teşkilat ataması güncellendi",
                aciklama: $"{currentUserName} tarafından {teskilat?.Ad} teşkilatı eklendi.",
                tip: "KurumsalAtama",
                refType: "Teskilat",
                refId: teskilatId);

            await _logService.LogAsync("Atama", $"Teşkilat eklendi: {teskilat?.Ad}", personelId, $"Teşkilat ID: {teskilatId}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> RemoveTeskilatAsync(int personelId, int teskilatId, int currentUserId, string? currentUserName)
        {
            var personelTeskilat = await _context.PersonelTeskilatlar
                .FirstOrDefaultAsync(x => x.PersonelId == personelId && x.TeskilatId == teskilatId);

            if (personelTeskilat != null)
            {
                _context.PersonelTeskilatlar.Remove(personelTeskilat);

                var koordsToRemove = await _context.PersonelKoordinatorlukler
                    .Where(x => x.PersonelId == personelId && x.Koordinatorluk.TeskilatId == teskilatId)
                    .ToListAsync();

                foreach (var koord in koordsToRemove)
                {
                    await RemoveKoordinatorlukInternalAsync(personelId, koord.KoordinatorlukId);
                }

                await _context.SaveChangesAsync();

                var teskilat = await _context.Teskilatlar.FindAsync(teskilatId);
                await _notificationService.CreateAsync(
                    aliciId: personelId,
                    gonderenId: currentUserId,
                    baslik: "Teşkilat ataması güncellendi",
                    aciklama: $"{currentUserName} tarafından {teskilat?.Ad} teşkilatı kaldırıldı.",
                    tip: "KurumsalAtama",
                    refType: "Teskilat",
                    refId: teskilatId);

                await _logService.LogAsync("Atama", $"Teşkilat çıkarıldı: {teskilat?.Ad}", personelId, $"Teşkilat ID: {teskilatId}");
            }

            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> AddKoordinatorlukAsync(int personelId, int koordinatorlukId, int currentUserId)
        {
            if (await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId))
            {
                return PersonelAssignmentResult.BadRequest("Personel zaten bu koordinatörlüğe ekli.");
            }

            var koordinatorluk = await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            if (koordinatorluk != null && !await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == koordinatorluk.TeskilatId))
            {
                _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = koordinatorluk.TeskilatId });
            }

            _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = koordinatorlukId });
            await _context.SaveChangesAsync();

            koordinatorluk ??= await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: currentUserId,
                baslik: "Koordinatörlük ataması güncellendi",
                aciklama: $"{koordinatorluk?.Ad} eklendi.",
                tip: "KurumsalAtama",
                refType: "Koordinatorluk",
                refId: koordinatorlukId);

            await _logService.LogAsync("Atama", $"Koordinatörlük eklendi: {koordinatorluk?.Ad}", personelId, $"Koordinatörlük ID: {koordinatorlukId}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> RemoveKoordinatorlukAsync(int personelId, int koordinatorlukId, int currentUserId)
        {
            await RemoveKoordinatorlukInternalAsync(personelId, koordinatorlukId);
            await _context.SaveChangesAsync();

            var koordinatorluk = await _context.Koordinatorlukler.FindAsync(koordinatorlukId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: currentUserId,
                baslik: "Koordinatörlük ataması güncellendi",
                aciklama: $"{koordinatorluk?.Ad} kaldırıldı.",
                tip: "KurumsalAtama",
                refType: "Koordinatorluk",
                refId: koordinatorlukId);

            await _logService.LogAsync("Atama", $"Koordinatörlük çıkarıldı: {koordinatorluk?.Ad}", personelId, $"Koordinatörlük ID: {koordinatorlukId}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> AddKomisyonAsync(int personelId, int komisyonId, int currentUserId)
        {
            if (await _context.PersonelKomisyonlar.AnyAsync(x => x.PersonelId == personelId && x.KomisyonId == komisyonId))
            {
                return PersonelAssignmentResult.BadRequest("Personel zaten bu komisyona ekli.");
            }

            var komisyon = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk)
                .FirstOrDefaultAsync(k => k.KomisyonId == komisyonId);

            if (komisyon != null && !await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == komisyon.KoordinatorlukId))
            {
                if (!await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == personelId && x.TeskilatId == komisyon.Koordinatorluk.TeskilatId))
                {
                    _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = komisyon.Koordinatorluk.TeskilatId });
                }

                _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = komisyon.KoordinatorlukId });
            }

            _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personelId, KomisyonId = komisyonId });
            await _context.SaveChangesAsync();

            komisyon ??= await _context.Komisyonlar.FindAsync(komisyonId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: currentUserId,
                baslik: "Komisyon ataması güncellendi",
                aciklama: $"{komisyon?.Ad} eklendi.",
                tip: "KurumsalAtama",
                refType: "Komisyon",
                refId: komisyonId);

            await _logService.LogAsync("Atama", $"Komisyon eklendi: {komisyon?.Ad}", personelId, $"Komisyon ID: {komisyonId}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> RemoveKomisyonAsync(int personelId, int komisyonId, int currentUserId)
        {
            await RemoveKomisyonInternalAsync(personelId, komisyonId);
            await _context.SaveChangesAsync();

            var komisyon = await _context.Komisyonlar.FindAsync(komisyonId);
            await _notificationService.CreateAsync(
                aliciId: personelId,
                gonderenId: currentUserId,
                baslik: "Komisyon ataması güncellendi",
                aciklama: $"{komisyon?.Ad} kaldırıldı.",
                tip: "KurumsalAtama",
                refType: "Komisyon",
                refId: komisyonId);

            await _logService.LogAsync("Atama", $"Komisyon çıkarıldı: {komisyon?.Ad}", personelId, $"Komisyon ID: {komisyonId}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> AddKurumsalRolAsync(int personelId, int kurumsalRolId, int? koordinatorlukId, int? komisyonId, bool force, int currentUserId)
        {
            var rol = await _context.KurumsalRoller.FindAsync(kurumsalRolId);
            if (rol == null)
            {
                return PersonelAssignmentResult.BadRequest("Rol bulunamadı.");
            }

            var tasraRoles = new[] { 3 };
            var merkezRoles = new[] { 5, 4, 10, 9, 8, 7, 6 };
            var exclusiveRoles = new[] { 6, 7, 8, 9, 10 };

            var isRequestingExclusive = exclusiveRoles.Contains(kurumsalRolId);
            var isUnitSelected = koordinatorlukId.HasValue || komisyonId.HasValue;

            var contextInfo = await ResolveContextInfoAsync(koordinatorlukId, komisyonId);

            var existingRoles = await _context.PersonelKurumsalRolAtamalari
                .Where(x => x.PersonelId == personelId)
                .Select(x => x.KurumsalRolId)
                .ToListAsync();

            var hasExistingExclusive = existingRoles.Any(r => exclusiveRoles.Contains(r));
            var hasExistingStandard = existingRoles.Any(r => !exclusiveRoles.Contains(r));

            if (isRequestingExclusive)
            {
                if (hasExistingExclusive || hasExistingStandard)
                {
                    return PersonelAssignmentResult.BadRequest("Uzman, Şef, Şube Müdürü, Daire Başkanı veya Genel Müdür rolleri atanırken personelin başka hiçbir rolü olmamalıdır.");
                }

                if (isUnitSelected)
                {
                    return PersonelAssignmentResult.BadRequest("Bu roller alt birimlere (Koordinatörlük/Komisyon) atanamaz, sadece Merkez teşkilatına doğrudan atanabilir.");
                }
            }
            else if (hasExistingExclusive)
            {
                return PersonelAssignmentResult.BadRequest("Personelin mevcut 'Uzman, Şef, Şube Müdürü, vb.' rolü varken başka bir rol/görev atanamaz.");
            }

            if (isUnitSelected)
            {
                if (contextInfo.IsMerkez && tasraRoles.Contains(kurumsalRolId))
                {
                    return PersonelAssignmentResult.BadRequest("Bu rol sadece Taşra teşkilatına atanabilir.");
                }

                if (contextInfo.IsTasra && merkezRoles.Contains(kurumsalRolId))
                {
                    return PersonelAssignmentResult.BadRequest("Bu rol sadece Merkez teşkilatına atanabilir.");
                }

                if (contextInfo.IsMerkezBirimKoordinatorlugu && new[] { 4, 7, 8, 9, 10 }.Contains(kurumsalRolId))
                {
                    return PersonelAssignmentResult.BadRequest("Bu rol Merkez Birim Koordinatörlüğüne atanamaz.");
                }
            }

            if (rol.Ad == "Komisyon Başkanı" && komisyonId == null)
            {
                return PersonelAssignmentResult.BadRequest("Komisyon seçilmedi.");
            }

            if ((rol.Ad == "İl Koordinatörü" || rol.Ad == "Genel Koordinatör" || rol.Ad == "Merkez Birim Koordinatörü") && koordinatorlukId == null)
            {
                return PersonelAssignmentResult.BadRequest("Koordinatörlük seçilmedi.");
            }

            var presidentWarning = await HandlePresidentAssignmentAsync(personelId, kurumsalRolId, rol.Ad, koordinatorlukId, komisyonId, force);
            if (presidentWarning != null)
            {
                return presidentWarning;
            }

            if (await _context.PersonelKurumsalRolAtamalari.AnyAsync(x =>
                x.PersonelId == personelId &&
                x.KurumsalRolId == kurumsalRolId &&
                x.KoordinatorlukId == koordinatorlukId &&
                x.KomisyonId == komisyonId))
            {
                return PersonelAssignmentResult.BadRequest("Bu rol zaten tanımlı.");
            }

            await EnsureUnitRegistrationsAsync(personelId, koordinatorlukId, komisyonId);

            _context.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama
            {
                PersonelId = personelId,
                KurumsalRolId = kurumsalRolId,
                KoordinatorlukId = koordinatorlukId,
                KomisyonId = komisyonId,
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            var contextName = await ResolveContextNameAsync(koordinatorlukId, komisyonId);
            if (rol.Ad == "Komisyon Başkanı" || rol.Ad == "İl Koordinatörü" || rol.Ad == "Genel Koordinatör")
            {
                await _notificationService.CreateAsync(personelId, currentUserId, "Başkanlık ataması", $"{contextName} için başkan/koordinatör olarak atandınız.", "BaskanDegisimi");
            }
            else
            {
                await _notificationService.CreateAsync(personelId, currentUserId, "Kurumsal rol ataması güncellendi", $"{rol.Ad} rolü {contextName} için eklendi.", "RolAtama");
            }

            await _logService.LogAsync("Kurumsal Rol", $"Kurumsal rol eklendi: {rol.Ad}", personelId, $"Kapsam: {contextName}");
            return PersonelAssignmentResult.SuccessResult();
        }

        public async Task<PersonelAssignmentResult> RemoveKurumsalRolAsync(int assignmentId, int currentUserId)
        {
            var assignment = await _context.PersonelKurumsalRolAtamalari
                .Include(x => x.KurumsalRol)
                .FirstOrDefaultAsync(x => x.Id == assignmentId);

            if (assignment != null)
            {
                if (assignment.KurumsalRol.Ad == "Komisyon Başkanı" && assignment.KomisyonId.HasValue)
                {
                    var komisyon = await _context.Komisyonlar.FindAsync(assignment.KomisyonId.Value);
                    if (komisyon != null && komisyon.BaskanPersonelId == assignment.PersonelId)
                    {
                        komisyon.BaskanPersonelId = null;
                    }
                }
                else if ((assignment.KurumsalRol.Ad == "İl Koordinatörü" || assignment.KurumsalRol.Ad == "Genel Koordinatör") && assignment.KoordinatorlukId.HasValue)
                {
                    var koordinatorluk = await _context.Koordinatorlukler.FindAsync(assignment.KoordinatorlukId.Value);
                    if (koordinatorluk != null && koordinatorluk.BaskanPersonelId == assignment.PersonelId)
                    {
                        koordinatorluk.BaskanPersonelId = null;
                    }
                }

                _context.PersonelKurumsalRolAtamalari.Remove(assignment);
                await _context.SaveChangesAsync();

                await _notificationService.CreateAsync(
                    aliciId: assignment.PersonelId,
                    gonderenId: currentUserId,
                    baslik: "Kurumsal rol ataması güncellendi",
                    aciklama: $"{assignment.KurumsalRol.Ad} rolü kaldırıldı.",
                    tip: "RolAtama");
            }

            return PersonelAssignmentResult.SuccessResult();
        }

        private async Task RemoveKoordinatorlukInternalAsync(int personelId, int koordinatorlukId)
        {
            var personelKoordinatorluk = await _context.PersonelKoordinatorlukler
                .FirstOrDefaultAsync(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId);

            if (personelKoordinatorluk != null)
            {
                _context.PersonelKoordinatorlukler.Remove(personelKoordinatorluk);

                var komisyonlar = await _context.PersonelKomisyonlar
                    .Where(x => x.PersonelId == personelId && x.Komisyon.KoordinatorlukId == koordinatorlukId)
                    .ToListAsync();

                foreach (var komisyon in komisyonlar)
                {
                    await RemoveKomisyonInternalAsync(personelId, komisyon.KomisyonId);
                }

                var rolesToRemove = await _context.PersonelKurumsalRolAtamalari
                    .Where(x => x.PersonelId == personelId && x.KoordinatorlukId == koordinatorlukId)
                    .ToListAsync();

                _context.PersonelKurumsalRolAtamalari.RemoveRange(rolesToRemove);
            }
        }

        private async Task RemoveKomisyonInternalAsync(int personelId, int komisyonId)
        {
            var personelKomisyon = await _context.PersonelKomisyonlar
                .FirstOrDefaultAsync(x => x.PersonelId == personelId && x.KomisyonId == komisyonId);

            if (personelKomisyon != null)
            {
                _context.PersonelKomisyonlar.Remove(personelKomisyon);

                var rolesToRemove = await _context.PersonelKurumsalRolAtamalari
                    .Where(x => x.PersonelId == personelId && x.KomisyonId == komisyonId)
                    .ToListAsync();

                _context.PersonelKurumsalRolAtamalari.RemoveRange(rolesToRemove);
            }
        }

        private async Task EnsureUnitRegistrationsAsync(int personelId, int? koordinatorlukId, int? komisyonId)
        {
            if (komisyonId.HasValue)
            {
                if (!await _context.PersonelKomisyonlar.AnyAsync(pk => pk.PersonelId == personelId && pk.KomisyonId == komisyonId.Value))
                {
                    _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personelId, KomisyonId = komisyonId.Value });

                    var komisyon = await _context.Komisyonlar
                        .Include(k => k.Koordinatorluk)
                        .FirstOrDefaultAsync(k => k.KomisyonId == komisyonId.Value);

                    if (komisyon != null)
                    {
                        if (!await _context.PersonelKoordinatorlukler.AnyAsync(pk => pk.PersonelId == personelId && pk.KoordinatorlukId == komisyon.KoordinatorlukId))
                        {
                            _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = komisyon.KoordinatorlukId });
                        }

                        if (!await _context.PersonelTeskilatlar.AnyAsync(pt => pt.PersonelId == personelId && pt.TeskilatId == komisyon.Koordinatorluk.TeskilatId))
                        {
                            _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = komisyon.Koordinatorluk.TeskilatId });
                        }
                    }
                }
            }
            else if (koordinatorlukId.HasValue && !await _context.PersonelKoordinatorlukler.AnyAsync(pk => pk.PersonelId == personelId && pk.KoordinatorlukId == koordinatorlukId.Value))
            {
                _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personelId, KoordinatorlukId = koordinatorlukId.Value });

                var koordinatorluk = await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value);
                if (koordinatorluk != null && !await _context.PersonelTeskilatlar.AnyAsync(pt => pt.PersonelId == personelId && pt.TeskilatId == koordinatorluk.TeskilatId))
                {
                    _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personelId, TeskilatId = koordinatorluk.TeskilatId });
                }
            }
        }

        private async Task<PersonelAssignmentResult?> HandlePresidentAssignmentAsync(int personelId, int kurumsalRolId, string rolAd, int? koordinatorlukId, int? komisyonId, bool force)
        {
            if (rolAd == "Komisyon Başkanı" && komisyonId.HasValue)
            {
                var komisyon = await _context.Komisyonlar.FindAsync(komisyonId.Value);
                if (komisyon == null)
                {
                    return PersonelAssignmentResult.BadRequest("Komisyon bulunamadı.");
                }

                if (komisyon.BaskanPersonelId != null && komisyon.BaskanPersonelId != personelId)
                {
                    if (!force)
                    {
                        var baskan = await _context.Personeller.FindAsync(komisyon.BaskanPersonelId);
                        return PersonelAssignmentResult.WarningResult($"Bu komisyonun başkanı zaten {baskan?.Ad} {baskan?.Soyad}. Değiştirmek istiyor musunuz?");
                    }

                    var oldRole = await _context.PersonelKurumsalRolAtamalari
                        .FirstOrDefaultAsync(x => x.PersonelId == komisyon.BaskanPersonelId && x.KurumsalRolId == kurumsalRolId && x.KomisyonId == komisyonId);
                    if (oldRole != null)
                    {
                        _context.PersonelKurumsalRolAtamalari.Remove(oldRole);
                    }
                }

                komisyon.BaskanPersonelId = personelId;
                return null;
            }

            if ((rolAd == "İl Koordinatörü" || rolAd == "Genel Koordinatör") && koordinatorlukId.HasValue)
            {
                var koordinatorluk = await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value);
                if (koordinatorluk == null)
                {
                    return PersonelAssignmentResult.BadRequest("Koordinatörlük bulunamadı.");
                }

                if (koordinatorluk.BaskanPersonelId != null && koordinatorluk.BaskanPersonelId != personelId)
                {
                    if (!force)
                    {
                        var baskan = await _context.Personeller.FindAsync(koordinatorluk.BaskanPersonelId);
                        return PersonelAssignmentResult.WarningResult($"Bu koordinatörlüğün başkanı zaten {baskan?.Ad} {baskan?.Soyad}. Değiştirmek istiyor musunuz?");
                    }

                    var oldRole = await _context.PersonelKurumsalRolAtamalari
                        .FirstOrDefaultAsync(x => x.PersonelId == koordinatorluk.BaskanPersonelId && x.KurumsalRolId == kurumsalRolId && x.KoordinatorlukId == koordinatorlukId);
                    if (oldRole != null)
                    {
                        _context.PersonelKurumsalRolAtamalari.Remove(oldRole);
                    }
                }

                koordinatorluk.BaskanPersonelId = personelId;
            }

            return null;
        }

        private async Task<(bool IsTasra, bool IsMerkez, bool IsMerkezBirimKoordinatorlugu)> ResolveContextInfoAsync(int? koordinatorlukId, int? komisyonId)
        {
            if (koordinatorlukId.HasValue)
            {
                var koordinatorluk = await _context.Koordinatorlukler
                    .Include(k => k.Teskilat)
                    .FirstOrDefaultAsync(k => k.KoordinatorlukId == koordinatorlukId.Value);

                if (koordinatorluk?.Teskilat != null)
                {
                    return (
                        koordinatorluk.Teskilat.Tur == "Taşra",
                        koordinatorluk.Teskilat.Tur == "Merkez",
                        koordinatorluk.Ad.Contains("Merkez Birim"));
                }
            }
            else if (komisyonId.HasValue)
            {
                var komisyon = await _context.Komisyonlar
                    .Include(k => k.Koordinatorluk)
                    .ThenInclude(koord => koord.Teskilat)
                    .FirstOrDefaultAsync(k => k.KomisyonId == komisyonId.Value);

                if (komisyon?.Koordinatorluk?.Teskilat != null)
                {
                    return (
                        komisyon.Koordinatorluk.Teskilat.Tur == "Taşra",
                        komisyon.Koordinatorluk.Teskilat.Tur == "Merkez",
                        komisyon.Koordinatorluk.Ad.Contains("Merkez Birim"));
                }
            }

            return (false, false, false);
        }

        private async Task<string> ResolveContextNameAsync(int? koordinatorlukId, int? komisyonId)
        {
            if (koordinatorlukId.HasValue)
            {
                return (await _context.Koordinatorlukler.FindAsync(koordinatorlukId.Value))?.Ad ?? string.Empty;
            }

            if (komisyonId.HasValue)
            {
                return (await _context.Komisyonlar.FindAsync(komisyonId.Value))?.Ad ?? string.Empty;
            }

            return "Genel";
        }
    }
}
