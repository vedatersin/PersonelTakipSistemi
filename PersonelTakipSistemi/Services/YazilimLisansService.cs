using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.ViewModels;
using System.Net;

namespace PersonelTakipSistemi.Services
{
    public class YazilimLisansService : IYazilimLisansService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public YazilimLisansService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task<YazilimLisansYonetimPageViewModel> GetYonetimPageAsync(int currentPersonelId, bool lisansYonetebilirMi, bool kullaniciYonetebilirMi)
        {
            var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
            var baseList = await _context.YazilimLisanslar.AsNoTracking()
                .Include(x => x.Yazilim)
                .Include(x => x.Kullanicilar)
                    .ThenInclude(x => x.Personel)
                        .ThenInclude(x => x.PersonelKoordinatorlukler)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var lisanslar = baseList
                .Where(x => lisansYonetebilirMi || x.Kullanicilar.Any(k => IsInCoordinatorScope(k.Personel, scopeIds)))
                .Select(x => new YazilimLisansListeItemViewModel
                {
                    YazilimLisansId = x.YazilimLisansId,
                    YazilimId = x.YazilimId,
                    YazilimAdi = DecodeText(x.Yazilim.Ad) ?? string.Empty,
                    LisansSuresiTuru = GetLisansSuresiTuruText(x.LisansSuresiTuru),
                    BaslangicTarihi = x.BaslangicTarihi,
                    BitisTarihi = x.BitisTarihi,
                    HesapBasiOrtakKullanimBilgisi = DecodeText(x.HesapBasiOrtakKullanimBilgisi),
                    KullanimAmaci = DecodeText(x.KullanimAmaci),
                    KullanilanLisansAdedi = x.Kullanicilar.Count(),
                    ToplamLisansAdedi = x.MaksimumLisansHesapAdedi
                })
                .ToList();

            return new YazilimLisansYonetimPageViewModel
            {
                LisansYonetebilirMi = lisansYonetebilirMi,
                KullaniciYonetebilirMi = kullaniciYonetebilirMi,
                KoordinatorMu = !lisansYonetebilirMi && kullaniciYonetebilirMi,
                Yazilimlar = lisansYonetebilirMi
                    ? await _context.Yazilimlar.AsNoTracking()
                        .OrderBy(x => x.Ad)
                        .Select(x => new LookupItemVm { Id = x.YazilimId, Ad = DecodeText(x.Ad) ?? string.Empty })
                        .ToListAsync()
                    : new(),
                Lisanslar = lisanslar
            };
        }

        public async Task<YazilimLisansDetayPageViewModel?> GetDetayPageAsync(int lisansId, int currentPersonelId, bool lisansYonetebilirMi, bool kullaniciYonetebilirMi)
        {
            var lisans = await _context.YazilimLisanslar.AsNoTracking()
                .Include(x => x.Yazilim)
                .Include(x => x.Kullanicilar)
                    .ThenInclude(x => x.Personel)
                        .ThenInclude(p => p.PersonelKoordinatorlukler)
                .ThenInclude(pk => pk.Koordinatorluk)
                .FirstOrDefaultAsync(x => x.YazilimLisansId == lisansId);

            if (lisans == null)
            {
                return null;
            }

            var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
            var kullanicilar = lisans.Kullanicilar
                .Where(x => lisansYonetebilirMi || IsInCoordinatorScope(x.Personel, scopeIds))
                .Select(x => new YazilimLisansKullaniciItemViewModel
                {
                    YazilimLisansKullaniciId = x.YazilimLisansKullaniciId,
                    PersonelId = x.PersonelId,
                    LisansSahibiAdSoyad = DecodeText($"{x.Personel.Ad} {x.Personel.Soyad}") ?? "-",
                    Koordinatorluk = DecodeText(x.Personel.PersonelKoordinatorlukler.Select(pk => pk.Koordinatorluk.Ad).FirstOrDefault() ?? "-") ?? "-",
                    KullaniciAdi = DecodeText(x.KullaniciAdi),
                    Eposta = DecodeText(x.Eposta),
                    OnayliMi = x.OnayDurumu == LisansKullaniciOnayDurumu.Onaylandi,
                    OnayDurumu = x.OnayDurumu,
                    OnayDurumuText = GetOnayDurumuTextForManager(x.OnayDurumu),
                    AdminOnayIslemleriGoster = x.OnayDurumu == LisansKullaniciOnayDurumu.OnayBekleniyor
                })
                .OrderBy(x => x.LisansSahibiAdSoyad)
                .ToList();

            if (!lisansYonetebilirMi && !kullanicilar.Any())
            {
                return null;
            }

            return new YazilimLisansDetayPageViewModel
            {
                YazilimLisansId = lisans.YazilimLisansId,
                YazilimAdi = DecodeText(lisans.Yazilim.Ad) ?? string.Empty,
                LisansSuresiTuru = GetLisansSuresiTuruText(lisans.LisansSuresiTuru),
                BaslangicTarihi = lisans.BaslangicTarihi,
                BitisTarihi = lisans.BitisTarihi,
                ToplamLisansAdedi = lisans.MaksimumLisansHesapAdedi,
                KullanilanLisansAdedi = lisans.Kullanicilar.Count,
                HesapBasiOrtakKullanimBilgisi = DecodeText(lisans.HesapBasiOrtakKullanimBilgisi),
                KullanimAmaci = DecodeText(lisans.KullanimAmaci),
                LisansYonetebilirMi = lisansYonetebilirMi,
                KullaniciYonetebilirMi = kullaniciYonetebilirMi,
                Yazilimlar = lisansYonetebilirMi
                    ? await _context.Yazilimlar.AsNoTracking()
                        .OrderBy(x => x.Ad)
                        .Select(x => new LookupItemVm { Id = x.YazilimId, Ad = DecodeText(x.Ad) ?? string.Empty })
                        .ToListAsync()
                    : new(),
                DuzenleFormu = new YazilimLisansFormViewModel
                {
                    YazilimLisansId = lisans.YazilimLisansId,
                    YazilimId = lisans.YazilimId,
                    MaksimumLisansHesapAdedi = lisans.MaksimumLisansHesapAdedi,
                    LisansSuresiTuru = lisans.LisansSuresiTuru,
                    BaslangicTarihi = lisans.BaslangicTarihi,
                    BitisTarihi = lisans.BitisTarihi,
                    HesapBasiOrtakKullanimBilgisi = DecodeText(lisans.HesapBasiOrtakKullanimBilgisi),
                    KullanimAmaci = DecodeText(lisans.KullanimAmaci)
                },
                YeniKullaniciFormu = new YazilimLisansKullaniciFormViewModel { YazilimLisansId = lisans.YazilimLisansId },
                SecilebilirPersoneller = await GetAssignablePersonnelAsync(currentPersonelId, lisansYonetebilirMi),
                Kullanicilar = kullanicilar
            };
        }

        public async Task CreateLisansAsync(YazilimLisansFormViewModel model, int currentPersonelId, bool lisansYonetebilirMi)
        {
            EnsureMasterManagePermission(lisansYonetebilirMi);
            ValidateLicenseForm(model);

            var yazilimVarMi = await _context.Yazilimlar.AsNoTracking().AnyAsync(x => x.YazilimId == model.YazilimId);
            if (!yazilimVarMi)
            {
                throw new InvalidOperationException("Secilen yazilim tanimi bulunamadi.");
            }

            var entity = new YazilimLisans
            {
                YazilimId = model.YazilimId,
                MaksimumLisansHesapAdedi = model.MaksimumLisansHesapAdedi,
                LisansSuresiTuru = model.LisansSuresiTuru,
                BaslangicTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BaslangicTarihi?.Date,
                BitisTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BitisTarihi?.Date,
                HesapBasiOrtakKullanimBilgisi = NormalizeNullableText(model.HesapBasiOrtakKullanimBilgisi),
                KullanimAmaci = NormalizeNullableText(model.KullanimAmaci),
                OlusturanPersonelId = currentPersonelId,
                CreatedAt = DateTime.Now
            };

            _context.YazilimLisanslar.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLisansAsync(YazilimLisansFormViewModel model, int currentPersonelId, bool lisansYonetebilirMi)
        {
            EnsureMasterManagePermission(lisansYonetebilirMi);
            if (!model.YazilimLisansId.HasValue || model.YazilimLisansId.Value <= 0)
            {
                throw new InvalidOperationException("Guncellenecek lisans kaydi bulunamadi.");
            }

            ValidateLicenseForm(model);
            var entity = await _context.YazilimLisanslar.FirstOrDefaultAsync(x => x.YazilimLisansId == model.YazilimLisansId.Value);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kaydi bulunamadi.");
            }

            entity.YazilimId = model.YazilimId;
            entity.MaksimumLisansHesapAdedi = model.MaksimumLisansHesapAdedi;
            entity.LisansSuresiTuru = model.LisansSuresiTuru;
            entity.BaslangicTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BaslangicTarihi?.Date;
            entity.BitisTarihi = model.LisansSuresiTuru == LisansSuresiTuru.Suresiz ? null : model.BitisTarihi?.Date;
            entity.HesapBasiOrtakKullanimBilgisi = NormalizeNullableText(model.HesapBasiOrtakKullanimBilgisi);
            entity.KullanimAmaci = NormalizeNullableText(model.KullanimAmaci);
            entity.UpdatedAt = DateTime.Now;

            var mevcutKullaniciSayisi = await _context.YazilimLisansKullanicilar.CountAsync(x => x.YazilimLisansId == entity.YazilimLisansId);
            if (entity.MaksimumLisansHesapAdedi < mevcutKullaniciSayisi)
            {
                throw new InvalidOperationException("Toplam lisans adedi, mevcut kullanici adedinden kucuk olamaz.");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteLisansAsync(int lisansId, int currentPersonelId, bool lisansYonetebilirMi)
        {
            EnsureMasterManagePermission(lisansYonetebilirMi);
            var entity = await _context.YazilimLisanslar.FirstOrDefaultAsync(x => x.YazilimLisansId == lisansId);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kaydi bulunamadi.");
            }

            _context.YazilimLisanslar.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddKullaniciAsync(YazilimLisansKullaniciFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi)
        {
            EnsureUserManagePermission(kullaniciYonetebilirMi);
            if (model.YazilimLisansId <= 0 || model.PersonelId <= 0)
            {
                throw new InvalidOperationException("Lisans ve personel secimi zorunludur.");
            }

            var lisans = await _context.YazilimLisanslar
                .Include(x => x.Kullanicilar)
                .FirstOrDefaultAsync(x => x.YazilimLisansId == model.YazilimLisansId);
            if (lisans == null)
            {
                throw new InvalidOperationException("Lisans kaydi bulunamadi.");
            }

            if (lisans.Kullanicilar.Count >= lisans.MaksimumLisansHesapAdedi)
            {
                throw new InvalidOperationException("Maksimum kullanıcı sayısına ulaşmış durumda, lütfen lisans kapasitenizi arttırın.");
            }

            var personel = await _context.Personeller
                .Include(x => x.PersonelKoordinatorlukler)
                .FirstOrDefaultAsync(x => x.PersonelId == model.PersonelId && x.AktifMi);
            if (personel == null)
            {
                throw new InvalidOperationException("Secilen personel bulunamadi veya aktif degil.");
            }

            await EnsureCoordinatorCanManagePersonelAsync(currentPersonelId, personel);

            var mevcut = await _context.YazilimLisansKullanicilar.AnyAsync(x => x.YazilimLisansId == model.YazilimLisansId && x.PersonelId == model.PersonelId);
            if (mevcut)
            {
                throw new InvalidOperationException("Bu personel bu lisansa zaten ekli.");
            }

            _context.YazilimLisansKullanicilar.Add(new YazilimLisansKullanici
            {
                YazilimLisansId = model.YazilimLisansId,
                PersonelId = model.PersonelId,
                KullaniciAdi = NormalizeNullableText(model.KullaniciAdi),
                Eposta = NormalizeNullableText(model.Eposta),
                OnayDurumu = LisansKullaniciOnayDurumu.OnayaGonderildi,
                OnaylayanPersonelId = null,
                OnayTarihi = null,
                KaydiOlusturanPersonelId = currentPersonelId,
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateKullaniciAsync(YazilimLisansKullaniciFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi)
        {
            EnsureUserManagePermission(kullaniciYonetebilirMi);
            if (!model.YazilimLisansKullaniciId.HasValue || model.YazilimLisansKullaniciId.Value <= 0)
            {
                throw new InvalidOperationException("Guncellenecek kullanici kaydi bulunamadi.");
            }

            var entity = await _context.YazilimLisansKullanicilar
                .Include(x => x.Personel)
                .ThenInclude(p => p.PersonelKoordinatorlukler)
                .FirstOrDefaultAsync(x => x.YazilimLisansKullaniciId == model.YazilimLisansKullaniciId.Value);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kullanici kaydi bulunamadi.");
            }

            await EnsureCoordinatorCanManagePersonelAsync(currentPersonelId, entity.Personel);

            entity.KullaniciAdi = NormalizeNullableText(model.KullaniciAdi);
            entity.Eposta = NormalizeNullableText(model.Eposta);
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteKullaniciAsync(int lisansKullaniciId, int lisansId, int currentPersonelId, bool kullaniciYonetebilirMi)
        {
            EnsureUserManagePermission(kullaniciYonetebilirMi);
            var entity = await _context.YazilimLisansKullanicilar
                .Include(x => x.Personel)
                .ThenInclude(p => p.PersonelKoordinatorlukler)
                .FirstOrDefaultAsync(x => x.YazilimLisansKullaniciId == lisansKullaniciId && x.YazilimLisansId == lisansId);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kullanici kaydi bulunamadi.");
            }

            await EnsureCoordinatorCanManagePersonelAsync(currentPersonelId, entity.Personel);

            _context.YazilimLisansKullanicilar.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task PersonelOnayaGonderAsync(YazilimLisansKullaniciOnayFormViewModel model, int currentPersonelId)
        {
            if (model.YazilimLisansKullaniciId <= 0 || model.YazilimLisansId <= 0)
            {
                throw new InvalidOperationException("Onaylanacak kayit bulunamadi.");
            }

            if (string.IsNullOrWhiteSpace(model.KullaniciAdi) || string.IsNullOrWhiteSpace(model.Eposta))
            {
                throw new InvalidOperationException("Kullanici adi ve e-posta zorunludur.");
            }

            var entity = await _context.YazilimLisansKullanicilar
                .FirstOrDefaultAsync(x => x.YazilimLisansKullaniciId == model.YazilimLisansKullaniciId && x.YazilimLisansId == model.YazilimLisansId);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kullanici kaydi bulunamadi.");
            }

            if (entity.PersonelId != currentPersonelId)
            {
                throw new InvalidOperationException("Bu lisans kaydi icin onay islemi yapamazsiniz.");
            }

            if (entity.OnayDurumu != LisansKullaniciOnayDurumu.OnayaGonderildi)
            {
                throw new InvalidOperationException("Bu kayit su anda onaya gonderilemez.");
            }

            entity.KullaniciAdi = NormalizeText(model.KullaniciAdi);
            entity.Eposta = NormalizeText(model.Eposta);
            entity.OnayDurumu = LisansKullaniciOnayDurumu.OnayBekleniyor;
            entity.OnaylayanPersonelId = null;
            entity.OnayTarihi = null;
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task YoneticiOnayKarariVerAsync(YazilimLisansKullaniciKararFormViewModel model, int currentPersonelId, bool kullaniciYonetebilirMi)
        {
            EnsureUserManagePermission(kullaniciYonetebilirMi);
            if (model.YazilimLisansKullaniciId <= 0 || model.YazilimLisansId <= 0)
            {
                throw new InvalidOperationException("Onay islemi icin kayit bulunamadi.");
            }

            var entity = await _context.YazilimLisansKullanicilar
                .Include(x => x.Personel)
                .ThenInclude(p => p.PersonelKoordinatorlukler)
                .FirstOrDefaultAsync(x => x.YazilimLisansKullaniciId == model.YazilimLisansKullaniciId && x.YazilimLisansId == model.YazilimLisansId);
            if (entity == null)
            {
                throw new InvalidOperationException("Lisans kullanici kaydi bulunamadi.");
            }

            await EnsureCoordinatorCanManagePersonelAsync(currentPersonelId, entity.Personel);

            if (entity.OnayDurumu != LisansKullaniciOnayDurumu.OnayBekleniyor)
            {
                throw new InvalidOperationException("Bu kayit icin yonetici onay islemi su anda yapilamaz.");
            }

            if (model.Onayla)
            {
                entity.OnayDurumu = LisansKullaniciOnayDurumu.Onaylandi;
                entity.OnaylayanPersonelId = currentPersonelId;
                entity.OnayTarihi = DateTime.Now;
            }
            else
            {
                entity.OnayDurumu = LisansKullaniciOnayDurumu.OnayaGonderildi;
                entity.OnaylayanPersonelId = null;
                entity.OnayTarihi = null;
            }

            entity.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<YazilimLisanslarimViewModel> GetPersonelLisanslarimAsync(int personelId)
        {
            var lisanslar = await _context.YazilimLisansKullanicilar.AsNoTracking()
                .Include(x => x.YazilimLisans)
                    .ThenInclude(x => x.Yazilim)
                .Where(x => x.PersonelId == personelId)
                .OrderBy(x => x.YazilimLisans.Yazilim.Ad)
                .Select(x => new YazilimLisanslarimItemViewModel
                {
                    YazilimLisansKullaniciId = x.YazilimLisansKullaniciId,
                    YazilimLisansId = x.YazilimLisansId,
                    YazilimAdi = DecodeText(x.YazilimLisans.Yazilim.Ad) ?? string.Empty,
                    LisansSuresiTuru = GetLisansSuresiTuruText(x.YazilimLisans.LisansSuresiTuru),
                    BaslangicTarihi = x.YazilimLisans.BaslangicTarihi,
                    BitisTarihi = x.YazilimLisans.BitisTarihi,
                    KullaniciAdi = DecodeText(x.KullaniciAdi),
                    Eposta = DecodeText(x.Eposta),
                    OnayDurumu = GetOnayDurumuTextForPersonel(x.OnayDurumu),
                    OnayIslemleriGoster = x.OnayDurumu == LisansKullaniciOnayDurumu.OnayaGonderildi
                })
                .ToListAsync();

            return new YazilimLisanslarimViewModel { Lisanslar = lisanslar };
        }

        private async Task<List<LookupItemVm>> GetAssignablePersonnelAsync(int currentPersonelId, bool lisansYonetebilirMi)
        {
            if (lisansYonetebilirMi)
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

        private async Task EnsureCoordinatorCanManagePersonelAsync(int currentPersonelId, Personel personel)
        {
            var isMaster = await IsMasterManagerAsync(currentPersonelId);
            if (isMaster)
            {
                return;
            }

            var scopeIds = await GetCoordinatorScopeIdsAsync(currentPersonelId);
            if (!scopeIds.Any() || !IsInCoordinatorScope(personel, scopeIds))
            {
                throw new InvalidOperationException("Bu personel icin islem yapma yetkiniz yok.");
            }
        }

        private static bool IsInCoordinatorScope(Personel personel, IReadOnlyCollection<int> scopeIds)
        {
            return personel.PersonelKoordinatorlukler.Any(pk => scopeIds.Contains(pk.KoordinatorlukId));
        }

        private async Task<List<int>> GetCoordinatorScopeIdsAsync(int personelId)
        {
            return await _context.PersonelKurumsalRolAtamalari.AsNoTracking()
                .Where(x => x.PersonelId == personelId && x.KoordinatorlukId.HasValue && (x.KurumsalRolId == 3 || x.KurumsalRolId == 5))
                .Select(x => x.KoordinatorlukId!.Value)
                .Distinct()
                .ToListAsync();
        }

        private async Task<bool> IsMasterManagerAsync(int personelId)
        {
            return await _context.Personeller.AsNoTracking()
                .AnyAsync(x => x.PersonelId == personelId && (x.SistemRolId == 1 || x.SistemRolId == 2));
        }

        private static void EnsureMasterManagePermission(bool lisansYonetebilirMi)
        {
            if (!lisansYonetebilirMi)
            {
                throw new InvalidOperationException("Yazilim lisans kaydi yonetimi icin yetkiniz bulunmuyor.");
            }
        }

        private static void EnsureUserManagePermission(bool kullaniciYonetebilirMi)
        {
            if (!kullaniciYonetebilirMi)
            {
                throw new InvalidOperationException("Lisans kullanici kaydi yonetimi icin yetkiniz bulunmuyor.");
            }
        }

        private static void ValidateLicenseForm(YazilimLisansFormViewModel model)
        {
            if (model.YazilimId <= 0)
            {
                throw new InvalidOperationException("Yazilim secimi zorunludur.");
            }

            if (model.MaksimumLisansHesapAdedi <= 0)
            {
                throw new InvalidOperationException("Maksimum lisans hesap adedi 0'dan buyuk olmalidir.");
            }

            if (model.LisansSuresiTuru != LisansSuresiTuru.Suresiz)
            {
                if (!model.BaslangicTarihi.HasValue || !model.BitisTarihi.HasValue)
                {
                    throw new InvalidOperationException("Sureli lisanslarda baslangic ve bitis tarihi zorunludur.");
                }

                if (model.BitisTarihi.Value.Date < model.BaslangicTarihi.Value.Date)
                {
                    throw new InvalidOperationException("Bitis tarihi, baslangic tarihinden once olamaz.");
                }
            }
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

        private static string GetOnayDurumuTextForManager(LisansKullaniciOnayDurumu durum)
        {
            return durum switch
            {
                LisansKullaniciOnayDurumu.OnayaGonderildi => "Onaya Gönderildi",
                LisansKullaniciOnayDurumu.OnayBekleniyor => "Onay Bekleniyor",
                _ => "Onaylandı"
            };
        }

        private static string GetOnayDurumuTextForPersonel(LisansKullaniciOnayDurumu durum)
        {
            return durum switch
            {
                LisansKullaniciOnayDurumu.OnayaGonderildi => "Onay Bekleniyor",
                LisansKullaniciOnayDurumu.OnayBekleniyor => "Onaya Gönderildi",
                _ => "Onaylandı"
            };
        }

        private static string NormalizeText(string value)
        {
            return WebUtility.HtmlDecode(value ?? string.Empty).Trim();
        }

        private static string? NormalizeNullableText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return NormalizeText(value);
        }

        private static string? DecodeText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return WebUtility.HtmlDecode(value);
        }
    }
}
