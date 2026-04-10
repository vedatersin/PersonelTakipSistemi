using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using PersonelTakipSistemi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelTakipSistemi.Filters
{
    public class ReadOnlyForHighLevelRolesAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdStr, out int userId))
                {
                    var dbContext = context.HttpContext.RequestServices.GetService(typeof(TegmPersonelTakipDbContext)) as TegmPersonelTakipDbContext;
                    if (dbContext != null)
                    {
                        var isHighLevel = await dbContext.PersonelKurumsalRolAtamalari
                            .AnyAsync(r => r.PersonelId == userId && new[] { 7, 8, 9, 10 }.Contains(r.KurumsalRolId));
                        
                        if (isHighLevel)
                        {
                            var method = context.HttpContext.Request.Method;
                            
                            // Let GET requests pass, but set ViewBag
                            if (method == "GET")
                            {
                                if (context.Controller is Controller controller)
                                {
                                    controller.ViewBag.IsHighLevelReadOnly = true;
                                }
                            }
                            else if (method == "POST" || method == "PUT" || method == "DELETE")
                            {
                                // Block modification requests
                                bool isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                                if (isAjax)
                                {
                                    context.Result = new JsonResult(new { success = false, message = "Yetki Hatası: Genel Müdür, Daire Başkanı, Şube Müdürü ve Şef rollerinin sadece görüntüleme yetkisi vardır." });
                                }
                                else
                                {
                                    if (context.Controller is Controller controller)
                                    {
                                        controller.TempData["Error"] = "Yetki Hatası: Genel Müdür, Daire Başkanı, Şube Müdürü ve Şef rollerinin sadece görüntüleme yetkisi vardır.";
                                    }
                                    context.Result = new RedirectToActionResult("Yetkisiz", "Home", null);
                                }
                                return;
                            }
                        }
                    }
                }
            }

            await next();
        }
    }
}
