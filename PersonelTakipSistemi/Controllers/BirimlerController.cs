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
                Branslar = await _context.Branslar.OrderBy(x => x.Ad).ToListAsync(),
                DaireBaskanliklari = await _context.DaireBaskanliklari.Include(x => x.Teskilatlar).OrderBy(x => x.Id).ToListAsync()
            };
            
            // Populate Iller via ViewBag for Create Modals
            ViewBag.Iller = await _context.Iller.OrderBy(x => x.Ad).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EkleDaire([FromBody] BirimEkleModel model)
        {
            if (string.IsNullOrEmpty(model.Ad)) return BadRequest("Ad zorunludur.");
            
            var daire = new DaireBaskanligi { Ad = model.Ad, IsActive = false }; // Default passive
            _context.DaireBaskanliklari.Add(daire);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, id = daire.Id, ad = daire.Ad });
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleDaire([FromBody] BirimEkleModel model)
        {
            if (model.Id == null || string.IsNullOrEmpty(model.Ad)) return BadRequest("Id ve Ad zorunludur.");
            
            var daire = await _context.DaireBaskanliklari.FindAsync(model.Id);
            if (daire == null) return NotFound();

            daire.Ad = model.Ad;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SilDaire(int id)
        {
            var daire = await _context.DaireBaskanliklari.FindAsync(id);
            if (daire == null) return NotFound();

            // Check if it's the protected one (ID 9) or any logic?
            // User said "eklenip silinebilsin", but "Programlar... aktif olacak".
            // Maybe prevent deleting ID 9 if it's special.
            if(daire.Id == 9) return BadRequest("Bu daire başkanlığı silinemez.");

            _context.DaireBaskanliklari.Remove(daire);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EkleTeskilat([FromBody] BirimEkleModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Eksik bilgi.");

            // Use ParentId as DaireBaskanligiId
            var t = new Teskilat 
            { 
                Ad = model.Ad, 
                Aciklama = model.Tanim, 
                DaireBaskanligiId = model.ParentId, // Nullable
                CreatedAt = DateTime.Now, 
                IsActive = true 
            };
            _context.Teskilatlar.Add(t);
            await _context.SaveChangesAsync();
            await _logService.LogAsync("Birim Ekleme", $"Teşkilat eklendi: {t.Ad}", null, null);
            return Ok(new { success = true, id = t.TeskilatId });
        }

        [HttpPost]
        public async Task<IActionResult> EkleKoordinatorluk([FromBody] BirimEkleModel model)
        {
            if (!ModelState.IsValid || model.ParentId == null) return BadRequest("Teşkilat seçimi zorunludur.");

            // 1. Taşra (Id: 2) Logic
            if (model.ParentId == 2)
            {
                 // Is Tasra -> TasraTeskilatiVarMi is likely false or irrelevant
                 model.TasraTeskilatiVarMi = false; 

                 // Get City Name & Auto-Format Name
                 var il = await _context.Iller.FindAsync(model.IlId);
                 if (il == null) return BadRequest("Geçersiz İl.");
                 
                 model.Ad = $"{il.Ad} İl Koordinatörlüğü";
                 
                 // Check Duplicate for City
                 var exists = await _context.Koordinatorlukler.AnyAsync(x => x.IlId == model.IlId && x.TeskilatId == 2);
                 if(exists) return BadRequest("Bu il için zaten bir koordinatörlük mevcut.");
            }
            else
            {
                // 2. Merkez (Id: 1) or Others
                if (string.IsNullOrEmpty(model.Ad)) return BadRequest("Koordinatörlük adı zorunludur.");
                
                // Name Formatting: "insan kaynakları" -> "İnsan Kaynakları Birim Koordinatörlüğü"
                var culture = new System.Globalization.CultureInfo("tr-TR");
                var textInfo = culture.TextInfo;
                var rawName = textInfo.ToTitleCase(model.Ad.ToLower(culture)).Trim();
                
                // Add suffix if not exists
                if (!rawName.EndsWith("Koordinatörlüğü", StringComparison.OrdinalIgnoreCase))
                {
                    rawName += " Birim Koordinatörlüğü";
                }
                model.Ad = rawName;
            }

            var k = new Koordinatorluk 
            { 
                Ad = model.Ad, 
                Aciklama = model.Tanim, 
                TeskilatId = model.ParentId.Value,
                IlId = model.IlId ?? 6, // Default Ankara for Merkez
                TasraTeskilatiVarMi = model.ParentId == 1 ? (model.TasraTeskilatiVarMi ?? true) : false,
                CreatedAt = DateTime.Now, 
                IsActive = true 
            };
            _context.Koordinatorlukler.Add(k);
            await _context.SaveChangesAsync();
            await _logService.LogAsync("Birim Ekleme", $"Koordinatörlük eklendi: {k.Ad}", null, null);
            return Ok(new { success = true, id = k.KoordinatorlukId, ad = k.Ad, aciklama = k.Aciklama });
        }

        [HttpPost]
        public async Task<IActionResult> EkleKomisyon([FromBody] BirimEkleModel model)
        {
             if (!ModelState.IsValid || model.ParentId == null) return BadRequest("Koordinatörlük seçimi zorunludur.");

             // ==========================================================================================
             // CASE A: Adding to Central Unit (Merkez) BUT for a specific City (Taşra) -> REDIRECTION
             // ==========================================================================================
             if (model.IlId.HasValue && model.IlId.Value > 0)
             {
                 // Logic: User selected "Fen Bilimleri" (Central) and "Ankara" (City).
                 // We must find "Ankara İl Koordinatörlüğü" and add the commission THERE.
                 // And link it back to "Fen Bilimleri".

                 var centralUnit = await _context.Koordinatorlukler.FindAsync(model.ParentId.Value);
                 if (centralUnit == null) return BadRequest("Merkez birim bulunamadı.");

                 // Find Provincial Coordinator for this City
                 // Assuming standard naming convention or ID structure. 
                 // We know Taşra TeskilatId = 2.
                 var provincialUnit = await _context.Koordinatorlukler
                                         .FirstOrDefaultAsync(k => k.TeskilatId == 2 && k.IlId == model.IlId.Value);
                 
                 if (provincialUnit == null)
                 {
                     // Attempt to find by Name pattern if IlId not directly stored on all (though we added it)
                     // Fallback: This might fail if "Ankara İl Koordinatörlüğü" doesn't exist.
                     return BadRequest("Seçilen ilde bir İl Koordinatörlüğü bulunamadı. Önce İl Koordinatörlüğünü ekleyiniz.");
                 }

                 // SWAP PARENT
                 model.BagliMerkezKoordinatorlukId = model.ParentId; // Link provided parent as "Related Central Unit"
                 model.ParentId = provincialUnit.KoordinatorlukId;   // Set actual parent to Provincial Unit
             }

             // ==========================================================================================
             // CASE B: Standard Logic or Redirected Logic (Prepare Name and Data)
             // ==========================================================================================
             
             // Logic for Linked Central Unit (Taşra -> Merkez Bağlantısı)
             if (model.BagliMerkezKoordinatorlukId.HasValue && model.BagliMerkezKoordinatorlukId.Value > 0)
             {
                  var merkez = await _context.Koordinatorlukler.FindAsync(model.BagliMerkezKoordinatorlukId.Value);
                  if (merkez == null) return BadRequest("Seçilen Merkez Birim bulunamadı.");
                  
                  // Name Generation: "Fen Bilimleri Birim Koordinatörlüğü" -> "Fen Bilimleri Komisyonu"
                  // User Requirement: 
                  // 1. In Central List: Show "Antalya Komisyonu" (Handled by Frontend View Logic)
                  // 2. In Provincial List: Show "Fen Bilimleri Komisyonu" (Stored DB Name)
                  
                  string baseName = merkez.Ad
                    .Replace("Birim Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("Koordinatörlüğü", "", StringComparison.OrdinalIgnoreCase)
                    .Trim();
                  
                  model.Ad = $"{baseName} Komisyonu";
              }
              else
              {
                  if (string.IsNullOrEmpty(model.Ad)) return BadRequest("Komisyon adı zorunludur.");
              }

             var k = new Komisyon
             {
                 Ad = model.Ad,
                 Aciklama = model.Tanim,
                 KoordinatorlukId = model.ParentId.Value,
                 BagliMerkezKoordinatorlukId = model.BagliMerkezKoordinatorlukId > 0 ? model.BagliMerkezKoordinatorlukId : null,
                 CreatedAt = DateTime.Now,
                 IsActive = true
             };
             _context.Komisyonlar.Add(k);
             await _context.SaveChangesAsync();
             await _logService.LogAsync("Birim Ekleme", $"Komisyon eklendi: {k.Ad}", null, null);
             return Ok(new { success = true, id = k.KomisyonId, ad = k.Ad, aciklama = k.Aciklama });
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleTeskilat([FromBody] BirimEkleModel model)
        {
            if (model.Id == null || string.IsNullOrEmpty(model.Ad)) return BadRequest("Id ve Ad zorunludur.");
            
            var item = await _context.Teskilatlar.FindAsync(model.Id);
            if (item == null) return NotFound();

            item.Ad = model.Ad;
            if(model.Tanim != null) item.Aciklama = model.Tanim; // Optional update if sent
            
            await _context.SaveChangesAsync();
            await _logService.LogAsync("Birim Güncelleme", $"Teşkilat güncellendi: {item.Ad}", null, null);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleKoordinatorluk([FromBody] BirimEkleModel model)
        {
            if (model.Id == null || string.IsNullOrEmpty(model.Ad)) return BadRequest("Id ve Ad zorunludur.");

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try 
                {
                    var item = await _context.Koordinatorlukler.FindAsync(model.Id);
                    if (item == null) return NotFound();

                    item.Ad = model.Ad;
                    if (model.Tanim != null) item.Aciklama = model.Tanim;

                    List<int> deletedKomisyonIds = new List<int>();

                    // Check for Tasra Organization Toggle
                    // Only applicable for Merkez Units (typically) but we check model input
                    if (model.TasraTeskilatiVarMi.HasValue) 
                    {
                        bool oldState = item.TasraTeskilatiVarMi;
                        bool newState = model.TasraTeskilatiVarMi.Value;
                        item.TasraTeskilatiVarMi = newState;

                        // If switching OFF (true -> false), delete linked provincial commissions
                        if (oldState && !newState)
                        {
                            // Find commissions linked via BagliMerkezKoordinatorlukId
                            var linkedCommissions = await _context.Komisyonlar
                                .Where(k => k.BagliMerkezKoordinatorlukId == item.KoordinatorlukId)
                                .ToListAsync();

                            if (linkedCommissions.Any())
                            {
                                deletedKomisyonIds = linkedCommissions.Select(k => k.KomisyonId).ToList();
                                _context.Komisyonlar.RemoveRange(linkedCommissions);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await _logService.LogAsync("Birim Güncelleme", $"Koordinatörlük güncellendi: {item.Ad} (Taşra: {item.TasraTeskilatiVarMi})", null, null);
                    
                    await transaction.CommitAsync();
                    
                    return Ok(new { success = true, ad = item.Ad, deletedKomisyonIds = deletedKomisyonIds });
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest("İşlem sırasında bir hata oluştu: " + ex.Message);
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> GuncelleKomisyon([FromBody] BirimEkleModel model)
        {
             if (model.Id == null || string.IsNullOrEmpty(model.Ad)) return BadRequest("Id ve Ad zorunludur.");

            var item = await _context.Komisyonlar.FindAsync(model.Id);
            if (item == null) return NotFound();

            item.Ad = model.Ad;
            if (model.Tanim != null) item.Aciklama = model.Tanim;
            if (model.ParentId.HasValue) item.KoordinatorlukId = model.ParentId.Value;

            await _context.SaveChangesAsync();
            await _logService.LogAsync("Birim Güncelleme", $"Komisyon güncellendi: {item.Ad}", null, null);
            return Ok();
        }

        [HttpPost]
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> SilBirim(string type, int id)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<IActionResult>(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (type == "tes") 
                    {
                        var item = await _context.Teskilatlar.FindAsync(id);
                        if (item != null) 
                        { 
                            // 1. Delete related role assignments
                            var roles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.TeskilatId == id).ToListAsync();
                            _context.PersonelKurumsalRolAtamalari.RemoveRange(roles);
    
                            _context.Teskilatlar.Remove(item); 
                            await _context.SaveChangesAsync();
                            await _logService.LogAsync("Birim Silme", $"Teşkilat silindi: {item.Ad}", null, null);
                        }
                    }
                    else if (type == "koord")
                    {
                        var item = await _context.Koordinatorlukler.FindAsync(id);
                        if (item != null) 
                        { 
                            // 1. Delete related role assignments
                            var roles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.KoordinatorlukId == id).ToListAsync();
                            if(roles.Any()) _context.PersonelKurumsalRolAtamalari.RemoveRange(roles);
    
                            // 2. Delete related Commissions
                            // A) Direct Children
                            var komis = await _context.Komisyonlar.Where(k => k.KoordinatorlukId == id).ToListAsync();
                            
                            // B) Linked Provincial Commissions (BagliMerkezKoordinatorlukId == id)
                            var linkedKomis = await _context.Komisyonlar.Where(k => k.BagliMerkezKoordinatorlukId == id).ToListAsync();
                            
                            // Combine list
                            var allKomisToDelete = komis.Union(linkedKomis).Distinct().ToList();

                            if(allKomisToDelete.Any()) {
                                // Delete roles for these commissions too
                                var komIds = allKomisToDelete.Select(k => k.KomisyonId).ToList();
                                var komRoles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.KomisyonId.HasValue && komIds.Contains(r.KomisyonId.Value)).ToListAsync();
                                if(komRoles.Any()) _context.PersonelKurumsalRolAtamalari.RemoveRange(komRoles);
                                
                                _context.Komisyonlar.RemoveRange(allKomisToDelete);
                            }
    
                            _context.Koordinatorlukler.Remove(item); 
                            await _context.SaveChangesAsync();
                            await _logService.LogAsync("Birim Silme", $"Koordinatörlük silindi: {item.Ad}", null, null);
                        }
                    }
                    else if (type == "kom")
                    {
                         var item = await _context.Komisyonlar.FindAsync(id);
                         if (item != null) 
                         { 
                             // 1. Delete related role assignments
                             var roles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.KomisyonId == id).ToListAsync();
                             if(roles.Any()) _context.PersonelKurumsalRolAtamalari.RemoveRange(roles);
    
                             _context.Komisyonlar.Remove(item); 
                             await _context.SaveChangesAsync();
                             await _logService.LogAsync("Birim Silme", $"Komisyon silindi: {item.Ad}", null, null);
                         }
                    }
                    else if (type == "daire")
                    {
                        var item = await _context.DaireBaskanliklari.FindAsync(id);
                        if (item != null) 
                        { 
                            // --- CASCADING DELETE START ---
                            // 1. Find and delete related Teskilat (Organization) records
                            var relatedTeskilats = await _context.Teskilatlar.Where(t => t.DaireBaskanligiId == id).ToListAsync();
                            if (relatedTeskilats.Any())
                            {
                                // For each Teskilat, we might need to delete its related data (Roles, Koordinatorluks)
                                // Standard practice: First delete children, then parent.
                                
                                // A. Delete Role Assignments for these Teskilats
                                var tIds = relatedTeskilats.Select(t => t.TeskilatId).ToList();
                                var tRoles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.TeskilatId.HasValue && tIds.Contains(r.TeskilatId.Value)).ToListAsync();
                                if(tRoles.Any()) _context.PersonelKurumsalRolAtamalari.RemoveRange(tRoles);

                                // B. Check and delete Koordinatorluks if exists
                                var hasNested = await _context.Koordinatorlukler.AnyAsync(k => tIds.Contains(k.TeskilatId));
                                if (hasNested) 
                                {
                                    var coords = await _context.Koordinatorlukler.Where(k => tIds.Contains(k.TeskilatId)).ToListAsync();
                                    var cIds = coords.Select(c => c.KoordinatorlukId).ToList();

                                    // B.1 Delete Roles for Coords
                                    var cRoles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.KoordinatorlukId.HasValue && cIds.Contains(r.KoordinatorlukId.Value)).ToListAsync();
                                    _context.PersonelKurumsalRolAtamalari.RemoveRange(cRoles);

                                    // B.2 Delete Commissions under Coords
                                    var comms = await _context.Komisyonlar.Where(k => cIds.Contains(k.KoordinatorlukId)).ToListAsync();
                                    if(comms.Any())
                                    {
                                        var tomIds = comms.Select(x=>x.KomisyonId).ToList();
                                        var tomRoles = await _context.PersonelKurumsalRolAtamalari.Where(r => r.KomisyonId.HasValue && tomIds.Contains(r.KomisyonId.Value)).ToListAsync();
                                         _context.PersonelKurumsalRolAtamalari.RemoveRange(tomRoles);
                                    }
                                    _context.Komisyonlar.RemoveRange(comms);

                                    // B.3 Delete Coords
                                    _context.Koordinatorlukler.RemoveRange(coords);
                                }

                                // C. Delete Teskilats
                                _context.Teskilatlar.RemoveRange(relatedTeskilats);
                            }
                            // --- CASCADING DELETE END ---

                            _context.DaireBaskanliklari.Remove(item); 
                            await _context.SaveChangesAsync();
                            await _logService.LogAsync("Birim Silme", $"Daire Başkanlığı ve alt birimleri silindi: {item.Ad}", null, null);
                        }
                    }
                    
                    await transaction.CommitAsync();
                    return Ok();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    var sqlEx = ex.InnerException as Microsoft.Data.SqlClient.SqlException;
                    if (sqlEx != null && sqlEx.Number == 547)
                    {
                        return BadRequest("Bu birime bağlı silinemeyen alt kayıtlar var. (Örn: Görev Atamaları)");
                    }
                    return BadRequest("Veritabanı hatası: " + ex.Message);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Bir hata oluştu: " + ex.Message);
                }
            });
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
                .Select(x => new { x.KoordinatorlukId, x.Ad, x.TasraTeskilatiVarMi })
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
