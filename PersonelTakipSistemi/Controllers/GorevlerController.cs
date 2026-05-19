using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using PersonelTakipSistemi.Filters;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    [ReadOnlyForHighLevelRoles]
    public class GorevlerController : Controller
    {
        private static readonly int[] HighLevelRoleIds = [7, 8, 9, 10];

        private readonly TegmPersonelTakipDbContext _context;
        private readonly Services.ILogService _logService;
        private readonly Services.IGorevWorkflowService _gorevWorkflowService;

        public GorevlerController(TegmPersonelTakipDbContext context, Services.ILogService logService, Services.IGorevWorkflowService gorevWorkflowService)
        {
            _context = context;
            _logService = logService;
            _gorevWorkflowService = gorevWorkflowService;
        }

        // ==============================================================
        // 1. LISTELE (FILTRE + PAGINATION)
        // ==============================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Liste(
            string? q, 
            int? isNiteligiId, 
            int? gorevDurumId, 
            // Assignment Filters
            int? assignedTeskilatId,
            int? assignedKoordinatorlukId,
            int? assignedKomisyonId,
            int? assignedPersonelId,
            string? baslangicSure,
            
            DateTime? baslangicTarihi, 
            DateTime? bitisTarihi, 
            bool onlyActive = false,
            string sort = "newest", 
            int page = 1)
        {
            var query = _context.Gorevler
                .Include(g => g.IsNiteligi)
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
            if (isNiteligiId.HasValue) query = query.Where(x => x.IsNiteligiId == isNiteligiId.Value);
            if (gorevDurumId.HasValue) query = query.Where(x => x.GorevDurumId == gorevDurumId.Value);

            if (!string.IsNullOrWhiteSpace(baslangicSure))
            {
                var today = DateTime.Today;
                query = baslangicSure switch
                {
                    "gt1y" => query.Where(x => x.BaslangicTarihi < today.AddYears(-1)),
                    "1y" => query.Where(x => x.BaslangicTarihi >= today.AddYears(-1)),
                    "6m" => query.Where(x => x.BaslangicTarihi >= today.AddMonths(-6)),
                    "3m" => query.Where(x => x.BaslangicTarihi >= today.AddMonths(-3)),
                    "1m" => query.Where(x => x.BaslangicTarihi >= today.AddMonths(-1)),
                    _ => query
                };
            }
            
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
                "oldest" => query.OrderBy(x => x.CreatedAt).ThenBy(x => x.GorevId),
                _ => query.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.GorevId),
            };

            // Paging
            int pageSize = 25;
            int totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Prepare ViewBags for Dropdowns
            ViewBag.IsNitelikleri = await _context.IsNitelikleri.OrderBy(x => x.Ad).ToListAsync();
            // Durumlar list
            ViewBag.GorevDurumlari = await _context.Set<GorevDurum>().OrderBy(x => x.Sira).ToListAsync();
            
            // Assignment Dropdowns for Filter
            ViewBag.Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync();
            ViewBag.Koordinatorlukler = await _context.Koordinatorlukler.OrderBy(x => x.Ad).ToListAsync();
            
            // Assign SelectListItem to ViewBag
            var komisyonQuery = _context.Komisyonlar
                                        .Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                                        .Include(k => k.BagliMerkezKoordinatorluk)
                                        .AsQueryable();

            if (assignedKoordinatorlukId.HasValue)
            {
                komisyonQuery = komisyonQuery.Where(k => k.KoordinatorlukId == assignedKoordinatorlukId.Value || k.BagliMerkezKoordinatorlukId == assignedKoordinatorlukId.Value);
            }

            var komisyonList = await komisyonQuery.OrderBy(x => x.Ad).ToListAsync();

            ViewBag.Komisyonlar = komisyonList
                .Select(k =>
                {
                    var displayText = k.Ad;

                    // Taşra komisyonlarında, bağlı olduğu Merkez Birim Koordinatörlüğünü de göster
                    // Örn: "Sivas Almanca Komisyonu" / "Sivas Türkçe Komisyonu"
                    if (k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk?.Il != null && k.BagliMerkezKoordinatorluk != null)
                    {
                        var ilAd = k.Koordinatorluk.Il.Ad;
                        var merkezBirimAd = k.BagliMerkezKoordinatorluk.Ad.Replace(" Birim Koordinatörlüğü", "");
                        displayText = $"{ilAd} {merkezBirimAd} Komisyonu";
                    }

                    return new SelectListItem
                    {
                        Value = k.KomisyonId.ToString(),
                        Text = displayText
                    };
                })
                .OrderBy(x => x.Text)
                .ToList();
            
            // Use SelectListItem to avoid "internal anonymous type" visibility issues in Razor
            var personelQuery = _context.Personeller.AsQueryable();
            if (assignedKomisyonId.HasValue)
            {
                personelQuery = personelQuery.Where(p => p.PersonelKomisyonlar.Any(pk => pk.KomisyonId == assignedKomisyonId.Value));
            }
            
            // Exclude highest level roles from being assigned to any duties
            personelQuery = personelQuery.Where(p => !p.PersonelKurumsalRolAtamalari.Any(r => new[] { 7, 8, 9, 10 }.Contains(r.KurumsalRolId)));

            ViewBag.Personeller = await personelQuery//.Where(x => x.AktifMi) -- Allow passive
                                          .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                                          .Select(x => new SelectListItem { 
                                              Value = x.PersonelId.ToString(), 
                                              Text = x.Ad + " " + x.Soyad + (!x.AktifMi ? " (Pasif)" : ""),
                                              Disabled = !x.AktifMi
                                          }).ToListAsync();

            // Görev Rolü (Görev Türü) Lookup for Assignments
            ViewBag.GorevRolleri = await _context.GorevTurleri
                .AsNoTracking()
                .OrderBy(x => x.Ad)
                .Select(x => new SelectListItem { Value = x.GorevTuruId.ToString(), Text = x.Ad })
                .ToListAsync();

            // Filter State Persistence
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;
            
            ViewBag.Q = q;
            ViewBag.IsNiteligiId = isNiteligiId;
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
            ViewBag.BaslangicSure = baslangicSure;

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
                
                var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(currentUserIdStr, out int currentUserId))
                {
                    model.CreatedByPersonelId = currentUserId;
                }

                // Default status if 0 (e.g. not selected or new)
                if(model.GorevDurumId == 0) model.GorevDurumId = 1; // Atanmayı Bekliyor

                _context.Gorevler.Add(model);
                await _context.SaveChangesAsync();
                
                await _logService.LogAsync("Görev Ekleme", $"Yeni görev oluşturuldu: {model.Ad}", null, null);

                TempData["SuccessMessage"] = "Görev başarıyla oluşturuldu.";
                TempData["NewGorevId"] = model.GorevId; // Pass ID for highlighting
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

            // Security Check: Only Admin or Creator can edit
            if (!User.IsInRole("Admin"))
            {
                var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(currentUserIdStr, out int currentUserId))
                {
                    if (task.CreatedByPersonelId != currentUserId)
                    {
                        TempData["Error"] = "Sadece kendi oluşturduğunuz görevleri düzenleyebilirsiniz.";
                        return RedirectToAction("Liste");
                    }
                }
            }

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

                // Security Check: Only Admin or Creator can edit
                if (!User.IsInRole("Admin"))
                {
                    var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (int.TryParse(currentUserIdStr, out int currentUserId))
                    {
                        if (existing.CreatedByPersonelId != currentUserId)
                        {
                            TempData["Error"] = "Sadece kendi oluşturduğunuz görevleri düzenleyebilirsiniz.";
                            return RedirectToAction("Liste");
                        }
                    }
                }

                // Update fields
                existing.Ad = model.Ad;
                existing.Aciklama = model.Aciklama;
                existing.IsNiteligiId = model.IsNiteligiId;
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
                
                await _logService.LogAsync("Görev Güncelleme", $"Görev güncellendi: {model.Ad}", null, null);
                
                TempData["SuccessMessage"] = "Görev güncellendi.";
                return RedirectToAction("Liste");
            }
            await PopulateDropdowns();
            return View("Yeni", model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> GetQuickEditData(int id)
        {
            var task = await _context.Gorevler
                .AsNoTracking()
                .Include(x => x.IsNiteligi)
                .FirstOrDefaultAsync(x => x.GorevId == id);

            if (task == null) return NotFound();
            if (!CanEditTask(task.CreatedByPersonelId))
            {
                return Json(new { success = false, message = "Sadece kendi oluşturduğunuz görevleri düzenleyebilirsiniz." });
            }

            return Json(new
            {
                success = true,
                gorevId = task.GorevId,
                ad = task.Ad,
                aciklama = task.Aciklama,
                isNiteligiId = task.IsNiteligiId,
                baslangicTarihi = task.BaslangicTarihi.ToString("yyyy-MM-dd"),
                bitisTarihi = task.BitisTarihi?.ToString("yyyy-MM-dd"),
                isActive = task.IsActive
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> QuickUpdate([FromBody] GorevQuickEditViewModel model)
        {
            if (model == null || model.GorevId == 0) return BadRequest();
            if (string.IsNullOrWhiteSpace(model.Ad))
            {
                return Json(new { success = false, message = "Görev adı zorunludur." });
            }

            var existing = await _context.Gorevler
                .Include(x => x.IsNiteligi)
                .FirstOrDefaultAsync(x => x.GorevId == model.GorevId);

            if (existing == null) return NotFound();
            if (!CanEditTask(existing.CreatedByPersonelId))
            {
                return Json(new { success = false, message = "Sadece kendi oluşturduğunuz görevleri düzenleyebilirsiniz." });
            }

            var isNiteligiExists = await _context.IsNitelikleri.AnyAsync(x => x.IsNiteligiId == model.IsNiteligiId);
            if (!isNiteligiExists)
            {
                return Json(new { success = false, message = "Geçerli bir iş niteliği seçiniz." });
            }

            existing.Ad = model.Ad.Trim();
            existing.Aciklama = string.IsNullOrWhiteSpace(model.Aciklama) ? null : model.Aciklama.Trim();
            existing.IsNiteligiId = model.IsNiteligiId;
            existing.BaslangicTarihi = model.BaslangicTarihi;
            existing.BitisTarihi = model.BitisTarihi;
            existing.IsActive = model.IsActive;
            existing.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            await _logService.LogAsync("Görev Güncelleme", $"Görev hızlı düzenleme ile güncellendi: {existing.Ad}", null, null);

            var isNiteligiAd = await _context.IsNitelikleri
                .Where(x => x.IsNiteligiId == existing.IsNiteligiId)
                .Select(x => x.Ad)
                .FirstOrDefaultAsync();

            return Json(new
            {
                success = true,
                gorevId = existing.GorevId,
                ad = existing.Ad,
                aciklama = existing.Aciklama,
                isNiteligiAd,
                bitisTarihi = existing.BitisTarihi?.ToString("dd.MM.yyyy") ?? "-",
                bitisVarMi = existing.BitisTarihi.HasValue
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> Sil(int id)
        {
            var task = await _context.Gorevler.FindAsync(id);
            if (task == null) return NotFound();

            // Security Check: Only Admin or Creator can delete
            if (!User.IsInRole("Admin"))
            {
                var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(currentUserIdStr, out int currentUserId))
                {
                    if (task.CreatedByPersonelId != currentUserId)
                    {
                        return Json(new { success = false, message = "Yetki Hatası: Sadece kendi oluşturduğunuz görevleri silebilirsiniz." });
                    }
                }
            }

            task.IsActive = false; // Soft Delete
            task.UpdatedAt = DateTime.Now;
            
            _context.Update(task);
            await _context.SaveChangesAsync();

            await _logService.LogAsync("Görev Silme", $"Görev silindi (pasife alındı): {task.Ad}", null, null);

            return Ok(new { success = true });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> TopluSil([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                 return Json(new { success = false, message = "Hiçbir kayıt seçilmedi." });
            }

            try
            {
                 // Perform Bulk Hard Delete
                 // İlişkili 'Assignments' (Atamalar) tabloları Cascade silinecek şekilde ayarlandı.
                  await _context.Gorevler
                                .Where(g => ids.Contains(g.GorevId))
                                .ExecuteDeleteAsync();

                  await _logService.LogAsync("Toplu Görev Silme", $"{ids.Count} adet görev silindi.", null, null);

                 return Json(new { success = true, message = $"{ids.Count} görev başarıyla silindi." });
            }
            catch (Exception ex)
            {
                 var innerMsg = ex.InnerException?.Message ?? ex.Message;
                 return Json(new { success = false, message = "Bir hata oluştu: " + innerMsg });
            }
        }

        private async Task PopulateDropdowns()
        {
            ViewBag.IsNitelikleri = new SelectList(await _context.IsNitelikleri.OrderBy(x => x.Ad).ToListAsync(), "IsNiteligiId", "Ad");
            ViewBag.GorevDurumlari = new SelectList(await _context.Set<GorevDurum>().OrderBy(x => x.Sira).ToListAsync(), "GorevDurumId", "Ad");
        }

        private async Task PopulateAssignmentDropdowns()
        {
            ViewBag.Teskilatlar = await _context.Teskilatlar.OrderBy(x => x.Ad).ToListAsync();
            ViewBag.Koordinatorlukler = await _context.Koordinatorlukler.OrderBy(x => x.Ad).ToListAsync();

            var komisyonlar = await _context.Komisyonlar
                .Include(k => k.Koordinatorluk).ThenInclude(koord => koord.Il)
                .Include(k => k.BagliMerkezKoordinatorluk)
                .OrderBy(x => x.Ad)
                .ToListAsync();

            ViewBag.Komisyonlar = komisyonlar
                .Select(k =>
                {
                    var displayText = k.Ad;
                    if (k.BagliMerkezKoordinatorlukId != null && k.Koordinatorluk?.Il != null && k.BagliMerkezKoordinatorluk != null)
                    {
                        var ilAd = k.Koordinatorluk.Il.Ad;
                        var merkezBirimAd = k.BagliMerkezKoordinatorluk.Ad.Replace(" Birim Koordinatörlüğü", "");
                        displayText = $"{ilAd} {merkezBirimAd} Komisyonu";
                    }

                    return new SelectListItem
                    {
                        Value = k.KomisyonId.ToString(),
                        Text = displayText
                    };
                })
                .OrderBy(x => x.Text)
                .ToList();

            ViewBag.Personeller = await _context.Personeller
                .Where(p => !p.PersonelKurumsalRolAtamalari.Any(r => HighLevelRoleIds.Contains(r.KurumsalRolId)))
                .OrderBy(x => x.Ad)
                .ThenBy(x => x.Soyad)
                .Select(x => new SelectListItem
                {
                    Value = x.PersonelId.ToString(),
                    Text = x.Ad + " " + x.Soyad + (!x.AktifMi ? " (Pasif)" : ""),
                    Disabled = !x.AktifMi
                })
                .ToListAsync();

            ViewBag.GorevRolleri = await _context.GorevTurleri
                .AsNoTracking()
                .OrderBy(x => x.Ad)
                .Select(x => new SelectListItem { Value = x.GorevTuruId.ToString(), Text = x.Ad })
                .ToListAsync();
        }

        private async Task<bool> CurrentUserCanUpdateStatusAsync(int personelId)
        {
            if (User.IsInRole("Admin"))
            {
                return true;
            }

            return await _context.PersonelKurumsalRolAtamalari
                .AsNoTracking()
                .AnyAsync(r => r.PersonelId == personelId && HighLevelRoleIds.Contains(r.KurumsalRolId));
        }

        private bool CanEditTask(int? createdByPersonelId)
        {
            if (User.IsInRole("Admin"))
            {
                return true;
            }

            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(currentUserIdStr, out int currentUserId) && createdByPersonelId == currentUserId;
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
                var result = await _gorevWorkflowService.GetAssignmentDataAsync(id);
                return result == null ? NotFound() : Json(result);
            }
            catch (Exception ex)
            {
                // Log error server-side, return generic message to client
                System.Diagnostics.Debug.WriteLine($"GetAssignmentData error: {ex.Message}");
                return StatusCode(500, new { error = "Bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> SaveAssignments([FromBody] GorevAtamaViewModel model)
        {
            if (model == null) return BadRequest();
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(currentUserId, out int senderId);

            var result = await _gorevWorkflowService.SaveAssignmentsAsync(model, senderId > 0 ? senderId : null);
            if (result.HttpStatusCode == 400)
            {
                return BadRequest(new { success = false, message = result.Message });
            }

            if (result.HttpStatusCode == 404)
            {
                return NotFound();
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici,Editör")]
        public async Task<IActionResult> UpdateStatus([FromBody] GorevDurumUpdateViewModel model)
        {
            if (model == null) return BadRequest();

            // Get Current User ID
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdStr, out int currentUserId)) return Unauthorized();

            if (!await CurrentUserCanUpdateStatusAsync(currentUserId))
            {
                return Ok(new { success = false, message = "Yetki Hatası: Durum güncellemesi sadece üst yetkili kurumsal rollere aittir." });
            }

            var result = await _gorevWorkflowService.UpdateStatusAsync(model, currentUserId, false);
            if (result.HttpStatusCode == 404)
            {
                return NotFound();
            }

            if (!result.Success)
            {
                return Ok(new { success = false, message = result.Message });
            }

            var durum = await _context.GorevDurumlari
                .AsNoTracking()
                .Where(x => x.GorevDurumId == model.DurumId)
                .Select(x => new { x.GorevDurumId, x.Ad, x.Renk })
                .FirstOrDefaultAsync();

            return Ok(new
            {
                success = true,
                message = result.Message,
                statusId = durum?.GorevDurumId,
                statusText = durum?.Ad,
                statusColor = durum?.Renk,
                note = model.Aciklama
            });
        }

        [HttpGet]
        public async Task<IActionResult> SearchEntities(string type, string q)
        {
            var result = await _gorevWorkflowService.SearchEntitiesAsync(type, q ?? string.Empty);
            return result.Any() || type is "Teskilat" or "Koordinatorluk" or "Komisyon" or "Personel"
                ? Json(result.Select(x => new { id = x.Id, text = x.Text, disabled = x.Disabled }))
                : BadRequest();
        }


        // ==============================================================
        // 6. GÖREVLERİM (User View)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Gorevlerim()
        {
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if(!int.TryParse(userIdStr, out int userId)) return Unauthorized();
            var userExists = await _context.Personeller.AnyAsync(x => x.PersonelId == userId);
            if(!userExists) return NotFound();

            var tasks = await _gorevWorkflowService.GetUserTasksAsync(userId);
            return View(tasks);
        }


        // ==============================================================
        // 8. GET STATUS HISTORY (AJAX)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> GetStatusHistory(int id)
        {
            var history = await _gorevWorkflowService.GetStatusHistoryAsync(id);
            return Json(history.Select(h => new
            {
                id = h.Id,
                tarih = h.Tarih,
                personel = h.Personel,
                personelAvatar = h.PersonelAvatar,
                durum = h.Durum,
                durumRenk = h.DurumRenk,
                aciklama = h.Aciklama
            }));
        }

        [HttpGet]
        public async Task<IActionResult> Detay(int id, string? returnUrl, string? returnLabel)
        {
            var task = await _gorevWorkflowService.GetDetailAsync(id);

            if (task == null) return NotFound();

            // Fetch History (Logs) - Deprecated for display but kept if needed
            // ViewBag.History = await ...

            // Populate Status Dropdown for Modal
            ViewBag.GorevDurumlari = await _context.GorevDurumlari.OrderBy(x => x.Sira).ToListAsync();
            await PopulateAssignmentDropdowns();

            var currentUserId = 0;
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdStr, out currentUserId);
            var isHighLevelReadOnly = ViewBag.IsHighLevelReadOnly == true;

            ViewBag.CanUpdateGorevStatus = currentUserId > 0 && await CurrentUserCanUpdateStatusAsync(currentUserId);
            ViewBag.CanEditAssignments = !isHighLevelReadOnly && (User.IsInRole("Admin") || task.CreatedByPersonelId == currentUserId);
            ViewBag.ReturnUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? returnUrl
                : Url.Action(nameof(Gorevlerim), "Gorevler");
            ViewBag.ReturnLabel = string.IsNullOrWhiteSpace(returnLabel)
                ? "Geri"
                : returnLabel;

            return View(task);
        }
    }
}

