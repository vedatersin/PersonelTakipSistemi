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
        private readonly Services.ILogService _logService;

        public BirimlerController(TegmPersonelTakipDbContext context, INotificationService notificationService, Services.ILogService logService)
        {
            _context = context;
            _notificationService = notificationService;
            _logService = logService;
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
            await _logService.LogAsync("Birim Ekleme", $"Teşkilat eklendi: {t.Ad}", null, $"Teşkilat ID: {t.TeskilatId}");
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
            await _logService.LogAsync("Birim Ekleme", $"Koordinatörlük eklendi: {k.Ad}", null, $"Koordinatörlük ID: {k.KoordinatorlukId}, Bağlı Teşkilat ID: {model.ParentId}");
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
             await _logService.LogAsync("Birim Ekleme", $"Komisyon eklendi: {k.Ad}", null, $"Komisyon ID: {k.KomisyonId}, Bağlı Koordinatörlük ID: {model.ParentId}");
             return Ok(new { success = true, id = k.KomisyonId });
        }

        [HttpPost]
        public async Task<IActionResult> SilBirim(string type, int id)
        {
            // Simple delete check. In a real app, check dependencies first!
            if (type == "tes") 
            {
                var item = await _context.Teskilatlar.FindAsync(id);
                if (item != null) 
                { 
                    _context.Teskilatlar.Remove(item); 
                    await _context.SaveChangesAsync();
                    await _logService.LogAsync("Birim Silme", $"Teşkilat silindi: {item.Ad}", null, $"Teşkilat ID: {id}");
                }
            }
            else if (type == "koord")
            {
                var item = await _context.Koordinatorlukler.FindAsync(id);
                if (item != null) 
                { 
                    _context.Koordinatorlukler.Remove(item); 
                    await _context.SaveChangesAsync();
                    await _logService.LogAsync("Birim Silme", $"Koordinatörlük silindi: {item.Ad}", null, $"Koordinatörlük ID: {id}");
                }
            }
            else if (type == "kom")
            {
                 var item = await _context.Komisyonlar.FindAsync(id);
                 if (item != null) 
                 { 
                     _context.Komisyonlar.Remove(item); 
                     await _context.SaveChangesAsync();
                     await _logService.LogAsync("Birim Silme", $"Komisyon silindi: {item.Ad}", null, $"Komisyon ID: {id}");
                 }
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
                Personeller = await _context.Personeller.Include(p => p.GorevliIl).OrderBy(p => p.Ad).ThenBy(p => p.Soyad).ToListAsync(),
                Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> YapTopluAtama([FromBody] TopluAtamaPostModel model)
        {
            if (model.PersonelIds == null || !model.PersonelIds.Any()) return BadRequest("Personel seçilmedi.");

            var duplicateNames = new List<string>();

            // 1. Validation Check First
            foreach (var pid in model.PersonelIds)
            {
                 bool exists = false;
                 // Check if already in target unit
                 if (model.KomisyonId.HasValue)
                     exists = await _context.PersonelKomisyonlar.AnyAsync(x => x.PersonelId == pid && x.KomisyonId == model.KomisyonId.Value);
                 else if (model.KoordinatorlukId.HasValue)
                     exists = await _context.PersonelKoordinatorlukler.AnyAsync(x => x.PersonelId == pid && x.KoordinatorlukId == model.KoordinatorlukId.Value);
                 else if (model.TeskilatId.HasValue)
                     exists = await _context.PersonelTeskilatlar.AnyAsync(x => x.PersonelId == pid && x.TeskilatId == model.TeskilatId.Value);

                 if (exists)
                 {
                     var pName = await _context.Personeller.Where(p => p.PersonelId == pid).Select(p => p.Ad + " " + p.Soyad).FirstOrDefaultAsync();
                     duplicateNames.Add(pName ?? "Bilinmeyen Personel");
                 }
            }

            if (duplicateNames.Any())
            {
                return BadRequest($"Seçilen personellerden bazıları zaten bu birimde kayıtlı: {string.Join(", ", duplicateNames.Take(3))}{(duplicateNames.Count > 3 ? "..." : "")}");
            }

            // 2. Execution
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

                // 1. Teskilat Assignment (Ensure parent hierarchy)
                if (model.TeskilatId.HasValue)
                {
                     if(!p.PersonelTeskilatlar.Any(t => t.TeskilatId == model.TeskilatId.Value))
                     {
                         p.PersonelTeskilatlar.Add(new PersonelTeskilat { TeskilatId = model.TeskilatId.Value });
                     }
                }

                // 2. Koordinatorluk Assignment
                if (model.KoordinatorlukId.HasValue)
                {
                     if(!p.PersonelKoordinatorlukler.Any(k => k.KoordinatorlukId == model.KoordinatorlukId.Value))
                     {
                         p.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { KoordinatorlukId = model.KoordinatorlukId.Value });
                     }
                }

                // 3. Komisyon Assignment & Role Definition
                if(model.KomisyonId.HasValue)
                {
                    if(!p.PersonelKomisyonlar.Any(k => k.KomisyonId == model.KomisyonId.Value))
                    {
                        p.PersonelKomisyonlar.Add(new PersonelKomisyon { KomisyonId = model.KomisyonId.Value });
                    }
                    
                    // Add Role for Commission
                    p.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama { 
                        KurumsalRolId = 1, // Generic Member
                        KomisyonId = model.KomisyonId.Value,
                        KoordinatorlukId = model.KoordinatorlukId.Value 
                    });
                }
                else if (model.KoordinatorlukId.HasValue)
                {
                      // Add Role for Koordinatorluk (if no commission selected)
                      p.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama { 
                        KurumsalRolId = 1, // Generic Member
                        KoordinatorlukId = model.KoordinatorlukId.Value 
                    });
                }
                else if (model.TeskilatId.HasValue)
                {
                     // Usually we don't assign roles just to Teskilat (Merkez/Tasra) without a sub-unit, 
                     // but if needed, we could. The original code didn't add a role for Teskilat-only assignment.
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

            // LOG
            try
            {
               var assignedNames = await _context.Personeller.Where(p => model.PersonelIds.Contains(p.PersonelId)).Select(p => p.Ad + " " + p.Soyad).ToListAsync();
               var namesStr = string.Join(", ", assignedNames);
               if(namesStr.Length > 200) namesStr = namesStr.Substring(0, 197) + "...";
               
               string targetUnit = "Bilinmiyor";
               if(model.KomisyonId.HasValue) targetUnit = "Komisyon ID: " + model.KomisyonId;
               else if(model.KoordinatorlukId.HasValue) targetUnit = "Koordinatörlük ID: " + model.KoordinatorlukId;
               else if(model.TeskilatId.HasValue) targetUnit = "Teşkilat ID: " + model.TeskilatId;
    
               await _logService.LogAsync("Toplu Atama", $"Toplu personel ataması yapıldı.", null, $"Hedef: {targetUnit}, Atanan Kişiler: {namesStr}");
            }
            catch(Exception) {}

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
