using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PersonelTakipSistemi.Filters;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    [ReadOnlyForHighLevelRoles]
    public class BirimYonetimiController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public BirimYonetimiController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> KordinatorlukYonetimi()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId)) return RedirectToAction("Login", "Account");

            // Coordinator roles: 3 (İl), 5 (Merkez)
            var coordinatingRoles = await _context.PersonelKurumsalRolAtamalari
                .Include(a => a.Koordinatorluk!).ThenInclude(k => k.Il)
                .Where(a => a.PersonelId == userId && (a.KurumsalRolId == 3 || a.KurumsalRolId == 5))
                .ToListAsync();

            if (!coordinatingRoles.Any()) return Forbid();

            // Kombine görünüm (Eğer birden fazla koordinatörlükte görevliyse hepsini kapsayacak şekilde de olabilir 
            // ama genellikle 1 tanedir. Basitlik için ilkini alıyoruz.)
            var role = coordinatingRoles.First();
            if (!role.KoordinatorlukId.HasValue || role.Koordinatorluk == null)
            {
                return Forbid();
            }

            var koordId = role.KoordinatorlukId!.Value;
            var koordinatorluk = role.Koordinatorluk!;

            var model = new KordinatorlukYonetimiViewModel
            {
                KoordinatorlukId = koordId,
                KoordinatorlukAd = koordinatorluk.Ad,
                IlAd = koordinatorluk.Il?.Ad,
                IlId = koordinatorluk.IlId,
                IsMerkez = role.KurumsalRolId == 5
            };

            // 1. Komisyonlar
            IQueryable<Komisyon> komisyonQuery;
            if (model.IsMerkez)
            {
                komisyonQuery = _context.Komisyonlar
                    .Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                    .Where(k => k.BagliMerkezKoordinatorlukId == koordId);
            }
            else
            {
                komisyonQuery = _context.Komisyonlar
                    .Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                    .Where(k => k.KoordinatorlukId == koordId);
            }

            var komisyonlar = await komisyonQuery.ToListAsync();

            model.Komisyonlar = komisyonlar.Select(k => new BirimKartItem
            {
                Id = k.KomisyonId,
                Ad = k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk?.Il != null 
                    ? $"{k.Koordinatorluk.Il.Ad} {k.Ad}" 
                    : k.Ad,
                Tur = "Komisyon",
                IlId = k.Koordinatorluk?.IlId,
                IlAdi = k.Koordinatorluk?.Il?.Ad,
                PersonelSayisi = _context.PersonelKomisyonlar.Count(pk => pk.KomisyonId == k.KomisyonId),
                GorevSayisi = _context.GorevAtamaKomisyonlar.Count(gk => gk.KomisyonId == k.KomisyonId)
            }).ToList();

            // 2. Harita Verisi
            List<int> highlightIlIds = new List<int>();

            if (model.IsMerkez)
            {
                // Merkez koordinatörlüğüne bağlı taşra komisyonlarının illeri
                highlightIlIds = komisyonlar
                    .Where(k => k.Koordinatorluk != null && k.Koordinatorluk.IlId.HasValue)
                    .Select(k => k.Koordinatorluk!.IlId!.Value)
                    .Distinct()
                    .ToList();
            }
            else
            {
                // Taşra koordinatörlüğü ise sadece kendi bulunduğu il
                if (model.IlId.HasValue)
                {
                    highlightIlIds.Add(model.IlId.Value);
                }
            }

            model.HaritaIlleri = await _context.Iller
                .Select(il => new HaritaIlItem
                {
                    IlId = il.IlId,
                    Ad = il.Ad,
                    HasOrganization = highlightIlIds.Contains(il.IlId)
                }).ToListAsync();

            // 3. Merkez Birimi için Özel Personel ve Görevler (Kategorize edilmemiş, doğrudan koordinatörlüğe bağlı)
            if (model.IsMerkez)
            {
                model.MerkezPersonelleri = await _context.PersonelKoordinatorlukler
                    .Include(pk => pk.Personel).ThenInclude(p => p.Brans)
                    .Where(pk => pk.KoordinatorlukId == koordId)
                    .Select(pk => new KomisyonPersonelItem
                    {
                        PersonelId = pk.PersonelId,
                        AdSoyad = pk.Personel.Ad + " " + pk.Personel.Soyad,
                        FotografYolu = pk.Personel.FotografYolu,
                        Brans = pk.Personel.Brans != null ? pk.Personel.Brans.Ad : null
                    }).ToListAsync();

                model.MerkezGorevleri = await _context.GorevAtamaKoordinatorlukler
                    .Include(gk => gk.Gorev).ThenInclude(g => g.GorevDurum)
                    .Where(gk => gk.KoordinatorlukId == koordId)
                    .Select(gk => new KomisyonGorevItem
                    {
                        GorevId = gk.GorevId,
                        Baslik = gk.Gorev.Ad,
                        Durum = gk.Gorev.GorevDurum != null ? gk.Gorev.GorevDurum.Ad : "Bilinmiyor",
                        DurumRenk = gk.Gorev.GorevDurum != null ? gk.Gorev.GorevDurum.Renk : "#6c757d"
                    }).ToListAsync();
            }

            return View(model);
        }

        public IActionResult KomisyonYonetimi(int id)
        {
            // İlgili komisyonun detay sayfasına yönlendirir.
            return RedirectToAction(nameof(KomisyonDetay), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> KomisyonDetay(int id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var isAdminOrManager = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            if (!isAdminOrManager)
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(userId);
                var chairAccess = await _context.PersonelKurumsalRolAtamalari.AsNoTracking()
                    .AnyAsync(a => a.PersonelId == userId && a.KurumsalRolId == 2 && a.KomisyonId == id);

                var coordinatorAccess = scopeIds.Any() && await _context.Komisyonlar
                    .AsNoTracking()
                    .AnyAsync(k =>
                        k.KomisyonId == id &&
                        (scopeIds.Contains(k.KoordinatorlukId) || (k.BagliMerkezKoordinatorlukId.HasValue && scopeIds.Contains(k.BagliMerkezKoordinatorlukId.Value))));

                if (!chairAccess && !coordinatorAccess)
                {
                    return Forbid();
                }
            }

            var komisyon = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk).ThenInclude(k => k.Teskilat)
                .Include(k => k.Koordinatorluk).ThenInclude(k => k.Il)
                .FirstOrDefaultAsync(k => k.KomisyonId == id);

            if (komisyon == null) return NotFound();

            var personeller = await _context.PersonelKomisyonlar
                .Include(pk => pk.Personel).ThenInclude(p => p.GorevliIl)
                .Include(pk => pk.Personel).ThenInclude(p => p.Brans)
                .Include(pk => pk.Personel).ThenInclude(p => p.KadroIl)
                .Include(pk => pk.Personel).ThenInclude(p => p.KadroIlce)
                .Include(pk => pk.Personel).ThenInclude(p => p.PersonelYazilimlar).ThenInclude(py => py.Yazilim)
                .Include(pk => pk.Personel).ThenInclude(p => p.PersonelUzmanliklar).ThenInclude(pu => pu.Uzmanlik)
                .Include(pk => pk.Personel).ThenInclude(p => p.PersonelGorevTurleri).ThenInclude(pg => pg.GorevTuru)
                .Include(pk => pk.Personel).ThenInclude(p => p.PersonelIsNitelikleri).ThenInclude(pi => pi.IsNiteligi)
                .Where(pk => pk.KomisyonId == id)
                .Select(pk => new KomisyonPersonelItem
                {
                    PersonelId = pk.PersonelId,
                    AdSoyad = pk.Personel.Ad + " " + pk.Personel.Soyad,
                    FotografYolu = pk.Personel.FotografYolu,
                    Brans = pk.Personel.Brans != null ? pk.Personel.Brans.Ad : null,
                    Il = pk.Personel.GorevliIl != null ? pk.Personel.GorevliIl.Ad : null,
                    KadroIl = pk.Personel.KadroIl != null ? pk.Personel.KadroIl.Ad : null,
                    KadroIlce = pk.Personel.KadroIlce != null ? pk.Personel.KadroIlce.Ad : null,
                    Yazilimlar = pk.Personel.PersonelYazilimlar.Select(y => y.Yazilim.Ad).ToList(),
                    Uzmanliklar = pk.Personel.PersonelUzmanliklar.Select(u => u.Uzmanlik.Ad).ToList(),
                    GorevTurleri = pk.Personel.PersonelGorevTurleri.Select(g => g.GorevTuru.Ad).ToList(),
                    IsNitelikleri = pk.Personel.PersonelIsNitelikleri.Select(n => n.IsNiteligi.Ad).ToList()
                })
                .OrderBy(p => p.AdSoyad)
                .ToListAsync();

            var gorevler = await _context.GorevAtamaKomisyonlar
                .Include(ga => ga.Gorev).ThenInclude(g => g.GorevDurum)
                .Include(ga => ga.Gorev).ThenInclude(g => g.IsNiteligi)
                .Where(ga => ga.KomisyonId == id)
                .Select(ga => new KomisyonGorevItem
                {
                    GorevId = ga.Gorev.GorevId,
                    Baslik = ga.Gorev.Ad,
                    Durum = ga.Gorev.GorevDurum != null ? ga.Gorev.GorevDurum.Ad : "Bilinmiyor",
                    DurumRenk = ga.Gorev.GorevDurum != null ? ga.Gorev.GorevDurum.Renk : "#6c757d",
                    Kategori = ga.Gorev.IsNiteligi != null ? ga.Gorev.IsNiteligi.Ad : null,
                    BaslangicTarihi = ga.Gorev.BaslangicTarihi,
                    BitisTarihi = ga.Gorev.BitisTarihi
                })
                .OrderByDescending(g => g.BaslangicTarihi)
                .ToListAsync();

            var kategoriGrup = gorevler
                .Where(g => g.Kategori != null)
                .GroupBy(g => g.Kategori!)
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .ToList();

            var durumGrup = gorevler
                .GroupBy(g => g.Durum)
                .Select(g => new { Label = g.Key, Count = g.Count(), Renk = g.First().DurumRenk ?? "#6c757d" })
                .ToList();

            var model = new KomisyonDetayViewModel
            {
                KomisyonId = komisyon.KomisyonId,
                KomisyonAd = komisyon.Ad,
                KoordinatorlukAd = komisyon.Koordinatorluk?.Ad,
                TeskilatAd = komisyon.Koordinatorluk?.Teskilat?.Ad,
                IlAd = komisyon.Koordinatorluk?.Il?.Ad,
                Personeller = personeller,
                Gorevler = gorevler,
                KategoriDagilimi = new ChartDataJson
                {
                    Labels = kategoriGrup.Select(k => k.Label).ToList(),
                    Data = kategoriGrup.Select(k => k.Count).ToList(),
                    Colors = new List<string> { "#696cff", "#ff6384", "#36a2eb", "#ffce56", "#4bc0c0", "#9966ff", "#ff9f43" }
                },
                DurumDagilimi = new ChartDataJson
                {
                    Labels = durumGrup.Select(d => d.Label).ToList(),
                    Data = durumGrup.Select(d => d.Count).ToList(),
                    Colors = durumGrup.Select(d => d.Renk).ToList()
                }
            };

            return View("~/Views/Birimler/KomisyonDetay.cshtml", model);
        }

        private async Task<List<int>> GetCoordinatorScopeIdsAsync(int personelId)
        {
            var roleBasedIds = await _context.PersonelKurumsalRolAtamalari.AsNoTracking()
                .Where(x => x.PersonelId == personelId && x.KoordinatorlukId.HasValue && (x.KurumsalRolId == 3 || x.KurumsalRolId == 5))
                .Select(x => x.KoordinatorlukId!.Value)
                .ToListAsync();

            var memberIds = await _context.PersonelKoordinatorlukler.AsNoTracking()
                .Where(x => x.PersonelId == personelId)
                .Select(x => x.KoordinatorlukId)
                .ToListAsync();

            return roleBasedIds.Concat(memberIds).Distinct().ToList();
        }
    }
}
