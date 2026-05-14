using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using System.Net;

namespace PersonelTakipSistemi.Services
{
    public class YazilimEnvanterService : IYazilimEnvanterService
    {
        private const string DigerSecenekAdi = "Diğer";
        private const int DigerYazilimId = 18;
        private readonly TegmPersonelTakipDbContext _context;

        public YazilimEnvanterService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public Task<List<LookupItemVm>> GetActiveSoftwareDefinitionsAsync()
        {
            return _context.YazilimTanimlari.AsNoTracking()
                .OrderBy(x => x.SistemSecenegiMi ? 1 : 0)
                .ThenBy(x => x.Ad)
                .Select(x => new LookupItemVm { Id = x.YazilimId, Ad = x.Ad })
                .ToListAsync();
        }

        public async Task<YazilimListePageViewModel> GetMySoftwareAsync(int currentPersonelId, YazilimListeFilterViewModel filter)
        {
            var query = BaseSoftwareQuery().Where(x => x.SahipPersonelId == currentPersonelId);
            query = ApplySoftwareFilters(query, filter);

            var model = await CreateListViewModelAsync(query, filter, currentPersonelId, false, false, "Yazılımlarım");
            model.PersonelGorunumuMu = true;
            model.YazilimEklemeYetkisiVarMi = true;
            return model;
        }

        public async Task<YazilimListePageViewModel> GetManagedSoftwareAsync(int currentPersonelId, bool isAdmin, YazilimListeFilterViewModel filter)
        {
            var query = BaseSoftwareQuery();
            if (!isAdmin)
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Any())
                {
                    throw new InvalidOperationException("Koordinatörlük yetkisi bulunamadı.");
                }

                query = query.Where(x => scopeIds.Contains(x.KoordinatorlukId));
            }

            query = ApplySoftwareFilters(query, filter);

            var model = await CreateListViewModelAsync(query, filter, currentPersonelId, true, isAdmin, isAdmin ? "Yazılımlar" : "Koordinatörlük Yazılımları");
            model.AdminPaneliMi = isAdmin;
            model.KoordinatorPaneliMi = !isAdmin;
            model.YazilimEklemeYetkisiVarMi = true;
            model.OnayYetkisiVarMi = true;
            model.EklemeIcinPersoneller = await GetAssignablePersonnelLookupAsync(currentPersonelId, isAdmin);
            return model;
        }

        public async Task<int> CreateSoftwareAsync(YazilimCreateViewModel model, int currentPersonelId, bool isCoordinator)
        {
            ValidateSoftwareModel(model);
            var isAdmin = await IsAdminAsync(currentPersonelId);
            var yazilim = await _context.YazilimTanimlari.FirstOrDefaultAsync(x => x.YazilimId == model.YazilimId);

            if (yazilim == null)
            {
                throw new InvalidOperationException("Seçilen yazılım geçersiz.");
            }

            if (yazilim.SistemSecenegiMi && string.IsNullOrWhiteSpace(model.DigerYazilimAd))
            {
                throw new InvalidOperationException("Diğer yazılım seçildiğinde yazılım adı girilmelidir.");
            }

            var ownerId = isCoordinator ? model.SahipPersonelId ?? 0 : currentPersonelId;
            if (ownerId <= 0)
            {
                throw new InvalidOperationException("Yazılım sahibi seçilmelidir.");
            }

            await EnsureActivePersonelAsync(ownerId, "Yazılım sahibi aktif personel olmalıdır.");
            var koordinatorlukId = await ResolveKoordinatorlukIdAsync(ownerId, currentPersonelId, isCoordinator, isAdmin);
            var now = DateTime.Now;

            var entity = new YazilimKaydi
            {
                YazilimId = yazilim.YazilimId,
                DigerYazilimAd = yazilim.SistemSecenegiMi ? NormalizeNullableUserText(model.DigerYazilimAd) : null,
                Surum = NormalizeUserText(model.Surum),
                Ozellikler = NormalizeUserText(model.Ozellikler),
                LisansAnahtari = NormalizeUserText(model.LisansAnahtari),
                LisansSuresiTuru = model.LisansSuresiTuru,
                BaslangicTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BaslangicTarihi?.Date,
                BitisTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BitisTarihi?.Date,
                KullaniciAdi = NormalizeNullableUserText(model.KullaniciAdi),
                Eposta = NormalizeNullableUserText(model.Eposta),
                SahipPersonelId = ownerId,
                KoordinatorlukId = koordinatorlukId,
                IlkKayitTarihi = now,
                AktifSahiplikBaslangicTarihi = now,
                OnayDurumu = isCoordinator ? YazilimOnayDurumu.Onaylandi : YazilimOnayDurumu.Beklemede,
                OnaylayanPersonelId = isCoordinator ? currentPersonelId : null,
                OnayTarihi = isCoordinator ? now : null,
                OlusturanPersonelId = currentPersonelId,
                KoordinatorTarafindanEklendi = isCoordinator,
                CreatedAt = now
            };

            _context.YazilimKayitlari.Add(entity);
            await _context.SaveChangesAsync();

            var islemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId);
            var sahipAdSoyad = await GetPersonelAdSoyadAsync(ownerId);
            _context.YazilimHareketleri.Add(new YazilimHareketi
            {
                YazilimKaydiId = entity.YazilimKaydiId,
                HareketTuru = YazilimHareketTuru.Kayit,
                YeniSahipPersonelId = ownerId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = islemYapanAdSoyad,
                YeniSahipAdSoyad = sahipAdSoyad,
                Aciklama = isCoordinator ? "Koordinatör tarafından yazılım kaydı oluşturuldu." : "Personel tarafından yazılım kaydı oluşturuldu ve onaya gönderildi.",
                Tarih = now
            });

            if (isCoordinator)
            {
                _context.YazilimHareketleri.Add(new YazilimHareketi
                {
                    YazilimKaydiId = entity.YazilimKaydiId,
                    HareketTuru = YazilimHareketTuru.Onay,
                    YeniSahipPersonelId = ownerId,
                    IslemYapanPersonelId = currentPersonelId,
                    IslemYapanAdSoyad = islemYapanAdSoyad,
                    YeniSahipAdSoyad = sahipAdSoyad,
                    Aciklama = "Koordinatör yazılımı doğrudan onaylı olarak ekledi.",
                    Tarih = now
                });
            }

            await _context.SaveChangesAsync();
            return entity.YazilimKaydiId;
        }

        public async Task<bool> UpdateSoftwareAsync(YazilimCreateViewModel model, int currentPersonelId, bool canManage)
        {
            if (!model.YazilimKaydiId.HasValue || model.YazilimKaydiId <= 0)
            {
                throw new InvalidOperationException("Güncellenecek yazılım bulunamadı.");
            }

            ValidateSoftwareModel(model);
            var entity = await _context.YazilimKayitlari.FirstOrDefaultAsync(x => x.YazilimKaydiId == model.YazilimKaydiId.Value);
            if (entity == null)
            {
                throw new InvalidOperationException("Yazılım kaydı bulunamadı.");
            }

            if (!canManage && entity.SahipPersonelId != currentPersonelId)
            {
                throw new InvalidOperationException("Bu yazılımı düzenleme yetkiniz bulunmuyor.");
            }

            var yazilim = await _context.YazilimTanimlari.FirstOrDefaultAsync(x => x.YazilimId == model.YazilimId);
            if (yazilim == null)
            {
                throw new InvalidOperationException("Seçilen yazılım geçersiz.");
            }

            if (yazilim.SistemSecenegiMi && string.IsNullOrWhiteSpace(model.DigerYazilimAd))
            {
                throw new InvalidOperationException("Diğer yazılım seçildiğinde yazılım adı girilmelidir.");
            }

            entity.YazilimId = yazilim.YazilimId;
            entity.DigerYazilimAd = yazilim.SistemSecenegiMi ? NormalizeNullableUserText(model.DigerYazilimAd) : null;
            entity.Surum = NormalizeUserText(model.Surum);
            entity.Ozellikler = NormalizeUserText(model.Ozellikler);
            entity.LisansAnahtari = NormalizeUserText(model.LisansAnahtari);
            entity.LisansSuresiTuru = model.LisansSuresiTuru;
            entity.BaslangicTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BaslangicTarihi?.Date;
            entity.BitisTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BitisTarihi?.Date;
            entity.KullaniciAdi = NormalizeNullableUserText(model.KullaniciAdi);
            entity.Eposta = NormalizeNullableUserText(model.Eposta);
            var now = DateTime.Now;
            entity.UpdatedAt = now;

            var approvalRequired = !canManage;
            if (approvalRequired)
            {
                entity.OnayDurumu = YazilimOnayDurumu.Beklemede;
                entity.OnaylayanPersonelId = null;
                entity.OnayTarihi = null;

                _context.YazilimHareketleri.Add(new YazilimHareketi
                {
                    YazilimKaydiId = entity.YazilimKaydiId,
                    HareketTuru = YazilimHareketTuru.Kayit,
                    YeniSahipPersonelId = entity.SahipPersonelId,
                    IslemYapanPersonelId = currentPersonelId,
                    IslemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId),
                    YeniSahipAdSoyad = await GetPersonelAdSoyadAsync(entity.SahipPersonelId),
                    Aciklama = "Personel yazılım bilgilerini güncelledi ve tekrar onaya gönderdi.",
                    Tarih = now
                });
            }

            await _context.SaveChangesAsync();
            return approvalRequired;
        }

        public async Task QuickUpdateSoftwareAsync(YazilimHizliDuzenleViewModel model, int currentPersonelId, bool isAdmin)
        {
            if (!isAdmin)
            {
                throw new InvalidOperationException("Bu işlem için admin yetkisi gereklidir.");
            }

            var entity = await _context.YazilimKayitlari.FirstOrDefaultAsync(x => x.YazilimKaydiId == model.YazilimKaydiId);
            if (entity == null)
            {
                throw new InvalidOperationException("Yazılım kaydı bulunamadı.");
            }

            var editModel = new YazilimCreateViewModel
            {
                YazilimKaydiId = model.YazilimKaydiId,
                YazilimId = model.YazilimId,
                DigerYazilimAd = model.DigerYazilimAd,
                Surum = model.Surum,
                Ozellikler = model.Ozellikler,
                LisansAnahtari = model.LisansAnahtari,
                LisansSuresiTuru = model.LisansSuresiTuru,
                BaslangicTarihi = model.BaslangicTarihi,
                BitisTarihi = model.BitisTarihi,
                KullaniciAdi = model.KullaniciAdi,
                Eposta = model.Eposta
            };
            ValidateSoftwareModel(editModel);

            var yazilim = await _context.YazilimTanimlari.FirstOrDefaultAsync(x => x.YazilimId == model.YazilimId);
            if (yazilim == null)
            {
                throw new InvalidOperationException("Seçilen yazılım geçersiz.");
            }

            if (yazilim.SistemSecenegiMi && string.IsNullOrWhiteSpace(model.DigerYazilimAd))
            {
                throw new InvalidOperationException("Diğer yazılım seçildiğinde yazılım adı girilmelidir.");
            }

            entity.YazilimId = yazilim.YazilimId;
            entity.DigerYazilimAd = yazilim.SistemSecenegiMi ? NormalizeNullableUserText(model.DigerYazilimAd) : null;
            entity.Surum = NormalizeUserText(model.Surum);
            entity.Ozellikler = NormalizeUserText(model.Ozellikler);
            entity.LisansAnahtari = NormalizeUserText(model.LisansAnahtari);
            entity.LisansSuresiTuru = model.LisansSuresiTuru;
            entity.BaslangicTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BaslangicTarihi?.Date;
            entity.BitisTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BitisTarihi?.Date;
            entity.KullaniciAdi = NormalizeNullableUserText(model.KullaniciAdi);
            entity.Eposta = NormalizeNullableUserText(model.Eposta);
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSoftwareAsync(int yazilimKaydiId, int currentPersonelId, bool isAdmin)
        {
            if (!isAdmin)
            {
                throw new InvalidOperationException("Bu işlem için admin yetkisi gereklidir.");
            }

            var entity = await _context.YazilimKayitlari.FirstOrDefaultAsync(x => x.YazilimKaydiId == yazilimKaydiId);
            if (entity == null)
            {
                throw new InvalidOperationException("Yazılım kaydı bulunamadı.");
            }

            _context.YazilimKayitlari.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveSoftwareAsync(int yazilimKaydiId, int currentPersonelId)
        {
            var isAdmin = await IsAdminAsync(currentPersonelId);
            var scopeIds = isAdmin ? new List<int>() : await GetCoordinatorScopeIdsAsync(currentPersonelId);
            var yazilim = await _context.YazilimKayitlari.FirstOrDefaultAsync(x => x.YazilimKaydiId == yazilimKaydiId);

            if (yazilim == null)
            {
                throw new InvalidOperationException("Yazılım kaydı bulunamadı.");
            }

            if (!isAdmin && !scopeIds.Contains(yazilim.KoordinatorlukId))
            {
                throw new InvalidOperationException("Bu yazılımı onaylama yetkiniz yok.");
            }

            if (yazilim.OnayDurumu == YazilimOnayDurumu.Onaylandi)
            {
                return;
            }

            var now = DateTime.Now;
            yazilim.OnayDurumu = YazilimOnayDurumu.Onaylandi;
            yazilim.OnaylayanPersonelId = currentPersonelId;
            yazilim.OnayTarihi = now;
            yazilim.UpdatedAt = now;

            _context.YazilimHareketleri.Add(new YazilimHareketi
            {
                YazilimKaydiId = yazilim.YazilimKaydiId,
                HareketTuru = YazilimHareketTuru.Onay,
                YeniSahipPersonelId = yazilim.SahipPersonelId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId),
                YeniSahipAdSoyad = await GetPersonelAdSoyadAsync(yazilim.SahipPersonelId),
                Aciklama = "Yazılım koordinatör tarafından onaylandı.",
                Tarih = now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<YazilimDetayViewModel?> GetDetailAsync(int yazilimKaydiId, int currentPersonelId, bool canManage)
        {
            var yazilim = await _context.YazilimKayitlari.AsNoTracking()
                .Include(x => x.Yazilim)
                .Include(x => x.SahipPersonel)
                .Include(x => x.Koordinatorluk)
                .Include(x => x.Hareketler).ThenInclude(x => x.IslemYapanPersonel)
                .Include(x => x.Hareketler).ThenInclude(x => x.OncekiSahipPersonel)
                .Include(x => x.Hareketler).ThenInclude(x => x.YeniSahipPersonel)
                .FirstOrDefaultAsync(x => x.YazilimKaydiId == yazilimKaydiId);

            if (yazilim == null)
            {
                return null;
            }

            if (!canManage && yazilim.SahipPersonelId != currentPersonelId)
            {
                return null;
            }

            if (canManage && !await IsAdminAsync(currentPersonelId))
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Contains(yazilim.KoordinatorlukId))
                {
                    return null;
                }
            }

            var devralinmisYazilimMi = yazilim.AktifSahiplikBaslangicTarihi > yazilim.IlkKayitTarihi;

            return new YazilimDetayViewModel
            {
                YazilimKaydiId = yazilim.YazilimKaydiId,
                YazilimAdi = ResolveSoftwareName(yazilim),
                Surum = DecodeLegacyEntityText(yazilim.Surum),
                Ozellikler = DecodeLegacyEntityText(yazilim.Ozellikler),
                LisansAnahtari = DecodeLegacyEntityText(yazilim.LisansAnahtari),
                LisansSuresiTuru = GetLisansSuresiTuruText(yazilim.LisansSuresiTuru),
                BaslangicTarihi = yazilim.BaslangicTarihi,
                BitisTarihi = yazilim.BitisTarihi,
                KullaniciAdi = DecodeLegacyEntityText(yazilim.KullaniciAdi),
                Eposta = DecodeLegacyEntityText(yazilim.Eposta),
                SahipAdSoyad = DecodeLegacyEntityText(yazilim.SahipPersonel != null ? $"{yazilim.SahipPersonel.Ad} {yazilim.SahipPersonel.Soyad}" : "Sahipsiz"),
                KoordinatorlukAd = DecodeLegacyEntityText(yazilim.Koordinatorluk.Ad),
                IlkKayitTarihi = yazilim.IlkKayitTarihi,
                GosterilecekKayitTarihi = canManage || !devralinmisYazilimMi ? yazilim.IlkKayitTarihi : yazilim.AktifSahiplikBaslangicTarihi,
                KayitTarihiBasligi = canManage || !devralinmisYazilimMi ? "İlk Kayıt Tarihi" : "Devir Tarihi",
                OnayDurumu = yazilim.OnayDurumu,
                OnayYetkisiVarMi = canManage && yazilim.OnayDurumu == YazilimOnayDurumu.Beklemede,
                HareketlerGorunsunMu = canManage,
                DevirYetkisiVarMi = canManage && yazilim.OnayDurumu == YazilimOnayDurumu.Onaylandi,
                DuzenlemeYetkisiVarMi = canManage || yazilim.SahipPersonelId == currentPersonelId,
                DuzenlemeOnayGerekliMi = !canManage && yazilim.SahipPersonelId == currentPersonelId,
                DigerYazilimId = await GetOtherSoftwareIdAsync() ?? DigerYazilimId,
                YazilimTanimlari = await GetActiveSoftwareDefinitionsAsync(),
                DuzenleFormu = new YazilimCreateViewModel
                {
                    YazilimKaydiId = yazilim.YazilimKaydiId,
                    YazilimId = yazilim.YazilimId,
                    DigerYazilimAd = DecodeLegacyEntityText(yazilim.DigerYazilimAd),
                    Surum = DecodeLegacyEntityText(yazilim.Surum),
                    Ozellikler = DecodeLegacyEntityText(yazilim.Ozellikler),
                    LisansAnahtari = DecodeLegacyEntityText(yazilim.LisansAnahtari),
                    LisansSuresiTuru = yazilim.LisansSuresiTuru,
                    BaslangicTarihi = yazilim.BaslangicTarihi,
                    BitisTarihi = yazilim.BitisTarihi,
                    KullaniciAdi = DecodeLegacyEntityText(yazilim.KullaniciAdi),
                    Eposta = DecodeLegacyEntityText(yazilim.Eposta)
                },
                DevredilebilirPersoneller = canManage ? await GetAssignablePersonnelByKoordinatorlukAsync(yazilim.KoordinatorlukId, yazilim.SahipPersonelId) : new(),
                DevirFormu = new YazilimTransferFormModel { YazilimKaydiId = yazilim.YazilimKaydiId },
                Hareketler = yazilim.Hareketler.OrderByDescending(x => x.Tarih).Select(x => new YazilimHareketItemViewModel
                {
                    Tarih = x.Tarih,
                    HareketTuru = x.HareketTuru switch
                    {
                        YazilimHareketTuru.Kayit => "Kayıt",
                        YazilimHareketTuru.Onay => "Onay",
                        YazilimHareketTuru.Devir => "Devir",
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

        public async Task TransferSoftwareAsync(YazilimTransferFormModel model, int currentPersonelId, bool canManage)
        {
            if (!canManage)
            {
                throw new InvalidOperationException("Bu işlem için yetkiniz bulunmuyor.");
            }

            if (model.YeniSahipPersonelId <= 0 || string.IsNullOrWhiteSpace(model.DevirNotu) || string.IsNullOrWhiteSpace(model.YazilimDurumNotu))
            {
                throw new InvalidOperationException("Devredilecek personel, devir notu ve yazılım notu zorunludur.");
            }

            var yazilim = await _context.YazilimKayitlari.FirstOrDefaultAsync(x => x.YazilimKaydiId == model.YazilimKaydiId);
            if (yazilim == null)
            {
                throw new InvalidOperationException("Yazılım kaydı bulunamadı.");
            }

            var isAdmin = await IsAdminAsync(currentPersonelId);
            if (!isAdmin)
            {
                var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
                if (!scopeIds.Contains(yazilim.KoordinatorlukId))
                {
                    throw new InvalidOperationException("Bu yazılımı devretme yetkiniz bulunmuyor.");
                }
            }

            if (yazilim.SahipPersonelId.HasValue && yazilim.SahipPersonelId.Value == model.YeniSahipPersonelId)
            {
                throw new InvalidOperationException("Yazılım zaten seçilen personele ait.");
            }

            await EnsureActivePersonelAsync(model.YeniSahipPersonelId, "Yazılım sadece aktif personele devredilebilir.");

            var hedefAyniKoordinatorlukteMi = await _context.PersonelKoordinatorlukler
                .AnyAsync(x => x.PersonelId == model.YeniSahipPersonelId && x.KoordinatorlukId == yazilim.KoordinatorlukId);

            if (!hedefAyniKoordinatorlukteMi)
            {
                throw new InvalidOperationException("Yazılım sadece aynı koordinatörlükteki personele devredilebilir.");
            }

            var kilitVarMi = await _context.YazilimHareketleri.AnyAsync(x =>
                x.YazilimKaydiId == yazilim.YazilimKaydiId &&
                x.HareketTuru == YazilimHareketTuru.Devir &&
                x.Tarih >= DateTime.Now.AddMinutes(-5));

            if (kilitVarMi)
            {
                throw new InvalidOperationException("Aynı yazılım 5 dakika içinde tekrar devredilemez.");
            }

            var now = DateTime.Now;
            var oncekiSahipId = yazilim.SahipPersonelId;
            var islemYapanAdSoyad = await GetPersonelAdSoyadAsync(currentPersonelId);
            var oncekiSahipAdSoyad = await GetPersonelAdSoyadAsync(oncekiSahipId);
            var yeniSahipAdSoyad = await GetPersonelAdSoyadAsync(model.YeniSahipPersonelId);
            yazilim.SahipPersonelId = model.YeniSahipPersonelId;
            yazilim.AktifSahiplikBaslangicTarihi = now;
            yazilim.SonDevirTarihi = now;
            yazilim.UpdatedAt = now;

            _context.YazilimHareketleri.Add(new YazilimHareketi
            {
                YazilimKaydiId = yazilim.YazilimKaydiId,
                HareketTuru = YazilimHareketTuru.Devir,
                OncekiSahipPersonelId = oncekiSahipId,
                YeniSahipPersonelId = model.YeniSahipPersonelId,
                IslemYapanPersonelId = currentPersonelId,
                IslemYapanAdSoyad = islemYapanAdSoyad,
                OncekiSahipAdSoyad = oncekiSahipAdSoyad,
                YeniSahipAdSoyad = yeniSahipAdSoyad,
                Aciklama = NormalizeUserText(model.DevirNotu),
                DurumNotu = NormalizeUserText(model.YazilimDurumNotu),
                Tarih = now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCoordinatorAsync(int personelId)
        {
            return (await GetCoordinatorScopeIdsAsync(personelId)).Any();
        }

        private IQueryable<YazilimKaydi> BaseSoftwareQuery()
        {
            return _context.YazilimKayitlari.AsNoTracking()
                .Include(x => x.Yazilim)
                .Include(x => x.SahipPersonel)
                .Include(x => x.Koordinatorluk)
                .Include(x => x.Hareketler);
        }

        private async Task<YazilimListePageViewModel> CreateListViewModelAsync(IQueryable<YazilimKaydi> query, YazilimListeFilterViewModel filter, int currentPersonelId, bool canManage, bool isAdmin, string title)
        {
            var items = await query.OrderByDescending(x => x.CreatedAt)
                .Select(x => new YazilimListeItemViewModel
                {
                    YazilimKaydiId = x.YazilimKaydiId,
                    YazilimId = x.YazilimId,
                    DigerYazilimAd = x.DigerYazilimAd,
                    YazilimAdi = x.DigerYazilimAd ?? x.Yazilim.Ad,
                    Surum = x.Surum,
                    Ozellikler = x.Ozellikler,
                    LisansAnahtari = x.LisansAnahtari,
                    LisansSuresiTuru = GetLisansSuresiTuruText(x.LisansSuresiTuru),
                    LisansSuresiTuruValue = x.LisansSuresiTuru,
                    BaslangicTarihi = x.BaslangicTarihi,
                    BitisTarihi = x.BitisTarihi,
                    KullaniciAdi = x.KullaniciAdi,
                    Eposta = x.Eposta,
                    SahipAdSoyad = x.SahipPersonel != null ? x.SahipPersonel.Ad + " " + x.SahipPersonel.Soyad : "Sahipsiz",
                    KoordinatorlukAd = x.Koordinatorluk.Ad,
                    IlkKayitTarihi = x.IlkKayitTarihi,
                    AktifSahiplikBaslangicTarihi = x.AktifSahiplikBaslangicTarihi,
                    KoordinatorTarafindanEklendi = x.KoordinatorTarafindanEklendi,
                    OnayDurumu = x.OnayDurumu
                }).ToListAsync();

            foreach (var item in items)
            {
                item.YazilimAdi = DecodeLegacyEntityText(item.YazilimAdi);
                item.DigerYazilimAd = DecodeLegacyEntityText(item.DigerYazilimAd);
                item.Surum = DecodeLegacyEntityText(item.Surum);
                item.Ozellikler = DecodeLegacyEntityText(item.Ozellikler);
                item.LisansAnahtari = DecodeLegacyEntityText(item.LisansAnahtari);
                item.KullaniciAdi = DecodeLegacyEntityText(item.KullaniciAdi);
                item.Eposta = DecodeLegacyEntityText(item.Eposta);
                item.SahipAdSoyad = DecodeLegacyEntityText(item.SahipAdSoyad);
                item.KoordinatorlukAd = DecodeLegacyEntityText(item.KoordinatorlukAd);
            }

            return new YazilimListePageViewModel
            {
                Baslik = title,
                Filter = filter,
                Yazilimlar = items,
                DigerYazilimId = await GetOtherSoftwareIdAsync() ?? DigerYazilimId,
                YazilimTanimlari = await GetActiveSoftwareDefinitionsAsync(),
                Koordinatorlukler = isAdmin ? await _context.Koordinatorlukler.AsNoTracking().OrderBy(x => x.Ad).Select(x => new LookupItemVm { Id = x.KoordinatorlukId, Ad = x.Ad }).ToListAsync() : await GetCoordinatorLookupAsync(currentPersonelId),
                Personeller = canManage ? await GetAssignablePersonnelLookupAsync(currentPersonelId, isAdmin) : new(),
                EklemeIcinYazilimlar = await GetActiveSoftwareDefinitionsAsync()
            };
        }

        private IQueryable<YazilimKaydi> ApplySoftwareFilters(IQueryable<YazilimKaydi> query, YazilimListeFilterViewModel filter)
        {
            if (filter.KoordinatorlukId.HasValue) query = query.Where(x => x.KoordinatorlukId == filter.KoordinatorlukId.Value);
            if (filter.PersonelId.HasValue)
            {
                query = filter.PersonelId.Value == -1
                    ? query.Where(x => x.SahipPersonelId == null)
                    : query.Where(x => x.SahipPersonelId == filter.PersonelId.Value);
            }
            if (filter.YazilimId.HasValue) query = query.Where(x => x.YazilimId == filter.YazilimId.Value);
            if (!string.IsNullOrWhiteSpace(filter.SurumAra)) query = query.Where(x => x.Surum.ToLower().Contains(filter.SurumAra.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.OzellikAra)) query = query.Where(x => x.Ozellikler.ToLower().Contains(filter.OzellikAra.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(filter.LisansAnahtariAra)) query = query.Where(x => x.LisansAnahtari.ToLower().Contains(filter.LisansAnahtariAra.Trim().ToLower()));
            if (filter.SadeceOnayBekleyenler) query = query.Where(x => x.OnayDurumu == YazilimOnayDurumu.Beklemede);

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
                throw new InvalidOperationException("Sadece kendi koordinatörlüğünüze bağlı personele yazılım ekleyebilirsiniz.");
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

        private Task<int?> GetOtherSoftwareIdAsync()
        {
            return _context.YazilimTanimlari.AsNoTracking()
                .Where(x => x.SistemSecenegiMi)
                .OrderBy(x => x.YazilimId)
                .Select(x => (int?)x.YazilimId)
                .FirstOrDefaultAsync();
        }

        private async Task<bool> IsAdminAsync(int personelId)
        {
            return await _context.Personeller.AsNoTracking()
                .AnyAsync(x => x.PersonelId == personelId && x.SistemRolId == 1);
        }

        private static void ValidateSoftwareModel(YazilimCreateViewModel model)
        {
            if (!model.YazilimId.HasValue || model.YazilimId <= 0)
            {
                throw new InvalidOperationException("Yazılım seçimi zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(model.Surum) || string.IsNullOrWhiteSpace(model.Ozellikler) || string.IsNullOrWhiteSpace(model.LisansAnahtari))
            {
                throw new InvalidOperationException("Sürüm, özellikler ve lisans anahtarı alanları zorunludur.");
            }

            if (model.LisansSuresiTuru != LisansSuresiTuru.Suresiz)
            {
                if (!model.BaslangicTarihi.HasValue || !model.BitisTarihi.HasValue)
                {
                    throw new InvalidOperationException("Süreli lisanslarda başlangıç ve bitiş tarihi zorunludur.");
                }

                if (model.BitisTarihi.Value.Date < model.BaslangicTarihi.Value.Date)
                {
                    throw new InvalidOperationException("Bitiş tarihi, başlangıç tarihinden önce olamaz.");
                }
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

        private static string ResolveSoftwareName(YazilimKaydi yazilim)
        {
            return DecodeLegacyEntityText(string.IsNullOrWhiteSpace(yazilim.DigerYazilimAd) ? yazilim.Yazilim.Ad : yazilim.DigerYazilimAd);
        }

        private static string GetLisansSuresiTuruText(LisansSuresiTuru tur)
        {
            return tur switch
            {
                LisansSuresiTuru.Aylik => "Aylık",
                LisansSuresiTuru.Yillik => "Yıllık",
                _ => "Süresiz"
            };
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
