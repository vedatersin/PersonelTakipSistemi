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
            // TCs: 000...0 to 999...9
            var tcs = new string[] 
            {
                "00000000000", "11111111111", "22222222222", "33333333333", "44444444444",
                "55555555555", "66666666666", "77777777777", "88888888888", "99999999999"
            };

            // Pre-fetch lookups to avoid multiple DB calls
            var iller = context.Iller.Select(x => x.IlId).ToList();
            var branslar = context.Branslar.Select(x => x.BransId).ToList();
            var yazilimlar = context.Yazilimlar.Select(x => x.YazilimId).ToList();
            var uzmanliklar = context.Uzmanliklar.Select(x => x.UzmanlikId).ToList();
            var gorevTurleri = context.GorevTurleri.Select(x => x.GorevTuruId).ToList();
            var isNitelikleri = context.IsNitelikleri.Select(x => x.IsNiteligiId).ToList();

            var rand = new Random();
            var turkishNames = new[] { "Ahmet", "Mehmet", "Ayşe", "Fatma", "Mustafa", "Zeynep", "Elif", "Hakan", "Burak", "Ceren", "Deniz", "Cem", "Can", "Ece", "Barış", "Selin", "Volkan", "Pınar", "Onur", "Gamze" };
            var turkishSurnames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Yıldırım", "Öztürk", "Aydın", "Özdemir", "Arslan", "Doğan", "Kılıç", "Aslan", "Çetin", "Kara", "Koç", "Kurt", "Özkan", "Şimşek" };

            foreach (var tc in tcs)
            {
                var existingPersonel = context.Personeller
                    .Include(p => p.PersonelYazilimlar)
                    .Include(p => p.PersonelUzmanliklar)
                    .Include(p => p.PersonelGorevTurleri)
                    .Include(p => p.PersonelIsNitelikleri)
                    .FirstOrDefault(p => p.TcKimlikNo == tc);

                // Determine name (Random if new, or update 'Otomatik' ones)
                string ad = turkishNames[rand.Next(turkishNames.Length)];
                string soyad = turkishSurnames[rand.Next(turkishSurnames.Length)];

                if (existingPersonel != null)
                {
                    // Update name if it is one of the old auto-generated ones
                    if (existingPersonel.Soyad == "Otomatik")
                    {
                        existingPersonel.Ad = ad;
                        existingPersonel.Soyad = soyad;
                    }

                    // Ensure relations exist
                    EnsureRelations(context, existingPersonel, rand, yazilimlar, uzmanliklar, gorevTurleri, isNitelikleri);
                    context.SaveChanges();
                }
                else
                {
                    CreatePersonel(context, tc, ad, soyad, iller, branslar, rand, 
                        yazilimlar, uzmanliklar, gorevTurleri, isNitelikleri);
                }
            }
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
            string plainPasword = tc.Substring(0, 6);
            CreatePasswordHash(plainPasword, out byte[] passwordHash, out byte[] passwordSalt);

            var personel = new Personel
            {
                Ad = ad,
                Soyad = soyad,
                TcKimlikNo = tc,
                Telefon = "555" + tc.Substring(0, 7),
                Eposta = $"personel{tc.Substring(0, 3)}@meb.gov.tr",
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
            
            EnsureRelations(context, personel, rand, yazilimlar, uzmanliklar, gorevTurleri, isNitelikleri);
            context.SaveChanges();
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
            // Validations usually require at least 1. We allow 0 only if strictly intended, but here we enforce >= 1 to look "normal".
            
            // Yazilimlar
            if (!context.PersonelYazilimlar.Any(x => x.PersonelId == personel.PersonelId))
            {
                int count = rand.Next(1, 4); // Min 1
                for(int i=0; i<count; i++) 
                {
                   var yId = yazilimlar[rand.Next(yazilimlar.Count)];
                   if (!context.PersonelYazilimlar.Any(x => x.PersonelId == personel.PersonelId && x.YazilimId == yId))
                       context.PersonelYazilimlar.Add(new PersonelYazilim { PersonelId = personel.PersonelId, YazilimId = yId });
                }
            }

            // Uzmanliklar
            if (!context.PersonelUzmanliklar.Any(x => x.PersonelId == personel.PersonelId))
            {
                int count = rand.Next(1, 4); // Min 1
                for(int i=0; i<count; i++) 
                {
                   var uId = uzmanliklar[rand.Next(uzmanliklar.Count)];
                   if (!context.PersonelUzmanliklar.Any(x => x.PersonelId == personel.PersonelId && x.UzmanlikId == uId))
                       context.PersonelUzmanliklar.Add(new PersonelUzmanlik { PersonelId = personel.PersonelId, UzmanlikId = uId });
                }
            }

            // GorevTurleri
            if (!context.PersonelGorevTurleri.Any(x => x.PersonelId == personel.PersonelId))
            {
                var gId = gorevTurleri[rand.Next(gorevTurleri.Count)]; // Just 1
                context.PersonelGorevTurleri.Add(new PersonelGorevTuru { PersonelId = personel.PersonelId, GorevTuruId = gId });
            }

             // IsNitelikleri
            if (!context.PersonelIsNitelikleri.Any(x => x.PersonelId == personel.PersonelId))
            {
                int count = rand.Next(1, 3); // Min 1
                for(int i=0; i<count; i++) 
                {
                   var inId = isNitelikleri[rand.Next(isNitelikleri.Count)];
                   if (!context.PersonelIsNitelikleri.Any(x => x.PersonelId == personel.PersonelId && x.IsNiteligiId == inId))
                       context.PersonelIsNitelikleri.Add(new PersonelIsNiteligi { PersonelId = personel.PersonelId, IsNiteligiId = inId });
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
