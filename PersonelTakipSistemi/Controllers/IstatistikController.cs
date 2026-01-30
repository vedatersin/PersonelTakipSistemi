using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class IstatistikController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public IstatistikController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IstatistikViewModel();

            // 1. Temel Sayılar
            model.ToplamPersonel = await _context.Personeller.CountAsync(p => p.AktifMi);
            model.ToplamKomisyon = await _context.Komisyonlar.CountAsync();
            model.ToplamGorev = await _context.PersonelKurumsalRolAtamalari.CountAsync();
            
            // 2. Cinsiyet Dağılımı
            model.KadinPersonelSayisi = await _context.Personeller.CountAsync(p => p.AktifMi && p.PersonelCinsiyet);
            model.ErkekPersonelSayisi = await _context.Personeller.CountAsync(p => p.AktifMi && !p.PersonelCinsiyet);

            // 3. Komisyon Dolulukları (Komisyon başına üye sayısı)
            // Varsayım: İdeal komisyon üye sayısı 10 olsun.
            var komisyonStats = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk)
                .Select(k => new
                {
                    k.Ad,
                    Birim = k.Koordinatorluk.Ad,
                    UyeSayisi = k.PersonelKomisyonlar.Count + _context.PersonelKurumsalRolAtamalari.Count(a => a.KomisyonId == k.KomisyonId) // Hem üyeler hem atamalar (Tekrarı önlemek lazım ama basit tutalım)
                                // Aslında PersonelKurumsalRolAtamalari daha doğru kaynak oldu.
                })
                .ToListAsync();

            // Atamalardan sayalım sadece (daha güvenli, yapımız değişti)
            var roleBasedCounts = await _context.PersonelKurumsalRolAtamalari
                .Where(a => a.KomisyonId != null)
                .GroupBy(a => a.KomisyonId)
                .Select(g => new { KomId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.KomId!.Value, x => x.Count);

            var komisyonList = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk)
                .Select(k => new { k.KomisyonId, k.Ad, Birim = k.Koordinatorluk.Ad })
                .ToListAsync();

            model.KomisyonDoluluklari = komisyonList.Select(k => new KomisyonDoluluk
            {
                KomisyonAd = k.Ad,
                BagliBirim = k.Birim,
                UyeSayisi = roleBasedCounts.ContainsKey(k.KomisyonId) ? roleBasedCounts[k.KomisyonId] : 0,
                DolulukYuzdesi = (roleBasedCounts.ContainsKey(k.KomisyonId) ? roleBasedCounts[k.KomisyonId] : 0) * 10 // %10 per person (max 10)
            }).OrderByDescending(x => x.UyeSayisi).Take(10).ToList();

            // 4. Teşkilat/Koordinatörlük Dağılımı
            var koordCounts = await _context.PersonelKurumsalRolAtamalari
                .Where(a => a.KoordinatorlukId != null)
                .GroupBy(a => a.KoordinatorlukId)
                .Select(g => new { KoordId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.KoordId!.Value, x => x.Count);
            
            var koordList = await _context.Koordinatorlukler
                 .Select(k => new { k.KoordinatorlukId, k.Ad })
                 .ToListAsync();

            model.TeskilatDagilimlari = koordList.Select(k => new TeskilatDagilimi
            {
                BirimAd = k.Ad,
                PersonelSayisi = koordCounts.ContainsKey(k.KoordinatorlukId) ? koordCounts[k.KoordinatorlukId] : 0
            }).OrderByDescending(x => x.PersonelSayisi).ToList();


            return View(model);
        }
    }
}
