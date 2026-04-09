using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Services;
using Xunit;

namespace PersonelTakipSistemi.Tests;

public class PersonelAuthorizationServiceTests
{
    [Fact]
    public async Task BuildAuthorizationIndexAsync_CoordinatorSeesOnlyOwnScopeAndSelf()
    {
        await using var context = CreateContext();
        var seed = await SeedCoordinatorScenarioAsync(context);
        var service = new PersonelAuthorizationService(context);

        var model = await service.BuildAuthorizationIndexAsync(seed.CurrentUserId, isAdmin: false, isEditor: false);

        var ids = model.Personeller.Select(x => x.PersonelId).OrderBy(x => x).ToList();
        Assert.Equal(new[] { seed.CurrentUserId, seed.CoordinatorMemberId, seed.CommissionMemberId }, ids);
    }

    [Fact]
    public async Task BuildAuthorizationDetailAsync_NonAdminLookupListsStayWithinAuthorizedScope()
    {
        await using var context = CreateContext();
        var seed = await SeedCoordinatorScenarioAsync(context);
        var service = new PersonelAuthorizationService(context);

        var model = await service.BuildAuthorizationDetailAsync(seed.CoordinatorMemberId, seed.CurrentUserId, isAdmin: false);

        Assert.NotNull(model);
        Assert.Equal(new[] { seed.AuthorizedTeskilatId }, model!.AllTeskilatlar.Select(x => x.Id));
        Assert.Equal(new[] { seed.AuthorizedKoordinatorlukId }, model.AllKoordinatorlukler.Select(x => x.Id));
        Assert.Equal(new[] { seed.AuthorizedKomisyonId }, model.AllKomisyonlar.Select(x => x.Id));
    }

    private static async Task<AuthorizationSeed> SeedCoordinatorScenarioAsync(TegmPersonelTakipDbContext context)
    {
        var il = new Il { IlId = 1, Ad = "Ankara" };
        var authorizedTeskilat = new Teskilat { TeskilatId = 1, Ad = "Merkez" };
        var otherTeskilat = new Teskilat { TeskilatId = 2, Ad = "Taşra" };
        var authorizedKoordinatorluk = new Koordinatorluk
        {
            KoordinatorlukId = 10,
            Ad = "Koord A",
            TeskilatId = authorizedTeskilat.TeskilatId,
            Teskilat = authorizedTeskilat,
            IlId = il.IlId,
            Il = il
        };
        var otherKoordinatorluk = new Koordinatorluk
        {
            KoordinatorlukId = 20,
            Ad = "Koord B",
            TeskilatId = otherTeskilat.TeskilatId,
            Teskilat = otherTeskilat,
            IlId = il.IlId,
            Il = il
        };
        var authorizedKomisyon = new Komisyon
        {
            KomisyonId = 100,
            Ad = "Komisyon A",
            KoordinatorlukId = authorizedKoordinatorluk.KoordinatorlukId,
            Koordinatorluk = authorizedKoordinatorluk
        };
        var otherKomisyon = new Komisyon
        {
            KomisyonId = 200,
            Ad = "Komisyon B",
            KoordinatorlukId = otherKoordinatorluk.KoordinatorlukId,
            Koordinatorluk = otherKoordinatorluk
        };
        var sistemRol = new SistemRol { SistemRolId = 1, Ad = "Yönetici" };
        var brans = new Brans { BransId = 1, Ad = "Matematik" };
        var coordinatorRole = new KurumsalRol { KurumsalRolId = 3, Ad = "İl Koordinatörü" };

        var currentUser = CreatePersonel(1, "Koordinator", brans, il, sistemRol);
        var coordinatorMember = CreatePersonel(2, "Uye", brans, il, sistemRol);
        var commissionMember = CreatePersonel(3, "Komisyon", brans, il, sistemRol);
        var outsider = CreatePersonel(4, "Disarida", brans, il, sistemRol);

        currentUser.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama
        {
            Id = 1,
            PersonelId = currentUser.PersonelId,
            Personel = currentUser,
            KurumsalRolId = coordinatorRole.KurumsalRolId,
            KurumsalRol = coordinatorRole,
            TeskilatId = authorizedTeskilat.TeskilatId,
            Teskilat = authorizedTeskilat,
            KoordinatorlukId = authorizedKoordinatorluk.KoordinatorlukId,
            Koordinatorluk = authorizedKoordinatorluk
        });

        coordinatorMember.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk
        {
            PersonelId = coordinatorMember.PersonelId,
            Personel = coordinatorMember,
            KoordinatorlukId = authorizedKoordinatorluk.KoordinatorlukId,
            Koordinatorluk = authorizedKoordinatorluk
        });

        commissionMember.PersonelKomisyonlar.Add(new PersonelKomisyon
        {
            PersonelId = commissionMember.PersonelId,
            Personel = commissionMember,
            KomisyonId = authorizedKomisyon.KomisyonId,
            Komisyon = authorizedKomisyon
        });

        outsider.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk
        {
            PersonelId = outsider.PersonelId,
            Personel = outsider,
            KoordinatorlukId = otherKoordinatorluk.KoordinatorlukId,
            Koordinatorluk = otherKoordinatorluk
        });
        outsider.PersonelKomisyonlar.Add(new PersonelKomisyon
        {
            PersonelId = outsider.PersonelId,
            Personel = outsider,
            KomisyonId = otherKomisyon.KomisyonId,
            Komisyon = otherKomisyon
        });

        context.AddRange(
            il,
            authorizedTeskilat,
            otherTeskilat,
            authorizedKoordinatorluk,
            otherKoordinatorluk,
            authorizedKomisyon,
            otherKomisyon,
            sistemRol,
            brans,
            coordinatorRole,
            currentUser,
            coordinatorMember,
            commissionMember,
            outsider);

        await context.SaveChangesAsync();

        return new AuthorizationSeed(
            currentUser.PersonelId,
            coordinatorMember.PersonelId,
            commissionMember.PersonelId,
            authorizedTeskilat.TeskilatId,
            authorizedKoordinatorluk.KoordinatorlukId,
            authorizedKomisyon.KomisyonId);
    }

    private static Personel CreatePersonel(int id, string ad, Brans brans, Il il, SistemRol sistemRol)
    {
        return new Personel
        {
            PersonelId = id,
            Ad = ad,
            Soyad = "Test",
            TcKimlikNo = $"111111111{id}",
            Telefon = "5551112233",
            Eposta = $"{ad.ToLowerInvariant()}@test.local",
            PersonelCinsiyet = false,
            GorevliIlId = il.IlId,
            GorevliIl = il,
            BransId = brans.BransId,
            Brans = brans,
            KadroKurum = "TEGM",
            AktifMi = true,
            DogumTarihi = new DateTime(1990, 1, 1),
            SifreHash = new byte[] { 1 },
            SifreSalt = new byte[] { 2 },
            SistemRolId = sistemRol.SistemRolId,
            SistemRol = sistemRol
        };
    }

    private static TegmPersonelTakipDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TegmPersonelTakipDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TegmPersonelTakipDbContext(options);
    }

    private sealed record AuthorizationSeed(
        int CurrentUserId,
        int CoordinatorMemberId,
        int CommissionMemberId,
        int AuthorizedTeskilatId,
        int AuthorizedKoordinatorlukId,
        int AuthorizedKomisyonId);
}
