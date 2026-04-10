using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;

namespace PersonelTakipSistemi.Services
{
    public class PersonelMaintenanceService : IPersonelMaintenanceService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public PersonelMaintenanceService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<string> FixTeskilatNamesAsync()
        {
            var merkez = await _context.Teskilatlar.FirstOrDefaultAsync(t => t.Ad.Contains("Merkez"));
            if (merkez != null)
            {
                merkez.Ad = "Merkez";
            }

            var tasra = await _context.Teskilatlar.FirstOrDefaultAsync(t => t.Ad.Contains("Taşra"));
            if (tasra != null)
            {
                tasra.Ad = "Taşra";
            }

            await _context.SaveChangesAsync();
            return "Teşkilat isimleri güncellendi: Merkez, Taşra";
        }
    }
}
