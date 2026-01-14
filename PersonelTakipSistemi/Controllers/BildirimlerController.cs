using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.Services;
using System.Security.Claims;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class BildirimlerController : Controller
    {
        private readonly INotificationService _notificationService;

        public BildirimlerController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(int? selectedId)
        {
            try
            {
                int userId = GetUserId();
                if (userId == 0) return Unauthorized();

                var inbox = await _notificationService.GetInboxAsync(userId);
                
                object? selectedNotification = null;
                if (selectedId.HasValue)
                {
                    var notif = inbox.FirstOrDefault(x => x.BildirimId == selectedId.Value);
                    if (notif != null)
                    {
                        selectedNotification = notif;
                        if (!notif.OkunduMu)
                        {
                            await _notificationService.MarkAsReadAsync(userId, notif.BildirimId);
                            notif.OkunduMu = true;
                        }
                    }
                }

                return Json(new { inbox, selectedNotification });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Topbar()
        {
            var userId = GetUserId();
            if (userId == 0) return Unauthorized();

            var (count, top) = await _notificationService.GetTopUnreadAsync(userId);
            return Json(new { count, top });
        }

        [HttpPost]
        public async Task<IActionResult> Read(int id)
        {
            var userId = GetUserId();
            if (userId == 0) return Unauthorized();

            await _notificationService.MarkAsReadAsync(userId, id);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> MarkAllRead()
        {
            var userId = GetUserId();
            if (userId == 0) return Unauthorized();

            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            if (userId == 0) return Unauthorized();

            await _notificationService.DeleteAsync(userId, id);
            return Ok();
        }

        private int GetUserId()
        {
            var userIdStr = User.FindFirst("PersonelId")?.Value;
            if (string.IsNullOrEmpty(userIdStr))
            {
                userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (int.TryParse(userIdStr, out int userId))
            {
                return userId;
            }
            return 0;
        }
    }
}
