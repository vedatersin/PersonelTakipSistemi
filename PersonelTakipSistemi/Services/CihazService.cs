using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using System.Net;

namespace PersonelTakipSistemi.Services
{
    public class CihazService : ICihazService
    {
        private const string DigerSecenekAdi = "Diğer";
        private const int DigerCihazTuruId = 999;
        private const int DigerMarkaId = 99999;
        private readonly TegmPersonelTakipDbContext _context;

        public CihazService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<CihazTanimlariViewModel> GetDefinitionsAsync(int? selectedTypeId = null)
        {
            var summary = await GetDefinitionsDataAsync(selectedTypeId);

            return new CihazTanimlariViewModel
            {
                SeciliCihazTuruId = summary.SeciliCihazTuruId,
                CihazTurleri = summary.CihazTurleri.Select(x => new CihazTuru
                {
                    CihazTuruId = x.CihazTuruId,
                    Ad = x.Ad,
                    KullanimAmaci = x.KullanimAmaci,
                    SistemSecenegiMi = x.SistemSecenegiMi
                }).ToList(),
                Markalar = summary.Markalar.Select(x => new CihazMarka
                {
                    CihazMarkaId = x.CihazMarkaId,
                    CihazTuruId = x.CihazTuruId,
                    Ad = x.Ad,
                    SistemSecenegiMi = x.SistemSecenegiMi
                }).ToList()
            };
        }

        public async Task<CihazTanimResponseViewModel> GetDefinitionsDataAsync(int? selectedTypeId = null)
        {
            var types = await _context.CihazTurleri.AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new CihazTuruListItemViewModel
                {
                    CihazTuruId = x.CihazTuruId,
                    Ad = x.Ad,
                    KullanimAmaci = x.KullanimAmaci,
                    SistemSecenegiMi = x.SistemSecenegiMi,
                    BagliMarkaSayisi = _context.CihazMarkalari.Count(m => m.IsActive && m.CihazTuruId == x.CihazTuruId),
                    BagliCihazSayisi = _context.Cihazlar.Count(c => c.CihazTuruId == x.CihazTuruId)
                })
                .OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0)
                .ThenBy(x => x.Ad)
                .ToListAsync();

            var activeTypeId = selectedTypeId ?? types.FirstOrDefault()?.CihazTuruId;
            var brands = activeTypeId.HasValue
                ? await _context.CihazMarkalari.AsNoTracking()
                    .Where(x => x.IsActive && (x.CihazTuruId == activeTypeId.Value || x.CihazMarkaId == DigerMarkaId))
                    .Select(x => new CihazMarkaListItemViewModel
                    {
                        CihazMarkaId = x.CihazMarkaId,
                        CihazTuruId = activeTypeId.Value,
                        Ad = x.Ad,
                        SistemSecenegiMi = x.SistemSecenegiMi,
                        BagliCihazSayisi = _context.Cihazlar.Count(c => c.CihazMarkaId == x.CihazMarkaId)
                    })
                    .OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0)
                    .ThenBy(x => x.Ad)
                    .ToListAsync()
                : new List<CihazMarkaListItemViewModel>();

            return new CihazTanimResponseViewModel
            {
                SeciliCihazTuruId = activeTypeId,
                CihazTurleri = types,
                Markalar = brands
            };
        }

        public async Task AddDeviceTypeAsync(CihazTuruFormModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Ad))
            {
                throw new InvalidOperationException("Cihaz türü adı zorunludur.");
            }

            var name = NormalizeUserText(model.Ad);
            if (await _context.CihazTurleri.AnyAsync(x => x.Ad == name))
            {
                throw new InvalidOperationException("Bu cihaz türü zaten tanımlı.");
            }

            var type = new CihazTuru
            {
                Ad = name,
                KullanimAmaci = NormalizeNullableUserText(model.KullanimAmaci)
            };

            _context.CihazTurleri.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceTypeAsync(CihazTuruFormModel model)
        {
            if (!model.CihazTuruId.HasValue || model.CihazTuruId <= 0)
            {
                throw new InvalidOperationException("Güncellenecek cihaz türü bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(model.Ad))
            {
                throw new InvalidOperationException("Cihaz türü adı zorunludur.");
            }

            var entity = await _context.CihazTurleri.FirstOrDefaultAsync(x => x.CihazTuruId == model.CihazTuruId.Value && x.IsActive);
            if (entity == null)
            {
                throw new InvalidOperationException("Cihaz türü bulunamadı.");
            }

            if (entity.SistemSecenegiMi && entity.Ad == DigerSecenekAdi)
            {
                throw new InvalidOperationException("Diğer cihaz türü düzenlenemez.");
            }

            var name = NormalizeUserText(model.Ad);
            if (await _context.CihazTurleri.AnyAsync(x => x.CihazTuruId != entity.CihazTuruId && x.Ad == name))
            {
                throw new InvalidOperationException("Bu cihaz türü adı zaten kullanımda.");
            }

            entity.Ad = name;
            entity.KullanimAmaci = NormalizeNullableUserText(model.KullanimAmaci);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceTypeAsync(int cihazTuruId, bool onaylandi)
        {
            var entity = await _context.CihazTurleri.FirstOrDefaultAsync(x => x.CihazTuruId == cihazTuruId && x.IsActive);
            if (entity == null)
            {
                throw new InvalidOperationException("Cihaz türü bulunamadı.");
            }

            if (entity.SistemSecenegiMi && entity.Ad == DigerSecenekAdi)
            {
                throw new InvalidOperationException("Diğer cihaz türü silinemez.");
            }

            var deviceCount = await _context.Cihazlar.CountAsync(x => x.CihazTuruId == cihazTuruId);
            if (deviceCount > 0)
            {
                throw new InvalidOperationException($"Bu cihaz türüne bağlı {deviceCount} cihaz var. Önce cihaz kayıtlarını güncelleyip bu türü boşa çıkarmalısınız.");
            }

            var brands = await _context.CihazMarkalari.Where(x => x.CihazTuruId == cihazTuruId).ToListAsync();
            _context.CihazMarkalari.RemoveRange(brands);
            _context.CihazTurleri.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddBrandAsync(CihazMarkaFormModel model)
        {
            if (model.CihazTuruId <= 0 || string.IsNullOrWhiteSpace(model.Ad))
            {
                throw new InvalidOperationException("Cihaz türü ve marka adı zorunludur.");
            }

            var brandName = NormalizeUserText(model.Ad);
            if (!await _context.CihazTurleri.AnyAsync(x => x.CihazTuruId == model.CihazTuruId && x.IsActive))
            {
                throw new InvalidOperationException("Seçilen cihaz türü bulunamadı.");
            }

            if (await _context.CihazMarkalari.AnyAsync(x => x.CihazTuruId == model.CihazTuruId && x.Ad == brandName))
            {
                throw new InvalidOperationException("Bu marka zaten seçilen cihaz türünde mevcut.");
            }

            _context.CihazMarkalari.Add(new CihazMarka
            {
                CihazTuruId = model.CihazTuruId,
                Ad = brandName
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(CihazMarkaFormModel model)
        {
            if (!model.CihazMarkaId.HasValue || model.CihazMarkaId <= 0)
            {
                throw new InvalidOperationException("Güncellenecek marka bulunamadı.");
            }

            if (model.CihazTuruId <= 0 || string.IsNullOrWhiteSpace(model.Ad))
            {
                throw new InvalidOperationException("Cihaz türü ve marka adı zorunludur.");
            }

            var entity = await _context.CihazMarkalari.FirstOrDefaultAsync(x => x.CihazMarkaId == model.CihazMarkaId.Value && x.IsActive);
            if (entity == null)
            {
                throw new InvalidOperationException("Marka bulunamadı.");
            }

            if (entity.SistemSecenegiMi && entity.Ad == DigerSecenekAdi)
            {
                throw new InvalidOperationException("Diğer marka düzenlenemez.");
            }

            var newName = NormalizeUserText(model.Ad);
            if (!await _context.CihazTurleri.AnyAsync(x => x.CihazTuruId == model.CihazTuruId && x.IsActive))
            {
                throw new InvalidOperationException("Seçilen cihaz türü bulunamadı.");
            }

            if (await _context.CihazMarkalari.AnyAsync(x => x.CihazMarkaId != entity.CihazMarkaId && x.CihazTuruId == model.CihazTuruId && x.Ad == newName))
            {
                throw new InvalidOperationException("Bu marka adı seçilen cihaz türünde zaten kullanımda.");
            }

            var cihazTuruDegisti = entity.CihazTuruId != model.CihazTuruId;
            var bagliCihazSayisi = await _context.Cihazlar.CountAsync(x => x.CihazMarkaId == entity.CihazMarkaId);
            if (cihazTuruDegisti && bagliCihazSayisi > 0)
            {
                throw new InvalidOperationException($"Bu markaya bağlı {bagliCihazSayisi} cihaz var. Önce cihaz kayıtlarını doğru türe/markaya güncellemelisiniz.");
            }

            entity.CihazTuruId = model.CihazTuruId;
            entity.Ad = newName;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBrandAsync(int cihazMarkaId, bool onaylandi)
        {
            var entity = await _context.CihazMarkalari.FirstOrDefaultAsync(x => x.CihazMarkaId == cihazMarkaId && x.IsActive);
            if (entity == null)
            {
                throw new InvalidOperationException("Marka bulunamadı.");
            }

            if (entity.SistemSecenegiMi && entity.Ad == DigerSecenekAdi)
            {
                throw new InvalidOperationException("Diğer marka silinemez.");
            }

            var deviceCount = await _context.Cihazlar.CountAsync(x => x.CihazMarkaId == cihazMarkaId);
            if (deviceCount > 0)
            {
                throw new InvalidOperationException($"Bu markaya bağlı {deviceCount} cihaz var. Önce cihaz kayıtlarını güncellemelisiniz.");
            }

            _context.CihazMarkalari.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<LookupItemVm>> GetBrandsByTypeAsync(int cihazTuruId)
        {
            return _context.CihazMarkalari.AsNoTracking()
                .Where(x => x.IsActive && (x.CihazTuruId == cihazTuruId || x.CihazMarkaId == DigerMarkaId))
                .OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0)
                .ThenBy(x => x.Ad)
                .Select(x => new LookupItemVm { Id = x.CihazMarkaId, Ad = x.Ad })
                .ToListAsync();
        }

        public Task<List<LookupItemVm>> GetActiveDeviceTypesAsync()
        {
            return _context.CihazTurleri.AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0)
                .ThenBy(x => x.Ad)
                .Select(x => new LookupItemVm { Id = x.CihazTuruId, Ad = x.Ad })
                .ToListAsync();
        }

        public async Task<CihazListePageViewModel> GetMyDevicesAsync(int currentPersonelId, CihazListeFilterViewModel filter)
        {
            var query = BaseDeviceQuery().Where(x => x.SahipPersonelId == currentPersonelId);
            query = ApplyDeviceFilters(query, filter);

            var model = await CreateListViewModelAsync(query, filter, currentPersonelId, false, false, "Cihazlarım");
            model.PersonelGorunumuMu = true;
            model.CihazEklemeYetkisiVarMi = true;
            model.EklemeIcinMarkalar = filter.CihazTuruId.HasValue ? await GetBrandsByTypeAsync(filter.CihazTuruId.Value) : new();
            return model;
        }

        public async Task<CihazListePageViewModel> GetManagedDevicesAsync(int currentPersonelId, bool isAdmin, CihazListeFilterViewModel filter)
        {
            var query = BaseDeviceQuery();
            if (isAdmin)
            {
                // Admin tum cihazlari gorebilmeli (onay bekleyenler dahil).
            }
            else
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Any())
                {
                    throw new InvalidOperationException("Koordinatörlük yetkisi bulunamadı.");
                }

                query = query.Where(x => scopeIds.Contains(x.KoordinatorlukId));
            }

            query = ApplyDeviceFilters(query, filter);

            var model = await CreateListViewModelAsync(query, filter, currentPersonelId, true, isAdmin, isAdmin ? "Cihazlar" : "Koordinatörlük Cihazları");
            model.AdminPaneliMi = isAdmin;
            model.KoordinatorPaneliMi = !isAdmin;
            model.CihazEklemeYetkisiVarMi = true;
            model.OnayYetkisiVarMi = true;
            model.EklemeIcinMarkalar = filter.CihazTuruId.HasValue ? await GetBrandsByTypeAsync(filter.CihazTuruId.Value) : new();
            model.EklemeIcinPersoneller = await GetAssignablePersonnelLookupAsync(currentPersonelId, isAdmin);
            return model;
        }

        public async Task<int> CreateDeviceAsync(CihazCreateViewModel model, int currentPersonelId, bool isCoordinator)
        {
            ValidateCreateModel(model);
            var isAdmin = await IsAdminAsync(currentPersonelId);

            var cihazTuru = await _context.CihazTurleri.FirstOrDefaultAsync(x => x.CihazTuruId == model.CihazTuruId);
            var marka = await _context.CihazMarkalari.FirstOrDefaultAsync(x => x.CihazMarkaId == model.CihazMarkaId);

            if (cihazTuru == null || marka == null)
            {
                throw new InvalidOperationException("Seçilen cihaz türü veya marka geçersiz.");
            }

            if (marka.CihazMarkaId != DigerMarkaId && marka.CihazTuruId != cihazTuru.CihazTuruId)
            {
                throw new InvalidOperationException("Seçilen marka, seçilen cihaz türüne bağlı değil.");
            }

            if (cihazTuru.CihazTuruId == DigerCihazTuruId && string.IsNullOrWhiteSpace(model.DigerCihazTuruAd))
            {
                throw new InvalidOperationException("Diğer cihaz türü seçildiğinde cihaz türü adı girilmelidir.");
            }

            if (marka.CihazMarkaId == DigerMarkaId && string.IsNullOrWhiteSpace(model.DigerMarkaAd))
            {
                throw new InvalidOperationException("Diğer marka seçildiğinde marka adı girilmelidir.");
            }

            var ownerId = isCoordinator ? model.SahipPersonelId ?? 0 : currentPersonelId;
            if (ownerId <= 0)
            {
                throw new InvalidOperationException("Cihaz sahibi seçilmelidir.");
            }

            await EnsureActivePersonelAsync(ownerId, "Cihaz sahibi aktif personel olmalıdır.");
            var koordinatorlukId = await ResolveKoordinatorlukIdAsync(ownerId, currentPersonelId, isCoordinator, isAdmin);
            var now = DateTime.Now;

            var entity = new Cihaz
            {
                CihazTuruId = cihazTuru.CihazTuruId,
                CihazMarkaId = marka.CihazMarkaId,
                DigerCihazTuruAd = cihazTuru.CihazTuruId == DigerCihazTuruId ? NormalizeNullableUserText(model.DigerCihazTuruAd) : null,
                DigerMarkaAd = marka.CihazMarkaId == DigerMarkaId ? NormalizeNullableUserText(model.DigerMarkaAd) : null,
                Model = NormalizeUserText(model.Model),
                Ozellikler = NormalizeUserText(model.Ozellikler),
                SeriNo = NormalizeUserText(model.SeriNo),
                SahipPersonelId = ownerId,
                KoordinatorlukId = koordinatorlukId,
                IlkKayitTarihi = now,
                AktifSahiplikBaslangicTarihi = now,
                OnayDurumu = isCoordinator ? CihazOnayDurumu.Onaylandi : CihazOnayDurumu.Beklemede,
                OnaylayanPersonelId = isCoordinator ? currentPersonelId : null,
                OnayTarihi = isCoordinator ? now : null,
                OlusturanPersonelId = currentPersonelId,
                KoordinatorTarafindanEklendi = isCoordinator,
                CreatedAt = now
            };

            _context.Cihazlar.Add(entity);
            await _context.SaveChangesAsync();
            var islemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId);
            var sahipAdSoyad = await GetPersonelAdSoyadAsync(ownerId);

            _context.CihazHareketleri.Add(new CihazHareketi
            {
                CihazId = entity.CihazId,
                HareketTuru = CihazHareketTuru.Kayit,
                YeniSahipPersonelId = ownerId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = islemYapanAdSoyad,
                YeniSahipAdSoyad = sahipAdSoyad,
                Aciklama = isCoordinator ? "Koordinatör tarafından cihaz kaydı oluşturuldu." : "Personel tarafından cihaz kaydı oluşturuldu ve onaya gönderildi.",
                Tarih = now
            });

            if (isCoordinator)
            {
                _context.CihazHareketleri.Add(new CihazHareketi
                {
                    CihazId = entity.CihazId,
                    HareketTuru = CihazHareketTuru.Onay,
                    YeniSahipPersonelId = ownerId,
                    IslemYapanPersonelId = currentPersonelId,
                    IslemYapanAdSoyad = islemYapanAdSoyad,
                    YeniSahipAdSoyad = sahipAdSoyad,
                    Aciklama = "Koordinatör cihazı doğrudan onaylı olarak ekledi.",
                    Tarih = now
                });
            }

            await _context.SaveChangesAsync();
            return entity.CihazId;
        }

        public async Task<bool> UpdateDeviceAsync(CihazCreateViewModel model, int currentPersonelId, bool canManage)
        {
            if (!model.CihazId.HasValue || model.CihazId <= 0)
            {
                throw new InvalidOperationException("Güncellenecek cihaz bulunamadı.");
            }

            ValidateCreateModel(model);

            var entity = await _context.Cihazlar.FirstOrDefaultAsync(x => x.CihazId == model.CihazId.Value);
            if (entity == null)
            {
                throw new InvalidOperationException("Cihaz bulunamadı.");
            }

            if (!canManage && entity.SahipPersonelId != currentPersonelId)
            {
                throw new InvalidOperationException("Bu cihazı düzenleme yetkiniz bulunmuyor.");
            }

            var cihazTuru = await _context.CihazTurleri.FirstOrDefaultAsync(x => x.CihazTuruId == model.CihazTuruId);
            var marka = await _context.CihazMarkalari.FirstOrDefaultAsync(x => x.CihazMarkaId == model.CihazMarkaId);
            if (cihazTuru == null || marka == null)
            {
                throw new InvalidOperationException("Seçilen cihaz türü veya marka geçersiz.");
            }

            if (marka.CihazMarkaId != DigerMarkaId && marka.CihazTuruId != cihazTuru.CihazTuruId)
            {
                throw new InvalidOperationException("Seçilen marka, seçilen cihaz türüne bağlı değil.");
            }

            if (cihazTuru.CihazTuruId == DigerCihazTuruId && string.IsNullOrWhiteSpace(model.DigerCihazTuruAd))
            {
                throw new InvalidOperationException("Diğer cihaz türü seçildiğinde cihaz türü adı girilmelidir.");
            }

            if (marka.CihazMarkaId == DigerMarkaId && string.IsNullOrWhiteSpace(model.DigerMarkaAd))
            {
                throw new InvalidOperationException("Diğer marka seçildiğinde marka adı girilmelidir.");
            }

            entity.CihazTuruId = cihazTuru.CihazTuruId;
            entity.CihazMarkaId = marka.CihazMarkaId;
            entity.DigerCihazTuruAd = cihazTuru.CihazTuruId == DigerCihazTuruId ? NormalizeNullableUserText(model.DigerCihazTuruAd) : null;
            entity.DigerMarkaAd = marka.CihazMarkaId == DigerMarkaId ? NormalizeNullableUserText(model.DigerMarkaAd) : null;
            entity.Model = NormalizeUserText(model.Model);
            entity.Ozellikler = NormalizeUserText(model.Ozellikler);
            entity.SeriNo = NormalizeUserText(model.SeriNo);
            var now = DateTime.Now;
            entity.UpdatedAt = now;

            var approvalRequired = !canManage;
            if (approvalRequired)
            {
                entity.OnayDurumu = CihazOnayDurumu.Beklemede;
                entity.OnaylayanPersonelId = null;
                entity.OnayTarihi = null;

                _context.CihazHareketleri.Add(new CihazHareketi
                {
                    CihazId = entity.CihazId,
                    HareketTuru = CihazHareketTuru.Kayit,
                    YeniSahipPersonelId = entity.SahipPersonelId,
                    IslemYapanPersonelId = currentPersonelId,
                    IslemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId),
                    YeniSahipAdSoyad = await GetPersonelAdSoyadAsync(entity.SahipPersonelId),
                    Aciklama = "Personel cihaz bilgilerini güncelledi ve tekrar onaya gönderdi.",
                    Tarih = now
                });
            }

            await _context.SaveChangesAsync();
            return approvalRequired;
        }

        public async Task QuickUpdateDeviceAsync(CihazHizliDuzenleViewModel model, int currentPersonelId, bool isAdmin)
        {
            if (!isAdmin)
            {
                throw new InvalidOperationException("Bu işlem için admin yetkisi gereklidir.");
            }

            var entity = await _context.Cihazlar.FirstOrDefaultAsync(x => x.CihazId == model.CihazId);
            if (entity == null)
            {
                throw new InvalidOperationException("Cihaz bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(model.Model) || string.IsNullOrWhiteSpace(model.Ozellikler) || string.IsNullOrWhiteSpace(model.SeriNo))
            {
                throw new InvalidOperationException("Model, özellikler ve seri no alanları zorunludur.");
            }

            var cihazTuru = await _context.CihazTurleri.FirstOrDefaultAsync(x => x.CihazTuruId == model.CihazTuruId && x.IsActive);
            var marka = await _context.CihazMarkalari.FirstOrDefaultAsync(x => x.CihazMarkaId == model.CihazMarkaId && x.IsActive);
            if (cihazTuru == null || marka == null)
            {
                throw new InvalidOperationException("Seçilen cihaz türü veya marka geçersiz.");
            }

            if (marka.CihazMarkaId != DigerMarkaId && marka.CihazTuruId != cihazTuru.CihazTuruId)
            {
                throw new InvalidOperationException("Seçilen marka, seçilen cihaz türüne bağlı değil.");
            }

            if (cihazTuru.CihazTuruId == DigerCihazTuruId && string.IsNullOrWhiteSpace(model.DigerCihazTuruAd))
            {
                throw new InvalidOperationException("Diğer cihaz türü seçildiğinde cihaz türü adı girilmelidir.");
            }

            if (marka.CihazMarkaId == DigerMarkaId && string.IsNullOrWhiteSpace(model.DigerMarkaAd))
            {
                throw new InvalidOperationException("Diğer marka seçildiğinde marka adı girilmelidir.");
            }

            entity.CihazTuruId = cihazTuru.CihazTuruId;
            entity.CihazMarkaId = marka.CihazMarkaId;
            entity.DigerCihazTuruAd = cihazTuru.CihazTuruId == DigerCihazTuruId ? NormalizeNullableUserText(model.DigerCihazTuruAd) : null;
            entity.DigerMarkaAd = marka.CihazMarkaId == DigerMarkaId ? NormalizeNullableUserText(model.DigerMarkaAd) : null;
            entity.Model = NormalizeUserText(model.Model);
            entity.Ozellikler = NormalizeUserText(model.Ozellikler);
            entity.SeriNo = NormalizeUserText(model.SeriNo);
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int cihazId, int currentPersonelId, bool isAdmin)
        {
            if (!isAdmin)
            {
                throw new InvalidOperationException("Bu işlem için admin yetkisi gereklidir.");
            }

            var entity = await _context.Cihazlar.FirstOrDefaultAsync(x => x.CihazId == cihazId);
            if (entity == null)
            {
                throw new InvalidOperationException("Cihaz bulunamadı.");
            }

            _context.Cihazlar.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveDeviceAsync(int cihazId, int currentPersonelId)
        {
            var isAdmin = await IsAdminAsync(currentPersonelId);
            var scopeIds = isAdmin ? new List<int>() : await GetCoordinatorScopeIdsAsync(currentPersonelId);
            var cihaz = await _context.Cihazlar.FirstOrDefaultAsync(x => x.CihazId == cihazId);

            if (cihaz == null)
            {
                throw new InvalidOperationException("Cihaz bulunamadı.");
            }

            if (!isAdmin && !scopeIds.Contains(cihaz.KoordinatorlukId))
            {
                throw new InvalidOperationException("Bu cihazı onaylama yetkiniz yok.");
            }

            if (cihaz.OnayDurumu == CihazOnayDurumu.Onaylandi)
            {
                return;
            }

            var now = DateTime.Now;
            cihaz.OnayDurumu = CihazOnayDurumu.Onaylandi;
            cihaz.OnaylayanPersonelId = currentPersonelId;
            cihaz.OnayTarihi = now;
            cihaz.UpdatedAt = now;

            _context.CihazHareketleri.Add(new CihazHareketi
            {
                CihazId = cihaz.CihazId,
                HareketTuru = CihazHareketTuru.Onay,
                YeniSahipPersonelId = cihaz.SahipPersonelId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId),
                YeniSahipAdSoyad = await GetPersonelAdSoyadAsync(cihaz.SahipPersonelId),
                Aciklama = "Cihaz koordinatör tarafından onaylandı.",
                Tarih = now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<CihazDetayViewModel?> GetDetailAsync(int cihazId, int currentPersonelId, bool canManage)
        {
            var cihaz = await _context.Cihazlar.AsNoTracking()
                .Include(x => x.CihazTuru)
                .Include(x => x.CihazMarka)
                .Include(x => x.SahipPersonel)
                .Include(x => x.Koordinatorluk)
                .Include(x => x.Hareketler).ThenInclude(x => x.IslemYapanPersonel)
                .Include(x => x.Hareketler).ThenInclude(x => x.OncekiSahipPersonel)
                .Include(x => x.Hareketler).ThenInclude(x => x.YeniSahipPersonel)
                .FirstOrDefaultAsync(x => x.CihazId == cihazId);

            if (cihaz == null)
            {
                return null;
            }

            if (!canManage && cihaz.SahipPersonelId != currentPersonelId)
            {
                return null;
            }

            if (canManage && !await IsAdminAsync(currentPersonelId))
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Contains(cihaz.KoordinatorlukId))
                {
                    return null;
                }
            }

            var devralinmisCihazMi = cihaz.AktifSahiplikBaslangicTarihi > cihaz.IlkKayitTarihi;

            return new CihazDetayViewModel
            {
                CihazId = cihaz.CihazId,
                CihazTuru = ResolveTypeName(cihaz),
                Marka = ResolveBrandName(cihaz),
                Model = DecodeLegacyEntityText(cihaz.Model),
                Ozellikler = DecodeLegacyEntityText(cihaz.Ozellikler),
                SeriNo = DecodeLegacyEntityText(cihaz.SeriNo),
                SahipAdSoyad = DecodeLegacyEntityText(cihaz.SahipPersonel != null ? $"{cihaz.SahipPersonel.Ad} {cihaz.SahipPersonel.Soyad}" : "Sahipsiz"),
                KoordinatorlukAd = DecodeLegacyEntityText(cihaz.Koordinatorluk.Ad),
                IlkKayitTarihi = cihaz.IlkKayitTarihi,
                GosterilecekKayitTarihi = canManage || !devralinmisCihazMi ? cihaz.IlkKayitTarihi : cihaz.AktifSahiplikBaslangicTarihi,
                KayitTarihiBasligi = canManage || !devralinmisCihazMi ? "İlk Kayıt Tarihi" : "Devir Tarihi",
                OnayDurumu = cihaz.OnayDurumu,
                OnayYetkisiVarMi = canManage && cihaz.OnayDurumu == CihazOnayDurumu.Beklemede,
                HareketlerGorunsunMu = canManage,
                DevirYetkisiVarMi = canManage && cihaz.OnayDurumu == CihazOnayDurumu.Onaylandi,
                DuzenlemeYetkisiVarMi = canManage || cihaz.SahipPersonelId == currentPersonelId,
                DuzenlemeOnayGerekliMi = !canManage && cihaz.SahipPersonelId == currentPersonelId,
                CihazTurleri = await _context.CihazTurleri.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0).ThenBy(x => x.Ad).Select(x => new LookupItemVm { Id = x.CihazTuruId, Ad = x.Ad }).ToListAsync(),
                Markalar = await GetBrandsByTypeAsync(cihaz.CihazTuruId),
                DuzenleFormu = new CihazCreateViewModel
                {
                    CihazId = cihaz.CihazId,
                    CihazTuruId = cihaz.CihazTuruId,
                    CihazMarkaId = cihaz.CihazMarkaId,
                    DigerCihazTuruAd = DecodeLegacyEntityText(cihaz.DigerCihazTuruAd),
                    DigerMarkaAd = DecodeLegacyEntityText(cihaz.DigerMarkaAd),
                    Model = DecodeLegacyEntityText(cihaz.Model),
                    Ozellikler = DecodeLegacyEntityText(cihaz.Ozellikler),
                    SeriNo = DecodeLegacyEntityText(cihaz.SeriNo)
                },
                DevredilebilirPersoneller = canManage ? await GetAssignablePersonnelByKoordinatorlukAsync(cihaz.KoordinatorlukId, cihaz.SahipPersonelId) : new(),
                DevirFormu = new CihazTransferFormModel { CihazId = cihaz.CihazId },
                Hareketler = cihaz.Hareketler.OrderByDescending(x => x.Tarih).Select(x => new CihazHareketItemViewModel
                {
                    Tarih = x.Tarih,
                    HareketTuru = x.HareketTuru switch
                    {
                        CihazHareketTuru.Kayit => "Kayıt",
                        CihazHareketTuru.Onay => "Onay",
                        CihazHareketTuru.Devir => "Devir",
                        _ => "Hareket"
                    },
                    IslemYapan = DecodeLegacyEntityText(ResolvePersonDisplayName(x.IslemYapanPersonel, x.IslemYapanAdSoyad, "Silinen Kullanıcı")),
                    IslemYapanCizili = ShouldRenderStrikethrough(x.IslemYapanPersonel, x.IslemYapanAdSoyad),
                    OncekiSahip = x.OncekiSahipPersonelId.HasValue || !string.IsNullOrWhiteSpace(x.OncekiSahipAdSoyad)
                        ? DecodeLegacyEntityText(ResolvePersonDisplayName(x.OncekiSahipPersonel, x.OncekiSahipAdSoyad, "Silinen Kullanıcı"))
                        : null,
                    OncekiSahipCizili = x.OncekiSahipPersonelId.HasValue || !string.IsNullOrWhiteSpace(x.OncekiSahipAdSoyad)
                        ? ShouldRenderStrikethrough(x.OncekiSahipPersonel, x.OncekiSahipAdSoyad)
                        : false,
                    YeniSahip = x.YeniSahipPersonelId.HasValue || !string.IsNullOrWhiteSpace(x.YeniSahipAdSoyad)
                        ? DecodeLegacyEntityText(ResolvePersonDisplayName(x.YeniSahipPersonel, x.YeniSahipAdSoyad, "Sahipsiz"))
                        : null,
                    YeniSahipCizili = x.YeniSahipPersonelId.HasValue || !string.IsNullOrWhiteSpace(x.YeniSahipAdSoyad)
                        ? ShouldRenderStrikethrough(x.YeniSahipPersonel, x.YeniSahipAdSoyad)
                        : false,
                    Aciklama = DecodeLegacyEntityText(x.Aciklama),
                    DurumNotu = DecodeLegacyEntityText(x.DurumNotu)
                }).ToList()
            };
        }

        public async Task TransferDeviceAsync(CihazTransferFormModel model, int currentPersonelId, bool canManage)
        {
            if (!canManage)
            {
                throw new InvalidOperationException("Bu işlem için yetkiniz bulunmuyor.");
            }

            if (model.YeniSahipPersonelId <= 0 || string.IsNullOrWhiteSpace(model.DevirNotu) || string.IsNullOrWhiteSpace(model.CihazDurumNotu))
            {
                throw new InvalidOperationException("Devredilecek personel, devir notu ve cihaz notu zorunludur.");
            }

            var cihaz = await _context.Cihazlar.FirstOrDefaultAsync(x => x.CihazId == model.CihazId);
            if (cihaz == null)
            {
                throw new InvalidOperationException("Cihaz bulunamadı.");
            }

            var isAdmin = await IsAdminAsync(currentPersonelId);
            if (!isAdmin)
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Contains(cihaz.KoordinatorlukId))
                {
                    throw new InvalidOperationException("Bu cihazı devretme yetkiniz bulunmuyor.");
                }
            }

            if (cihaz.SahipPersonelId.HasValue && cihaz.SahipPersonelId.Value == model.YeniSahipPersonelId)
            {
                throw new InvalidOperationException("Cihaz zaten seçilen personele ait.");
            }

            await EnsureActivePersonelAsync(model.YeniSahipPersonelId, "Cihaz sadece aktif personele devredilebilir.");

            var hedefAyniKoordinatorlukteMi = await _context.PersonelKoordinatorlukler
                .AnyAsync(x => x.PersonelId == model.YeniSahipPersonelId && x.KoordinatorlukId == cihaz.KoordinatorlukId);

            if (!hedefAyniKoordinatorlukteMi)
            {
                throw new InvalidOperationException("Cihaz sadece aynı koordinatörlükteki personele devredilebilir.");
            }

            var kilitVarMi = await _context.CihazHareketleri.AnyAsync(x =>
                x.CihazId == cihaz.CihazId &&
                x.HareketTuru == CihazHareketTuru.Devir &&
                x.Tarih >= DateTime.Now.AddMinutes(-5));

            if (kilitVarMi)
            {
                throw new InvalidOperationException("Aynı cihaz 5 dakika içinde tekrar devredilemez.");
            }

            var now = DateTime.Now;
            var oncekiSahipId = cihaz.SahipPersonelId;
            var islemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId);
            var oncekiSahipAdSoyad = await GetPersonelAdSoyadAsync(oncekiSahipId);
            var yeniSahipAdSoyad = await GetPersonelAdSoyadAsync(model.YeniSahipPersonelId);
            cihaz.SahipPersonelId = model.YeniSahipPersonelId;
            cihaz.AktifSahiplikBaslangicTarihi = now;
            cihaz.SonDevirTarihi = now;
            cihaz.UpdatedAt = now;

            _context.CihazHareketleri.Add(new CihazHareketi
            {
                CihazId = cihaz.CihazId,
                HareketTuru = CihazHareketTuru.Devir,
                OncekiSahipPersonelId = oncekiSahipId,
                YeniSahipPersonelId = model.YeniSahipPersonelId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = islemYapanAdSoyad,
                OncekiSahipAdSoyad = oncekiSahipAdSoyad,
                YeniSahipAdSoyad = yeniSahipAdSoyad,
                Aciklama = NormalizeUserText(model.DevirNotu),
                DurumNotu = NormalizeUserText(model.CihazDurumNotu),
                Tarih = now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<YazilimLisanslarimViewModel> GetSoftwareLicensesAsync(int personelId)
        {
            var yazilimlar = await _context.PersonelYazilimlar.AsNoTracking()
                .Where(x => x.PersonelId == personelId)
                .Include(x => x.Yazilim)
                .OrderBy(x => x.Yazilim.Ad)
                .Select(x => x.Yazilim.Ad)
                .ToListAsync();

            return new YazilimLisanslarimViewModel
            {
                Lisanslar = yazilimlar.Select(x => new YazilimLisanslarimItemViewModel
                {
                    YazilimAdi = DecodeLegacyEntityText(x),
                    LisansSuresiTuru = "-",
                    OnayDurumu = "Onaylandı"
                }).ToList()
            };
        }

        public async Task<bool> IsCoordinatorAsync(int personelId)
        {
            return (await GetCoordinatorScopeIdsAsync(personelId)).Any();
        }

        private IQueryable<Cihaz> BaseDeviceQuery()
        {
            return _context.Cihazlar.AsNoTracking()
                .Include(x => x.CihazTuru)
                .Include(x => x.CihazMarka)
                .Include(x => x.SahipPersonel)
                .Include(x => x.Koordinatorluk)
                .Include(x => x.Hareketler);
        }

        private async Task<CihazListePageViewModel> CreateListViewModelAsync(IQueryable<Cihaz> query, CihazListeFilterViewModel filter, int currentPersonelId, bool canManage, bool isAdmin, string title)
        {
            var items = await query.OrderByDescending(x => x.CreatedAt)
                .Select(x => new CihazListeItemViewModel
                {
                    CihazId = x.CihazId,
                    CihazTuruId = x.CihazTuruId,
                    CihazMarkaId = x.CihazMarkaId,
                    DigerCihazTuruAd = x.DigerCihazTuruAd,
                    DigerMarkaAd = x.DigerMarkaAd,
                    CihazTuru = x.DigerCihazTuruAd ?? x.CihazTuru.Ad,
                    Marka = x.DigerMarkaAd ?? x.CihazMarka.Ad,
                    Model = x.Model,
                    Ozellikler = x.Ozellikler,
                    SeriNo = x.SeriNo,
                    SahipAdSoyad = x.SahipPersonel != null ? x.SahipPersonel.Ad + " " + x.SahipPersonel.Soyad : "Sahipsiz",
                    KoordinatorlukAd = x.Koordinatorluk.Ad,
                    IlkKayitTarihi = x.IlkKayitTarihi,
                    AktifSahiplikBaslangicTarihi = x.AktifSahiplikBaslangicTarihi,
                    KoordinatorTarafindanEklendi = x.KoordinatorTarafindanEklendi,
                    OnayDurumu = x.OnayDurumu
                }).ToListAsync();

            foreach (var item in items)
            {
                item.CihazTuru = DecodeLegacyEntityText(item.CihazTuru);
                item.Marka = DecodeLegacyEntityText(item.Marka);
                item.DigerCihazTuruAd = DecodeLegacyEntityText(item.DigerCihazTuruAd);
                item.DigerMarkaAd = DecodeLegacyEntityText(item.DigerMarkaAd);
                item.Model = DecodeLegacyEntityText(item.Model);
                item.Ozellikler = DecodeLegacyEntityText(item.Ozellikler);
                item.SeriNo = DecodeLegacyEntityText(item.SeriNo);
                item.SahipAdSoyad = DecodeLegacyEntityText(item.SahipAdSoyad);
                item.KoordinatorlukAd = DecodeLegacyEntityText(item.KoordinatorlukAd);
            }

            return new CihazListePageViewModel
            {
                Baslik = title,
                Filter = filter,
                Cihazlar = items,
                CihazTurleri = await _context.CihazTurleri.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Ad).Select(x => new LookupItemVm { Id = x.CihazTuruId, Ad = x.Ad }).ToListAsync(),
                Markalar = filter.CihazTuruId.HasValue ? await GetBrandsByTypeAsync(filter.CihazTuruId.Value) : new(),
                Koordinatorlukler = isAdmin ? await _context.Koordinatorlukler.AsNoTracking().OrderBy(x => x.Ad).Select(x => new LookupItemVm { Id = x.KoordinatorlukId, Ad = x.Ad }).ToListAsync() : await GetCoordinatorLookupAsync(currentPersonelId),
                Personeller = canManage ? await GetAssignablePersonnelLookupAsync(currentPersonelId, isAdmin) : new(),
                EklemeIcinCihazTurleri = await _context.CihazTurleri.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Ad == DigerSecenekAdi ? 1 : 0).ThenBy(x => x.Ad).Select(x => new LookupItemVm { Id = x.CihazTuruId, Ad = x.Ad }).ToListAsync()
            };
        }

        private IQueryable<Cihaz> ApplyDeviceFilters(IQueryable<Cihaz> query, CihazListeFilterViewModel filter)
        {
            if (filter.KoordinatorlukId.HasValue) query = query.Where(x => x.KoordinatorlukId == filter.KoordinatorlukId.Value);
            if (filter.PersonelId.HasValue)
            {
                if (filter.PersonelId.Value == -1)
                {
                    query = query.Where(x => x.SahipPersonelId == null);
                }
                else
                {
                    query = query.Where(x => x.SahipPersonelId == filter.PersonelId.Value);
                }
            }
            if (filter.CihazTuruId.HasValue) query = query.Where(x => x.CihazTuruId == filter.CihazTuruId.Value);
            if (filter.CihazMarkaId.HasValue) query = query.Where(x => x.CihazMarkaId == filter.CihazMarkaId.Value);
            if (!string.IsNullOrWhiteSpace(filter.ModelAra)) query = query.Where(x => x.Model.ToLower().Contains(filter.ModelAra.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.OzellikAra)) query = query.Where(x => x.Ozellikler.ToLower().Contains(filter.OzellikAra.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.SeriNoAra)) query = query.Where(x => x.SeriNo.ToLower().Contains(filter.SeriNoAra.Trim().ToLower()));
            if (filter.SadeceOnayBekleyenler) query = query.Where(x => x.OnayDurumu == CihazOnayDurumu.Beklemede);

            if (filter.YasSliderDegeri > 0)
            {
                var today = DateTime.Now.Date;
                query = filter.YasSliderDegeri switch
                {
                    1 => query.Where(x => x.IlkKayitTarihi >= today.AddYears(-1)),
                    2 => query.Where(x => x.IlkKayitTarihi < today.AddYears(-1)),
                    3 => query.Where(x => x.IlkKayitTarihi < today.AddYears(-2)),
                    4 => query.Where(x => x.IlkKayitTarihi < today.AddYears(-3)),
                    5 => query.Where(x => x.IlkKayitTarihi < today.AddYears(-4)),
                    6 => query.Where(x => x.IlkKayitTarihi < today.AddYears(-5)),
                    _ => query
                };
            }

            return query;
        }

        private async Task<int> ResolveKoordinatorlukIdAsync(int ownerId, int currentPersonelId, bool isCoordinator, bool isAdmin)
        {
            var ownerKoordinatorlukleri = await _context.PersonelKoordinatorlukler.AsNoTracking()
                .Where(x => x.PersonelId == ownerId)
                .Select(x => x.KoordinatorlukId)
                .ToListAsync();

            if (!ownerKoordinatorlukleri.Any())
            {
                throw new InvalidOperationException("Seçilen personelin bağlı olduğu koordinatörlük bulunamadı.");
            }

            if (isAdmin || !isCoordinator)
            {
                return ownerKoordinatorlukleri.First();
            }

            var currentScope = await GetCoordinatorScopeIdsAsync(currentPersonelId);
            var ortak = ownerKoordinatorlukleri.FirstOrDefault(currentScope.Contains);
            if (ortak <= 0)
            {
                throw new InvalidOperationException("Sadece kendi koordinatörlüğünüze bağlı personele cihaz ekleyebilirsiniz.");
            }

            return ortak;
        }

        private Task<List<LookupItemVm>> GetCoordinatorLookupAsync(int personelId)
        {
            return _context.Koordinatorlukler.AsNoTracking()
                .Where(x => _context.PersonelKoordinatorlukler.Any(pk => pk.PersonelId == personelId && pk.KoordinatorlukId == x.KoordinatorlukId)
                    || _context.PersonelKurumsalRolAtamalari.Any(pr => pr.PersonelId == personelId && pr.KoordinatorlukId == x.KoordinatorlukId && (pr.KurumsalRolId == 3 || pr.KurumsalRolId == 5)))
                .OrderBy(x => x.Ad)
                .Select(x => new LookupItemVm { Id = x.KoordinatorlukId, Ad = x.Ad })
                .ToListAsync();
        }

        private async Task<List<LookupItemVm>> GetAssignablePersonnelLookupAsync(int currentPersonelId, bool isAdmin)
        {
            if (isAdmin)
            {
                return await _context.Personeller.AsNoTracking()
                    .Where(x => x.AktifMi)
                    .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                    .Select(x => new LookupItemVm { Id = x.PersonelId, Ad = x.Ad + " " + x.Soyad })
                    .ToListAsync();
            }

            var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
            return await _context.Personeller.AsNoTracking()
                .Where(x => x.AktifMi && x.PersonelKoordinatorlukler.Any(pk => scopeIds.Contains(pk.KoordinatorlukId)))
                .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                .Select(x => new LookupItemVm { Id = x.PersonelId, Ad = x.Ad + " " + x.Soyad })
                .ToListAsync();
        }

        private Task<List<LookupItemVm>> GetAssignablePersonnelByKoordinatorlukAsync(int koordinatorlukId, int? excludeId)
        {
            return _context.Personeller.AsNoTracking()
                .Where(x => x.AktifMi && (!excludeId.HasValue || x.PersonelId != excludeId.Value) && x.PersonelKoordinatorlukler.Any(pk => pk.KoordinatorlukId == koordinatorlukId))
                .OrderBy(x => x.Ad).ThenBy(x => x.Soyad)
                .Select(x => new LookupItemVm { Id = x.PersonelId, Ad = x.Ad + " " + x.Soyad })
                .ToListAsync();
        }

        private Task<List<int>> GetCoordinatorScopeIdsAsync(int personelId)
        {
            return _context.PersonelKurumsalRolAtamalari.AsNoTracking()
                .Where(x => x.PersonelId == personelId && x.KoordinatorlukId.HasValue && (x.KurumsalRolId == 3 || x.KurumsalRolId == 5))
                .Select(x => x.KoordinatorlukId!.Value)
                .Distinct()
                .ToListAsync();
        }

        private async Task<bool> IsAdminAsync(int personelId)
        {
            return await _context.Personeller.AsNoTracking()
                .AnyAsync(x => x.PersonelId == personelId && x.SistemRolId == 1);
        }

        private static void ValidateCreateModel(CihazCreateViewModel model)
        {
            if (!model.CihazTuruId.HasValue || !model.CihazMarkaId.HasValue)
            {
                throw new InvalidOperationException("Cihaz türü ve marka seçimi zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(model.Model) || string.IsNullOrWhiteSpace(model.Ozellikler) || string.IsNullOrWhiteSpace(model.SeriNo))
            {
                throw new InvalidOperationException("Model, özellikler ve seri no alanları zorunludur.");
            }
        }

        private async Task EnsureActivePersonelAsync(int personelId, string errorMessage)
        {
            var aktifMi = await _context.Personeller.AsNoTracking()
                .AnyAsync(x => x.PersonelId == personelId && x.AktifMi);

            if (!aktifMi)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
        private static string ResolveTypeName(Cihaz cihaz)
        {
            return DecodeLegacyEntityText(string.IsNullOrWhiteSpace(cihaz.DigerCihazTuruAd) ? cihaz.CihazTuru.Ad : cihaz.DigerCihazTuruAd);
        }

        private static string ResolveBrandName(Cihaz cihaz)
        {
            return DecodeLegacyEntityText(string.IsNullOrWhiteSpace(cihaz.DigerMarkaAd) ? cihaz.CihazMarka.Ad : cihaz.DigerMarkaAd);
        }

        private static string NormalizeUserText(string value)
        {
            var decoded = WebUtility.HtmlDecode(value ?? string.Empty);
            return decoded.Trim();
        }

        private static string? NormalizeNullableUserText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return NormalizeUserText(value);
        }

        private static string DecodeLegacyEntityText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value ?? string.Empty;
            }

            return WebUtility.HtmlDecode(value);
        }

        private static string ResolvePersonDisplayName(Personel? personel, string? snapshotName, string fallback)
        {
            if (personel != null)
            {
                return $"{personel.Ad} {personel.Soyad}";
            }

            if (!string.IsNullOrWhiteSpace(snapshotName))
            {
                return snapshotName;
            }

            return fallback;
        }

        private static bool ShouldRenderStrikethrough(Personel? personel, string? snapshotName)
        {
            if (personel != null)
            {
                return !personel.AktifMi;
            }

            return !string.IsNullOrWhiteSpace(snapshotName);
        }

        private async Task<string?> GetPersonelAdSoyadAsync(int? personelId)
        {
            if (!personelId.HasValue || personelId.Value <= 0)
            {
                return null;
            }

            return await _context.Personeller.AsNoTracking()
                .Where(x => x.PersonelId == personelId.Value)
                .Select(x => x.Ad + " " + x.Soyad)
                .FirstOrDefaultAsync();
        }
    }
}

