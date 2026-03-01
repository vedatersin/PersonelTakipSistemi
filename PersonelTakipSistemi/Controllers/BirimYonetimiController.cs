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
                .Include(a => a.Koordinatorluk).ThenInclude(k => k.Il)
                .Where(a => a.PersonelId == userId && (a.KurumsalRolId == 3 || a.KurumsalRolId == 5))
                .ToListAsync();

            if (!coordinatingRoles.Any()) return Forbid();

            // Kombine görünüm (Eğer birden fazla koordinatörlükte görevliyse hepsini kapsayacak şekilde de olabilir 
            // ama genellikle 1 tanedir. Basitlik için ilkini alıyoruz.)
            var role = coordinatingRoles.First();
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
            return RedirectToAction("KomisyonDetay", "Birimler", new { id = id });
        }
    }
}
