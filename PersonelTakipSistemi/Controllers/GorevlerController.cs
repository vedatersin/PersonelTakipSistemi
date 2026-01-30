using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class GorevlerController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;
        private readonly Services.INotificationService _notificationService;

        public GorevlerController(TegmPersonelTakipDbContext context, Services.INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // ==============================================================
        // 1. LISTELE (FILTRE + PAGINATION)
        // ==============================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Liste(
            string? q, 
            int? kategoriId, 
            int? gorevDurumId, 
            // Assignment Filters
            int? assignedTeskilatId,
            int? assignedKoordinatorlukId,
            int? assignedKomisyonId,
            int? assignedPersonelId,
            
            DateTime? baslangicTarihi, 
            DateTime? bitisTarihi, 
            bool onlyActive = false,
            string sort = "newest", 
            int page = 1)
        {
            var query = _context.Gorevler
                .Include(g => g.Kategori)
                .Include(g => g.GorevDurum)
                // Eager Load Assignments for Display & Filter
                .Include(g => g.GorevAtamaTeskilatlar).ThenInclude(t => t.Teskilat)
                .Include(g => g.GorevAtamaKoordinatorlukler).ThenInclude(k => k.Koordinatorluk)
                .Include(g => g.GorevAtamaKomisyonlar).ThenInclude(k => k.Komisyon)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(p => p.Personel)
                .AsNoTracking()
                .AsQueryable();

            // 1. Text Search (Name, Description)
            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.ToLower();
                query = query.Where(x => x.Ad.ToLower().Contains(q) || (x.Aciklama != null && x.Aciklama.ToLower().Contains(q)));
            }

            // 2. Standard Filters
            if (kategoriId.HasValue) query = query.Where(x => x.KategoriId == kategoriId.Value);
            if (gorevDurumId.HasValue) query = query.Where(x => x.GorevDurumId == gorevDurumId.Value);
            
            if (baslangicTarihi.HasValue) query = query.Where(x => x.BaslangicTarihi >= baslangicTarihi.Value);
            if (bitisTarihi.HasValue) query = query.Where(x => x.BaslangicTarihi <= bitisTarihi.Value);

            if (onlyActive) query = query.Where(x => x.IsActive);

            // 3. Assignment Filters (Junction Tables)
            if (assignedTeskilatId.HasValue)
                query = query.Where(g => g.GorevAtamaTeskilatlar.Any(at => at.TeskilatId == assignedTeskilatId.Value));
            
            if (assignedKoordinatorlukId.HasValue)
                query = query.Where(g => g.GorevAtamaKoordinatorlukler.Any(ak => ak.KoordinatorlukId == assignedKoordinatorlukId.Value));

            if (assignedKomisyonId.HasValue)
                query = query.Where(g => g.GorevAtamaKomisyonlar.Any(ak => ak.KomisyonId == assignedKomisyonId.Value));

            if (assignedPersonelId.HasValue)
                query = query.Where(g => g.GorevAtamaPersoneller.Any(ap => ap.PersonelId == assignedPersonelId.Value));


            // Sorting
            query = sort switch
            {
                "oldest" => query.OrderBy(x => x.BaslangicTarihi).ThenBy(x => x.GorevId),
                _ => query.OrderByDescending(x => x.BaslangicTarihi).ThenByDescending(x => x.GorevId),
            };

            // Paging
            int pageSize = 25;
            int totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Prepare ViewBags for Dropdowns
            ViewBag.Kategoriler = await _context.GorevKategorileri.Where(x => x.IsActive).OrderBy(x => x.Ad).ToListAsync();
            // Durumlar list
            ViewBag.GorevDurumlari = await _context.Set<GorevDurum>().OrderBy(x => x.Sira).ToListAsync();
            
            // Assignment Dropdowns for Filter
            ViewBag.Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync();
            ViewBag.Koordinatorlukler = await _context.Koordinatorlukler.OrderBy(x => x.Ad).ToListAsync();
            
            // Assign SelectListItem to ViewBag
            var komisyonList = await _context.Komisyonlar
                                        .Include(k => k.Koordinatorluk)
                                        .OrderBy(x => x.Ad)
                                        .ToListAsync();

            ViewBag.Komisyonlar = komisyonList.Select(k => new SelectListItem {
                                            Value = k.KomisyonId.ToString(),
                                            Text = k.Ad + (k.Koordinatorluk != null ? " (" + k.Koordinatorluk.Ad + ")" : "")
                                        }).ToList();
            
            // Use SelectListItem to avoid "internal anonymous type" visibility issues in Razor
            ViewBag.Personeller = await _context.Personeller.Where(x => x.AktifMi)
                                          .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                                          .Select(x => new SelectListItem { 
                                              Value = x.PersonelId.ToString(), 
                                              Text = x.Ad + " " + x.Soyad 
                                          }).ToListAsync();

            // Filter State Persistence
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;
            
            ViewBag.Q = q;
            ViewBag.KategoriId = kategoriId;
            ViewBag.GorevDurumId = gorevDurumId;
            ViewBag.BaslangicTarihi = baslangicTarihi?.ToString("yyyy-MM-dd");
            ViewBag.BitisTarihi = bitisTarihi?.ToString("yyyy-MM-dd");
            ViewBag.OnlyActive = onlyActive;
            ViewBag.Sort = sort;

            // Assignment Filter State
            ViewBag.AssignedTeskilatId = assignedTeskilatId;
            ViewBag.AssignedKoordinatorlukId = assignedKoordinatorlukId;
            ViewBag.AssignedKomisyonId = assignedKomisyonId;
            ViewBag.AssignedPersonelId = assignedPersonelId;

            return View(items);
        }

        // ==============================================================
        // 2. YENİ GÖREV
        // ==============================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Yeni()
        {
            await PopulateDropdowns();
            return View(new Gorev { GorevDurumId = 1 });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yeni(Gorev model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.IsActive = true; 
                // Default status if 0 (e.g. not selected or new)
                if(model.GorevDurumId == 0) model.GorevDurumId = 1; // Atanmayı Bekliyor

                _context.Gorevler.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Görev başarıyla oluşturuldu.";
                return RedirectToAction("Liste");
            }
            await PopulateDropdowns();
            return View(model);
        }

        // ==============================================================
        // 3. GÖREV DÜZENLE
        // ==============================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Duzenle(int id)
        {
            var task = await _context.Gorevler.FindAsync(id);
            if (task == null) return NotFound();

            await PopulateDropdowns();
            return View("Yeni", task); // Re-use Yeni view or create specialized output
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Duzenle(Gorev model)
        {
            if (model.GorevId == 0) return BadRequest();

            // Re-validate logic strictly if needed, but model binding handles mostly
            if (ModelState.IsValid)
            {
                var existing = await _context.Gorevler.FindAsync(model.GorevId);
                if (existing == null) return NotFound();

                // Update fields
                existing.Ad = model.Ad;
                existing.Aciklama = model.Aciklama;
                existing.KategoriId = model.KategoriId;
                // PersonelId related logic is handled via assignments now
                // existing.PersonelId = model.PersonelId; 
                existing.BirimId = model.BirimId;
                existing.GorevDurumId = model.GorevDurumId;
                existing.BaslangicTarihi = model.BaslangicTarihi;
                existing.BitisTarihi = model.BitisTarihi;
                existing.IsActive = model.IsActive;
                existing.UpdatedAt = DateTime.Now;

                _context.Update(existing);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Görev güncellendi.";
                return RedirectToAction("Liste");
            }
            await PopulateDropdowns();
            return View("Yeni", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Sil(int id)
        {
            var task = await _context.Gorevler.FindAsync(id);
            if (task == null) return NotFound();

            task.IsActive = false; // Soft Delete
            task.UpdatedAt = DateTime.Now;
            
            _context.Update(task);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }

        // ==============================================================
        // 4. KATEGORİ EKLE
        // ==============================================================
        [HttpGet]
        public IActionResult KategoriYeni()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KategoriYeni(GorevKategori model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.GorevKategorileri.AnyAsync(x => x.Ad == model.Ad))
                {
                    ModelState.AddModelError("Ad", "Bu kategori adı zaten mevcut.");
                    return View(model);
                }

                model.CreatedAt = DateTime.Now;
                _context.GorevKategorileri.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Kategori eklendi.";
                return RedirectToAction("Liste"); // Or stay
            }
            return View(model);
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.Kategoriler = new SelectList(await _context.GorevKategorileri.Where(x => x.IsActive).OrderBy(x => x.Ad).ToListAsync(), "GorevKategoriId", "Ad");
            ViewBag.GorevDurumlari = new SelectList(await _context.Set<GorevDurum>().OrderBy(x => x.Sira).ToListAsync(), "GorevDurumId", "Ad");
        }
        // ==============================================================
        // 5. ATAMA / YETKİLENDİRME (AJAX)
        // ==============================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> GetAssignmentData(int id)
        {
            try
            {
                var gorev = await _context.Gorevler.FindAsync(id);
                if (gorev == null) return NotFound();

                var result = new GorevAtamaResultViewModel
                {
                    GorevId = id,
                    GorevDurumId = gorev.GorevDurumId,
                    DurumAciklamasi = gorev.DurumAciklamasi,
                    Teskilatlar = await _context.GorevAtamaTeskilatlar
                        .Where(x => x.GorevId == id)
                        .Include(x => x.Teskilat)
                        .Select(x => new IdNamePair { Id = x.TeskilatId, Name = x.Teskilat != null ? x.Teskilat.Ad : "Silinmiş", Type = "Teskilat" })
                        .ToListAsync(),
                    Koordinatorlukler = await _context.GorevAtamaKoordinatorlukler
                        .Where(x => x.GorevId == id)
                        .Include(x => x.Koordinatorluk)
                        .Select(x => new IdNamePair { Id = x.KoordinatorlukId, Name = x.Koordinatorluk != null ? x.Koordinatorluk.Ad : "Silinmiş", Type = "Koordinatorluk" })
                        .ToListAsync(),
                    Komisyonlar = await _context.GorevAtamaKomisyonlar
                        .Where(x => x.GorevId == id)
                        .Include(x => x.Komisyon)
                        .Select(x => new IdNamePair { Id = x.KomisyonId, Name = x.Komisyon != null ? x.Komisyon.Ad : "Silinmiş", Type = "Komisyon" })
                        .ToListAsync(),
                    Personeller = await _context.GorevAtamaPersoneller
                        .Where(x => x.GorevId == id)
                        .Include(x => x.Personel)
                        .Select(x => new IdNamePair { Id = x.PersonelId, Name = x.Personel != null ? (x.Personel.Ad + " " + x.Personel.Soyad) : "Silinmiş", Type = "Personel" })
                        .ToListAsync()
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                // Return json with 500 status code but payload with error
                return StatusCode(500, new { error = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> SaveAssignments([FromBody] GorevAtamaViewModel model)
        {
            if (model == null) return BadRequest();

            // 1. Teskilat
            var existingTeskilat = _context.GorevAtamaTeskilatlar.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaTeskilatlar.RemoveRange(existingTeskilat);
            foreach (var id in model.TeskilatIds)
            {
                _context.GorevAtamaTeskilatlar.Add(new GorevAtamaTeskilat { GorevId = model.GorevId, TeskilatId = id });
            }

            // 2. Koordinatorluk
            var existingKoord = _context.GorevAtamaKoordinatorlukler.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKoordinatorlukler.RemoveRange(existingKoord);
            foreach (var id in model.KoordinatorlukIds)
            {
                _context.GorevAtamaKoordinatorlukler.Add(new GorevAtamaKoordinatorluk { GorevId = model.GorevId, KoordinatorlukId = id });
            }

            // 3. Komisyon
            var existingKom = _context.GorevAtamaKomisyonlar.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaKomisyonlar.RemoveRange(existingKom);
            foreach (var id in model.KomisyonIds)
            {
                _context.GorevAtamaKomisyonlar.Add(new GorevAtamaKomisyon { GorevId = model.GorevId, KomisyonId = id });
            }

            // 4. Personel
            var existingPers = _context.GorevAtamaPersoneller.Where(x => x.GorevId == model.GorevId);
            _context.GorevAtamaPersoneller.RemoveRange(existingPers);
            foreach (var id in model.PersonelIds)
            {
                _context.GorevAtamaPersoneller.Add(new GorevAtamaPersonel { GorevId = model.GorevId, PersonelId = id });
            }

            await _context.SaveChangesAsync();

            // Notify Users
            await SendAssignmentNotifications(model);

            return Ok(new { success = true });
        }

        private async Task SendAssignmentNotifications(GorevAtamaViewModel model)
        {
            var taskName = await _context.Gorevler.Where(x => x.GorevId == model.GorevId).Select(x => x.Ad).FirstOrDefaultAsync() ?? "Görev";
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(currentUserId, out int senderId);

            // 1. Notify Individuals
            if (model.PersonelIds.Any())
            {
                foreach(var pid in model.PersonelIds) {
                     // Check if notification already exists recently? Maybe redundant.
                     // Simple implementation: Send notification.
                     await _notificationService.CreateAsync(pid, senderId > 0 ? senderId : null, "Yeni Görev Ataması", $"Size '{taskName}' adlı görev atandı.", "GorevAtama", null, null, $"/Gorevler/Detay/{model.GorevId}");
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> UpdateStatus([FromBody] GorevDurumUpdateViewModel model)
        {
            if (model == null) return BadRequest();

            var task = await _context.Gorevler.FindAsync(model.GorevId);
            if (task == null) return NotFound();

            task.GorevDurumId = model.DurumId;
            task.DurumAciklamasi = model.Aciklama;
            task.UpdatedAt = DateTime.Now;
            
            _context.Update(task);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> SearchEntities(string type, string q)
        {
            q = q?.ToLower() ?? "";
            
            if (type == "Teskilat")
            {
                var list = await _context.Teskilatlar
                    .Where(x => x.Ad.ToLower().Contains(q))
                    .Select(x => new { id = x.TeskilatId, text = x.Ad })
                    .Take(20)
                    .ToListAsync();
                return Json(list);
            }
            else if (type == "Koordinatorluk")
            {
                 var list = await _context.Koordinatorlukler
                    .Where(x => x.Ad.ToLower().Contains(q))
                    .Select(x => new { id = x.KoordinatorlukId, text = x.Ad })
                    .Take(20)
                    .ToListAsync();
                return Json(list);
            }
            else if (type == "Komisyon")
            {
                 var list = await _context.Komisyonlar
                    .Where(x => x.Ad.ToLower().Contains(q))
                    .Select(x => new { id = x.KomisyonId, text = x.Ad })
                    .Take(20)
                    .ToListAsync();
                return Json(list);
            }
            else if (type == "Personel")
            {
                 var list = await _context.Personeller
                    .Where(x => x.AktifMi && (x.Ad.ToLower().Contains(q) || x.Soyad.ToLower().Contains(q)))
                    .Select(x => new { id = x.PersonelId, text = x.Ad + " " + x.Soyad })
                    .Take(20)
                    .ToListAsync();
                return Json(list);
            }

            return BadRequest();
        }


        // ==============================================================
        // 6. GÖREVLERİM (User View)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Gorevlerim()
        {
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdStr, out int userId)) return Unauthorized();

            // Find tasks assigned to this user DIRECTLY
            // Also could check Group assignments if needed? "üzerine atanan görevleri" usually implies direct or group.
            // Let's check Direct + Group assignments for completeness.
            
            // Get user's group IDs
            var user = await _context.Personeller
                .Include(p => p.PersonelKurumsalRolAtamalari) // Usually link to Units via roles or direct UnitId?
                // The relationship is not fully clear on how Personel links to Teskilat/Koordinatorluk/Komisyon implicitly.
                // Usually it's via explicit assignment to those units in Personel table or separate tables.
                // Assuming simple Direct Assignment for now based on "GorevAtamaPersonel".
                // If user wants Group assignments to show up, we need to know updated schema. 
                // Let's stick to Direct Assignment first. 
                .FirstOrDefaultAsync(x => x.PersonelId == userId);

            if(user == null) return NotFound();

            var query = _context.Gorevler
                .Include(g => g.Kategori)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevAtamaPersoneller)
                .Where(g => g.IsActive && 
                            g.GorevAtamaPersoneller.Any(p => p.PersonelId == userId))
                .OrderByDescending(g => g.BaslangicTarihi)
                .AsNoTracking();

            var tasks = await query.ToListAsync();
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Detay(int id)
        {
            var task = await _context.Gorevler
                .Include(g => g.Kategori)
                .Include(g => g.GorevDurum)
                .Include(g => g.GorevAtamaTeskilatlar).ThenInclude(t => t.Teskilat)
                .Include(g => g.GorevAtamaKoordinatorlukler).ThenInclude(k => k.Koordinatorluk)
                .Include(g => g.GorevAtamaKomisyonlar).ThenInclude(k => k.Komisyon)
                .Include(g => g.GorevAtamaPersoneller).ThenInclude(p => p.Personel)
                .FirstOrDefaultAsync(x => x.GorevId == id);

            if (task == null) return NotFound();

            return View(task);
        }
    }
}
