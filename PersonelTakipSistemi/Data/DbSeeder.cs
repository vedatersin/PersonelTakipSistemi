using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace PersonelTakipSistemi.Data
{
    public static class DbSeeder
    {
        public static void Seed(TegmPersonelTakipDbContext context)
        {
            var rand = new Random();

            // 0. Seed Lookup Tables (Upsert/Ensure Pattern)
            var standardYazilimlar = new[] { "C#", "Java", "Python", "React", "Angular", "SQL", "Docker", "Kubernetes", "Azure", "AWS" };
            foreach(var y in standardYazilimlar)
            {
                if (!context.Yazilimlar.Any(x => x.Ad == y)) context.Yazilimlar.Add(new Yazilim { Ad = y });
            }
            context.SaveChanges();

            var standardUzmanliklar = new[] { "Backend Geliştirici", "Frontend Geliştirici", "Full Stack", "DevOps", "Veri Analisti", "Sistem Yöneticisi", "Mobil Geliştirici" };
            foreach(var u in standardUzmanliklar)
            {
                if (!context.Uzmanliklar.Any(x => x.Ad == u)) context.Uzmanliklar.Add(new Uzmanlik { Ad = u });
            }
            context.SaveChanges();

            var standardGorevTurleri = new[] { "Yazılım Geliştirme", "Test", "Analiz", "Bakım", "Destek" };
            foreach(var g in standardGorevTurleri)
            {
                 if (!context.GorevTurleri.Any(x => x.Ad == g)) context.GorevTurleri.Add(new GorevTuru { Ad = g });
            }
            context.SaveChanges();

            var standardIsNitelikleri = new[] { "Acil", "Önemli", "Rutin", "Düşük Öncelik" };
            foreach(var i in standardIsNitelikleri)
            {
                 if (!context.IsNitelikleri.Any(x => x.Ad == i)) context.IsNitelikleri.Add(new IsNiteligi { Ad = i });
            }
            context.SaveChanges();

            // Gorev Durumlari (Check by ID or Name - Name is safer for duplicates)
            // Gorev Durumlari (Updated to Correct List)
            var durumlar = new[] 
            {
                new { Ad = "Atanmayı Bekliyor", Renk = "bg-secondary", Sira = 1 },
                new { Ad = "Devam Ediyor", Renk = "bg-primary", Sira = 2 },
                new { Ad = "Kontrolde", Renk = "bg-info", Sira = 3 },
                new { Ad = "Tamamlandı", Renk = "bg-success", Sira = 4 },
                new { Ad = "İptal Edildi", Renk = "bg-danger", Sira = 5 }
            };

            foreach(var d in durumlar)
            {
                if (!context.GorevDurumlari.Any(x => x.Ad == d.Ad))
                {
                    context.GorevDurumlari.Add(new GorevDurum { Ad = d.Ad, RenkSinifi = d.Renk, Sira = d.Sira });
                }
            }
            context.SaveChanges();

            // CLEANUP DUPLICATES/OLD NAMES
            var badNames = new Dictionary<string, string>
            {
                { "Bekliyor", "Atanmayı Bekliyor" },
                { "Kontrol Bekliyor", "Kontrolde" },
                { "İptal", "İptal Edildi" }
            };

            var validStatuses = context.GorevDurumlari.ToList(); // Fetch all handling
            
            foreach(var bad in badNames)
            {
                var badEntity = context.GorevDurumlari.FirstOrDefault(x => x.Ad == bad.Key);
                if (badEntity != null)
                {
                    var validEntity = validStatuses.FirstOrDefault(x => x.Ad == bad.Value);
                    if (validEntity != null)
                    {
                        // Migrate Tasks
                        var tasksToMigrate = context.Gorevler.Where(x => x.GorevDurumId == badEntity.GorevDurumId).ToList();
                        foreach(var t in tasksToMigrate)
                        {
                            t.GorevDurumId = validEntity.GorevDurumId;
                        }
                        
                        // Delete Bad Status
                        context.GorevDurumlari.Remove(badEntity);
                    }
                }
            }
            context.SaveChanges();

             if (!context.GorevKategorileri.Any()) // Kategoriler can be vague, Any check is okay but let's be safe
             {
                 context.GorevKategorileri.AddRange(new[]
                 {
                    new GorevKategori { Ad = "Yazılım Geliştirme", Aciklama="Yazılım projeleri ile ilgili görevler" },
                    new GorevKategori { Ad = "Donanım", Aciklama="Donanım ve altyapı işleri" },
                    new GorevKategori { Ad = "İdari", Aciklama="İdari işler ve raporlama" },
                    new GorevKategori { Ad = "Eğitim", Aciklama="Personel eğitimi ve sertifikasyon" }
                 });
                 context.SaveChanges();
             }

            // 1. Ensure Lookups exist
            var iller = context.Iller.Select(x => x.IlId).ToList();
            var branslar = context.Branslar.Select(x => x.BransId).ToList();
            
            // Re-fetch these after seeding
            var yazilimlar = context.Yazilimlar.Select(x => x.YazilimId).ToList();
            var uzmanliklar = context.Uzmanliklar.Select(x => x.UzmanlikId).ToList();
            var gorevTurleri = context.GorevTurleri.Select(x => x.GorevTuruId).ToList();
            var isNitelikleri = context.IsNitelikleri.Select(x => x.IsNiteligiId).ToList();
            
            // Check if essential lookups are present
            if (!iller.Any() || !branslar.Any() || !yazilimlar.Any() || !uzmanliklar.Any() || !gorevTurleri.Any() || !isNitelikleri.Any()) 
                return; 

            var turkishNames = new[] { "Ahmet", "Mehmet", "Ayşe", "Fatma", "Mustafa", "Zeynep", "Elif", "Hakan", "Burak", "Ceren", "Deniz", "Cem", "Can", "Ece", "Barış", "Selin", "Volkan", "Pınar", "Onur", "Gamze", "Murat", "Özlem", "Serkan", "Esra", "Sinan", "Beren" };
            var turkishSurnames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Yıldırım", "Öztürk", "Aydın", "Özdemir", "Arslan", "Doğan", "Kılıç", "Aslan", "Çetin", "Kara", "Koç", "Kurt", "Özkan", "Şimşek", "Polat", "Keskin", "Duran", "Avcı", "Ünal" };

            // 2. Clean up duplicates & Limit to 250
            var allPeople = context.Personeller.OrderBy(p => p.CreatedAt).ToList();
            var uniqueTcs = new HashSet<string>();
            var toDelete = new List<Personel>();
            var toKeep = new List<Personel>();

            foreach (var p in allPeople)
            {
                if (uniqueTcs.Contains(p.TcKimlikNo)) 
                    toDelete.Add(p);
                else 
                { 
                    uniqueTcs.Add(p.TcKimlikNo); 
                    toKeep.Add(p); 
                }
            }

            int targetCount = 250;
            
            // Protect VIPs check
            var vips = toKeep.Where(x => x.Ad.Contains("Vedat", StringComparison.OrdinalIgnoreCase) || 
                                         x.Ad.Contains("Sevilay", StringComparison.OrdinalIgnoreCase)).ToList();
            var deletableCandidates = toKeep.Except(vips).ToList();
            
            int currentCount = toKeep.Count;
            if (currentCount > targetCount)
            {
                int amountToDelete = currentCount - targetCount;
                var excess = deletableCandidates.OrderByDescending(x => x.CreatedAt).Take(amountToDelete).ToList();
                toDelete.AddRange(excess); // Add valid excess to deletion list
                toKeep.RemoveAll(x => excess.Contains(x));
            }

            // DELETE PROCESSING (Safe Manual Cascade)
            if (toDelete.Any())
            {
                var deleteIds = toDelete.Select(x => x.PersonelId).ToList();

                // 2.1 Remove Direct Relations (Join Tables)
                var py = context.PersonelYazilimlar.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelYazilimlar.RemoveRange(py);
                var pu = context.PersonelUzmanliklar.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelUzmanliklar.RemoveRange(pu);
                var pg = context.PersonelGorevTurleri.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelGorevTurleri.RemoveRange(pg);
                var pi = context.PersonelIsNitelikleri.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelIsNitelikleri.RemoveRange(pi);
                
                // 2.2 Remove Auth Relations
                var pt = context.PersonelTeskilatlar.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelTeskilatlar.RemoveRange(pt);
                var pkond = context.PersonelKoordinatorlukler.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelKoordinatorlukler.RemoveRange(pkond);
                var pkom = context.PersonelKomisyonlar.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelKomisyonlar.RemoveRange(pkom);
                var prole = context.PersonelKurumsalRolAtamalari.Where(x => deleteIds.Contains(x.PersonelId)); context.PersonelKurumsalRolAtamalari.RemoveRange(prole);

                // 2.3 Remove Task Assignments (GorevAtamaPersonel)
                var gap = context.GorevAtamaPersoneller.Where(x => deleteIds.Contains(x.PersonelId)); context.GorevAtamaPersoneller.RemoveRange(gap);

                // 2.4 Nullify or Delete Tasks created by this person (Or Assigned directly in old schema)
                var tasks = context.Gorevler.Where(x => x.PersonelId.HasValue && deleteIds.Contains(x.PersonelId.Value)).ToList();
                foreach(var t in tasks) { t.PersonelId = null; } // Unassign

                // 2.5 Remove Notifications
                var b1 = context.Bildirimler.Where(x => deleteIds.Contains(x.AliciPersonelId)); context.Bildirimler.RemoveRange(b1);
                var b2 = context.Bildirimler.Where(x => x.GonderenPersonelId.HasValue && deleteIds.Contains(x.GonderenPersonelId.Value)); context.Bildirimler.RemoveRange(b2);

                context.SaveChanges(); // Commit dependency cleanup first

                // 2.6 Finally Delete Personel
                context.Personeller.RemoveRange(toDelete);
                context.SaveChanges();
            }

            // 4. Fill up to 250
            currentCount = toKeep.Count;
            if (currentCount < targetCount)
            {
                int needed = targetCount - currentCount;
                for (int i = 0; i < needed; i++)
                {
                    string tc;
                    do { tc = GenerateRandomTC(rand); } while (uniqueTcs.Contains(tc));
                    uniqueTcs.Add(tc);

                    string ad = turkishNames[rand.Next(turkishNames.Length)];
                    string soyad = turkishSurnames[rand.Next(turkishSurnames.Length)];
                    
                    CreatePersonel(context, tc, ad, soyad, iller, branslar, rand, yazilimlar, uzmanliklar, gorevTurleri, isNitelikleri);
                }
            }

            // 5. Relations Check for ALL (Old & New)
            // Fetch freshly to include logic check or just ID based
            var finalUsers = context.Personeller.ToList(); 
            foreach (var u in finalUsers)
            {
                EnsureRelations(context, u, rand, yazilimlar, uzmanliklar, gorevTurleri, isNitelikleri);
            }
            context.SaveChanges();

            // 6. Redistribute Task Statuses (Weighted)
            var allTasks = context.Gorevler.ToList();
            if (allTasks.Any())
            {
                foreach(var t in allTasks)
                {
                    int r = rand.Next(100);
                    if (r < 5) t.GorevDurumId = 3; // 5% Cancelled
                    else if (r < 25) t.GorevDurumId = 1; // 20% Waiting
                    else if (r < 60) t.GorevDurumId = 2; // 35% In Progress
                    else t.GorevDurumId = 5; // 40% Completed
                }
                context.SaveChanges();
            }
        }

        private static string GenerateRandomTC(Random rand)
        {
            string s = "";
            for (int i = 0; i < 11; i++) s += rand.Next(10).ToString();
            return s;
        }

        private static void CreatePersonel(
            TegmPersonelTakipDbContext context, 
            string tc,
            string ad,
            string soyad,
            List<int> iller, 
            List<int> branslar, 
            Random rand, 
            List<int> yazilimlar,
            List<int> uzmanliklar,
            List<int> gorevTurleri,
            List<int> isNitelikleri)
        {
            string plainPasword = tc.Length >= 6 ? tc.Substring(0, 6) : "123456";
            CreatePasswordHash(plainPasword, out byte[] passwordHash, out byte[] passwordSalt);

            var personel = new Personel
            {
                Ad = ad,
                Soyad = soyad,
                TcKimlikNo = tc,
                Telefon = "555" + (tc.Length >= 7 ? tc.Substring(0, 7) : "0000000"),
                Eposta = $"personel{tc.Substring(0, Math.Min(4, tc.Length))}@meb.gov.tr",
                PersonelCinsiyet = rand.Next(2) == 1,
                DogumTarihi = DateTime.Now.AddYears(-rand.Next(20, 50)),
                GorevliIlId = iller[rand.Next(iller.Count)],
                BransId = branslar[rand.Next(branslar.Count)],
                KadroKurum = "MEB Merkez",
                AktifMi = true,
                FotografYolu = null,
                Sifre = plainPasword,
                SifreHash = passwordHash,
                SifreSalt = passwordSalt,
                CreatedAt = DateTime.Now
            };
            context.Personeller.Add(personel);
            context.SaveChanges();
            
            // Allow relations to be added in main loop
        }

        private static void EnsureRelations(
            TegmPersonelTakipDbContext context,
            Personel personel,
            Random rand,
            List<int> yazilimlar,
            List<int> uzmanliklar,
            List<int> gorevTurleri,
            List<int> isNitelikleri)
        {
            // Yazilimlar
            // 1. Get existing IDs + Local Tracker IDs to avoid conflicts
            // Simplified: Just use Hash of Ids to be added
            if (!context.PersonelYazilimlar.Any(x => x.PersonelId == personel.PersonelId))
            {
                 int count = rand.Next(1, 4); 
                 var addedIds = new HashSet<int>();
                 for(int i=0; i<count; i++) 
                 {
                    var yId = yazilimlar[rand.Next(yazilimlar.Count)];
                    if (!addedIds.Contains(yId)) 
                    {
                        addedIds.Add(yId);
                        context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personel.PersonelId, YazilimId = yId });
                    }
                 }
            }

            // Uzmanliklar
            if (!context.PersonelUzmanliklar.Any(x => x.PersonelId == personel.PersonelId))
            {
                 int count = rand.Next(1, 4); 
                 var addedIds = new HashSet<int>();
                 for(int i=0; i<count; i++) 
                 {
                    var uId = uzmanliklar[rand.Next(uzmanliklar.Count)];
                    if (!addedIds.Contains(uId))
                    {
                        addedIds.Add(uId);
                        context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personel.PersonelId, UzmanlikId = uId });
                    }
                 }
            }

            // Gorev Turu
            if (!context.PersonelGorevTurleri.Any(x => x.PersonelId == personel.PersonelId))
            {
                 var gId = gorevTurleri[rand.Next(gorevTurleri.Count)];
                 // Since we add only 1, no duplicate risk within loop.
                 // But check DB just in case? No, the outer IF checks (!Any).
                 context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personel.PersonelId, GorevTuruId = gId });
            }

             // Is Niteligi
            if (!context.PersonelIsNitelikleri.Any(x => x.PersonelId == personel.PersonelId))
            {
                 int count = rand.Next(1, 3);
                 var addedIds = new HashSet<int>();
                 for(int i=0; i<count; i++) 
                 {
                    var inId = isNitelikleri[rand.Next(isNitelikleri.Count)];
                    if (!addedIds.Contains(inId))
                    {
                        addedIds.Add(inId);
                        context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personel.PersonelId, IsNiteligiId = inId });
                    }
                 }
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(passwordSalt);
            }

            passwordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: passwordSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
        }
    }
}
