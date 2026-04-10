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
        private readonly ILogService _logService;

        public CihazlarController(ICihazService cihazService, ILogService logService)
        {
            _cihazService = cihazService;
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
            var currentPersonelId = GetCurrentPersonelId();
            var coordinatorCreate = returnAction == "Liste" && (User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(currentPersonelId));

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

            return RedirectToAction(returnAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guncelle(CihazCreateViewModel form)
        {
            var canManage = User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(GetCurrentPersonelId());

            try
            {
                await _cihazService.UpdateDeviceAsync(form, GetCurrentPersonelId(), canManage);
                await _logService.LogAsync("Cihaz Güncelleme", $"Cihaz kaydı güncellendi. CihazId: {form.CihazId}", detay: $"TürId: {form.CihazTuruId}, MarkaId: {form.CihazMarkaId}, Model: {form.Model}");
                TempData["SuccessMessage"] = "Cihaz kaydı güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Detay), new { id = form.CihazId });
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

        [HttpGet]
        public async Task<IActionResult> Detay(int id)
        {
            var canManage = User.IsInRole("Admin") || await _cihazService.IsCoordinatorAsync(GetCurrentPersonelId());
            var model = await _cihazService.GetDetailAsync(id, GetCurrentPersonelId(), canManage);
            return model == null ? NotFound() : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devret(CihazTransferFormModel model)
        {
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

            return RedirectToAction(nameof(Detay), new { id = model.CihazId });
        }

        [HttpGet]
        public async Task<IActionResult> YazilimLisanslarim()
        {
            return View(await _cihazService.GetSoftwareLicensesAsync(GetCurrentPersonelId()));
        }

        private int GetCurrentPersonelId()
        {
            var claim = User.FindFirst("PersonelId")?.Value ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(claim, out var personelId) ? personelId : 0;
        }
    }
}

