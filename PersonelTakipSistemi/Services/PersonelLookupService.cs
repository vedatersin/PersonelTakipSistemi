using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;

namespace PersonelTakipSistemi.Services
{
    public class PersonelLookupService : IPersonelLookupService
    {
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        private readonly TegmPersonelTakipDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public PersonelLookupService(TegmPersonelTakipDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task FillIndexLookupsAsync(LookupListsViewModel model, PersonelIndexFilterViewModel? filter = null)
        {
            model.Yazilimlar = await GetCachedLookupAsync("YazilimlarList", _context.Yazilimlar, x => x.YazilimId, x => x.Ad);
            model.Uzmanliklar = await GetCachedLookupAsync("UzmanliklarList", _context.Uzmanliklar, x => x.UzmanlikId, x => x.Ad);
            model.GorevTurleri = await GetCachedLookupAsync("GorevTurleriList", _context.GorevTurleri, x => x.GorevTuruId, x => x.Ad);
            model.IsNitelikleri = await GetCachedLookupAsync("IsNitelikleriList", _context.IsNitelikleri, x => x.IsNiteligiId, x => x.Ad);

            model.Branslar = await _context.Branslar.AsNoTracking()
                .OrderBy(b => b.Ad)
                .Select(b => new LookupItemVm { Id = b.BransId, Ad = b.Ad })
                .ToListAsync();

            model.Iller = await _context.Iller.AsNoTracking()
                .OrderBy(i => i.Ad)
                .Select(i => new LookupItemVm { Id = i.IlId, Ad = i.Ad })
                .ToListAsync();

            model.Teskilatlar = await _context.Teskilatlar.AsNoTracking()
                .OrderBy(t => t.Ad)
                .Select(t => new LookupItemVm { Id = t.TeskilatId, Ad = t.Ad })
                .ToListAsync();

            model.Koordinatorlukler = filter?.TeskilatId.HasValue == true
                ? await _context.Koordinatorlukler.AsNoTracking()
                    .Where(k => k.TeskilatId == filter.TeskilatId.Value)
                    .OrderBy(k => k.Ad)
                    .Select(k => new LookupItemVm { Id = k.KoordinatorlukId, Ad = k.Ad })
                    .ToListAsync()
                : new List<LookupItemVm>();

            if (filter?.KoordinatorlukId.HasValue == true)
            {
                var komisyonlar = await _context.Komisyonlar
                    .AsNoTracking()
                    .Include(k => k.Koordinatorluk)
                    .ThenInclude(koord => koord.Il)
                    .Where(x => (x.KoordinatorlukId == filter.KoordinatorlukId.Value || x.BagliMerkezKoordinatorlukId == filter.KoordinatorlukId.Value) && x.IsActive)
                    .ToListAsync();

                model.Komisyonlar = komisyonlar
                    .Select(k => new LookupItemVm
                    {
                        Id = k.KomisyonId,
                        Ad = k.BagliMerkezKoordinatorlukId == filter.KoordinatorlukId.Value && k.Koordinatorluk?.Il != null
                            ? $"{k.Koordinatorluk.Il.Ad} Komisyonu"
                            : k.Ad
                    })
                    .OrderBy(x => x.Ad)
                    .ToList();
            }
            else
            {
                model.Komisyonlar = new List<LookupItemVm>();
            }
        }

        public async Task<PersonelFormLookupData> FillFormLookupsAsync(PersonelEkleViewModel model)
        {
            model.Yazilimlar = await GetCachedLookupAsync("YazilimlarList", _context.Yazilimlar, x => x.YazilimId, x => x.Ad);
            model.Uzmanliklar = await GetCachedLookupAsync("UzmanliklarList", _context.Uzmanliklar, x => x.UzmanlikId, x => x.Ad);
            model.GorevTurleri = await GetCachedLookupAsync("GorevTurleriList", _context.GorevTurleri, x => x.GorevTuruId, x => x.Ad);
            model.IsNitelikleri = await GetCachedLookupAsync("IsNitelikleriList", _context.IsNitelikleri, x => x.IsNiteligiId, x => x.Ad);

            model.Iller = await _context.Iller.AsNoTracking()
                .OrderBy(i => i.Ad)
                .Select(x => new LookupItemVm { Id = x.IlId, Ad = x.Ad })
                .ToListAsync();

            model.Branslar = await _context.Branslar.AsNoTracking()
                .OrderBy(b => b.Ad)
                .Select(x => new LookupItemVm { Id = x.BransId, Ad = x.Ad })
                .ToListAsync();

            model.SistemRolleri = await _context.SistemRoller.AsNoTracking()
                .Select(x => new LookupItemVm { Id = x.SistemRolId, Ad = x.Ad })
                .ToListAsync();

            model.KurumsalRoller = await _context.KurumsalRoller.AsNoTracking()
                .Select(x => new LookupItemVm { Id = x.KurumsalRolId, Ad = x.Ad })
                .ToListAsync();

            model.Teskilatlar = await _context.Teskilatlar.AsNoTracking()
                .Select(x => new LookupItemVm { Id = x.TeskilatId, Ad = x.Ad, Tur = x.Tur })
                .ToListAsync();

            try
            {
                var allKoordinatorlukler = await _context.Koordinatorlukler
                    .AsNoTracking()
                    .Select(x => new PersonelHierarchyItemDto
                    {
                        Id = x.KoordinatorlukId,
                        Ad = x.Ad,
                        ParentId = x.TeskilatId
                    })
                    .ToListAsync();

                var allKomisyonlar = await _context.Komisyonlar
                    .AsNoTracking()
                    .Include(k => k.Koordinatorluk)
                    .ThenInclude(koord => koord.Il)
                    .Select(x => new PersonelHierarchyKomisyonItemDto
                    {
                        Id = x.KomisyonId,
                        Ad = x.BagliMerkezKoordinatorlukId != null && x.Koordinatorluk!.Il != null
                            ? $"{x.Koordinatorluk.Il.Ad} {x.Ad}"
                            : x.Ad,
                        ParentId = x.KoordinatorlukId,
                        BagliMerkezKoordinatorlukId = x.BagliMerkezKoordinatorlukId
                    })
                    .ToListAsync();

                return new PersonelFormLookupData
                {
                    AllKoordinatorlukler = allKoordinatorlukler,
                    AllKomisyonlar = allKomisyonlar
                };
            }
            catch
            {
                return new PersonelFormLookupData();
            }
        }

        public Task<List<LookupItemVm>> GetIlceLookupItemsAsync(int ilId)
        {
            return _context.Ilceler
                .AsNoTracking()
                .Where(x => x.IlId == ilId)
                .OrderBy(x => x.Ad)
                .Select(x => new LookupItemVm { Id = x.IlceId, Ad = x.Ad })
                .ToListAsync();
        }

        private async Task<List<LookupItemVm>> GetCachedLookupAsync<TEntity>(
            string cacheKey,
            IQueryable<TEntity> source,
            Func<TEntity, int> idSelector,
            Func<TEntity, string> nameSelector) where TEntity : class
        {
            var cachedItems = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheDuration;

                var items = await source
                    .AsNoTracking()
                    .ToListAsync();

                return items
                    .Select(x => new LookupItemVm { Id = idSelector(x), Ad = nameSelector(x) })
                    .ToList();
            });

            return cachedItems ?? new List<LookupItemVm>();
        }
    }
}
