using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class BirimlerController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        private readonly INotificationService _notificationService;

        public BirimlerController(TegmPersonelTakipDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // ==============================================================
        // BİRİM AYARLARI
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Ayarlar()
        {
            var model = new BirimAyarlariViewModel
            {
                Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync(),
                Koordinatorlukler = await _context.Koordinatorlukler.Include(x => x.Teskilat).Include(x => x.Il).OrderBy(x => x.Ad).ToListAsync(),
                Komisyonlar = await _context.Komisyonlar.Include(x => x.Koordinatorluk).OrderBy(x => x.Ad).ToListAsync(),
                Branslar = await _context.Branslar.OrderBy(x => x.Ad).ToListAsync()
            };
            
            // Populate Iller via ViewBag for Create Modals
            ViewBag.Iller = await _context.Iller.OrderBy(x => x.Ad).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EkleTeskilat([FromBody] BirimEkleModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Eksik bilgi.");

            var t = new Teskilat { Ad = model.Ad, Aciklama = model.Tanim, CreatedAt = DateTime.Now, IsActive = true };
            _context.Teskilatlar.Add(t);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, id = t.TeskilatId });
        }

        [HttpPost]
        public async Task<IActionResult> EkleKoordinatorluk([FromBody] BirimEkleModel model)
        {
            if (!ModelState.IsValid || model.ParentId == null) return BadRequest("Teşkilat seçimi zorunludur.");

            var k = new Koordinatorluk 
            { 
                Ad = model.Ad, 
                Aciklama = model.Tanim, 
                TeskilatId = model.ParentId.Value,
                IlId = model.IlId ?? 6, // Default Ankara if null?
                CreatedAt = DateTime.Now, 
                IsActive = true 
            };
            _context.Koordinatorlukler.Add(k);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, id = k.KoordinatorlukId });
        }

        [HttpPost]
        public async Task<IActionResult> EkleKomisyon([FromBody] BirimEkleModel model)
        {
             if (!ModelState.IsValid || model.ParentId == null) return BadRequest("Koordinatörlük seçimi zorunludur.");

             var k = new Komisyon
             {
                 Ad = model.Ad,
                 Aciklama = model.Tanim,
                 KoordinatorlukId = model.ParentId.Value,
                 CreatedAt = DateTime.Now,
                 IsActive = true
             };
             _context.Komisyonlar.Add(k);
             await _context.SaveChangesAsync();
             return Ok(new { success = true, id = k.KomisyonId });
        }

        [HttpPost]
        public async Task<IActionResult> SilBirim(string type, int id)
        {
            // Simple delete check. In a real app, check dependencies first!
            if (type == "tes") 
            {
                var item = await _context.Teskilatlar.FindAsync(id);
                if (item != null) { _context.Teskilatlar.Remove(item); await _context.SaveChangesAsync(); }
            }
            else if (type == "koord")
            {
                var item = await _context.Koordinatorlukler.FindAsync(id);
                if (item != null) { _context.Koordinatorlukler.Remove(item); await _context.SaveChangesAsync(); }
            }
            else if (type == "kom")
            {
                 var item = await _context.Komisyonlar.FindAsync(id);
                 if (item != null) { _context.Komisyonlar.Remove(item); await _context.SaveChangesAsync(); }
            }

            return Ok(new { success = true });
        }


        // ==============================================================
        // TOPLU ATAMA
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> TopluAtama()
        {
            var model = new TopluAtamaViewModel
            {
                // Only Active Personnel
                Personeller = await _context.Personeller.Where(p => p.AktifMi).OrderBy(p => p.Ad).ThenBy(p => p.Soyad).ToListAsync(),
                Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> YapTopluAtama([FromBody] TopluAtamaPostModel model)
        {
            if (model.PersonelIds == null || !model.PersonelIds.Any()) return BadRequest("Personel seçilmedi.");

            int count = 0;

            foreach (var pid in model.PersonelIds)
            {
                var p = await _context.Personeller
                    .Include(x => x.PersonelKurumsalRolAtamalari)
                    .Include(x => x.PersonelKoordinatorlukler)
                    .Include(x => x.PersonelTeskilatlar)
                    .Include(x => x.PersonelKomisyonlar)
                    .FirstOrDefaultAsync(x => x.PersonelId == pid);

                if (p == null) continue;

                // 1. Add Explicit Role (Generic Member Role)
                // Assuming Role ID 1 is "Personel" (Member)
                // We need to check if we should add implicit relations
                
                // Add to Komisyon
                if(model.KomisyonId.HasValue)
                {
                    if(!p.PersonelKomisyonlar.Any(k => k.KomisyonId == model.KomisyonId.Value))
                    {
                        p.PersonelKomisyonlar.Add(new PersonelKomisyon { KomisyonId = model.KomisyonId.Value });
                    }
                    // Role
                    p.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama { 
                        KurumsalRolId = 1, // Generic Member
                        KomisyonId = model.KomisyonId.Value,
                        KoordinatorlukId = model.KoordinatorlukId.Value 
                    });
                }
                // Add to Koordinatorluk
                else if (model.KoordinatorlukId.HasValue)
                {
                     if(!p.PersonelKoordinatorlukler.Any(k => k.KoordinatorlukId == model.KoordinatorlukId.Value))
                     {
                         p.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { KoordinatorlukId = model.KoordinatorlukId.Value });
                     }
                      p.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama { 
                        KurumsalRolId = 1, // Generic Member
                        KoordinatorlukId = model.KoordinatorlukId.Value 
                    });
                }
                // Add to Teskilat (Rare but supported)
                else if (model.TeskilatId.HasValue)
                {
                     if(!p.PersonelTeskilatlar.Any(t => t.TeskilatId == model.TeskilatId.Value))
                     {
                         p.PersonelTeskilatlar.Add(new PersonelTeskilat { TeskilatId = model.TeskilatId.Value });
                     }
                }
                
                count++;

                // BİLDİRİM GÖNDER (Her başarılı atama için)
                try 
                {
                    string birimAd = "";
                    string rol = "Personel"; // Default role added is 1 (Personel/Member)
                    
                    if (model.KomisyonId.HasValue) 
                    {
                         var k = await _context.Komisyonlar.FindAsync(model.KomisyonId.Value);
                         if(k != null) birimAd = k.Ad + " Komisyonu";
                    }
                    else if (model.KoordinatorlukId.HasValue)
                    {
                         var k = await _context.Koordinatorlukler.FindAsync(model.KoordinatorlukId.Value);
                         if(k != null) birimAd = k.Ad + " Koordinatörlüğü";
                    }
                    else if (model.TeskilatId.HasValue)
                    {
                         var t = await _context.Teskilatlar.FindAsync(model.TeskilatId.Value);
                         if(t != null) birimAd = t.Ad + " Teşkilatı";
                    }

                    if (!string.IsNullOrEmpty(birimAd))
                    {
                        var message = $"Sayın {p.Ad} {p.Soyad}, {birimAd} bünyesinde {rol} olarak görevlendirildiniz.";
                        // Sender: System (null) or Current Admin? User didn't specify sender, just that it should be sent.
                        // Let's use null for System.
                        await _notificationService.CreateAsync(p.PersonelId, null, "Görev Atama", message, "KurumsalAtama", "General", model.KomisyonId ?? model.KoordinatorlukId ?? model.TeskilatId);
                    }
                }
                catch (Exception) { /* Fail silently for notification to not block assignment logic */ }
            }

            await _context.SaveChangesAsync();
            return Ok(new { success = true, count = count });
        }

        // ==============================================================
        // HELPER API
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> GetKoordinatorlukler(int teskilatId)
        {
            var data = await _context.Koordinatorlukler
                .Where(x => x.TeskilatId == teskilatId && x.IsActive)
                .Select(x => new { x.KoordinatorlukId, x.Ad })
                .OrderBy(x => x.Ad)
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetKomisyonlar(int koordinatorlukId)
        {
            var data = await _context.Komisyonlar
                .Where(x => x.KoordinatorlukId == koordinatorlukId && x.IsActive)
                .Select(x => new { x.KomisyonId, x.Ad })
                .OrderBy(x => x.Ad)
                .ToListAsync();
            return Ok(data);
        }
    }
}
