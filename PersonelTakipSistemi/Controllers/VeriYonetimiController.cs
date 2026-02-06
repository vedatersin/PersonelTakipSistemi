using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelTakipSistemi.Services;
using Microsoft.Extensions.Logging;

namespace PersonelTakipSistemi.Controllers
{
    [Route("VeriYonetimi")]
    public class VeriYonetimiController : Controller
    {
        private readonly DataSeedingService _seeder;
        private readonly ILogger<VeriYonetimiController> _logger;

        public VeriYonetimiController(DataSeedingService seeder, ILogger<VeriYonetimiController> logger)
        {
            _seeder = seeder;
            _logger = logger;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            _logger.LogInformation("VeriYonetimi Index accessed");
            return View();
        }

        [HttpPost("ResetAndSeed")]
        public async Task<IActionResult> ResetAndSeed()
        {
            _logger.LogInformation("ResetAndSeed posted");
            try
            {
                await _seeder.SeedAsync();
                return Content("Database reset and seeded successfully according to scenario.");
            }
            catch (Exception ex)
            {
                var msg = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    msg += $" || INNER: {ex.InnerException.Message}";
                    if (ex.InnerException.InnerException != null)
                    {
                        msg += $" || DEEPER: {ex.InnerException.InnerException.Message}";
                    }
                }
                return Content(msg);
            }
        }
    }
}
