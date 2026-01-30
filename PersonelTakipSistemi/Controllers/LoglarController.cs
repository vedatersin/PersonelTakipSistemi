using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize(Roles = "Admin,Yönetici")]
    public class LoglarController : Controller
    {
        private readonly ILogService _logService;

        public LoglarController(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "", string type = "", DateTime? baslangic = null, DateTime? bitis = null)
        {
            int pageSize = 20;
            var logs = await _logService.GetLogsAsync(page, pageSize, search, type, baslangic, bitis);
            var totalCount = await _logService.GetTotalCountAsync(search, type, baslangic, bitis);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;
            ViewBag.Type = type;
            ViewBag.Baslangic = baslangic?.ToString("yyyy-MM-dd");
            ViewBag.Bitis = bitis?.ToString("yyyy-MM-dd");

            return View(logs);
        }
    }
}
