using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Services;
using Xunit;

namespace PersonelTakipSistemi.Tests;

public class PersonelMaintenanceServiceTests
{
    [Fact]
    public async Task FixTeskilatNamesAsync_NormalizesKnownNames()
    {
        await using var context = CreateContext();
        context.Teskilatlar.AddRange(
            new Teskilat { Ad = "Merkez Teşkilatı" },
            new Teskilat { Ad = "Taşra Birimi" });
        await context.SaveChangesAsync();

        var service = new PersonelMaintenanceService(context);

        var message = await service.FixTeskilatNamesAsync();

        var names = await context.Teskilatlar
            .OrderBy(x => x.TeskilatId)
            .Select(x => x.Ad)
            .ToListAsync();

        Assert.Equal("Teşkilat isimleri güncellendi: Merkez, Taşra", message);
        Assert.Equal(new[] { "Merkez", "Taşra" }, names);
    }

    [Fact]
    public async Task FixTeskilatNamesAsync_LeavesUnmatchedRowsUntouched()
    {
        await using var context = CreateContext();
        context.Teskilatlar.Add(new Teskilat { Ad = "Destek Birimi" });
        await context.SaveChangesAsync();

        var service = new PersonelMaintenanceService(context);

        await service.FixTeskilatNamesAsync();

        var ad = await context.Teskilatlar.Select(x => x.Ad).SingleAsync();
        Assert.Equal("Destek Birimi", ad);
    }

    private static TegmPersonelTakipDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TegmPersonelTakipDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TegmPersonelTakipDbContext(options);
    }
}
