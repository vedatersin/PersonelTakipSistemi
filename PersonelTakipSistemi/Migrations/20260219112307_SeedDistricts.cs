using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDistricts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5230));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5237));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5243));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 498, DateTimeKind.Local).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 1,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Seyhan", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 2,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Yüreğir", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 3,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Çukurova", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 4,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Sarıçam", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 5,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Ceyhan", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 6,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Kozan", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 7,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "İmamoğlu", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 8,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Karataş", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 9,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Karaisalı", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 10,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Pozantı", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 11,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Yumurtalık", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 12,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Tufanbeyli", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 13,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Feke", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 14,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Aladağ", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 15,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Saimbeyli", 1 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 16,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Adıyaman Merkez", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 17,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Kahta", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 18,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Besni", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 19,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Gölbaşı", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 20,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Gerger", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 21,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Sincik", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 22,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Çelikhan", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 23,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Tut", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 24,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Samsat", 2 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 25,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Afyonkarahisar Merkez", 3 });

            migrationBuilder.InsertData(
                table: "Ilceler",
                columns: new[] { "IlceId", "Ad", "IlId" },
                values: new object[,]
                {
                    { 26, "Sandıklı", 3 },
                    { 27, "Dinar", 3 },
                    { 28, "Bolvadin", 3 },
                    { 29, "Sinanpaşa", 3 },
                    { 30, "Emirdağ", 3 },
                    { 31, "Şuhut", 3 },
                    { 32, "Çay", 3 },
                    { 33, "İhsaniye", 3 },
                    { 34, "İscehisar", 3 },
                    { 35, "Sultandağı", 3 },
                    { 36, "Çobanlar", 3 },
                    { 37, "Dazkırı", 3 },
                    { 38, "Başmakçı", 3 },
                    { 39, "Hocalar", 3 },
                    { 40, "Bayat", 3 },
                    { 41, "Evciler", 3 },
                    { 42, "Kızılören", 3 },
                    { 43, "Ağrı Merkez", 4 },
                    { 44, "Patnos", 4 },
                    { 45, "Doğubayazıt", 4 },
                    { 46, "Diyadin", 4 },
                    { 47, "Eleşkirt", 4 },
                    { 48, "Tutak", 4 },
                    { 49, "Taşlıçay", 4 },
                    { 50, "Hamur", 4 },
                    { 51, "Amasya Merkez", 5 },
                    { 52, "Merzifon", 5 },
                    { 53, "Suluova", 5 },
                    { 54, "Taşova", 5 },
                    { 55, "Gümüşhacıköy", 5 },
                    { 56, "Göynücek", 5 },
                    { 57, "Hamamözü", 5 },
                    { 58, "Çankaya", 6 },
                    { 59, "Keçiören", 6 },
                    { 60, "Yenimahalle", 6 },
                    { 61, "Mamak", 6 },
                    { 62, "Etimesgut", 6 },
                    { 63, "Sincan", 6 },
                    { 64, "Altındağ", 6 },
                    { 65, "Pursaklar", 6 },
                    { 66, "Gölbaşı", 6 },
                    { 67, "Polatlı", 6 },
                    { 68, "Çubuk", 6 },
                    { 69, "Kahramankazan", 6 },
                    { 70, "Beypazarı", 6 },
                    { 71, "Elmadağ", 6 },
                    { 72, "Şereflikoçhisar", 6 },
                    { 73, "Akyurt", 6 },
                    { 74, "Nallıhan", 6 },
                    { 75, "Haymana", 6 },
                    { 76, "Kızılcahamam", 6 },
                    { 77, "Bala", 6 },
                    { 78, "Kalecik", 6 },
                    { 79, "Ayaş", 6 },
                    { 80, "Güdül", 6 },
                    { 81, "Çamlıdere", 6 },
                    { 82, "Evren", 6 },
                    { 83, "Kepez", 7 },
                    { 84, "Muratpaşa", 7 },
                    { 85, "Alanya", 7 },
                    { 86, "Manavgat", 7 },
                    { 87, "Konyaaltı", 7 },
                    { 88, "Serik", 7 },
                    { 89, "Aksu", 7 },
                    { 90, "Kumluca", 7 },
                    { 91, "Döşemealtı", 7 },
                    { 92, "Kaş", 7 },
                    { 93, "Korkuteli", 7 },
                    { 94, "Gazipaşa", 7 },
                    { 95, "Finike", 7 },
                    { 96, "Kemer", 7 },
                    { 97, "Elmalı", 7 },
                    { 98, "Demre", 7 },
                    { 99, "Akseki", 7 },
                    { 100, "Gündoğmuş", 7 },
                    { 101, "İbradı", 7 },
                    { 102, "Artvin Merkez", 8 },
                    { 103, "Hopa", 8 },
                    { 104, "Borçka", 8 },
                    { 105, "Yusufeli", 8 },
                    { 106, "Arhavi", 8 },
                    { 107, "Şavşat", 8 },
                    { 108, "Ardanuç", 8 },
                    { 109, "Murgul", 8 },
                    { 110, "Kemalpaşa", 8 },
                    { 111, "Efeler", 9 },
                    { 112, "Nazilli", 9 },
                    { 113, "Söke", 9 },
                    { 114, "Kuşadası", 9 },
                    { 115, "Didim", 9 },
                    { 116, "İncirliova", 9 },
                    { 117, "Çine", 9 },
                    { 118, "Germencik", 9 },
                    { 119, "Bozdoğan", 9 },
                    { 120, "Köşk", 9 },
                    { 121, "Kuyucak", 9 },
                    { 122, "Sultanhisar", 9 },
                    { 123, "Karacasu", 9 },
                    { 124, "Buharkent", 9 },
                    { 125, "Yenipazar", 9 },
                    { 126, "Karpuzlu", 9 },
                    { 127, "Altıeylül", 10 },
                    { 128, "Karesi", 10 },
                    { 129, "Edremit", 10 },
                    { 130, "Bandırma", 10 },
                    { 131, "Gönen", 10 },
                    { 132, "Ayvalık", 10 },
                    { 133, "Burhaniye", 10 },
                    { 134, "Bigadiç", 10 },
                    { 135, "Susurluk", 10 },
                    { 136, "Dursunbey", 10 },
                    { 137, "Sındırgı", 10 },
                    { 138, "İvrindi", 10 },
                    { 139, "Erdek", 10 },
                    { 140, "Havran", 10 },
                    { 141, "Kepsut", 10 },
                    { 142, "Manyas", 10 },
                    { 143, "Savaştepe", 10 },
                    { 144, "Balya", 10 },
                    { 145, "Gömeç", 10 },
                    { 146, "Marmara", 10 },
                    { 147, "Bilecik Merkez", 11 },
                    { 148, "Bozüyük", 11 },
                    { 149, "Osmaneli", 11 },
                    { 150, "Söğüt", 11 },
                    { 151, "Gölpazarı", 11 },
                    { 152, "Pazaryeri", 11 },
                    { 153, "İnhisar", 11 },
                    { 154, "Yenipazar", 11 },
                    { 155, "Bingöl Merkez", 12 },
                    { 156, "Genç", 12 },
                    { 157, "Solhan", 12 },
                    { 158, "Karlıova", 12 },
                    { 159, "Adaklı", 12 },
                    { 160, "Kiğı", 12 },
                    { 161, "Yedisu", 12 },
                    { 162, "Yayladere", 12 },
                    { 163, "Tatvan", 13 },
                    { 164, "Bitlis Merkez", 13 },
                    { 165, "Güroymak", 13 },
                    { 166, "Ahlat", 13 },
                    { 167, "Hizan", 13 },
                    { 168, "Mutki", 13 },
                    { 169, "Adilcevaz", 13 },
                    { 170, "Bolu Merkez", 14 },
                    { 171, "Gerede", 14 },
                    { 172, "Mudurnu", 14 },
                    { 173, "Göynük", 14 },
                    { 174, "Mengen", 14 },
                    { 175, "Yeniçağa", 14 },
                    { 176, "Dörtdivan", 14 },
                    { 177, "Seben", 14 },
                    { 178, "Kıbrıscık", 14 },
                    { 179, "Burdur Merkez", 15 },
                    { 180, "Bucak", 15 },
                    { 181, "Gölhisar", 15 },
                    { 182, "Yeşilova", 15 },
                    { 183, "Çavdır", 15 },
                    { 184, "Tefenni", 15 },
                    { 185, "Ağlasun", 15 },
                    { 186, "Karamanlı", 15 },
                    { 187, "Altınyayla", 15 },
                    { 188, "Çeltikçi", 15 },
                    { 189, "Kemer", 15 },
                    { 190, "Osmangazi", 16 },
                    { 191, "Yıldırım", 16 },
                    { 192, "Nilüfer", 16 },
                    { 193, "İnegöl", 16 },
                    { 194, "Gemlik", 16 },
                    { 195, "Mustafakemalpaşa", 16 },
                    { 196, "Mudanya", 16 },
                    { 197, "Gürsu", 16 },
                    { 198, "Karacabey", 16 },
                    { 199, "Orhangazi", 16 },
                    { 200, "Kestel", 16 },
                    { 201, "Yenişehir", 16 },
                    { 202, "İznik", 16 },
                    { 203, "Orhaneli", 16 },
                    { 204, "Keles", 16 },
                    { 205, "Büyükorhan", 16 },
                    { 206, "Harmancık", 16 },
                    { 207, "Çanakkale Merkez", 17 },
                    { 208, "Biga", 17 },
                    { 209, "Çan", 17 },
                    { 210, "Gelibolu", 17 },
                    { 211, "Ayvacık", 17 },
                    { 212, "Yenice", 17 },
                    { 213, "Ezine", 17 },
                    { 214, "Bayramiç", 17 },
                    { 215, "Lapseki", 17 },
                    { 216, "Eceabat", 17 },
                    { 217, "Gökçeada", 17 },
                    { 218, "Bozcaada", 17 },
                    { 219, "Çankırı Merkez", 18 },
                    { 220, "Çerkeş", 18 },
                    { 221, "Ilgaz", 18 },
                    { 222, "Orta", 18 },
                    { 223, "Şabanözü", 18 },
                    { 224, "Kurşunlu", 18 },
                    { 225, "Yapraklı", 18 },
                    { 226, "Kızılırmak", 18 },
                    { 227, "Eldivan", 18 },
                    { 228, "Atkaracalar", 18 },
                    { 229, "Korgun", 18 },
                    { 230, "Bayramören", 18 },
                    { 231, "Çorum Merkez", 19 },
                    { 232, "Sungurlu", 19 },
                    { 233, "Osmancık", 19 },
                    { 234, "İskilip", 19 },
                    { 235, "Alaca", 19 },
                    { 236, "Bayat", 19 },
                    { 237, "Mecitözü", 19 },
                    { 238, "Kargı", 19 },
                    { 239, "Ortaköy", 19 },
                    { 240, "Uğurludağ", 19 },
                    { 241, "Dodurga", 19 },
                    { 242, "Oğuzlar", 19 },
                    { 243, "Laçin", 19 },
                    { 244, "Boğazkale", 19 },
                    { 245, "Pamukkale", 20 },
                    { 246, "Merkezefendi", 20 },
                    { 247, "Çivril", 20 },
                    { 248, "Acıpayam", 20 },
                    { 249, "Tavas", 20 },
                    { 250, "Honaz", 20 },
                    { 251, "Sarayköy", 20 },
                    { 252, "Buldan", 20 },
                    { 253, "Kale", 20 },
                    { 254, "Çal", 20 },
                    { 255, "Serinhisar", 20 },
                    { 256, "Çameli", 20 },
                    { 257, "Bozkurt", 20 },
                    { 258, "Güney", 20 },
                    { 259, "Çardak", 20 },
                    { 260, "Bekilli", 20 },
                    { 261, "Beyağaç", 20 },
                    { 262, "Babadağ", 20 },
                    { 263, "Baklan", 20 },
                    { 264, "Bağlar", 21 },
                    { 265, "Kayapınar", 21 },
                    { 266, "Yenişehir", 21 },
                    { 267, "Sur", 21 },
                    { 268, "Ergani", 21 },
                    { 269, "Bismil", 21 },
                    { 270, "Silvan", 21 },
                    { 271, "Çınar", 21 },
                    { 272, "Çermik", 21 },
                    { 273, "Dicle", 21 },
                    { 274, "Kulp", 21 },
                    { 275, "Hani", 21 },
                    { 276, "Lice", 21 },
                    { 277, "Eğil", 21 },
                    { 278, "Hazro", 21 },
                    { 279, "Kocaköy", 21 },
                    { 280, "Çüngüş", 21 },
                    { 281, "Edirne Merkez", 22 },
                    { 282, "Keşan", 22 },
                    { 283, "Uzunköprü", 22 },
                    { 284, "İpsala", 22 },
                    { 285, "Havsa", 22 },
                    { 286, "Meriç", 22 },
                    { 287, "Enez", 22 },
                    { 288, "Süloğlu", 22 },
                    { 289, "Lalapaşa", 22 },
                    { 290, "Elazığ Merkez", 23 },
                    { 291, "Kovancılar", 23 },
                    { 292, "Karakoçan", 23 },
                    { 293, "Palu", 23 },
                    { 294, "Arıcak", 23 },
                    { 295, "Baskil", 23 },
                    { 296, "Maden", 23 },
                    { 297, "Sivrice", 23 },
                    { 298, "Alacakaya", 23 },
                    { 299, "Keban", 23 },
                    { 300, "Ağın", 23 },
                    { 301, "Erzincan Merkez", 24 },
                    { 302, "Tercan", 24 },
                    { 303, "Üzümlü", 24 },
                    { 304, "Çayırlı", 24 },
                    { 305, "İliç", 24 },
                    { 306, "Kemah", 24 },
                    { 307, "Kemaliye", 24 },
                    { 308, "Otlukbeli", 24 },
                    { 309, "Refahiye", 24 },
                    { 310, "Yakutiye", 25 },
                    { 311, "Palandöken", 25 },
                    { 312, "Aziziye", 25 },
                    { 313, "Horasan", 25 },
                    { 314, "Oltu", 25 },
                    { 315, "Pasinler", 25 },
                    { 316, "Karayazı", 25 },
                    { 317, "Hınıs", 25 },
                    { 318, "Tekman", 25 },
                    { 319, "Karaçoban", 25 },
                    { 320, "Aşkale", 25 },
                    { 321, "Şenkaya", 25 },
                    { 322, "Çat", 25 },
                    { 323, "Köprüköy", 25 },
                    { 324, "İspir", 25 },
                    { 325, "Tortum", 25 },
                    { 326, "Narman", 25 },
                    { 327, "Uzundere", 25 },
                    { 328, "Olur", 25 },
                    { 329, "Pazaryolu", 25 },
                    { 330, "Odunpazarı", 26 },
                    { 331, "Tepebaşı", 26 },
                    { 332, "Sivrihisar", 26 },
                    { 333, "Çifteler", 26 },
                    { 334, "Seyitgazi", 26 },
                    { 335, "Alpu", 26 },
                    { 336, "Mihalıççık", 26 },
                    { 337, "Mahmudiye", 26 },
                    { 338, "Beylikova", 26 },
                    { 339, "İnönü", 26 },
                    { 340, "Günyüzü", 26 },
                    { 341, "Han", 26 },
                    { 342, "Mihalgazi", 26 },
                    { 343, "Sarıcakaya", 26 },
                    { 344, "Şahinbey", 27 },
                    { 345, "Şehitkamil", 27 },
                    { 346, "Nizip", 27 },
                    { 347, "İslahiye", 27 },
                    { 348, "Nurdağı", 27 },
                    { 349, "Oğuzeli", 27 },
                    { 350, "Araban", 27 },
                    { 351, "Yavuzeli", 27 },
                    { 352, "Karkamış", 27 },
                    { 353, "Giresun Merkez", 28 },
                    { 354, "Bulancak", 28 },
                    { 355, "Espiye", 28 },
                    { 356, "Görele", 28 },
                    { 357, "Tirebolu", 28 },
                    { 358, "Dereli", 28 },
                    { 359, "Şebinkarahisar", 28 },
                    { 360, "Keşap", 28 },
                    { 361, "Yağlıdere", 28 },
                    { 362, "Piraziz", 28 },
                    { 363, "Eynesil", 28 },
                    { 364, "Alucra", 28 },
                    { 365, "Çamoluk", 28 },
                    { 366, "Güce", 28 },
                    { 367, "Doğankent", 28 },
                    { 368, "Çanakçı", 28 },
                    { 369, "Gümüşhane Merkez", 29 },
                    { 370, "Kelkit", 29 },
                    { 371, "Şiran", 29 },
                    { 372, "Kürtün", 29 },
                    { 373, "Torul", 29 },
                    { 374, "Köse", 29 },
                    { 375, "Yüksekova", 30 },
                    { 376, "Hakkari Merkez", 30 },
                    { 377, "Şemdinli", 30 },
                    { 378, "Çukurca", 30 },
                    { 379, "Derecik", 30 },
                    { 380, "Antakya", 31 },
                    { 381, "İskenderun", 31 },
                    { 382, "Defne", 31 },
                    { 383, "Dörtyol", 31 },
                    { 384, "Samandağ", 31 },
                    { 385, "Kırıkhan", 31 },
                    { 386, "Reyhanlı", 31 },
                    { 387, "Arsuz", 31 },
                    { 388, "Altınözü", 31 },
                    { 389, "Hassa", 31 },
                    { 390, "Payas", 31 },
                    { 391, "Erzin", 31 },
                    { 392, "Yayladağı", 31 },
                    { 393, "Belen", 31 },
                    { 394, "Kumlu", 31 },
                    { 395, "Isparta Merkez", 32 },
                    { 396, "Yalvaç", 32 },
                    { 397, "Eğirdir", 32 },
                    { 398, "Şarkikaraağaç", 32 },
                    { 399, "Gelendost", 32 },
                    { 400, "Keçiborlu", 32 },
                    { 401, "Senirkent", 32 },
                    { 402, "Sütçüler", 32 },
                    { 403, "Gönen", 32 },
                    { 404, "Uluborlu", 32 },
                    { 405, "Atabey", 32 },
                    { 406, "Aksu", 32 },
                    { 407, "Yenişarbademli", 32 },
                    { 408, "Tarsus", 33 },
                    { 409, "Toroslar", 33 },
                    { 410, "Yenişehir", 33 },
                    { 411, "Akdeniz", 33 },
                    { 412, "Mezitli", 33 },
                    { 413, "Erdemli", 33 },
                    { 414, "Silifke", 33 },
                    { 415, "Mut", 33 },
                    { 416, "Anamur", 33 },
                    { 417, "Bozyazı", 33 },
                    { 418, "Aydıncık", 33 },
                    { 419, "Gülnar", 33 },
                    { 420, "Çamlıyayla", 33 },
                    { 421, "Esenyurt", 34 },
                    { 422, "Küçükçekmece", 34 },
                    { 423, "Bağcılar", 34 },
                    { 424, "Ümraniye", 34 },
                    { 425, "Pendik", 34 },
                    { 426, "Bahçelievler", 34 },
                    { 427, "Sultangazi", 34 },
                    { 428, "Maltepe", 34 },
                    { 429, "Üsküdar", 34 },
                    { 430, "Gaziosmanpaşa", 34 },
                    { 431, "Kadıköy", 34 },
                    { 432, "Kartal", 34 },
                    { 433, "Başakşehir", 34 },
                    { 434, "Esenler", 34 },
                    { 435, "Avcılar", 34 },
                    { 436, "Kağıthane", 34 },
                    { 437, "Fatih", 34 },
                    { 438, "Sancaktepe", 34 },
                    { 439, "Ataşehir", 34 },
                    { 440, "Eyüpsultan", 34 },
                    { 441, "Beylikdüzü", 34 },
                    { 442, "Sarıyer", 34 },
                    { 443, "Sultanbeyli", 34 },
                    { 444, "Zeytinburnu", 34 },
                    { 445, "Güngören", 34 },
                    { 446, "Arnavutköy", 34 },
                    { 447, "Şişli", 34 },
                    { 448, "Bayrampaşa", 34 },
                    { 449, "Tuzla", 34 },
                    { 450, "Çekmeköy", 34 },
                    { 451, "Büyükçekmece", 34 },
                    { 452, "Beykoz", 34 },
                    { 453, "Beyoğlu", 34 },
                    { 454, "Bakırköy", 34 },
                    { 455, "Silivri", 34 },
                    { 456, "Beşiktaş", 34 },
                    { 457, "Çatalca", 34 },
                    { 458, "Şile", 34 },
                    { 459, "Adalar", 34 },
                    { 460, "Buca", 35 },
                    { 461, "Karabağlar", 35 },
                    { 462, "Bornova", 35 },
                    { 463, "Konak", 35 },
                    { 464, "Karşıyaka", 35 },
                    { 465, "Bayraklı", 35 },
                    { 466, "Çiğli", 35 },
                    { 467, "Torbalı", 35 },
                    { 468, "Menemen", 35 },
                    { 469, "Gaziemir", 35 },
                    { 470, "Ödemiş", 35 },
                    { 471, "Kemalpaşa", 35 },
                    { 472, "Bergama", 35 },
                    { 473, "Aliağa", 35 },
                    { 474, "Menderes", 35 },
                    { 475, "Tire", 35 },
                    { 476, "Balçova", 35 },
                    { 477, "Narlıdere", 35 },
                    { 478, "Urla", 35 },
                    { 479, "Kiraz", 35 },
                    { 480, "Dikili", 35 },
                    { 481, "Çeşme", 35 },
                    { 482, "Bayındır", 35 },
                    { 483, "Seferihisar", 35 },
                    { 484, "Selçuk", 35 },
                    { 485, "Güzelbahçe", 35 },
                    { 486, "Foça", 35 },
                    { 487, "Kınık", 35 },
                    { 488, "Beydağ", 35 },
                    { 489, "Karaburun", 35 },
                    { 490, "Kars Merkez", 36 },
                    { 491, "Kağızman", 36 },
                    { 492, "Sarıkamış", 36 },
                    { 493, "Selim", 36 },
                    { 494, "Digor", 36 },
                    { 495, "Arpaçay", 36 },
                    { 496, "Akyaka", 36 },
                    { 497, "Susuz", 36 },
                    { 498, "Kastamonu Merkez", 37 },
                    { 499, "Tosya", 37 },
                    { 500, "Taşköprü", 37 },
                    { 501, "Cide", 37 },
                    { 502, "İnebolu", 37 },
                    { 503, "Araç", 37 },
                    { 504, "Devrekani", 37 },
                    { 505, "Bozkurt", 37 },
                    { 506, "Daday", 37 },
                    { 507, "Azdavay", 37 },
                    { 508, "Çatalzeytin", 37 },
                    { 509, "Küre", 37 },
                    { 510, "Doğanyurt", 37 },
                    { 511, "İhsangazi", 37 },
                    { 512, "Pınarbaşı", 37 },
                    { 513, "Şenpazar", 37 },
                    { 514, "Abana", 37 },
                    { 515, "Seydiler", 37 },
                    { 516, "Hanönü", 37 },
                    { 517, "Ağlı", 37 },
                    { 518, "Melikgazi", 38 },
                    { 519, "Kocasinan", 38 },
                    { 520, "Talas", 38 },
                    { 521, "Develi", 38 },
                    { 522, "Yahyalı", 38 },
                    { 523, "Bünyan", 38 },
                    { 524, "Pınarbaşı", 38 },
                    { 525, "Tomarza", 38 },
                    { 526, "İncesu", 38 },
                    { 527, "Yeşilhisar", 38 },
                    { 528, "Sarıoğlan", 38 },
                    { 529, "Hacılar", 38 },
                    { 530, "Sarız", 38 },
                    { 531, "Akkışla", 38 },
                    { 532, "Felahiye", 38 },
                    { 533, "Özvatan", 38 },
                    { 534, "Lüleburgaz", 39 },
                    { 535, "Kırklareli Merkez", 39 },
                    { 536, "Babaeski", 39 },
                    { 537, "Vize", 39 },
                    { 538, "Pınarhisar", 39 },
                    { 539, "Demirköy", 39 },
                    { 540, "Pehlivanköy", 39 },
                    { 541, "Kofçaz", 39 },
                    { 542, "Kırşehir Merkez", 40 },
                    { 543, "Kaman", 40 },
                    { 544, "Mucur", 40 },
                    { 545, "Çiçekdağı", 40 },
                    { 546, "Akpınar", 40 },
                    { 547, "Boztepe", 40 },
                    { 548, "Akçakent", 40 },
                    { 549, "Gebze", 41 },
                    { 550, "İzmit", 41 },
                    { 551, "Darıca", 41 },
                    { 552, "Körfez", 41 },
                    { 553, "Gölcük", 41 },
                    { 554, "Derince", 41 },
                    { 555, "Çayırova", 41 },
                    { 556, "Kartepe", 41 },
                    { 557, "Başiskele", 41 },
                    { 558, "Karamürsel", 41 },
                    { 559, "Kandıra", 41 },
                    { 560, "Dilovası", 41 },
                    { 561, "Selçuklu", 42 },
                    { 562, "Meram", 42 },
                    { 563, "Karatay", 42 },
                    { 564, "Ereğli", 42 },
                    { 565, "Akşehir", 42 },
                    { 566, "Beyşehir", 42 },
                    { 567, "Çumra", 42 },
                    { 568, "Seydişehir", 42 },
                    { 569, "Ilgın", 42 },
                    { 570, "Cihanbeyli", 42 },
                    { 571, "Kulu", 42 },
                    { 572, "Karapınar", 42 },
                    { 573, "Kadınhanı", 42 },
                    { 574, "Sarayönü", 42 },
                    { 575, "Bozkır", 42 },
                    { 576, "Yunak", 42 },
                    { 577, "Doğanhisar", 42 },
                    { 578, "Hüyük", 42 },
                    { 579, "Altınekin", 42 },
                    { 580, "Hadim", 42 },
                    { 581, "Çeltik", 42 },
                    { 582, "Güneysınır", 42 },
                    { 583, "Emirgazi", 42 },
                    { 584, "Taşkent", 42 },
                    { 585, "Tuzlukçu", 42 },
                    { 586, "Derebucak", 42 },
                    { 587, "Akören", 42 },
                    { 588, "Ahırlı", 42 },
                    { 589, "Derbent", 42 },
                    { 590, "Halkapınar", 42 },
                    { 591, "Yalıhüyük", 42 },
                    { 592, "Kütahya Merkez", 43 },
                    { 593, "Tavşanlı", 43 },
                    { 594, "Simav", 43 },
                    { 595, "Gediz", 43 },
                    { 596, "Emet", 43 },
                    { 597, "Altıntaş", 43 },
                    { 598, "Domaniç", 43 },
                    { 599, "Hisarcık", 43 },
                    { 600, "Aslanapa", 43 },
                    { 601, "Çavdarhisar", 43 },
                    { 602, "Şaphane", 43 },
                    { 603, "Pazarlar", 43 },
                    { 604, "Dumlupınar", 43 },
                    { 605, "Battalgazi", 44 },
                    { 606, "Yeşilyurt", 44 },
                    { 607, "Doğanşehir", 44 },
                    { 608, "Akçadağ", 44 },
                    { 609, "Darende", 44 },
                    { 610, "Hekimhan", 44 },
                    { 611, "Yazıhan", 44 },
                    { 612, "Pütürge", 44 },
                    { 613, "Arapgir", 44 },
                    { 614, "Kuluncak", 44 },
                    { 615, "Arguvan", 44 },
                    { 616, "Kale", 44 },
                    { 617, "Doğanyol", 44 },
                    { 618, "Yunusemre", 45 },
                    { 619, "Şehzadeler", 45 },
                    { 620, "Akhisar", 45 },
                    { 621, "Turgutlu", 45 },
                    { 622, "Salihli", 45 },
                    { 623, "Soma", 45 },
                    { 624, "Alaşehir", 45 },
                    { 625, "Saruhanlı", 45 },
                    { 626, "Kula", 45 },
                    { 627, "Kırkağaç", 45 },
                    { 628, "Demirci", 45 },
                    { 629, "Sarıgöl", 45 },
                    { 630, "Gördes", 45 },
                    { 631, "Selendi", 45 },
                    { 632, "Ahmetli", 45 },
                    { 633, "Gölmarmara", 45 },
                    { 634, "Köprübaşı", 45 },
                    { 635, "Onikişubat", 46 },
                    { 636, "Dulkadiroğlu", 46 },
                    { 637, "Elbistan", 46 },
                    { 638, "Afşin", 46 },
                    { 639, "Türkoğlu", 46 },
                    { 640, "Pazarcık", 46 },
                    { 641, "Göksun", 46 },
                    { 642, "Andırın", 46 },
                    { 643, "Çağlayancerit", 46 },
                    { 644, "Nurhak", 46 },
                    { 645, "Ekinözü", 46 },
                    { 646, "Kızıltepe", 47 },
                    { 647, "Artuklu", 47 },
                    { 648, "Midyat", 47 },
                    { 649, "Nusaybin", 47 },
                    { 650, "Derik", 47 },
                    { 651, "Mazıdağı", 47 },
                    { 652, "Dargeçit", 47 },
                    { 653, "Savur", 47 },
                    { 654, "Yeşilli", 47 },
                    { 655, "Ömerli", 47 },
                    { 656, "Bodrum", 48 },
                    { 657, "Fethiye", 48 },
                    { 658, "Milas", 48 },
                    { 659, "Menteşe", 48 },
                    { 660, "Marmaris", 48 },
                    { 661, "Seydikemer", 48 },
                    { 662, "Ortaca", 48 },
                    { 663, "Dalaman", 48 },
                    { 664, "Yatağan", 48 },
                    { 665, "Köyceğiz", 48 },
                    { 666, "Ula", 48 },
                    { 667, "Datça", 48 },
                    { 668, "Kavaklıdere", 48 },
                    { 669, "Muş Merkez", 49 },
                    { 670, "Bulanık", 49 },
                    { 671, "Malazgirt", 49 },
                    { 672, "Varto", 49 },
                    { 673, "Hasköy", 49 },
                    { 674, "Korkut", 49 },
                    { 675, "Nevşehir Merkez", 50 },
                    { 676, "Ürgüp", 50 },
                    { 677, "Avanos", 50 },
                    { 678, "Gülşehir", 50 },
                    { 679, "Derinkuyu", 50 },
                    { 680, "Acıgöl", 50 },
                    { 681, "Kozaklı", 50 },
                    { 682, "Hacıbektaş", 50 },
                    { 683, "Niğde Merkez", 51 },
                    { 684, "Bor", 51 },
                    { 685, "Çiftlik", 51 },
                    { 686, "Ulukışla", 51 },
                    { 687, "Altunhisar", 51 },
                    { 688, "Çamardı", 51 },
                    { 689, "Altınordu", 52 },
                    { 690, "Ünye", 52 },
                    { 691, "Fatsa", 52 },
                    { 692, "Perşembe", 52 },
                    { 693, "Kumru", 52 },
                    { 694, "Korgan", 52 },
                    { 695, "Gölköy", 52 },
                    { 696, "Ulubey", 52 },
                    { 697, "Gülyalı", 52 },
                    { 698, "Mesudiye", 52 },
                    { 699, "Aybastı", 52 },
                    { 700, "İkizce", 52 },
                    { 701, "Akkuş", 52 },
                    { 702, "Gürgentepe", 52 },
                    { 703, "Çatalpınar", 52 },
                    { 704, "Çaybaşı", 52 },
                    { 705, "Kabataş", 52 },
                    { 706, "Kabadüz", 52 },
                    { 707, "Çamaş", 52 },
                    { 708, "Rize Merkez", 53 },
                    { 709, "Çayeli", 53 },
                    { 710, "Ardeşen", 53 },
                    { 711, "Pazar", 53 },
                    { 712, "Fındıklı", 53 },
                    { 713, "Güneysu", 53 },
                    { 714, "Kalkandere", 53 },
                    { 715, "İyidere", 53 },
                    { 716, "Derepazarı", 53 },
                    { 717, "Çamlıhemşin", 53 },
                    { 718, "İkizdere", 53 },
                    { 719, "Hemşin", 53 },
                    { 720, "Adapazarı", 54 },
                    { 721, "Serdivan", 54 },
                    { 722, "Akyazı", 54 },
                    { 723, "Erenler", 54 },
                    { 724, "Hendek", 54 },
                    { 725, "Karasu", 54 },
                    { 726, "Geyve", 54 },
                    { 727, "Arifiye", 54 },
                    { 728, "Sapanca", 54 },
                    { 729, "Pamukova", 54 },
                    { 730, "Ferizli", 54 },
                    { 731, "Kaynarca", 54 },
                    { 732, "Kocaali", 54 },
                    { 733, "Söğütlü", 54 },
                    { 734, "Karapürçek", 54 },
                    { 735, "Taraklı", 54 },
                    { 736, "İlkadım", 55 },
                    { 737, "Atakum", 55 },
                    { 738, "Bafra", 55 },
                    { 739, "Çarşamba", 55 },
                    { 740, "Canik", 55 },
                    { 741, "Vezirköprü", 55 },
                    { 742, "Terme", 55 },
                    { 743, "Tekkeköy", 55 },
                    { 744, "Havza", 55 },
                    { 745, "Alaçam", 55 },
                    { 746, "19 Mayıs", 55 },
                    { 747, "Ayvacık", 55 },
                    { 748, "Kavak", 55 },
                    { 749, "Salıpazarı", 55 },
                    { 750, "Asarcık", 55 },
                    { 751, "Ladik", 55 },
                    { 752, "Yakakent", 55 },
                    { 753, "Siirt Merkez", 56 },
                    { 754, "Kurtalan", 56 },
                    { 755, "Pervari", 56 },
                    { 756, "Baykan", 56 },
                    { 757, "Şirvan", 56 },
                    { 758, "Eruh", 56 },
                    { 759, "Tillo", 56 },
                    { 760, "Sinop Merkez", 57 },
                    { 761, "Boyabat", 57 },
                    { 762, "Gerze", 57 },
                    { 763, "Ayancık", 57 },
                    { 764, "Durağan", 57 },
                    { 765, "Türkeli", 57 },
                    { 766, "Erfelek", 57 },
                    { 767, "Dikmen", 57 },
                    { 768, "Saraydüzü", 57 },
                    { 769, "Sivas Merkez", 58 },
                    { 770, "Şarkışla", 58 },
                    { 771, "Yıldızeli", 58 },
                    { 772, "Suşehri", 58 },
                    { 773, "Gemerek", 58 },
                    { 774, "Zara", 58 },
                    { 775, "Kangal", 58 },
                    { 776, "Gürün", 58 },
                    { 777, "Divriği", 58 },
                    { 778, "Koyulhisar", 58 },
                    { 779, "Hafik", 58 },
                    { 780, "Ulaş", 58 },
                    { 781, "Altınyayla", 58 },
                    { 782, "İmranlı", 58 },
                    { 783, "Akıncılar", 58 },
                    { 784, "Gölova", 58 },
                    { 785, "Doğanşar", 58 },
                    { 786, "Çorlu", 59 },
                    { 787, "Süleymanpaşa", 59 },
                    { 788, "Çerkezköy", 59 },
                    { 789, "Kapaklı", 59 },
                    { 790, "Ergene", 59 },
                    { 791, "Malkara", 59 },
                    { 792, "Saray", 59 },
                    { 793, "Hayrabolu", 59 },
                    { 794, "Şarköy", 59 },
                    { 795, "Muratlı", 59 },
                    { 796, "Marmaraereğlisi", 59 },
                    { 797, "Tokat Merkez", 60 },
                    { 798, "Erbaa", 60 },
                    { 799, "Turhal", 60 },
                    { 800, "Niksar", 60 },
                    { 801, "Zile", 60 },
                    { 802, "Reşadiye", 60 },
                    { 803, "Almus", 60 },
                    { 804, "Pazar", 60 },
                    { 805, "Yeşilyurt", 60 },
                    { 806, "Artova", 60 },
                    { 807, "Sulusaray", 60 },
                    { 808, "Başçiftlik", 60 },
                    { 809, "Ortahisar", 61 },
                    { 810, "Akçaabat", 61 },
                    { 811, "Araklı", 61 },
                    { 812, "Of", 61 },
                    { 813, "Yomra", 61 },
                    { 814, "Arsin", 61 },
                    { 815, "Vakfıkebir", 61 },
                    { 816, "Sürmene", 61 },
                    { 817, "Maçka", 61 },
                    { 818, "Beşikdüzü", 61 },
                    { 819, "Çarşıbaşı", 61 },
                    { 820, "Tonya", 61 },
                    { 821, "Düzköy", 61 },
                    { 822, "Çaykara", 61 },
                    { 823, "Şalpazarı", 61 },
                    { 824, "Hayrat", 61 },
                    { 825, "Köprübaşı", 61 },
                    { 826, "Dernekpazarı", 61 },
                    { 827, "Tunceli Merkez", 62 },
                    { 828, "Pertek", 62 },
                    { 829, "Mazgirt", 62 },
                    { 830, "Çemişgezek", 62 },
                    { 831, "Hozat", 62 },
                    { 832, "Ovacık", 62 },
                    { 833, "Pülümür", 62 },
                    { 834, "Nazımiye", 62 },
                    { 835, "Eyyübiye", 63 },
                    { 836, "Haliliye", 63 },
                    { 837, "Siverek", 63 },
                    { 838, "Viranşehir", 63 },
                    { 839, "Karaköprü", 63 },
                    { 840, "Akçakale", 63 },
                    { 841, "Suruç", 63 },
                    { 842, "Birecik", 63 },
                    { 843, "Harran", 63 },
                    { 844, "Ceylanpınar", 63 },
                    { 845, "Bozova", 63 },
                    { 846, "Hilvan", 63 },
                    { 847, "Halfeti", 63 },
                    { 848, "Uşak Merkez", 64 },
                    { 849, "Banaz", 64 },
                    { 850, "Eşme", 64 },
                    { 851, "Sivaslı", 64 },
                    { 852, "Ulubey", 64 },
                    { 853, "Karahallı", 64 },
                    { 854, "İpekyolu", 65 },
                    { 855, "Erciş", 65 },
                    { 856, "Tuşba", 65 },
                    { 857, "Edremit", 65 },
                    { 858, "Özalp", 65 },
                    { 859, "Çaldıran", 65 },
                    { 860, "Başkale", 65 },
                    { 861, "Muradiye", 65 },
                    { 862, "Gürpınar", 65 },
                    { 863, "Gevaş", 65 },
                    { 864, "Saray", 65 },
                    { 865, "Çatak", 65 },
                    { 866, "Bahçesaray", 65 },
                    { 867, "Yozgat Merkez", 66 },
                    { 868, "Sorgun", 66 },
                    { 869, "Akdağmadeni", 66 },
                    { 870, "Yerköy", 66 },
                    { 871, "Boğazlıyan", 66 },
                    { 872, "Sarıkaya", 66 },
                    { 873, "Çekerek", 66 },
                    { 874, "Şefaatli", 66 },
                    { 875, "Saraykent", 66 },
                    { 876, "Çayıralan", 66 },
                    { 877, "Kadışehri", 66 },
                    { 878, "Aydıncık", 66 },
                    { 879, "Yenifakılı", 66 },
                    { 880, "Çandır", 66 },
                    { 881, "Ereğli", 67 },
                    { 882, "Zonguldak Merkez", 67 },
                    { 883, "Çaycuma", 67 },
                    { 884, "Devrek", 67 },
                    { 885, "Kozlu", 67 },
                    { 886, "Alaplı", 67 },
                    { 887, "Kilimli", 67 },
                    { 888, "Gökçebey", 67 },
                    { 889, "Aksaray Merkez", 68 },
                    { 890, "Ortaköy", 68 },
                    { 891, "Eskil", 68 },
                    { 892, "Gülağaç", 68 },
                    { 893, "Güzelyurt", 68 },
                    { 894, "Sultanhanı", 68 },
                    { 895, "Ağaçören", 68 },
                    { 896, "Sarıyahşi", 68 },
                    { 897, "Bayburt Merkez", 69 },
                    { 898, "Demirözü", 69 },
                    { 899, "Aydıntepe", 69 },
                    { 900, "Karaman Merkez", 70 },
                    { 901, "Ermenek", 70 },
                    { 902, "Sarıveliler", 70 },
                    { 903, "Ayrancı", 70 },
                    { 904, "Başyayla", 70 },
                    { 905, "Kazımkarabekir", 70 },
                    { 906, "Kırıkkale Merkez", 71 },
                    { 907, "Yahşihan", 71 },
                    { 908, "Keskin", 71 },
                    { 909, "Delice", 71 },
                    { 910, "Bahşılı", 71 },
                    { 911, "Sulakyurt", 71 },
                    { 912, "Balışeyh", 71 },
                    { 913, "Karakeçili", 71 },
                    { 914, "Çelebi", 71 },
                    { 915, "Batman Merkez", 72 },
                    { 916, "Kozluk", 72 },
                    { 917, "Sason", 72 },
                    { 918, "Beşiri", 72 },
                    { 919, "Gercüş", 72 },
                    { 920, "Hasankeyf", 72 },
                    { 921, "Cizre", 73 },
                    { 922, "Silopi", 73 },
                    { 923, "Şırnak Merkez", 73 },
                    { 924, "İdil", 73 },
                    { 925, "Uludere", 73 },
                    { 926, "Beytüşşebap", 73 },
                    { 927, "Güçlükonak", 73 },
                    { 928, "Bartın Merkez", 74 },
                    { 929, "Ulus", 74 },
                    { 930, "Amasra", 74 },
                    { 931, "Kurucaşile", 74 },
                    { 932, "Ardahan Merkez", 75 },
                    { 933, "Göle", 75 },
                    { 934, "Çıldır", 75 },
                    { 935, "Hanak", 75 },
                    { 936, "Posof", 75 },
                    { 937, "Damal", 75 },
                    { 938, "Iğdır Merkez", 76 },
                    { 939, "Tuzluca", 76 },
                    { 940, "Aralık", 76 },
                    { 941, "Karakoyunlu", 76 },
                    { 942, "Yalova Merkez", 77 },
                    { 943, "Çiftlikköy", 77 },
                    { 944, "Çınarcık", 77 },
                    { 945, "Altınova", 77 },
                    { 946, "Armutlu", 77 },
                    { 947, "Termal", 77 },
                    { 948, "Karabük Merkez", 78 },
                    { 949, "Safranbolu", 78 },
                    { 950, "Yenice", 78 },
                    { 951, "Eskipazar", 78 },
                    { 952, "Eflani", 78 },
                    { 953, "Ovacık", 78 },
                    { 954, "Kilis Merkez", 79 },
                    { 955, "Musabeyli", 79 },
                    { 956, "Elbeyli", 79 },
                    { 957, "Polateli", 79 },
                    { 958, "Osmaniye Merkez", 80 },
                    { 959, "Kadirli", 80 },
                    { 960, "Düziçi", 80 },
                    { 961, "Bahçe", 80 },
                    { 962, "Toprakkale", 80 },
                    { 963, "Sumbas", 80 },
                    { 964, "Hasanbeyli", 80 },
                    { 965, "Düzce Merkez", 81 },
                    { 966, "Akçakoca", 81 },
                    { 967, "Kaynaşlı", 81 },
                    { 968, "Gölyaka", 81 },
                    { 969, "Çilimli", 81 },
                    { 970, "Yığılca", 81 },
                    { 971, "Gümüşova", 81 },
                    { 972, "Cumayeri", 81 }
                });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8244));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8245));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8214));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8218));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8219));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8220));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8221));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8223));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 14, 23, 7, 493, DateTimeKind.Local).AddTicks(7223));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 399);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 409);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 413);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 419);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 420);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 421);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 422);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 423);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 424);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 425);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 426);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 430);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 431);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 435);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 437);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 438);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 439);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 443);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 451);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 453);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 454);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 455);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 456);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 473);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 474);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 475);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 476);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 477);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 478);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 538);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 543);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 550);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 552);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 553);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 554);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 555);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 556);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 558);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 560);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 561);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 563);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 565);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 566);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 567);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 568);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 569);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 570);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 571);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 572);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 573);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 574);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 575);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 576);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 577);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 578);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 579);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 580);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 581);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 582);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 583);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 584);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 585);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 586);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 587);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 588);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 589);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 590);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 591);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 592);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 593);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 594);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 595);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 596);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 597);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 598);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 599);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 600);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 601);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 603);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 605);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 606);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 607);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 608);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 609);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 610);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 611);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 612);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 613);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 614);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 615);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 616);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 617);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 618);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 619);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 620);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 621);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 622);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 623);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 624);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 625);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 626);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 627);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 628);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 629);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 630);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 631);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 632);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 633);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 634);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 635);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 636);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 637);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 638);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 639);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 640);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 641);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 642);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 643);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 644);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 645);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 646);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 647);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 648);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 649);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 650);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 651);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 652);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 653);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 654);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 655);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 656);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 657);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 658);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 659);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 660);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 661);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 662);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 663);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 664);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 665);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 666);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 667);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 668);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 669);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 670);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 671);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 672);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 673);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 674);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 675);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 676);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 677);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 678);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 679);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 680);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 681);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 682);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 683);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 684);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 685);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 686);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 687);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 688);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 689);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 690);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 691);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 692);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 693);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 694);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 695);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 696);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 697);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 698);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 699);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 700);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 701);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 702);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 703);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 704);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 705);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 706);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 707);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 708);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 709);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 710);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 711);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 712);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 713);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 714);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 715);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 716);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 717);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 718);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 719);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 720);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 721);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 722);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 723);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 724);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 725);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 726);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 727);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 728);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 729);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 730);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 731);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 732);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 733);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 734);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 735);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 736);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 737);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 738);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 739);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 740);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 741);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 742);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 743);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 744);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 745);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 746);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 747);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 748);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 749);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 750);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 751);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 752);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 753);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 754);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 755);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 756);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 757);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 758);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 759);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 760);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 761);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 762);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 763);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 764);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 765);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 766);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 767);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 768);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 769);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 770);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 771);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 772);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 773);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 774);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 775);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 776);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 777);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 778);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 779);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 780);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 781);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 782);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 783);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 784);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 785);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 786);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 787);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 788);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 789);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 790);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 791);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 792);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 793);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 794);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 795);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 796);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 797);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 798);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 799);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 800);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 801);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 802);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 803);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 804);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 805);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 806);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 807);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 808);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 809);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 810);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 811);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 812);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 813);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 814);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 815);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 816);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 817);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 818);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 819);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 820);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 821);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 822);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 823);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 824);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 825);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 826);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 827);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 828);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 829);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 830);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 831);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 832);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 833);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 834);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 835);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 836);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 837);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 838);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 839);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 840);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 841);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 842);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 843);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 844);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 845);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 846);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 847);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 848);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 849);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 850);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 851);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 852);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 853);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 854);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 855);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 856);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 857);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 858);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 859);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 860);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 861);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 862);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 863);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 864);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 865);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 866);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 867);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 868);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 869);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 870);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 871);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 872);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 873);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 874);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 875);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 876);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 877);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 878);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 879);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 880);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 881);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 882);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 883);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 884);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 885);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 886);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 887);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 888);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 889);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 890);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 891);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 892);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 893);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 894);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 895);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 896);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 897);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 898);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 899);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 900);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 910);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 911);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 912);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 913);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 914);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 915);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 916);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 917);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 918);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 919);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 920);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 921);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 922);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 923);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 924);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 925);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 926);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 927);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 928);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 929);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 930);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 931);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 932);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 933);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 934);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 935);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 936);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 937);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 938);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 939);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 940);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 941);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 942);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 943);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 944);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 945);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 946);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 947);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 948);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 949);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 950);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 951);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 952);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 953);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 954);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 955);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 956);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 957);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 958);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 959);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 960);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 961);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 962);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 963);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 964);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 965);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 966);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 967);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 968);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 969);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 970);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 971);

            migrationBuilder.DeleteData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 972);

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7441));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7449));

            migrationBuilder.UpdateData(
                table: "GorevKategorileri",
                keyColumn: "GorevKategoriId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 175, DateTimeKind.Local).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1165));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1167));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1170));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1229));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1232));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1237));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1239));

            migrationBuilder.UpdateData(
                table: "Gorevler",
                keyColumn: "GorevId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 176, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 1,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Çankaya", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 2,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Keçiören", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 3,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Yenimahalle", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 4,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Mamak", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 5,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Etimesgut", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 6,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Sincan", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 7,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Altındağ", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 8,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Pursaklar", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 9,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Gölbaşı", 6 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 10,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Esenyurt", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 11,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Şahinbey", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 12,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Çankaya", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 13,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Üsküdar", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 14,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Kadıköy", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 15,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Beşiktaş", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 16,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Şişli", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 17,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Fatih", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 18,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Beyoğlu", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 19,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Bakırköy", 34 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 20,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Buca", 35 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 21,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Karabağlar", 35 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 22,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Bornova", 35 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 23,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Konak", 35 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 24,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Karşıyaka", 35 });

            migrationBuilder.UpdateData(
                table: "Ilceler",
                keyColumn: "IlceId",
                keyValue: 25,
                columns: new[] { "Ad", "IlId" },
                values: new object[] { "Bayraklı", 35 });

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5189));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5196));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5199));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5201));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Koordinatorlukler",
                keyColumn: "KoordinatorlukId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(4013));

            migrationBuilder.UpdateData(
                table: "Teskilatlar",
                keyColumn: "TeskilatId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 19, 13, 38, 41, 171, DateTimeKind.Local).AddTicks(4028));
        }
    }
}
