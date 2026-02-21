using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;
using System.Drawing;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize] // Require login for all actions by default
    public class IstatistikController : Controller
    {
        private readonly TegmPersonelTakipDbContext _context;

        public IstatistikController(TegmPersonelTakipDbContext context)
        {
            _context = context;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        [Authorize(Roles = "Admin,Yönetici")]
        public async Task<IActionResult> Index(int? personelId)
        {
            var model = new IstatistikViewModel();
            model.PersonelId = personelId; // ID from URL param

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
            
            var model = new IstatistikViewModel();
            model.PersonnelIdForKisisel = userId;
            ViewBag.CurrentPersonelId = userId;

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
                .Include(x => x.Koordinatorluk).ThenInclude(k => k.Il)
                .Include(x => x.BagliMerkezKoordinatorluk)
                .Where(x => (x.KoordinatorlukId == koordinatorlukId || x.BagliMerkezKoordinatorlukId == koordinatorlukId) && x.IsActive)
                .ToListAsync();

            var result = data.Select(x => new { 
                    id = x.KomisyonId, 
                    text = x.BagliMerkezKoordinatorlukId == koordinatorlukId && x.Koordinatorluk?.Il != null
                         ? $"{x.Koordinatorluk.Il.Ad} Komisyonu"
                         : x.Ad 
                })
                .OrderBy(x => x.text)
                .ToList();

            return Json(result);
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
        public async Task<IActionResult> GetStats(int? teskilatId, int? koordinatorlukId, int? komisyonId, int? personelId, int months = 6)
        {
            // SECURITY CHECK: If not Admin/Manager, FORCE filtering by their own ID
            if (!User.IsInRole("Admin") && !User.IsInRole("Yönetici"))
            {
                var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdStr, out int uid))
                {
                    personelId = uid;
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

            if (personelId.HasValue)
            {
                query = query.Where(g => g.GorevAtamaPersoneller.Any(gap => gap.PersonelId == personelId));
            }
            else if (komisyonId.HasValue)
            {
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
                query = query.Where(g => 
                    g.GorevAtamaKoordinatorlukler.Any(gak => gak.KoordinatorlukId == koordinatorlukId) ||
                    g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.KoordinatorlukId == koordinatorlukId)
                );
            }
            else if (teskilatId.HasValue)
            {
                query = query.Where(g => 
                    g.GorevAtamaKoordinatorlukler.Any(gak => gak.Koordinatorluk.TeskilatId == teskilatId) ||
                    g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.Koordinatorluk.TeskilatId == teskilatId)
                );
            }

            // 1. Categories (Pie)
            var kategoriData = await query
                .GroupBy(g => new { g.Kategori.Ad, g.Kategori.Renk })
                .Select(g => new { Label = g.Key.Ad, Count = g.Count(), Color = g.Key.Renk })
                .ToListAsync();

            // 2. Task Status (Bar)
             var durumData = await query
                .Include(g => g.GorevDurum)
                .GroupBy(g => new { g.GorevDurum.Ad, g.GorevDurum.Renk })
                .Select(g => new 
                { 
                    Label = g.Key.Ad, 
                    Count = g.Count(),
                    Color = g.Key.Renk
                })
                .ToListAsync();

            // 3. Assigned Task Count
            int totalTasks = await query.CountAsync();

            // ... (Skipping unchanged parts for brevity if tool supported it, but here I must provide contiguous block or careful splicing. 
            // I'll skip the middle parts by narrowing the range or using multiple chunks if I wasn't replacing a huge block.
            // Actually, I can target just the queries and then the return statement separate? 
            // Better to do it in one go if they are close, but they are separated by 100 lines.
            // I will use multi_replace_file_content for this.)



            // 4. Other Counts (Dynamic)
            int totalPersonel = 0;
            int totalKoordinatorluk = 0;
            int totalKomisyon = 0;

            if (personelId.HasValue)
            {
                totalPersonel = 1;
                totalKomisyon = 1;
                totalKoordinatorluk = 1;
            }
            else if (komisyonId.HasValue)
            {
                totalPersonel = await _context.PersonelKomisyonlar.CountAsync(pk => pk.KomisyonId == komisyonId);
                totalKomisyon = 1;
                totalKoordinatorluk = 1;
            }
            else if (koordinatorlukId.HasValue)
            {
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

            // 5. Trend Line Chart
            MultiSeriesChartJson? lineChart = null;

            if (personelId.HasValue)
            {
                // ========== PERSONEL MODU: Görev Listesi Getir ==========
                var tasks = await query
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(t => new PersonelGorevItem
                    {
                        GorevId = t.GorevId,
                        Baslik = t.Ad,
                        Durum = t.GorevDurum.Ad,
                        Renk = t.GorevDurum.RenkSinifi ?? "bg-secondary",
                        RenkKod = t.GorevDurum.Renk,
                        Tarih = t.CreatedAt,
                        SonIslem = t.UpdatedAt
                    })
                    .ToListAsync();
                
                // We can't put this directly into the anonymous object below without changing it to the ViewModel or a new structure
                // But the return type is Json(new { ... })
                // Let's rely on the dynamic nature of the anonymous object in the return statement
                // I will add a local variable here to hold the list
                ViewBag.TempGorevListesi = tasks; 
            }
            else
            {
                // ========== BİRİM MODU: Çift Çizgili Grafik ==========
                int monthCount = months == 12 ? 12 : 6;
                var startDate = DateTime.Now.AddMonths(-(monthCount - 1));
                var monthList = Enumerable.Range(0, monthCount)
                   .Select(i => startDate.AddMonths(i))
                   .Select(d => new { Month = d.Month, Year = d.Year, Label = d.ToString("MM/yyyy") })
                   .ToList();
                
                lineChart = new MultiSeriesChartJson
                {
                    Labels = monthList.Select(m => m.Label).ToList(),
                    Datasets = new List<ChartDataset>()
                };

                // Görev sayısı (her ay oluşturulan görevler)
                var trendQuery = query.Where(g => g.CreatedAt >= startDate);
                var taskData = await trendQuery
                    .Select(g => new 
                    {
                        Month = g.CreatedAt.Month,
                        Year = g.CreatedAt.Year,
                        PersonelIds = g.GorevAtamaPersoneller.Select(p => p.PersonelId).ToList()
                    })
                    .ToListAsync();
                
                var gorevSayilari = new List<int>();
                var personelSayilari = new List<int>();

                foreach(var m in monthList)
                {
                    var monthTasks = taskData.Where(x => x.Year == m.Year && x.Month == m.Month).ToList();
                    gorevSayilari.Add(monthTasks.Count);

                    var uniquePersonel = monthTasks
                        .SelectMany(x => x.PersonelIds)
                        .Distinct()
                        .Count();
                    personelSayilari.Add(uniquePersonel);
                }

                // Personel çizgisi (mavi)
                lineChart.Datasets.Add(new ChartDataset
                {
                    Label = "Personel Sayısı",
                    Data = personelSayilari,
                    BorderColor = "#696cff",
                    BackgroundColor = "rgba(105, 108, 255, 0.1)"
                });

                // Görev çizgisi (turuncu)
                lineChart.Datasets.Add(new ChartDataset
                {
                    Label = "Görev Sayısı",
                    Data = gorevSayilari,
                    BorderColor = "#ffab00",
                    BackgroundColor = "rgba(255, 171, 0, 0.1)"
                });
            }

            return Json(new 
            {
                kategori = new ChartDataJson 
                {
                     Labels = kategoriData.Select(x => x.Label).ToList(),
                     Data = kategoriData.Select(x => x.Count).ToList(),
                     Colors = kategoriData.Select(x => x.Color ?? "#696cff").ToList()
                },
                durum = new ChartDataJson
                {
                    Labels = durumData.Select(x => x.Label).ToList(),
                    Data = durumData.Select(x => x.Count).ToList(),
                    Colors = durumData.Select(x => x.Color ?? "#696cff").ToList()
                },
                totalGorev = totalTasks,
                totalPersonel = totalPersonel,
                totalKoordinatorluk = totalKoordinatorluk,
                totalKomisyon = totalKomisyon,
                line = lineChart,
                gorevListesi = personelId.HasValue ? ViewBag.TempGorevListesi : null
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



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ExportExcel(int? teskilatId, int? koordinatorlukId, int? komisyonId, int? personelId, int months = 6)
        {
            // Security check
            if (!User.IsInRole("Admin") && !User.IsInRole("Yönetici"))
            {
                var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdStr, out int uid)) { personelId = uid; teskilatId = null; koordinatorlukId = null; komisyonId = null; }
                else return Unauthorized();
            }

            var query = _context.Gorevler.Include(g => g.Kategori).Include(g => g.GorevDurum).AsQueryable();

            if (personelId.HasValue)
                query = query.Where(g => g.GorevAtamaPersoneller.Any(gap => gap.PersonelId == personelId));
            else if (komisyonId.HasValue)
            {
                var pIds = await _context.PersonelKomisyonlar.Where(pk => pk.KomisyonId == komisyonId).Select(pk => pk.PersonelId).ToListAsync();
                query = query.Where(g => g.GorevAtamaKomisyonlar.Any(gak => gak.KomisyonId == komisyonId) || g.GorevAtamaPersoneller.Any(gap => pIds.Contains(gap.PersonelId)));
            }
            else if (koordinatorlukId.HasValue)
                query = query.Where(g => g.GorevAtamaKoordinatorlukler.Any(gak => gak.KoordinatorlukId == koordinatorlukId) || g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.KoordinatorlukId == koordinatorlukId));
            else if (teskilatId.HasValue)
                query = query.Where(g => g.GorevAtamaKoordinatorlukler.Any(gak => gak.Koordinatorluk.TeskilatId == teskilatId) || g.GorevAtamaKomisyonlar.Any(k => k.Komisyon.Koordinatorluk.TeskilatId == teskilatId));

            using var package = new ExcelPackage();
            
            // Sheet 1: Özet ve Kategori Pasta Grafiği
            var ws1 = package.Workbook.Worksheets.Add("Genel Özet");
            ws1.Cells["A1"].Value = "Metrik";
            ws1.Cells["B1"].Value = "Değer";
            ws1.Cells["A1:B1"].Style.Font.Bold = true;
            ws1.Cells["A1:B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws1.Cells["A1:B1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(105, 108, 255));
            ws1.Cells["A1:B1"].Style.Font.Color.SetColor(Color.White);

            var totalGorev = await query.CountAsync();
            ws1.Cells["A2"].Value = "Toplam Görev";
            ws1.Cells["B2"].Value = totalGorev;
            ws1.Cells.AutoFitColumns();

            // Kategori Data for Chart
            var kategoriData = await query.GroupBy(g => g.Kategori.Ad).Select(g => new { Label = g.Key, Count = g.Count() }).ToListAsync();
            var wsKat = package.Workbook.Worksheets.Add("KategoriData"); // Data sheet, hidden
            wsKat.Hidden = eWorkSheetHidden.Hidden;
            wsKat.Cells["A1"].Value = "Kategori";
            wsKat.Cells["B1"].Value = "Sayı";
            for(int i=0; i<kategoriData.Count; i++)
            {
                wsKat.Cells[i + 2, 1].Value = kategoriData[i].Label;
                wsKat.Cells[i + 2, 2].Value = kategoriData[i].Count;
            }
            
            // Pie Chart
            if (kategoriData.Count > 0)
            {
                var chart = ws1.Drawings.AddChart("KategoriPastaGrafigi", eChartType.Pie);
                chart.Title.Text = "Görev Kategori Dağılımı";
                chart.SetPosition(3, 0, 0, 0);
                chart.SetSize(600, 400);
                chart.Series.Add(wsKat.Cells[2, 2, 1 + kategoriData.Count, 2], wsKat.Cells[2, 1, 1 + kategoriData.Count, 1]);
            }

            // Sheet 2: Durum Analizi ve Sütun Grafik
            var durumData = await query.GroupBy(g => g.GorevDurum.Ad).Select(g => new { Label = g.Key, Count = g.Count() }).ToListAsync();
            var ws2 = package.Workbook.Worksheets.Add("Durum Analizi");
            ws2.Cells["A1"].Value = "Durum";
            ws2.Cells["B1"].Value = "Görev Sayısı";
            ws2.Cells["A1:B1"].Style.Font.Bold = true;
            ws2.Cells["A1:B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws2.Cells["A1:B1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(105, 108, 255));
            ws2.Cells["A1:B1"].Style.Font.Color.SetColor(Color.White);

            for (int i = 0; i < durumData.Count; i++)
            {
                ws2.Cells[i + 2, 1].Value = durumData[i].Label;
                ws2.Cells[i + 2, 2].Value = durumData[i].Count;
            }
            ws2.Cells.AutoFitColumns();

            if (durumData.Count > 0)
            {
                var chart2 = ws2.Drawings.AddChart("DurumSutunGrafigi", eChartType.ColumnClustered);
                chart2.Title.Text = "Görev Durum Analizi";
                chart2.SetPosition(1, 0, 3, 0);
                chart2.SetSize(600, 400);
                chart2.Series.Add(ws2.Cells[2, 2, 1 + durumData.Count, 2], ws2.Cells[2, 1, 1 + durumData.Count, 1]);
            }

            // Sheet 3: Aktivite (personel seçiliyse)
            if (personelId.HasValue)
            {
                var gorevler = await query.OrderByDescending(g => g.CreatedAt).ToListAsync();
                var ws3 = package.Workbook.Worksheets.Add("Görev Aktivite");
                ws3.Cells["A1"].Value = "Görev";
                ws3.Cells["B1"].Value = "Durum";
                ws3.Cells["C1"].Value = "Atanma Tarihi";
                ws3.Cells["D1"].Value = "Son Güncelleme";
                ws3.Cells["A1:D1"].Style.Font.Bold = true;
                ws3.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws3.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(105, 108, 255));
                ws3.Cells["A1:D1"].Style.Font.Color.SetColor(Color.White);

                for (int i = 0; i < gorevler.Count; i++)
                {
                    ws3.Cells[i + 2, 1].Value = gorevler[i].Ad;
                    ws3.Cells[i + 2, 2].Value = gorevler[i].GorevDurum?.Ad ?? "";
                    ws3.Cells[i + 2, 3].Value = gorevler[i].CreatedAt.ToString("dd.MM.yyyy");
                    ws3.Cells[i + 2, 4].Value = gorevler[i].UpdatedAt?.ToString("dd.MM.yyyy") ?? "";
                }
                ws3.Cells.AutoFitColumns();
            }

            var fileName = $"Istatistik_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
