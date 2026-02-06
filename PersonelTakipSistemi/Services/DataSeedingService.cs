using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PersonelTakipSistemi.Services
{
    public class DataSeedingService
    {
        private readonly TegmPersonelTakipDbContext _context;

        public DataSeedingService(TegmPersonelTakipDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            var random = new Random();

            // 1. Identify Protected Users
            var protectedIds = await _context.Personeller
                .Where(p => p.Ad.ToLower().Contains("vedat") || p.Ad.ToLower().Contains("sevilay") ||
                            p.Soyad.ToLower().Contains("ceviz"))
                .Select(p => p.PersonelId)
                .ToListAsync();

            // 2. Clear Data (Preserving Protected Users & Lookups)
            await _context.GorevAtamaPersoneller.ExecuteDeleteAsync();
            await _context.GorevAtamaKomisyonlar.ExecuteDeleteAsync();
            await _context.GorevAtamaKoordinatorlukler.ExecuteDeleteAsync();
            await _context.GorevAtamaTeskilatlar.ExecuteDeleteAsync();
            await _context.Gorevler.ExecuteDeleteAsync();
            
            await _context.Bildirimler.ExecuteDeleteAsync();
            await _context.SistemLoglar.ExecuteDeleteAsync();

            await _context.PersonelKurumsalRolAtamalari.ExecuteDeleteAsync();
            await _context.PersonelGorevTurleri.ExecuteDeleteAsync();
            await _context.PersonelIsNitelikleri.ExecuteDeleteAsync();
            await _context.PersonelKomisyonlar.ExecuteDeleteAsync();
            await _context.PersonelKoordinatorlukler.ExecuteDeleteAsync();
            await _context.PersonelTeskilatlar.ExecuteDeleteAsync();
            await _context.PersonelUzmanliklar.ExecuteDeleteAsync();
            await _context.PersonelYazilimlar.ExecuteDeleteAsync();

            if (protectedIds.Any())
            {
                await _context.Personeller
                    .Where(p => !protectedIds.Contains(p.PersonelId))
                    .ExecuteDeleteAsync();
            }
            else
            {
                await _context.Personeller.ExecuteDeleteAsync();
            }

            await _context.Komisyonlar.ExecuteDeleteAsync();
            await _context.Koordinatorlukler.ExecuteDeleteAsync();
            await _context.Teskilatlar.ExecuteDeleteAsync(); 

            // 3. Create Structure
            // A. Teşkilatlar
            var merkez = new Teskilat { Ad = "Temel Eğitim Genel Müdürlüğü (Merkez)" };
            var tasra = new Teskilat { Ad = "Taşra Teşkilatı" };
            _context.Teskilatlar.AddRange(merkez, tasra);
            await _context.SaveChangesAsync();

            // B. Koordinatörlükler
            var koordinatorlukler = new List<Koordinatorluk>();

            // Merkez
            var ankaraTegm = new Koordinatorluk { Ad = "Ankara TEGM Koordinatörlüğü", TeskilatId = merkez.TeskilatId };
            koordinatorlukler.Add(ankaraTegm);

            // Taşra (20 Cities)
            // Priority Cities
            var priorityCities = new[] { "İzmir", "Konya", "Antalya", "Sivas" };
            // Other 16 Cities
            var otherCities = new[] { "Adana", "Bursa", "Gaziantep", "Mersin", "Diyarbakır", "Denizli", "Samsun", "Eskişehir", "Trabzon", "Şanlıurfa", "Van", "Kayseri", "Sakarya", "Manisa", "Hatay", "Balıkesir" };

            foreach (var c in priorityCities)
                koordinatorlukler.Add(new Koordinatorluk { Ad = $"{c} İl Koordinatörlüğü", TeskilatId = tasra.TeskilatId });
            
            foreach (var c in otherCities)
                koordinatorlukler.Add(new Koordinatorluk { Ad = $"{c} İl Koordinatörlüğü", TeskilatId = tasra.TeskilatId });

            _context.Koordinatorlukler.AddRange(koordinatorlukler);
            await _context.SaveChangesAsync();

            // C. Komisyonlar (Her Koordinatörlük için 6 tane)
            var komisyonNames = new[] { "Matematik", "Fen Bilimleri", "Türkçe", "İngilizce", "Müzik", "Sosyal Bilimler" };
            var komisyonlar = new List<Komisyon>();

            foreach (var koord in koordinatorlukler)
            {
                foreach (var kName in komisyonNames)
                {
                    komisyonlar.Add(new Komisyon { Ad = $"{kName} Komisyonu", KoordinatorlukId = koord.KoordinatorlukId });
                }
            }
            _context.Komisyonlar.AddRange(komisyonlar);
            await _context.SaveChangesAsync();

            // 4. Personnel Generation (Target 450)
            // Existing Protected Users
            var protectedUsers = await _context.Personeller.Where(p => protectedIds.Contains(p.PersonelId)).ToListAsync();
            
            // Assign Protected to İzmir
            var izmirKoord = koordinatorlukler.FirstOrDefault(k => k.Ad.Contains("İzmir"));
            if (izmirKoord != null)
            {
                foreach (var p in protectedUsers)
                {
                    p.AktifMi = true; // Ensure active
                }
            }

            int currentCount = protectedUsers.Count;
            int targetCount = 450;
            var newPersonelList = new List<Personel>();
            
            CreatePasswordHash("123456", out byte[] hash, out byte[] salt);
            var ilIds = await _context.Iller.Select(x => x.IlId).ToListAsync();
            var bransIds = await _context.Branslar.Select(x => x.BransId).ToListAsync();

            var names = new[] { "Elif", "Can", "Murat", "Seda", "Burak", "Ayşe", "Mehmet", "Fatma", "Ahmet", "Zeynep", "Mustafa", "Gamze", "Esra", "Ömer", "Derya", "Kemal", "Leyla", "Hakan", "Berna", "Cem" };
            var surnames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Öztürk", "Aydın", "Özdemir", "Arslan", "Doğan", "Kılıç", "etin", "Kara", "Koç", "Kurt", "Özkan", "Şimşek", "Polat", "Erdoğan" };

            // TCs of protected users to avoid collision
            var existingTcs = new HashSet<string>(protectedUsers.Select(p => p.TcKimlikNo));
            
            // Start generating from a base
            long tcCounter = 10000000000;

            for (int i = 0; i < (targetCount - currentCount); i++)
            {
                 string name = names[random.Next(names.Length)];
                 string surname = surnames[random.Next(surnames.Length)];
                 
                 // Find a unique TC
                 string tc;
                 do 
                 {
                     tcCounter += random.Next(3, 15); // Increment by random 3-15 to spread them out slightly but keep strictly increasing
                     tc = tcCounter.ToString();
                 } while (existingTcs.Contains(tc));
                 
                 existingTcs.Add(tc); // Add to set to track

                 newPersonelList.Add(new Personel
                 {
                     Ad = name,
                     Soyad = surname,
                     TcKimlikNo = tc,
                     Telefon = "555" + random.Next(1000000, 9999999),
                     Eposta = $"user{tc.Substring(tc.Length-5)}_{random.Next(9999)}@meb.gov.tr",
                     SifreHash = hash,
                     SifreSalt = salt,
                     SistemRolId = 4, // Kullanıcı
                     AktifMi = true,
                     KadroKurum = "MEB",
                     DogumTarihi = DateTime.Now.AddYears(-25 - random.Next(20)),
                     GorevliIlId = ilIds.Any() ? ilIds[random.Next(ilIds.Count)] : 1,
                     BransId = bransIds.Any() ? bransIds[random.Next(bransIds.Count)] : 1
                 });
            }
            _context.Personeller.AddRange(newPersonelList);
            await _context.SaveChangesAsync();

            // Reload All Personnel
            var allPersonel = await _context.Personeller.ToListAsync(); // ~450
            
            // 5. Assign Roles & Structure
            // Lookups
            var rPersonel = (await _context.KurumsalRoller.FirstOrDefaultAsync(r => r.Ad == "Personel"))?.KurumsalRolId ?? 1;
            var rKomBaskan = (await _context.KurumsalRoller.FirstOrDefaultAsync(r => r.Ad == "Komisyon Başkanı"))?.KurumsalRolId ?? 2;
            var rIlKoord = (await _context.KurumsalRoller.FirstOrDefaultAsync(r => r.Ad == "İl Koordinatörü"))?.KurumsalRolId ?? 3;
            var rGenelKoord = (await _context.KurumsalRoller.FirstOrDefaultAsync(r => r.Ad == "Genel Koordinatör" || r.Ad == "Merkez Koordinatörü"))?.KurumsalRolId ?? 4;

            var assignedPersonelIds = new HashSet<int>();
            var roleAssignments = new List<PersonelKurumsalRolAtama>();
            // var pKomisyon = new List<PersonelKomisyon>(); // EF Core tracks related entities via nav props if we add them to context. 
            // Better to add explicit entities for clear association.
            var pKomisyon = new List<PersonelKomisyon>();

            // A. Coordinators
            foreach(var k in koordinatorlukler)
            {
                // Prefer Sevilay/Vedat for İzmir
                Personel? p = null;
                if (k.Ad.Contains("İzmir"))
                {
                    p = allPersonel.FirstOrDefault(x => (x.Ad.Contains("Sevilay") || x.Ad.Contains("Vedat")) && !assignedPersonelIds.Contains(x.PersonelId));
                }
                
                if (p == null)
                     p = allPersonel.FirstOrDefault(x => !assignedPersonelIds.Contains(x.PersonelId));

                if (p != null)
                {
                    assignedPersonelIds.Add(p.PersonelId);
                    roleAssignments.Add(new PersonelKurumsalRolAtama 
                    { 
                        PersonelId = p.PersonelId, 
                        KoordinatorlukId = k.KoordinatorlukId, 
                        KurumsalRolId = (k.TeskilatId == merkez.TeskilatId) ? rGenelKoord : rIlKoord 
                    });
                    
                    _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = p.PersonelId, KoordinatorlukId = k.KoordinatorlukId });
                }
            }

            // B. Commission Presidents
            foreach(var kom in komisyonlar)
            {
                var p = allPersonel.FirstOrDefault(x => !assignedPersonelIds.Contains(x.PersonelId));
                if (p != null)
                {
                    assignedPersonelIds.Add(p.PersonelId);
                    roleAssignments.Add(new PersonelKurumsalRolAtama 
                    { 
                        PersonelId = p.PersonelId, 
                        KoordinatorlukId = kom.KoordinatorlukId,
                        KomisyonId = kom.KomisyonId,
                        KurumsalRolId = rKomBaskan
                    });
                    
                    _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = p.PersonelId, KoordinatorlukId = kom.KoordinatorlukId });
                    pKomisyon.Add(new PersonelKomisyon { PersonelId = p.PersonelId, KomisyonId = kom.KomisyonId });
                }
            }

            // C. Members (Remainder)
            var unassigned = allPersonel.Where(p => !assignedPersonelIds.Contains(p.PersonelId)).ToList();
            foreach(var p in unassigned)
            {
                var targetKomisyon = komisyonlar.Any() ? komisyonlar[random.Next(komisyonlar.Count)] : null;
                if (targetKomisyon != null)
                {
                    roleAssignments.Add(new PersonelKurumsalRolAtama 
                    { 
                        PersonelId = p.PersonelId, 
                        KoordinatorlukId = targetKomisyon.KoordinatorlukId,
                        KomisyonId = targetKomisyon.KomisyonId,
                        KurumsalRolId = rPersonel
                    });

                    _context.PersonelKoordinatorlukler.Add(new PersonelKoordinatorluk { PersonelId = p.PersonelId, KoordinatorlukId = targetKomisyon.KoordinatorlukId });
                    pKomisyon.Add(new PersonelKomisyon { PersonelId = p.PersonelId, KomisyonId = targetKomisyon.KomisyonId });
                }
            }

            _context.PersonelKurumsalRolAtamalari.AddRange(roleAssignments);
            // EF Core might have already added pKomisyon entries if I added to context via navigation property?
            // No, I created new entities in list. Need to add.
            _context.PersonelKomisyonlar.AddRange(pKomisyon);
            await _context.SaveChangesAsync();


            // 6. Task Generation (Weighted)
            var gorevList = new List<Gorev>();
            var gorevAssignments = new List<GorevAtamaPersonel>();

            // Kategori Lookups
            var catDersObj = await _context.GorevKategorileri.FirstOrDefaultAsync(c => c.Ad.Contains("Ders"));
            int catDers = catDersObj != null ? catDersObj.GorevKategoriId : 1;
            
            var catDijitalObj = await _context.GorevKategorileri.FirstOrDefaultAsync(c => c.Ad.Contains("Dijital"));
            int catDijital = catDijitalObj != null ? catDijitalObj.GorevKategoriId : 3;
            
            var allCats = await _context.GorevKategorileri.Select(c => c.GorevKategoriId).ToListAsync();
            if (!allCats.Any()) allCats.Add(1);

            // Statuses
            var statuses = await _context.Set<GorevDurum>().Select(s => s.GorevDurumId).ToListAsync();
            if (!statuses.Any()) statuses.Add(1);

            // Birim
            var birimObj = await _context.Birimler.FirstOrDefaultAsync();
            int? birimId = birimObj?.BirimId;

            // Loop Commissioners
            var workers = await _context.PersonelKomisyonlar
                .Include(pk => pk.Personel)
                .Include(pk => pk.Komisyon).ThenInclude(k => k.Koordinatorluk)
                .ToListAsync();

            foreach(var w in workers)
            {
                if (w.Komisyon?.Koordinatorluk == null) continue;

                string city = w.Komisyon.Koordinatorluk.Ad ?? "";
                bool isHighDensity = city.Contains("İzmir") || city.Contains("Konya") || city.Contains("Antalya") || city.Contains("Sivas");
                
                int taskCount = isHighDensity ? random.Next(8, 15) : random.Next(2, 5); 
                
                for(int t=0; t<taskCount; t++)
                {
                    // Date
                    var date = DateTime.Now.AddDays(-random.Next(360));
                    
                    // Category
                    int catId;
                    if (isHighDensity)
                    {
                        // 50% Ders, 30% Dijital, 20% Others
                        int roll = random.Next(100);
                        if (roll < 50) catId = catDers;
                        else if (roll < 80) catId = catDijital;
                        else catId = allCats[random.Next(allCats.Count)];
                    }
                    else
                    {
                        catId = (random.Next(100) < 80) ? catDers : allCats[random.Next(allCats.Count)];
                    }

                    var gorev = new Gorev
                    {
                        Ad = $"{w.Komisyon.Ad} Görevi - {t+1} - {random.Next(1000)}",
                        Aciklama = "Otomatik oluşturulan görev.",
                        KategoriId = catId,
                        PersonelId = w.PersonelId, 
                        BirimId = birimId,
                        GorevDurumId = statuses[random.Next(statuses.Count)],
                        BaslangicTarihi = date,
                        BitisTarihi = date.AddDays(random.Next(5, 30)),
                        CreatedAt = date,
                        IsActive = true
                    };
                    
                    gorevList.Add(gorev);
                }
            }
            
            _context.Gorevler.AddRange(gorevList);
            await _context.SaveChangesAsync();

            // Create Assignments
            foreach(var g in gorevList)
            {
                 gorevAssignments.Add(new GorevAtamaPersonel { GorevId = g.GorevId, PersonelId = g.PersonelId ?? 0 }); 
                 
                 var userComm = workers.FirstOrDefault(u => u.PersonelId == g.PersonelId);
                 if (userComm != null)
                 {
                     _context.GorevAtamaKomisyonlar.Add(new GorevAtamaKomisyon { GorevId = g.GorevId, KomisyonId = userComm.KomisyonId });
                     _context.GorevAtamaKoordinatorlukler.Add(new GorevAtamaKoordinatorluk { GorevId = g.GorevId, KoordinatorlukId = userComm.Komisyon.KoordinatorlukId });
                 }
            }
            _context.GorevAtamaPersoneller.AddRange(gorevAssignments);
            await _context.SaveChangesAsync();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
