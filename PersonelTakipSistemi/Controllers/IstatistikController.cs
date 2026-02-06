using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize] // Require login for all actions by default
    public class IstatistikController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public IstatistikController(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> Index()
        {
            var model = new IstatistikViewModel();

            // 1. Temel Sayılar (Kartlar için - Genel)
            model.ToplamPersonel = await _context.Personeller.CountAsync(p => p.AktifMi);
            model.ToplamKomisyon = await _context.Komisyonlar.CountAsync();
            model.ToplamKoordinatorluk = await _context.Koordinatorlukler.CountAsync();
            model.ToplamGorev = await _context.Gorevler.CountAsync(); 
            
            // 2. Filtre Listesi (Teşkilat)
            model.TeskilatList = await _context.Teskilatlar
                .OrderBy(t => t.Ad)
                .Select(t => new SelectListItem { Value = t.TeskilatId.ToString(), Text = t.Ad })
                .ToListAsync();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Kisisel()
        {
            // Get Current User ID
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                 return RedirectToAction("Index", "Home");
            }
            
            // Re-use ViewModel but we only populate what's needed or create a new VM. 
            // Reuse IstatistikViewModel for simplicity, but fields like "ToplamPersonel" can be "Toplam Gorevim" etc.
            // Let's filter the totals later in JS or fetch specific totals here.
            
            var model = new IstatistikViewModel();
            model.PersonnelIdForKisisel = userId; // Add a property or pass via ViewBag? Passing via ViewBag/TempData is okay for now.
            ViewBag.CurrentPersonelId = userId;

             // Fetch specific totals for this user to show on cards immediately?
             // Or let the JS fetch it via GetStats (which returns totals).
             // Let's rely on GetStats JS call to fill the cards.

            return View(model);
        }

        // Cascading Dropdown Endpoints
        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> GetKoordinatorlukler(int teskilatId)
        {
            var data = await _context.Koordinatorlukler
                .Where(k => k.TeskilatId == teskilatId)
                .OrderBy(k => k.Ad)
                .Select(k => new { id = k.KoordinatorlukId, text = k.Ad })
                .ToListAsync();
            return Json(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> GetKomisyonlar(int koordinatorlukId)
        {
             var data = await _context.Komisyonlar
                .Where(k => k.KoordinatorlukId == koordinatorlukId)
                .OrderBy(k => k.Ad)
                .Select(k => new { id = k.KomisyonId, text = k.Ad })
                .ToListAsync();
            return Json(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> GetPersoneller(int komisyonId)
        {
            var data = await _context.PersonelKomisyonlar
                .Where(pk => pk.KomisyonId == komisyonId)
                .Select(pk => new { id = pk.PersonelId, text = pk.Personel.Ad + " " + pk.Personel.Soyad })
                .OrderBy(x => x.text)
                .ToListAsync();
            return Json(data);
        }

        // Main Statistics API
        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> GetStats(int? teskilatId, int? koordinatorlukId, int? komisyonId, int? personelId)
        {
            // SECURITY CHECK: If not Admin/Manager, FORCE filtering by their own ID
            if (!User.IsInRole("Admin") && !User.IsInRole("Yönetici"))
            {
                var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdStr, out int uid))
                {
                    personelId = uid;
                    // Reset others to null to prevent authorized data leak? 
                    // Actually, if they filter by their own ID, other filters are irrelevant (intersection).
                    // But to be safe:
                    teskilatId = null;
                    koordinatorlukId = null;
                    komisyonId = null;
                }
                else
                {
                    return Unauthorized();
                }
            }
            
            var query = _context.Gorevler.Include(g => g.Kategori).AsQueryable();

            if (personelId.HasValue) // This branch will always be hit for regular users
            {
                query = query.Where(g => g.GorevAtamaPersoneller.Any(gap => gap.PersonelId == personelId));
            }
            else if (komisyonId.HasValue)
            {
                // Commission -> Tasks assigned to Commission OR (Tasks assigned to Personnel IN Commission?)
                // User said: "Komisyonu da seçerse bağlı komisyondaki bütün personelin verileri toplanarak"
                // This implies we need tasks where the assigned person is in this commission?
                // OR tasks assigned directly to the Commission.
                // Let's assume inclusive: Tasks assigned to the commission + Tasks assigned to personnel who are members of the commission.
                
                var personnelIdsInComm = await _context.PersonelKomisyonlar
                    .Where(pk => pk.KomisyonId == komisyonId)
                    .Select(pk => pk.PersonelId)
                    .ToListAsync();
                
                query = query.Where(g => 
                    g.GorevAtamaKomisyonlar.Any(gak => gak.KomisyonId == komisyonId) ||
                    g.GorevAtamaPersoneller.Any(gap => personnelIdsInComm.Contains(gap.PersonelId))
                );
            }
            else if (koordinatorlukId.HasValue)
            {
                // Coordinator -> Tasks assigned to Coordinator OR (Tasks assigned to Commissions under it) OR (Tasks assigned to Personnel under it? - Maybe too broad)
                // "bağlı komisyondaki bütün personelin verileri" -> implies drilling down.
                // Let's stick to explicit Assignment links usually.
                // Tasks assigned to this Coordinator OR Tasks assigned to Commissions of this Coordinator.
                
                query = query.Where(g => 
                    g.GorevAtamaKoordinatorlukler.Any(gak => gak.KoordinatorlukId == koordinatorlukId) ||
                    g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.KoordinatorlukId == koordinatorlukId)
                );
            }
            else if (teskilatId.HasValue)
            {
                // Teskilat -> Tasks assigned to Coordinators under it
                 query = query.Where(g => 
                    g.GorevAtamaKoordinatorlukler.Any(gak => gak.Koordinatorluk.TeskilatId == teskilatId) ||
                    g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.Koordinatorluk.TeskilatId == teskilatId)
                );
            }

            // 1. Categories (Pie)
            var kategoriData = await query
                .GroupBy(g => g.Kategori.Ad)
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .ToListAsync();

            // 2. Task Status (Bar)
             var durumData = await query
                .Include(g => g.GorevDurum)
                .GroupBy(g => g.GorevDurum.Ad)
                .Select(g => new 
                { 
                    Label = g.Key, 
                    Count = g.Count(),
                    RenkSinifi = g.Max(x => x.GorevDurum.RenkSinifi) 
                })
                .ToListAsync();

            // 3. Assigned Task Count
            int totalTasks = await query.CountAsync();

            // 4. Other Counts (Dynamic)
            int totalPersonel = 0;
            int totalKoordinatorluk = 0;
            int totalKomisyon = 0;

            if (personelId.HasValue)
            {
                totalPersonel = 1;
                totalKomisyon = 1; // Belonging commission
                totalKoordinatorluk = 1; // Belonging coordinator
            }
            else if (komisyonId.HasValue)
            {
                // Count personnel in this commission
                totalPersonel = await _context.PersonelKomisyonlar.CountAsync(pk => pk.KomisyonId == komisyonId);
                totalKomisyon = 1;
                totalKoordinatorluk = 1;
            }
            else if (koordinatorlukId.HasValue)
            {
                // Count personnel in this coordinator (via commissions or direct assignment if any)
                // Assuming personnel are linked to commissions which are linked to coordinator
                // OR linked directly to coordinator.
                // Union of both sets of IDs.
                var pIds1 = _context.PersonelKoordinatorlukler.Where(pk => pk.KoordinatorlukId == koordinatorlukId).Select(p => p.PersonelId);
                var pIds2 = _context.PersonelKomisyonlar.Where(pk => pk.Komisyon.KoordinatorlukId == koordinatorlukId).Select(p => p.PersonelId);
                totalPersonel = await pIds1.Union(pIds2).Distinct().CountAsync();
                
                totalKomisyon = await _context.Komisyonlar.CountAsync(k => k.KoordinatorlukId == koordinatorlukId);
                totalKoordinatorluk = 1;
            }
            else if (teskilatId.HasValue)
            {
                var coordIds = _context.Koordinatorlukler.Where(k => k.TeskilatId == teskilatId).Select(k => k.KoordinatorlukId);
                
                var pIds1 = _context.PersonelKoordinatorlukler.Where(pk => coordIds.Contains(pk.KoordinatorlukId)).Select(p => p.PersonelId);
                var pIds2 = _context.PersonelKomisyonlar.Where(pk => coordIds.Contains(pk.Komisyon.KoordinatorlukId)).Select(p => p.PersonelId);
                
                totalPersonel = await pIds1.Union(pIds2).Distinct().CountAsync();
                totalKoordinatorluk = await coordIds.CountAsync();
                totalKomisyon = await _context.Komisyonlar.CountAsync(k => coordIds.Contains(k.KoordinatorlukId));
            }
            else
            {
                // ALL
                totalPersonel = await _context.Personeller.CountAsync(p => p.AktifMi);
                totalKoordinatorluk = await _context.Koordinatorlukler.CountAsync();
                totalKomisyon = await _context.Komisyonlar.CountAsync();
            }

            // 5. Personnel Trend (Line)
            MultiSeriesChartJson? lineChart = null;
            if (!personelId.HasValue)
            {
                  var sixMonthsAgo = DateTime.Now.AddMonths(-5);
                  var months = Enumerable.Range(0, 6)
                     .Select(i => sixMonthsAgo.AddMonths(i))
                     .Select(d => new { Month = d.Month, Year = d.Year, Label = d.ToString("MM/yyyy") })
                     .ToList();
                
                 lineChart = new MultiSeriesChartJson
                 {
                     Labels = months.Select(m => m.Label).ToList(),
                     Datasets = new List<ChartDataset>()
                 };

                 var trendQuery = query.Where(g => g.CreatedAt >= sixMonthsAgo);
                 var taskData = await trendQuery
                     .Select(g => new 
                     {
                         Month = g.CreatedAt.Month,
                         Year = g.CreatedAt.Year,
                         PersonelIds = g.GorevAtamaPersoneller.Select(p => p.PersonelId).ToList()
                     })
                     .ToListAsync();
                
                 var dataPoints = new List<int>();
                 foreach(var m in months)
                 {
                     var pIds = taskData
                         .Where(x => x.Year == m.Year && x.Month == m.Month)
                         .SelectMany(x => x.PersonelIds)
                         .Distinct()
                         .Count();
                     dataPoints.Add(pIds);
                 }

                 lineChart.Datasets.Add(new ChartDataset
                 {
                     Label = "Görevli Personel Sayısı",
                     Data = dataPoints,
                     BorderColor = "#696cff",
                     BackgroundColor = "rgba(105, 108, 255, 0.1)"
                 });
            }

            return Json(new 
            {
                kategori = new ChartDataJson 
                {
                     Labels = kategoriData.Select(x => x.Label).ToList(),
                     Data = kategoriData.Select(x => x.Count).ToList(),
                     Colors = GetColors(kategoriData.Count)
                },
                durum = new ChartDataJson
                {
                    Labels = durumData.Select(x => x.Label).ToList(),
                    Data = durumData.Select(x => x.Count).ToList(),
                    Colors = durumData.Select(x => GetColorCode(x.RenkSinifi ?? "")).ToList()
                },
                totalGorev = totalTasks,
                totalPersonel = totalPersonel,
                totalKoordinatorluk = totalKoordinatorluk,
                totalKomisyon = totalKomisyon,
                line = lineChart
            });
        }

        private string GetColorCode(string className)
        {
            return className switch
            {
                "bg-primary" => "#696cff",
                "bg-secondary" => "#8592a3",
                "bg-success" => "#71dd37",
                "bg-danger" => "#ff3e1d",
                "bg-warning" => "#ffab00",
                "bg-info" => "#03c3ec",
                "bg-dark" => "#233446",
                _ => "#696cff"
            };
        }

        private List<string> GetColors(int count)
        {
            var colors = new List<string> { "#696cff", "#8592a3", "#71dd37", "#ff3e1d", "#ffab00", "#03c3ec", "#233446" };
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                result.Add(colors[i % colors.Count]);
            }
            return result;
        }
    }
}
