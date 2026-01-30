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
            // 1. Identify Protected Users
            // Protect "Vedat" (includes "Vedat Ersin Ceviz") and "Sevilay"
            // Using ToLower() for safety
            var protectedIds = await _context.Personeller
                .Where(p => p.Ad.ToLower().Contains("vedat") || p.Ad.ToLower().Contains("sevilay") ||
                            p.Soyad.ToLower().Contains("ceviz")) // Extra safety for Vedat Ersin Ceviz
                .Select(p => p.PersonelId)
                .ToListAsync();

            // 2. Clear Dependent Data
            // Delete all Logs
            await _context.SistemLoglar.ExecuteDeleteAsync();
            
            // Delete all Notifications
            await _context.Bildirimler.ExecuteDeleteAsync();

            // Delete Assignments (Relations)
            await _context.PersonelKurumsalRolAtamalari.ExecuteDeleteAsync();
            await _context.PersonelGorevTurleri.ExecuteDeleteAsync();
            await _context.PersonelIsNitelikleri.ExecuteDeleteAsync();
            await _context.PersonelKomisyonlar.ExecuteDeleteAsync();
            await _context.PersonelKoordinatorlukler.ExecuteDeleteAsync();
            await _context.PersonelTeskilatlar.ExecuteDeleteAsync();
            await _context.PersonelUzmanliklar.ExecuteDeleteAsync();
            await _context.PersonelYazilimlar.ExecuteDeleteAsync();

            // Delete Other Personnel
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

            // Delete Structure
            await _context.Komisyonlar.ExecuteDeleteAsync();
            await _context.Koordinatorlukler.ExecuteDeleteAsync();
            await _context.Teskilatlar.ExecuteDeleteAsync();

            // 3. Re-Feed Structure (10 Cities)
            var cities = new[] { "Ankara", "İstanbul", "İzmir", "Antalya", "Bursa", "Adana", "Gaziantep", "Konya", "Mersin", "Diyarbakır" };
            var random = new Random();

            // Create Teskilat (Root)
            var teskilat = new Teskilat { Ad = "Temel Eğitim Genel Müdürlüğü" }; // UstBirimId removed as it doesn't exist
            _context.Teskilatlar.Add(teskilat);
            await _context.SaveChangesAsync();

            var koordinatorlukler = new List<Koordinatorluk>();
            foreach (var city in cities)
            {
                var koord = new Koordinatorluk 
                { 
                    Ad = $"{city} İl Koordinatörlüğü", 
                    TeskilatId = teskilat.TeskilatId // ParentId removed, correctly using TeskilatId
                };
                koordinatorlukler.Add(koord);
            }
            _context.Koordinatorlukler.AddRange(koordinatorlukler);
            await _context.SaveChangesAsync();

            // 4. Create Commissions (4-5 per City)
            var commissions = new List<Komisyon>();
            var commissionTypes = new[] { "Eğitim Komisyonu", "Denetim Komisyonu", "Planlama Komisyonu", "Bütçe Komisyonu", "AR-GE Komisyonu", "Dijitalleşme Komisyonu" };

            foreach (var koord in koordinatorlukler)
            {
                int count = random.Next(4, 6); // 4 or 5
                for (int i = 0; i < count; i++)
                {
                    commissions.Add(new Komisyon
                    {
                        Ad = commissionTypes[i % commissionTypes.Length],
                        KoordinatorlukId = koord.KoordinatorlukId // ParentId removed, correctly using KoordinatorlukId
                    });
                }
            }
            _context.Komisyonlar.AddRange(commissions);
            await _context.SaveChangesAsync();

            // 5. Create Personnel (~120 total)
            var names = new[] { "Ali", "Ayşe", "Mehmet", "Fatma", "Ahmet", "Zeynep", "Mustafa", "Elif", "Can", "Seda", "Burak", "Gamze", "Murat", "Esra", "Ömer", "Derya" };
            var surnames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Öztürk", "Aydın", "Özdemir", "Arslan", "Doğan", "Kılıç", "etin", "Kara" };
            
            var newPersonelList = new List<Personel>();
            
            CreatePasswordHash("123456", out byte[] hash, out byte[] salt);

            // Ensure Lookups Exist
            var ilIds = await _context.Iller.Select(x => x.IlId).ToListAsync();
            if (!ilIds.Any())
            {
                // Seed basic Cities if missing
                var basicCities = new[] { "Ankara", "İstanbul", "İzmir", "Bursa", "Antalya" };
                foreach(var c in basicCities) { _context.Iller.Add(new Il { Ad = c }); }
                await _context.SaveChangesAsync();
                ilIds = await _context.Iller.Select(x => x.IlId).ToListAsync();
            }

            var bransIds = await _context.Branslar.Select(x => x.BransId).ToListAsync();
            if (!bransIds.Any())
            {
                var basicBrans = new[] { "Sınıf Öğretmenliği", "İngilizce", "Matematik", "Fen Bilimleri", "Türkçe", "Rehberlik" };
                foreach(var b in basicBrans) { _context.Branslar.Add(new Brans { Ad = b }); }
                await _context.SaveChangesAsync();
                bransIds = await _context.Branslar.Select(x => x.BransId).ToListAsync();
            }

            for (int i = 0; i < 120; i++)
            {
                string name = names[random.Next(names.Length)];
                string surname = surnames[random.Next(surnames.Length)];
                string tc = GenerateTC(random);

                newPersonelList.Add(new Personel
                {
                    Ad = name,
                    Soyad = surname,
                    TcKimlikNo = tc,
                    Telefon = "555" + random.Next(1000000, 9999999),
                    Eposta = $"{name.ToLower()}.{surname.ToLower()}{i}@meb.gov.tr",
                    SifreHash = hash,
                    SifreSalt = salt,
                    SistemRolId = 2, 
                    AktifMi = true,
                    CreatedAt = DateTime.Now,
                    
                    // Fixed Required Fields
                    KadroKurum = "MEB",
                    GorevliIlId = ilIds[random.Next(ilIds.Count)],
                    BransId = bransIds[random.Next(bransIds.Count)]
                });
            }
            _context.Personeller.AddRange(newPersonelList);
            await _context.SaveChangesAsync();

            // 6. Assign Roles & Distributions
            var allPersonel = await _context.Personeller.Where(p => !protectedIds.Contains(p.PersonelId)).ToListAsync();

            // Roles Lookup
            var roles = await _context.KurumsalRoller.ToListAsync();
            // Assuming KurumsalRol has KurumsalRolId, not RolId. Checked ViewFile 1118: yes, KurumsalRolId.
            var rolPersonel = roles.FirstOrDefault(r => r.Ad.Contains("Personel"))?.KurumsalRolId ?? 1;
            var rolBaskan = roles.FirstOrDefault(r => r.Ad.Contains("Başkan"))?.KurumsalRolId ?? 2;
            var rolKoord = roles.FirstOrDefault(r => r.Ad.Contains("İl Koordinatörü") || r.Ad.Contains("Koordinatör"))?.KurumsalRolId ?? 3;

            var index = 0;
            // A. Assign City Coordinators (10)
            foreach (var koord in koordinatorlukler)
            {
                if (index >= allPersonel.Count) break;
                var p = allPersonel[index++];
                
                _context.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama
                {
                    PersonelId = p.PersonelId,
                    KurumsalRolId = rolKoord,
                    KoordinatorlukId = koord.KoordinatorlukId
                });
            }

            // B. Assign Commission Presidents (50)
            foreach (var kom in commissions)
            {
                if (index >= allPersonel.Count) break;
                var p = allPersonel[index++];

                _context.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama
                {
                    PersonelId = p.PersonelId,
                    KurumsalRolId = rolBaskan,
                    KomisyonId = kom.KomisyonId,
                    KoordinatorlukId = kom.KoordinatorlukId 
                });
            }

            // C. Assign Members (Rest)
            while (index < allPersonel.Count)
            {
                var p = allPersonel[index++];
                var kom = commissions[random.Next(commissions.Count)]; 
                
                _context.PersonelKurumsalRolAtamalari.Add(new PersonelKurumsalRolAtama
                {
                    PersonelId = p.PersonelId,
                    KurumsalRolId = rolPersonel,
                    KomisyonId = kom.KomisyonId,
                    KoordinatorlukId = kom.KoordinatorlukId
                });
            }

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

        private string GenerateTC(Random rnd)
        {
            long tc = 10000000000 + (long)(rnd.NextDouble() * 89999999999);
            return tc.ToString();
        }
    }
}
