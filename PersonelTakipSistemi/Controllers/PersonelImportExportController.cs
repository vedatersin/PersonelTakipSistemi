using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonelTakipSistemi.Controllers
{
    public partial class PersonelController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null) return Json(new { success = false, message = "Dosya seçilmedi." });

            var validation = _fileValidationService.ValidateExcel(file);
            if (!validation.isValid) return Json(new { success = false, message = validation.message });

            var (personeller, errors) = await _excelService.ImportPersonelListAsync(file);

            if (personeller.Count > 0)
            {
                await _logService.LogAsync("Veri Aktarimi", $"Excel ile {personeller.Count} personel eklendi.", null, null);
            }

            if (errors.Any())
            {
                // Partial success or total failure
                return Json(new
                {
                    success = personeller.Count > 0, // True if some succeeded, false if all failed
                    partial = personeller.Count > 0,
                    message = personeller.Count > 0 ? $"{personeller.Count} personel eklendi. Ancak bazi satirlarda hatalar mevcut:" : "Hiçbir personel eklenemedi. Lütfen hatalari kontrol edin:",
                    errors = errors,
                    importedIds = personeller.Select(p => p.PersonelId).ToList()
                });
            }

            return Json(new
            {
                success = true,
                message = $"{personeller.Count} personel basariyla eklendi.",
                importedIds = personeller.Select(p => p.PersonelId).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTemplate()
        {
            var content = await _excelService.GeneratePersonelTemplateAsync();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonelYuklemeSablonu.xlsx");
        }
    }
}

