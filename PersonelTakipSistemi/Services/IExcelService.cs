using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PersonelTakipSistemi.Models;
using System.Collections.Generic;

namespace PersonelTakipSistemi.Services
{
    public interface IExcelService
    {
        Task<byte[]> GeneratePersonelTemplateAsync();
        Task<(List<Personel> personeller, List<string> errors)> ImportPersonelListAsync(IFormFile file);
    }
}
