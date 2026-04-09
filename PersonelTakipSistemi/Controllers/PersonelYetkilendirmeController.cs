using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    public partial class PersonelController
    {
        private int CurrentUserId => int.Parse(User.FindFirst("PersonelId")?.Value ?? "0");

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> Yetkilendirme()
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var serviceModel = await _personelAuthorizationService.BuildAuthorizationIndexAsync(
                currentUserId,
                User.IsInRole("Admin"),
                User.IsInRole("Editör"));

            return View(serviceModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> GetYetkilendirmeData(int id)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var serviceModel = await _personelAuthorizationService.BuildAuthorizationDetailAsync(id, currentUserId, User.IsInRole("Admin"));
            if (serviceModel == null)
            {
                return NotFound();
            }

            return Json(serviceModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> SetSistemRol(int id, string rol)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }

            var targetRole = await _context.SistemRoller.FirstOrDefaultAsync(r => r.Ad == rol);
            if (targetRole == null)
            {
                return BadRequest("Geçersiz rol.");
            }

            personel.SistemRolId = targetRole.SistemRolId;
            await _context.SaveChangesAsync();

            try
            {
                await _notificationService.CreateAsync(
                    aliciId: personel.PersonelId,
                    gonderenId: null,
                    baslik: "Yetkilendirme bildirimi",
                    aciklama: $"Sayın {personel.Ad} {personel.Soyad}, yetkilendirme ayarlarınız güncellenmiştir.",
                    tip: "Yetki",
                    refType: "Personel",
                    refId: personel.PersonelId);

                await _logService.LogAsync(
                    "Yetkilendirme",
                    $"Sistem rolü güncellendi: {personel.Ad} {personel.Soyad}",
                    personel.PersonelId,
                    $"Yeni Rol: {rol}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetSistemRol bildirim hatası: {ex.Message}");
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> AddTeskilat(int personelId, int teskilatId)
        {
            var result = await _personelAssignmentService.AddTeskilatAsync(personelId, teskilatId, CurrentUserId, User.Identity?.Name);
            if (result.HttpStatusCode == 400)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> RemoveTeskilat(int personelId, int teskilatId)
        {
            await _personelAssignmentService.RemoveTeskilatAsync(personelId, teskilatId, CurrentUserId, User.Identity?.Name);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> AddKoordinatorluk(int personelId, int koordinatorlukId)
        {
            var result = await _personelAssignmentService.AddKoordinatorlukAsync(personelId, koordinatorlukId, CurrentUserId);
            if (result.HttpStatusCode == 400)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> RemoveKoordinatorluk(int personelId, int koordinatorlukId)
        {
            await _personelAssignmentService.RemoveKoordinatorlukAsync(personelId, koordinatorlukId, CurrentUserId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> AddKomisyon(int personelId, int komisyonId)
        {
            var result = await _personelAssignmentService.AddKomisyonAsync(personelId, komisyonId, CurrentUserId);
            if (result.HttpStatusCode == 400)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> RemoveKomisyon(int personelId, int komisyonId)
        {
            await _personelAssignmentService.RemoveKomisyonAsync(personelId, komisyonId, CurrentUserId);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> AddKurumsalRol(int personelId, int kurumsalRolId, int? koordinatorlukId, int? komisyonId, bool force = false)
        {
            var result = await _personelAssignmentService.AddKurumsalRolAsync(personelId, kurumsalRolId, koordinatorlukId, komisyonId, force, CurrentUserId);
            if (result.HttpStatusCode == 400)
            {
                return BadRequest(result.Message);
            }

            if (!string.IsNullOrEmpty(result.Warning))
            {
                return Json(new { success = false, warning = result.Warning });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> RemoveKurumsalRol(int assignmentId)
        {
            await _personelAssignmentService.RemoveKurumsalRolAsync(assignmentId, CurrentUserId);
            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> IndirBasitSablon()
        {
            var content = await _excelService.GenerateSimplePersonelTemplateAsync();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personel_Yukleme_Sablonu.xlsx");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> SaveYetkilendirme([FromBody] DTOs.PersonelYetkilendirmeSaveDto dto)
        {
            if (dto == null || dto.PersonelId <= 0)
            {
                return BadRequest("Geçersiz veri.");
            }

            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var personel = await _context.Personeller
                        .Include(p => p.PersonelTeskilatlar)
                        .Include(p => p.PersonelKoordinatorlukler)
                        .Include(p => p.PersonelKomisyonlar)
                        .Include(p => p.PersonelKurumsalRolAtamalari)
                        .Include(p => p.SistemRol)
                        .FirstOrDefaultAsync(p => p.PersonelId == dto.PersonelId);

                    if (personel == null)
                    {
                        return NotFound("Personel bulunamadı.");
                    }

                    if (!string.IsNullOrEmpty(dto.SistemRol))
                    {
                        var sistemRol = await _context.SistemRoller.FirstOrDefaultAsync(r => r.Ad == dto.SistemRol);
                        if (sistemRol != null)
                        {
                            personel.SistemRolId = sistemRol.SistemRolId;
                        }
                    }

                    var teskilatsToRemove = personel.PersonelTeskilatlar.Where(pt => !dto.TeskilatIds.Contains(pt.TeskilatId)).ToList();
                    _context.PersonelTeskilatlar.RemoveRange(teskilatsToRemove);
                    foreach (var tid in dto.TeskilatIds)
                    {
                        if (!personel.PersonelTeskilatlar.Any(pt => pt.TeskilatId == tid))
                        {
                            _context.PersonelTeskilatlar.Add(new PersonelTeskilat { PersonelId = personel.PersonelId, TeskilatId = tid });
                        }
                    }

                    var koordsToRemove = personel.PersonelKoordinatorlukler.Where(pk => !dto.KoordinatorlukIds.Contains(pk.KoordinatorlukId)).ToList();
                    _context.PersonelKoordinatorlukler.RemoveRange(koordsToRemove);
                    foreach (var kid in dto.KoordinatorlukIds)
                    {
                        if (!personel.PersonelKoordinatorlukler.Any(pk => pk.KoordinatorlukId == kid))
                        {
                            _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = personel.PersonelId, KoordinatorlukId = kid });
                        }
                    }

                    var komsToRemove = personel.PersonelKomisyonlar.Where(pk => !dto.KomisyonIds.Contains(pk.KomisyonId)).ToList();
                    _context.PersonelKomisyonlar.RemoveRange(komsToRemove);
                    foreach (var kid in dto.KomisyonIds)
                    {
                        if (!personel.PersonelKomisyonlar.Any(pk => pk.KomisyonId == kid))
                        {
                            _context.PersonelKomisyonlar.Add(new PersonelKomisyon { PersonelId = personel.PersonelId, KomisyonId = kid });
                        }
                    }

                    _context.PersonelKurumsalRolAtamalari.RemoveRange(personel.PersonelKurumsalRolAtamalari);

                    var duplicates = dto.Gorevler
                        .GroupBy(x => new { x.KurumsalRolId, x.KoordinatorlukId, x.KomisyonId })
                        .Where(g => g.Count() > 1)
                        .Select(y => y.First())
                        .ToList();

                    if (duplicates.Any())
                    {
                        var roleIds = duplicates.Select(d => d.KurumsalRolId).Distinct().ToList();
                        var roleNames = await _context.KurumsalRoller
                            .Where(r => roleIds.Contains(r.KurumsalRolId))
                            .ToDictionaryAsync(r => r.KurumsalRolId, r => r.Ad);
                        var errs = duplicates
                            .Select(d => roleNames.ContainsKey(d.KurumsalRolId) ? roleNames[d.KurumsalRolId] : "Rol")
                            .Distinct();

                        return BadRequest($"Aynı yetkiyi birden fazla kez ekleyemezsiniz: {string.Join(", ", errs)}");
                    }

                    foreach (var gorev in dto.Gorevler)
                    {
                        var atama = new PersonelKurumsalRolAtama
                        {
                            PersonelId = personel.PersonelId,
                            KurumsalRolId = gorev.KurumsalRolId,
                            KoordinatorlukId = gorev.KoordinatorlukId,
                            KomisyonId = gorev.KomisyonId,
                            CreatedAt = DateTime.Now
                        };
                        _context.PersonelKurumsalRolAtamalari.Add(atama);

                        if (gorev.KurumsalRolId == 2 && gorev.KomisyonId.HasValue)
                        {
                            var komisyon = await _context.Komisyonlar.FindAsync(gorev.KomisyonId.Value);
                            if (komisyon != null)
                            {
                                komisyon.BaskanPersonelId = personel.PersonelId;
                                _context.Komisyonlar.Update(komisyon);
                            }
                        }
                        else if ((gorev.KurumsalRolId == 3 || gorev.KurumsalRolId == 4) && gorev.KoordinatorlukId.HasValue)
                        {
                            var koord = await _context.Koordinatorlukler.FindAsync(gorev.KoordinatorlukId.Value);
                            if (koord != null)
                            {
                                koord.BaskanPersonelId = personel.PersonelId;
                                _context.Koordinatorlukler.Update(koord);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    try
                    {
                        var actorName = User.Identity?.Name ?? "Bilinmiyor";
                        await _notificationService.CreateAsync(
                            aliciId: personel.PersonelId,
                            gonderenId: null,
                            baslik: "Yetkilendirme bildirimi",
                            aciklama: $"Sayın {personel.Ad} {personel.Soyad}, yetkilendirme ayarlarınız güncellenmiştir.",
                            tip: "Yetki",
                            refType: "Personel",
                            refId: personel.PersonelId);

                        await _logService.LogAsync(
                            "Yetkilendirme",
                            $"Yetkilendirme güncellendi: {personel.Ad} {personel.Soyad}",
                            personel.PersonelId,
                            $"İşlemi Yapan: {actorName}");
                    }
                    catch (Exception)
                    {
                    }

                    return await GetYetkilendirmeData(dto.PersonelId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Kaydetme hatası: " + ex.Message);
                }
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> FixTeskilatNames()
        {
            try
            {
                return Content(await _personelMaintenanceService.FixTeskilatNamesAsync());
            }
            catch (Exception ex)
            {
                return Content($"Hata: {ex.Message}");
            }
        }
    }
}
