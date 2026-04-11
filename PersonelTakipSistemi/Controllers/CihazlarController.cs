using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.Services;
using PersonelTakipSistemi.ViewModels;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class CihazlarController : Controller
    {
        private readonly ICihazService _cihazService;
        private readonly IYazilimLisansService _yazilimLisansService;
        private readonly ILogService _logService;

        public CihazlarController(ICihazService cihazService, IYazilimLisansService yazilimLisansService, ILogService logService)
        {
            _cihazService = cihazService;
            _yazilimLisansService = yazilimLisansService;
            _logService = logService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> Tanimlar(int? selectedTypeId = null)
        {
            return View(await _cihazService.GetDefinitionsAsync(selectedTypeId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> TanimlarData(int? selectedTypeId = null)
        {
            return Json(await _cihazService.GetDefinitionsDataAsync(selectedTypeId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CihazTuruEkle(CihazTuruFormModel model)
        {
            try
            {
                await _cihazService.AddDeviceTypeAsync(model);
                await _logService.LogAsync("Cihaz Türü Ekleme", $"Yeni cihaz türü eklendi: {model.Ad}");
                return Json(new
                {
                    success = true,
                    message = "Cihaz türü eklendi.",
                    data = await _cihazService.GetDefinitionsDataAsync()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CihazTuruGuncelle(CihazTuruFormModel model)
        {
            try
            {
                await _cihazService.UpdateDeviceTypeAsync(model);
                await _logService.LogAsync("Cihaz Türü Güncelleme", $"Cihaz türü güncellendi: {model.Ad}", detay: $"CihazTuruId: {model.CihazTuruId}");
                return Json(new
                {
                    success = true,
                    message = "Cihaz türü güncellendi.",
                    data = await _cihazService.GetDefinitionsDataAsync(model.CihazTuruId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CihazTuruSil(CihazTanimSilModel model)
        {
            try
            {
                await _cihazService.DeleteDeviceTypeAsync(model.Id, model.Onaylandi);
                await _logService.LogAsync("Cihaz Türü Silme", $"Cihaz türü silindi. Id: {model.Id}");
                return Json(new
                {
                    success = true,
                    message = "Cihaz türü silindi.",
                    data = await _cihazService.GetDefinitionsDataAsync(model.SeciliCihazTuruId == model.Id ? null : model.SeciliCihazTuruId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkaEkle(CihazMarkaFormModel model)
        {
            try
            {
                await _cihazService.AddBrandAsync(model);
                await _logService.LogAsync("Cihaz Marka Ekleme", $"Yeni cihaz markası eklendi: {model.Ad}", detay: $"CihazTuruId: {model.CihazTuruId}");
                return Json(new
                {
                    success = true,
                    message = "Marka eklendi.",
                    data = await _cihazService.GetDefinitionsDataAsync(model.CihazTuruId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkaGuncelle(CihazMarkaFormModel model)
        {
            try
            {
                await _cihazService.UpdateBrandAsync(model);
                await _logService.LogAsync("Cihaz Marka Güncelleme", $"Cihaz markası güncellendi: {model.Ad}", detay: $"CihazMarkaId: {model.CihazMarkaId}, CihazTuruId: {model.CihazTuruId}");
                return Json(new
                {
                    success = true,
                    message = "Marka güncellendi.",
                    data = await _cihazService.GetDefinitionsDataAsync(model.CihazTuruId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkaSil(CihazTanimSilModel model)
        {
            try
            {
                await _cihazService.DeleteBrandAsync(model.Id, model.Onaylandi);
                await _logService.LogAsync("Cihaz Marka Silme", $"Cihaz markası silindi. Id: {model.Id}");
                return Json(new
                {
                    success = true,
                    message = "Marka silindi.",
                    data = await _cihazService.GetDefinitionsDataAsync(model.SeciliCihazTuruId)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _cihazService.GetActiveDeviceTypesAsync();
            return Json(types.Select(x => new { id = x.Id, ad = x.Ad }));
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands(int cihazTuruId)
        {
            var brands = await _cihazService.GetBrandsByTypeAsync(cihazTuruId);
            return Json(brands.Select(x => new { id = x.Id, ad = x.Ad }));
        }

        [HttpGet]
        public async Task<IActionResult> BenimCihazlarim([FromQuery] CihazListeFilterViewModel filter)
        {
            return View("Liste", await _cihazService.GetMyDevicesAsync(GetCurrentPersonelId(), filter));
        }

        [HttpGet]
        public async Task<IActionResult> Liste([FromQuery] CihazListeFilterViewModel filter)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var isAdmin = User.IsInRole("Admin");
            var isCoordinator = await _cihazService.IsCoordinatorAsync(currentPersonelId);

            if (!isAdmin && !isCoordinator)
            {
                return RedirectToAction(nameof(BenimCihazlarim));
            }

            return View(await _cihazService.GetManagedDevicesAsync(currentPersonelId, isAdmin, filter));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(CihazListePageViewModel pageModel, string returnAction = "BenimCihazlarim")
        {
            var safeReturnAction = NormalizeReturnAction(returnAction);
            var currentPersonelId = GetCurrentPersonelId();
            var coordinatorCreate = safeReturnAction == nameof(Liste) && (User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(currentPersonelId));

            try
            {
                var cihazId = await _cihazService.CreateDeviceAsync(pageModel.YeniCihaz, currentPersonelId, coordinatorCreate);
                await _logService.LogAsync("Cihaz Ekleme", $"Yeni cihaz kaydı oluşturuldu. CihazId: {cihazId}");
                TempData["NewCihazId"] = cihazId;
                TempData["SuccessMessage"] = coordinatorCreate ? "Cihaz kaydı oluşturuldu." : "Cihaz kaydı oluşturuldu ve onaya gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(safeReturnAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(CihazCreateViewModel form, string? returnAction = null)
        {
            var safeReturnAction = NormalizeReturnAction(returnAction);
            var canManage = User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(GetCurrentPersonelId());

            try
            {
                var onayaGonderildi = await _cihazService.UpdateDeviceAsync(form, GetCurrentPersonelId(), canManage);
                await _logService.LogAsync("Cihaz Güncelleme", $"Cihaz kaydı güncellendi. CihazId: {form.CihazId}", detay: $"TürId: {form.CihazTuruId}, MarkaId: {form.CihazMarkaId}, Model: {form.Model}");
                TempData["SuccessMessage"] = onayaGonderildi
                    ? "Cihaz düzenlemesi kaydedildi ve onaya gönderildi."
                    : "Cihaz kaydı güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Detay), new { id = form.CihazId, returnAction = safeReturnAction });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HizliGuncelle(CihazHizliDuzenleViewModel form)
        {
            try
            {
                await _cihazService.QuickUpdateDeviceAsync(form, GetCurrentPersonelId(), User.IsInRole("Admin"));
                await _logService.LogAsync("Cihaz Hızlı Güncelleme", $"Cihaz hızlı güncellendi. CihazId: {form.CihazId}");
                return Json(new { success = true, message = "Cihaz güncellendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int cihazId)
        {
            try
            {
                await _cihazService.DeleteDeviceAsync(cihazId, GetCurrentPersonelId(), User.IsInRole("Admin"));
                await _logService.LogAsync("Cihaz Silme", $"Cihaz silindi. CihazId: {cihazId}");
                return Json(new { success = true, message = "Cihaz silindi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SilDetay(int cihazId, string? returnAction = null)
        {
            var safeReturnAction = NormalizeReturnAction(returnAction);
            try
            {
                await _cihazService.DeleteDeviceAsync(cihazId, GetCurrentPersonelId(), true);
                await _logService.LogAsync("Cihaz Silme", $"Cihaz silindi. CihazId: {cihazId}");
                TempData["SuccessMessage"] = "Cihaz silindi.";
                return RedirectToAction(safeReturnAction);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Detay), new { id = cihazId, returnAction = safeReturnAction });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Onayla(int cihazId)
        {
            try
            {
                await _cihazService.ApproveDeviceAsync(cihazId, GetCurrentPersonelId());
                await _logService.LogAsync("Cihaz Onayı", $"Cihaz onaylandı. CihazId: {cihazId}");
                TempData["SuccessMessage"] = "Cihaz onaylandı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Liste), new { SadeceOnayBekleyenler = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnaylaDetay(int cihazId, string? returnAction = null)
        {
            var safeReturnAction = NormalizeReturnAction(returnAction);
            try
            {
                await _cihazService.ApproveDeviceAsync(cihazId, GetCurrentPersonelId());
                await _logService.LogAsync("Cihaz Onayı", $"Cihaz onaylandı. CihazId: {cihazId}");
                TempData["SuccessMessage"] = "Cihaz onaylandı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Detay), new { id = cihazId, returnAction = safeReturnAction });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnaylaHizli(int cihazId)
        {
            try
            {
                await _cihazService.ApproveDeviceAsync(cihazId, GetCurrentPersonelId());
                await _logService.LogAsync("Cihaz Onayı", $"Cihaz onaylandı. CihazId: {cihazId}");
                return Json(new { success = true, message = "Cihaz onaylandı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detay(int id, string? returnAction = null)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var canManage = User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(currentPersonelId);
            var model = await _cihazService.GetDetailAsync(id, currentPersonelId, canManage);
            var safeReturnAction = NormalizeReturnAction(returnAction);
            ViewBag.ReturnAction = safeReturnAction;
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devret(CihazTransferFormModel model, string? returnAction = null)
        {
            var safeReturnAction = NormalizeReturnAction(returnAction);
            var canManage = User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(GetCurrentPersonelId());

            try
            {
                await _cihazService.TransferDeviceAsync(model, GetCurrentPersonelId(), canManage);
                await _logService.LogAsync("Cihaz Devri", $"Cihaz devredildi. CihazId: {model.CihazId}", detay: model.DevirNotu);
                TempData["SuccessMessage"] = "Cihaz başarıyla devredildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Detay), new { id = model.CihazId, returnAction = safeReturnAction });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> YazilimLisansYonetimi()
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            if (!lisansYonetebilirMi)
            {
                return RedirectToAction(nameof(BenimCihazlarim));
            }

            return View(await _yazilimLisansService.GetYonetimPageAsync(currentPersonelId, lisansYonetebilirMi, lisansYonetebilirMi));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansEkle([Bind(Prefix = "YeniLisans")] YazilimLisansFormViewModel model)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.CreateLisansAsync(model, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Ekleme", $"Yeni yazılım lisansı eklendi. YazılımId: {model.YazilimId}");
                TempData["SuccessMessage"] = "Yazılım lisansı kaydedildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisansYonetimi));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansGuncelle([Bind(Prefix = "DuzenleFormu")] YazilimLisansFormViewModel model, bool returnToYonetim = false)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.UpdateLisansAsync(model, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Güncelleme", $"Yazılım lisansı güncellendi. LisansId: {model.YazilimLisansId}");
                TempData["SuccessMessage"] = "Yazılım lisansı güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            if (returnToYonetim)
            {
                return RedirectToAction(nameof(YazilimLisansYonetimi));
            }

            return RedirectToAction(nameof(YazilimLisansDetay), new { id = model.YazilimLisansId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansSil(int lisansId)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.DeleteLisansAsync(lisansId, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Silme", $"Yazılım lisansı silindi. LisansId: {lisansId}");
                TempData["SuccessMessage"] = "Yazılım lisansı silindi.";
                return RedirectToAction(nameof(YazilimLisansYonetimi));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(YazilimLisansDetay), new { id = lisansId });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> YazilimLisansDetay(int id)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            if (!lisansYonetebilirMi)
            {
                return RedirectToAction(nameof(BenimCihazlarim));
            }

            var model = await _yazilimLisansService.GetDetayPageAsync(id, currentPersonelId, lisansYonetebilirMi, lisansYonetebilirMi);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansKullaniciEkle(YazilimLisansKullaniciFormViewModel model)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.AddKullaniciAsync(model, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Kullanıcı Ekleme", $"Lisansa kullanıcı eklendi. LisansId: {model.YazilimLisansId}, PersonelId: {model.PersonelId}");
                TempData["SuccessMessage"] = "Lisans kullanıcısı eklendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisansDetay), new { id = model.YazilimLisansId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansKullaniciGuncelle(YazilimLisansKullaniciFormViewModel model)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.UpdateKullaniciAsync(model, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Kullanıcı Güncelleme", $"Lisans kullanıcı kaydı güncellendi. LisansKullaniciId: {model.YazilimLisansKullaniciId}");
                TempData["SuccessMessage"] = "Lisans kullanıcı bilgileri güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisansDetay), new { id = model.YazilimLisansId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansKullaniciSil(int lisansKullaniciId, int lisansId)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.DeleteKullaniciAsync(lisansKullaniciId, lisansId, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Kullanıcı Silme", $"Lisans kullanıcı kaydı silindi. LisansKullaniciId: {lisansKullaniciId}");
                TempData["SuccessMessage"] = "Lisans kullanıcısı silindi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisansDetay), new { id = lisansId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansKullaniciOnayaGonder(YazilimLisansKullaniciOnayFormViewModel model)
        {
            var currentPersonelId = GetCurrentPersonelId();
            try
            {
                await _yazilimLisansService.PersonelOnayaGonderAsync(model, currentPersonelId);
                await _logService.LogAsync("Yazılım Lisans Kullanıcı Onaya Gönder", $"Personel lisans kullanıcı kaydını onaya gönderdi. LisansKullaniciId: {model.YazilimLisansKullaniciId}");
                TempData["SuccessMessage"] = "Lisans kullanıcı kaydı onaya gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisanslarim));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Yönetici")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> YazilimLisansKullaniciOnayKarar(YazilimLisansKullaniciKararFormViewModel model)
        {
            var currentPersonelId = GetCurrentPersonelId();
            var lisansYonetebilirMi = User.IsInRole("Admin") || User.IsInRole("Yönetici");
            try
            {
                await _yazilimLisansService.YoneticiOnayKarariVerAsync(model, currentPersonelId, lisansYonetebilirMi);
                await _logService.LogAsync("Yazılım Lisans Kullanıcı Onay Kararı", $"Lisans kullanıcı kaydı için onay kararı verildi. LisansKullaniciId: {model.YazilimLisansKullaniciId}, Onayla: {model.Onayla}");
                TempData["SuccessMessage"] = model.Onayla
                    ? "Lisans kullanıcı kaydı onaylandı."
                    : "Lisans kullanıcı kaydı onaya geri gönderildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(YazilimLisansDetay), new { id = model.YazilimLisansId });
        }

        [HttpGet]
        public async Task<IActionResult> YazilimLisanslarim()
        {
            return View(await _yazilimLisansService.GetPersonelLisanslarimAsync(GetCurrentPersonelId()));
        }

        private int GetCurrentPersonelId()
        {
            var claim = User.FindFirst("PersonelId")?.Value ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var personelId) ? personelId : 0;
        }

        private static string NormalizeReturnAction(string? returnAction)
        {
            return string.Equals(returnAction, nameof(Liste), StringComparison.OrdinalIgnoreCase)
                ? nameof(Liste)
                : nameof(BenimCihazlarim);
        }
    }
}

