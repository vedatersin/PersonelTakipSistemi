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

        public GorevlerController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        // ==============================================================
        // 1. LISTELE (FILTRE + PAGINATION)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> Liste(
            string? q, 
            int? kategoriId, 
            byte? durum, 
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
            if (durum.HasValue) query = query.Where(x => x.Durum == durum.Value);
            
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
            ViewBag.Durum = durum;
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
        public async Task<IActionResult> Yeni()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yeni(Gorev model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.IsActive = true; 
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
        public async Task<IActionResult> Duzenle(int id)
        {
            var task = await _context.Gorevler.FindAsync(id);
            if (task == null) return NotFound();

            await PopulateDropdowns();
            return View("Yeni", task); // Re-use Yeni view or create specialized output
        }

        [HttpPost]
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
                existing.PersonelId = model.PersonelId;
                existing.BirimId = model.BirimId;
                existing.Durum = model.Durum;
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
            ViewBag.Birimler = new SelectList(await _context.Birimler.OrderBy(x => x.Ad).ToListAsync(), "BirimId", "Ad");
            
            var personeller = await _context.Personeller.Where(x => x.AktifMi).OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                .Select(x => new { x.PersonelId, AdSoyad = x.Ad + " " + x.Soyad }).ToListAsync();
            ViewBag.Personeller = new SelectList(personeller, "PersonelId", "AdSoyad");
        }
        // ==============================================================
        // 5. ATAMA / YETKİLENDİRME (AJAX)
        // ==============================================================
        [HttpGet]
        public async Task<IActionResult> GetAssignmentData(int gorevId)
        {
            var gorev = await _context.Gorevler.FindAsync(gorevId);
            if (gorev == null) return NotFound();

            var result = new GorevAtamaResultViewModel
            {
                GorevId = gorevId,
                Teskilatlar = await _context.GorevAtamaTeskilatlar
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Teskilat)
                    .Select(x => new IdNamePair { Id = x.TeskilatId, Name = x.Teskilat.Ad, Type = "Teskilat" })
                    .ToListAsync(),
                Koordinatorlukler = await _context.GorevAtamaKoordinatorlukler
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Koordinatorluk)
                    .Select(x => new IdNamePair { Id = x.KoordinatorlukId, Name = x.Koordinatorluk.Ad, Type = "Koordinatorluk" })
                    .ToListAsync(),
                Komisyonlar = await _context.GorevAtamaKomisyonlar
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Komisyon)
                    .Select(x => new IdNamePair { Id = x.KomisyonId, Name = x.Komisyon.Ad, Type = "Komisyon" })
                    .ToListAsync(),
                Personeller = await _context.GorevAtamaPersoneller
                    .Where(x => x.GorevId == gorevId)
                    .Include(x => x.Personel)
                    .Select(x => new IdNamePair { Id = x.PersonelId, Name = x.Personel.Ad + " " + x.Personel.Soyad, Type = "Personel" })
                    .ToListAsync()
            };

            return Json(result);
        }

        [HttpPost]
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
    }
}
