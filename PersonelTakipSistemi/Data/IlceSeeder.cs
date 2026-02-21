using System.Collections.Generic;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Data
{
    public static class IlceSeeder
    {
        public static List<Ilce> GetDistricts()
        {
            var ilceList = new List<Ilce>();
            int idCounter = 1;

            var districtsByPlate = new Dictionary<int, string[]>
            {
                { 1, new[] { "Seyhan", "Yüreğir", "Çukurova", "Sarıçam", "Ceyhan", "Kozan", "İmamoğlu", "Karataş", "Karaisalı", "Pozantı", "Yumurtalık", "Tufanbeyli", "Feke", "Aladağ", "Saimbeyli" } }, // Adana
                { 2, new[] { "Adıyaman Merkez", "Kahta", "Besni", "Gölbaşı", "Gerger", "Sincik", "Çelikhan", "Tut", "Samsat" } }, // Adıyaman
                { 3, new[] { "Afyonkarahisar Merkez", "Sandıklı", "Dinar", "Bolvadin", "Sinanpaşa", "Emirdağ", "Şuhut", "Çay", "İhsaniye", "İscehisar", "Sultandağı", "Çobanlar", "Dazkırı", "Başmakçı", "Hocalar", "Bayat", "Evciler", "Kızılören" } }, // Afyonkarahisar
                { 4, new[] { "Ağrı Merkez", "Patnos", "Doğubayazıt", "Diyadin", "Eleşkirt", "Tutak", "Taşlıçay", "Hamur" } }, // Ağrı
                { 5, new[] { "Amasya Merkez", "Merzifon", "Suluova", "Taşova", "Gümüşhacıköy", "Göynücek", "Hamamözü" } }, // Amasya
                { 6, new[] { "Çankaya", "Keçiören", "Yenimahalle", "Mamak", "Etimesgut", "Sincan", "Altındağ", "Pursaklar", "Gölbaşı", "Polatlı", "Çubuk", "Kahramankazan", "Beypazarı", "Elmadağ", "Şereflikoçhisar", "Akyurt", "Nallıhan", "Haymana", "Kızılcahamam", "Bala", "Kalecik", "Ayaş", "Güdül", "Çamlıdere", "Evren" } }, // Ankara
                { 7, new[] { "Kepez", "Muratpaşa", "Alanya", "Manavgat", "Konyaaltı", "Serik", "Aksu", "Kumluca", "Döşemealtı", "Kaş", "Korkuteli", "Gazipaşa", "Finike", "Kemer", "Elmalı", "Demre", "Akseki", "Gündoğmuş", "İbradı" } }, // Antalya
                { 8, new[] { "Artvin Merkez", "Hopa", "Borçka", "Yusufeli", "Arhavi", "Şavşat", "Ardanuç", "Murgul", "Kemalpaşa" } }, // Artvin
                { 9, new[] { "Efeler", "Nazilli", "Söke", "Kuşadası", "Didim", "İncirliova", "Çine", "Germencik", "Bozdoğan", "Köşk", "Kuyucak", "Sultanhisar", "Karacasu", "Buharkent", "Yenipazar", "Karpuzlu" } }, // Aydın
                { 10, new[] { "Altıeylül", "Karesi", "Edremit", "Bandırma", "Gönen", "Ayvalık", "Burhaniye", "Bigadiç", "Susurluk", "Dursunbey", "Sındırgı", "İvrindi", "Erdek", "Havran", "Kepsut", "Manyas", "Savaştepe", "Balya", "Gömeç", "Marmara" } }, // Balıkesir
                { 11, new[] { "Bilecik Merkez", "Bozüyük", "Osmaneli", "Söğüt", "Gölpazarı", "Pazaryeri", "İnhisar", "Yenipazar" } }, // Bilecik
                { 12, new[] { "Bingöl Merkez", "Genç", "Solhan", "Karlıova", "Adaklı", "Kiğı", "Yedisu", "Yayladere" } }, // Bingöl
                { 13, new[] { "Tatvan", "Bitlis Merkez", "Güroymak", "Ahlat", "Hizan", "Mutki", "Adilcevaz" } }, // Bitlis
                { 14, new[] { "Bolu Merkez", "Gerede", "Mudurnu", "Göynük", "Mengen", "Yeniçağa", "Dörtdivan", "Seben", "Kıbrıscık" } }, // Bolu
                { 15, new[] { "Burdur Merkez", "Bucak", "Gölhisar", "Yeşilova", "Çavdır", "Tefenni", "Ağlasun", "Karamanlı", "Altınyayla", "Çeltikçi", "Kemer" } }, // Burdur
                { 16, new[] { "Osmangazi", "Yıldırım", "Nilüfer", "İnegöl", "Gemlik", "Mustafakemalpaşa", "Mudanya", "Gürsu", "Karacabey", "Orhangazi", "Kestel", "Yenişehir", "İznik", "Orhaneli", "Keles", "Büyükorhan", "Harmancık" } }, // Bursa
                { 17, new[] { "Çanakkale Merkez", "Biga", "Çan", "Gelibolu", "Ayvacık", "Yenice", "Ezine", "Bayramiç", "Lapseki", "Eceabat", "Gökçeada", "Bozcaada" } }, // Çanakkale
                { 18, new[] { "Çankırı Merkez", "Çerkeş", "Ilgaz", "Orta", "Şabanözü", "Kurşunlu", "Yapraklı", "Kızılırmak", "Eldivan", "Atkaracalar", "Korgun", "Bayramören" } }, // Çankırı
                { 19, new[] { "Çorum Merkez", "Sungurlu", "Osmancık", "İskilip", "Alaca", "Bayat", "Mecitözü", "Kargı", "Ortaköy", "Uğurludağ", "Dodurga", "Oğuzlar", "Laçin", "Boğazkale" } }, // Çorum
                { 20, new[] { "Pamukkale", "Merkezefendi", "Çivril", "Acıpayam", "Tavas", "Honaz", "Sarayköy", "Buldan", "Kale", "Çal", "Serinhisar", "Çameli", "Bozkurt", "Güney", "Çardak", "Bekilli", "Beyağaç", "Babadağ", "Baklan" } }, // Denizli
                { 21, new[] { "Bağlar", "Kayapınar", "Yenişehir", "Sur", "Ergani", "Bismil", "Silvan", "Çınar", "Çermik", "Dicle", "Kulp", "Hani", "Lice", "Eğil", "Hazro", "Kocaköy", "Çüngüş" } }, // Diyarbakır
                { 22, new[] { "Edirne Merkez", "Keşan", "Uzunköprü", "İpsala", "Havsa", "Meriç", "Enez", "Süloğlu", "Lalapaşa" } }, // Edirne
                { 23, new[] { "Elazığ Merkez", "Kovancılar", "Karakoçan", "Palu", "Arıcak", "Baskil", "Maden", "Sivrice", "Alacakaya", "Keban", "Ağın" } }, // Elazığ
                { 24, new[] { "Erzincan Merkez", "Tercan", "Üzümlü", "Çayırlı", "İliç", "Kemah", "Kemaliye", "Otlukbeli", "Refahiye" } }, // Erzincan
                { 25, new[] { "Yakutiye", "Palandöken", "Aziziye", "Horasan", "Oltu", "Pasinler", "Karayazı", "Hınıs", "Tekman", "Karaçoban", "Aşkale", "Şenkaya", "Çat", "Köprüköy", "İspir", "Tortum", "Narman", "Uzundere", "Olur", "Pazaryolu" } }, // Erzurum
                { 26, new[] { "Odunpazarı", "Tepebaşı", "Sivrihisar", "Çifteler", "Seyitgazi", "Alpu", "Mihalıççık", "Mahmudiye", "Beylikova", "İnönü", "Günyüzü", "Han", "Mihalgazi", "Sarıcakaya" } }, // Eskişehir
                { 27, new[] { "Şahinbey", "Şehitkamil", "Nizip", "İslahiye", "Nurdağı", "Oğuzeli", "Araban", "Yavuzeli", "Karkamış" } }, // Gaziantep
                { 28, new[] { "Giresun Merkez", "Bulancak", "Espiye", "Görele", "Tirebolu", "Dereli", "Şebinkarahisar", "Keşap", "Yağlıdere", "Piraziz", "Eynesil", "Alucra", "Çamoluk", "Güce", "Doğankent", "Çanakçı" } }, // Giresun
                { 29, new[] { "Gümüşhane Merkez", "Kelkit", "Şiran", "Kürtün", "Torul", "Köse" } }, // Gümüşhane
                { 30, new[] { "Yüksekova", "Hakkari Merkez", "Şemdinli", "Çukurca", "Derecik" } }, // Hakkari
                { 31, new[] { "Antakya", "İskenderun", "Defne", "Dörtyol", "Samandağ", "Kırıkhan", "Reyhanlı", "Arsuz", "Altınözü", "Hassa", "Payas", "Erzin", "Yayladağı", "Belen", "Kumlu" } }, // Hatay
                { 32, new[] { "Isparta Merkez", "Yalvaç", "Eğirdir", "Şarkikaraağaç", "Gelendost", "Keçiborlu", "Senirkent", "Sütçüler", "Gönen", "Uluborlu", "Atabey", "Aksu", "Yenişarbademli" } }, // Isparta
                { 33, new[] { "Tarsus", "Toroslar", "Yenişehir", "Akdeniz", "Mezitli", "Erdemli", "Silifke", "Mut", "Anamur", "Bozyazı", "Aydıncık", "Gülnar", "Çamlıyayla" } }, // Mersin
                { 34, new[] { "Esenyurt", "Küçükçekmece", "Bağcılar", "Ümraniye", "Pendik", "Bahçelievler", "Sultangazi", "Maltepe", "Üsküdar", "Gaziosmanpaşa", "Kadıköy", "Kartal", "Başakşehir", "Esenler", "Avcılar", "Kağıthane", "Fatih", "Sancaktepe", "Ataşehir", "Eyüpsultan", "Beylikdüzü", "Sarıyer", "Sultanbeyli", "Zeytinburnu", "Güngören", "Arnavutköy", "Şişli", "Bayrampaşa", "Tuzla", "Çekmeköy", "Büyükçekmece", "Beykoz", "Beyoğlu", "Bakırköy", "Silivri", "Beşiktaş", "Çatalca", "Şile", "Adalar" } }, // İstanbul
                { 35, new[] { "Buca", "Karabağlar", "Bornova", "Konak", "Karşıyaka", "Bayraklı", "Çiğli", "Torbalı", "Menemen", "Gaziemir", "Ödemiş", "Kemalpaşa", "Bergama", "Aliağa", "Menderes", "Tire", "Balçova", "Narlıdere", "Urla", "Kiraz", "Dikili", "Çeşme", "Bayındır", "Seferihisar", "Selçuk", "Güzelbahçe", "Foça", "Kınık", "Beydağ", "Karaburun" } }, // İzmir
                { 36, new[] { "Kars Merkez", "Kağızman", "Sarıkamış", "Selim", "Digor", "Arpaçay", "Akyaka", "Susuz" } }, // Kars
                { 37, new[] { "Kastamonu Merkez", "Tosya", "Taşköprü", "Cide", "İnebolu", "Araç", "Devrekani", "Bozkurt", "Daday", "Azdavay", "Çatalzeytin", "Küre", "Doğanyurt", "İhsangazi", "Pınarbaşı", "Şenpazar", "Abana", "Seydiler", "Hanönü", "Ağlı" } }, // Kastamonu
                { 38, new[] { "Melikgazi", "Kocasinan", "Talas", "Develi", "Yahyalı", "Bünyan", "Pınarbaşı", "Tomarza", "İncesu", "Yeşilhisar", "Sarıoğlan", "Hacılar", "Sarız", "Akkışla", "Felahiye", "Özvatan" } }, // Kayseri
                { 39, new[] { "Lüleburgaz", "Kırklareli Merkez", "Babaeski", "Vize", "Pınarhisar", "Demirköy", "Pehlivanköy", "Kofçaz" } }, // Kırklareli
                { 40, new[] { "Kırşehir Merkez", "Kaman", "Mucur", "Çiçekdağı", "Akpınar", "Boztepe", "Akçakent" } }, // Kırşehir
                { 41, new[] { "Gebze", "İzmit", "Darıca", "Körfez", "Gölcük", "Derince", "Çayırova", "Kartepe", "Başiskele", "Karamürsel", "Kandıra", "Dilovası" } }, // Kocaeli
                { 42, new[] { "Selçuklu", "Meram", "Karatay", "Ereğli", "Akşehir", "Beyşehir", "Çumra", "Seydişehir", "Ilgın", "Cihanbeyli", "Kulu", "Karapınar", "Kadınhanı", "Sarayönü", "Bozkır", "Yunak", "Doğanhisar", "Hüyük", "Altınekin", "Hadim", "Çeltik", "Güneysınır", "Emirgazi", "Taşkent", "Tuzlukçu", "Derebucak", "Akören", "Ahırlı", "Derbent", "Halkapınar", "Yalıhüyük" } }, // Konya
                { 43, new[] { "Kütahya Merkez", "Tavşanlı", "Simav", "Gediz", "Emet", "Altıntaş", "Domaniç", "Hisarcık", "Aslanapa", "Çavdarhisar", "Şaphane", "Pazarlar", "Dumlupınar" } }, // Kütahya
                { 44, new[] { "Battalgazi", "Yeşilyurt", "Doğanşehir", "Akçadağ", "Darende", "Hekimhan", "Yazıhan", "Pütürge", "Arapgir", "Kuluncak", "Arguvan", "Kale", "Doğanyol" } }, // Malatya
                { 45, new[] { "Yunusemre", "Şehzadeler", "Akhisar", "Turgutlu", "Salihli", "Soma", "Alaşehir", "Saruhanlı", "Kula", "Kırkağaç", "Demirci", "Sarıgöl", "Gördes", "Selendi", "Ahmetli", "Gölmarmara", "Köprübaşı" } }, // Manisa
                { 46, new[] { "Onikişubat", "Dulkadiroğlu", "Elbistan", "Afşin", "Türkoğlu", "Pazarcık", "Göksun", "Andırın", "Çağlayancerit", "Nurhak", "Ekinözü" } }, // Kahramanmaraş
                { 47, new[] { "Kızıltepe", "Artuklu", "Midyat", "Nusaybin", "Derik", "Mazıdağı", "Dargeçit", "Savur", "Yeşilli", "Ömerli" } }, // Mardin
                { 48, new[] { "Bodrum", "Fethiye", "Milas", "Menteşe", "Marmaris", "Seydikemer", "Ortaca", "Dalaman", "Yatağan", "Köyceğiz", "Ula", "Datça", "Kavaklıdere" } }, // Muğla
                { 49, new[] { "Muş Merkez", "Bulanık", "Malazgirt", "Varto", "Hasköy", "Korkut" } }, // Muş
                { 50, new[] { "Nevşehir Merkez", "Ürgüp", "Avanos", "Gülşehir", "Derinkuyu", "Acıgöl", "Kozaklı", "Hacıbektaş" } }, // Nevşehir
                { 51, new[] { "Niğde Merkez", "Bor", "Çiftlik", "Ulukışla", "Altunhisar", "Çamardı" } }, // Niğde
                { 52, new[] { "Altınordu", "Ünye", "Fatsa", "Perşembe", "Kumru", "Korgan", "Gölköy", "Ulubey", "Gülyalı", "Mesudiye", "Aybastı", "İkizce", "Akkuş", "Gürgentepe", "Çatalpınar", "Çaybaşı", "Kabataş", "Kabadüz", "Çamaş" } }, // Ordu
                { 53, new[] { "Rize Merkez", "Çayeli", "Ardeşen", "Pazar", "Fındıklı", "Güneysu", "Kalkandere", "İyidere", "Derepazarı", "Çamlıhemşin", "İkizdere", "Hemşin" } }, // Rize
                { 54, new[] { "Adapazarı", "Serdivan", "Akyazı", "Erenler", "Hendek", "Karasu", "Geyve", "Arifiye", "Sapanca", "Pamukova", "Ferizli", "Kaynarca", "Kocaali", "Söğütlü", "Karapürçek", "Taraklı" } }, // Sakarya
                { 55, new[] { "İlkadım", "Atakum", "Bafra", "Çarşamba", "Canik", "Vezirköprü", "Terme", "Tekkeköy", "Havza", "Alaçam", "19 Mayıs", "Ayvacık", "Kavak", "Salıpazarı", "Asarcık", "Ladik", "Yakakent" } }, // Samsun
                { 56, new[] { "Siirt Merkez", "Kurtalan", "Pervari", "Baykan", "Şirvan", "Eruh", "Tillo" } }, // Siirt
                { 57, new[] { "Sinop Merkez", "Boyabat", "Gerze", "Ayancık", "Durağan", "Türkeli", "Erfelek", "Dikmen", "Saraydüzü" } }, // Sinop
                { 58, new[] { "Sivas Merkez", "Şarkışla", "Yıldızeli", "Suşehri", "Gemerek", "Zara", "Kangal", "Gürün", "Divriği", "Koyulhisar", "Hafik", "Ulaş", "Altınyayla", "İmranlı", "Akıncılar", "Gölova", "Doğanşar" } }, // Sivas
                { 59, new[] { "Çorlu", "Süleymanpaşa", "Çerkezköy", "Kapaklı", "Ergene", "Malkara", "Saray", "Hayrabolu", "Şarköy", "Muratlı", "Marmaraereğlisi" } }, // Tekirdağ
                { 60, new[] { "Tokat Merkez", "Erbaa", "Turhal", "Niksar", "Zile", "Reşadiye", "Almus", "Pazar", "Yeşilyurt", "Artova", "Sulusaray", "Başçiftlik" } }, // Tokat
                { 61, new[] { "Ortahisar", "Akçaabat", "Araklı", "Of", "Yomra", "Arsin", "Vakfıkebir", "Sürmene", "Maçka", "Beşikdüzü", "Çarşıbaşı", "Tonya", "Düzköy", "Çaykara", "Şalpazarı", "Hayrat", "Köprübaşı", "Dernekpazarı" } }, // Trabzon
                { 62, new[] { "Tunceli Merkez", "Pertek", "Mazgirt", "Çemişgezek", "Hozat", "Ovacık", "Pülümür", "Nazımiye" } }, // Tunceli
                { 63, new[] { "Eyyübiye", "Haliliye", "Siverek", "Viranşehir", "Karaköprü", "Akçakale", "Suruç", "Birecik", "Harran", "Ceylanpınar", "Bozova", "Hilvan", "Halfeti" } }, // Şanlıurfa
                { 64, new[] { "Uşak Merkez", "Banaz", "Eşme", "Sivaslı", "Ulubey", "Karahallı" } }, // Uşak
                { 65, new[] { "İpekyolu", "Erciş", "Tuşba", "Edremit", "Özalp", "Çaldıran", "Başkale", "Muradiye", "Gürpınar", "Gevaş", "Saray", "Çatak", "Bahçesaray" } }, // Van
                { 66, new[] { "Yozgat Merkez", "Sorgun", "Akdağmadeni", "Yerköy", "Boğazlıyan", "Sarıkaya", "Çekerek", "Şefaatli", "Saraykent", "Çayıralan", "Kadışehri", "Aydıncık", "Yenifakılı", "Çandır" } }, // Yozgat
                { 67, new[] { "Ereğli", "Zonguldak Merkez", "Çaycuma", "Devrek", "Kozlu", "Alaplı", "Kilimli", "Gökçebey" } }, // Zonguldak
                { 68, new[] { "Aksaray Merkez", "Ortaköy", "Eskil", "Gülağaç", "Güzelyurt", "Sultanhanı", "Ağaçören", "Sarıyahşi" } }, // Aksaray
                { 69, new[] { "Bayburt Merkez", "Demirözü", "Aydıntepe" } }, // Bayburt
                { 70, new[] { "Karaman Merkez", "Ermenek", "Sarıveliler", "Ayrancı", "Başyayla", "Kazımkarabekir" } }, // Karaman
                { 71, new[] { "Kırıkkale Merkez", "Yahşihan", "Keskin", "Delice", "Bahşılı", "Sulakyurt", "Balışeyh", "Karakeçili", "Çelebi" } }, // Kırıkkale
                { 72, new[] { "Batman Merkez", "Kozluk", "Sason", "Beşiri", "Gercüş", "Hasankeyf" } }, // Batman
                { 73, new[] { "Cizre", "Silopi", "Şırnak Merkez", "İdil", "Uludere", "Beytüşşebap", "Güçlükonak" } }, // Şırnak
                { 74, new[] { "Bartın Merkez", "Ulus", "Amasra", "Kurucaşile" } }, // Bartın
                { 75, new[] { "Ardahan Merkez", "Göle", "Çıldır", "Hanak", "Posof", "Damal" } }, // Ardahan
                { 76, new[] { "Iğdır Merkez", "Tuzluca", "Aralık", "Karakoyunlu" } }, // Iğdır
                { 77, new[] { "Yalova Merkez", "Çiftlikköy", "Çınarcık", "Altınova", "Armutlu", "Termal" } }, // Yalova
                { 78, new[] { "Karabük Merkez", "Safranbolu", "Yenice", "Eskipazar", "Eflani", "Ovacık" } }, // Karabük
                { 79, new[] { "Kilis Merkez", "Musabeyli", "Elbeyli", "Polateli" } }, // Kilis
                { 80, new[] { "Osmaniye Merkez", "Kadirli", "Düziçi", "Bahçe", "Toprakkale", "Sumbas", "Hasanbeyli" } }, // Osmaniye
                { 81, new[] { "Düzce Merkez", "Akçakoca", "Kaynaşlı", "Gölyaka", "Çilimli", "Yığılca", "Gümüşova", "Cumayeri" } } // Düzce
            };

            foreach (var kvp in districtsByPlate)
            {
                int ilId = kvp.Key;
                foreach (string districtName in kvp.Value)
                {
                    ilceList.Add(new Ilce
                    {
                        IlceId = idCounter++,
                        IlId = ilId,
                        Ad = districtName
                    });
                }
            }

            return ilceList;
        }
    }
}
