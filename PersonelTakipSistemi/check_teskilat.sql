CREATE TABLE [Birimler] (
    [BirimId] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_Birimler] PRIMARY KEY ([BirimId])
);
GO


CREATE TABLE [Branslar] (
    [BransId] int NOT NULL IDENTITY,
    [Ad] nvarchar(250) NOT NULL,
    CONSTRAINT [PK_Branslar] PRIMARY KEY ([BransId])
);
GO


CREATE TABLE [DaireBaskanliklari] (
    [Id] int NOT NULL IDENTITY,
    [Ad] nvarchar(200) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_DaireBaskanliklari] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [GorevDurumlari] (
    [GorevDurumId] int NOT NULL IDENTITY,
    [Ad] nvarchar(100) NOT NULL,
    [Sira] int NOT NULL,
    [RenkSinifi] nvarchar(50) NULL,
    [Renk] nvarchar(7) NULL,
    CONSTRAINT [PK_GorevDurumlari] PRIMARY KEY ([GorevDurumId])
);
GO


CREATE TABLE [GorevKategorileri] (
    [GorevKategoriId] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    [Aciklama] nvarchar(500) NULL,
    [Renk] nvarchar(7) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_GorevKategorileri] PRIMARY KEY ([GorevKategoriId])
);
GO


CREATE TABLE [GorevTurleri] (
    [GorevTuruId] int NOT NULL IDENTITY,
    [Ad] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_GorevTurleri] PRIMARY KEY ([GorevTuruId])
);
GO


CREATE TABLE [Iller] (
    [IlId] int NOT NULL IDENTITY,
    [Ad] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Iller] PRIMARY KEY ([IlId])
);
GO


CREATE TABLE [IsNitelikleri] (
    [IsNiteligiId] int NOT NULL IDENTITY,
    [Ad] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_IsNitelikleri] PRIMARY KEY ([IsNiteligiId])
);
GO


CREATE TABLE [KurumsalRoller] (
    [KurumsalRolId] int NOT NULL IDENTITY,
    [Ad] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_KurumsalRoller] PRIMARY KEY ([KurumsalRolId])
);
GO


CREATE TABLE [SistemLoglar] (
    [Id] int NOT NULL IDENTITY,
    [PersonelId] int NULL,
    [TcKimlikNo] nvarchar(11) NULL,
    [KullaniciAdSoyad] nvarchar(100) NULL,
    [IpAdresi] nvarchar(50) NOT NULL,
    [Tarih] datetime2 NOT NULL,
    [IslemTuru] nvarchar(50) NOT NULL,
    [Aciklama] nvarchar(max) NOT NULL,
    [Detay] nvarchar(max) NULL,
    CONSTRAINT [PK_SistemLoglar] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [SistemRoller] (
    [SistemRolId] int NOT NULL IDENTITY,
    [Ad] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SistemRoller] PRIMARY KEY ([SistemRolId])
);
GO


CREATE TABLE [Uzmanliklar] (
    [UzmanlikId] int NOT NULL IDENTITY,
    [Ad] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Uzmanliklar] PRIMARY KEY ([UzmanlikId])
);
GO


CREATE TABLE [Yazilimlar] (
    [YazilimId] int NOT NULL IDENTITY,
    [Ad] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Yazilimlar] PRIMARY KEY ([YazilimId])
);
GO


CREATE TABLE [Teskilatlar] (
    [TeskilatId] int NOT NULL IDENTITY,
    [Ad] nvarchar(50) NOT NULL,
    [Aciklama] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [DaireBaskanligiId] int NULL,
    CONSTRAINT [PK_Teskilatlar] PRIMARY KEY ([TeskilatId]),
    CONSTRAINT [FK_Teskilatlar_DaireBaskanliklari_DaireBaskanligiId] FOREIGN KEY ([DaireBaskanligiId]) REFERENCES [DaireBaskanliklari] ([Id]) ON DELETE SET NULL
);
GO


CREATE TABLE [Personeller] (
    [PersonelId] int NOT NULL IDENTITY,
    [Ad] nvarchar(max) NOT NULL,
    [Soyad] nvarchar(max) NOT NULL,
    [TcKimlikNo] nvarchar(11) NOT NULL,
    [Telefon] nchar(10) NOT NULL,
    [Eposta] nvarchar(450) NOT NULL,
    [PersonelCinsiyet] bit NOT NULL,
    [GorevliIlId] int NOT NULL,
    [BransId] int NOT NULL,
    [KadroKurum] nvarchar(max) NOT NULL,
    [AktifMi] bit NOT NULL DEFAULT CAST(1 AS bit),
    [FotografYolu] nvarchar(max) NULL,
    [DogumTarihi] date NOT NULL,
    [Sifre] nvarchar(max) NULL,
    [SifreHash] varbinary(max) NOT NULL,
    [SifreSalt] varbinary(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [SistemRolId] int NULL,
    CONSTRAINT [PK_Personeller] PRIMARY KEY ([PersonelId]),
    CONSTRAINT [FK_Personeller_Branslar_BransId] FOREIGN KEY ([BransId]) REFERENCES [Branslar] ([BransId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Personeller_Iller_GorevliIlId] FOREIGN KEY ([GorevliIlId]) REFERENCES [Iller] ([IlId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Personeller_SistemRoller_SistemRolId] FOREIGN KEY ([SistemRolId]) REFERENCES [SistemRoller] ([SistemRolId]) ON DELETE NO ACTION
);
GO


CREATE TABLE [BildirimGonderenler] (
    [Id] int NOT NULL IDENTITY,
    [Tip] int NOT NULL,
    [PersonelId] int NULL,
    [GorunenAd] nvarchar(100) NOT NULL,
    [AvatarUrl] nvarchar(300) NULL,
    CONSTRAINT [PK_BildirimGonderenler] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BildirimGonderenler_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Gorevler] (
    [GorevId] int NOT NULL IDENTITY,
    [Ad] nvarchar(200) NOT NULL,
    [Aciklama] nvarchar(max) NULL,
    [KategoriId] int NOT NULL,
    [PersonelId] int NULL,
    [BirimId] int NULL,
    [GorevDurumId] int NOT NULL,
    [DurumAciklamasi] nvarchar(500) NULL,
    [BaslangicTarihi] datetime2 NOT NULL,
    [BitisTarihi] datetime2 NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Gorevler] PRIMARY KEY ([GorevId]),
    CONSTRAINT [FK_Gorevler_Birimler_BirimId] FOREIGN KEY ([BirimId]) REFERENCES [Birimler] ([BirimId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Gorevler_GorevDurumlari_GorevDurumId] FOREIGN KEY ([GorevDurumId]) REFERENCES [GorevDurumlari] ([GorevDurumId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Gorevler_GorevKategorileri_KategoriId] FOREIGN KEY ([KategoriId]) REFERENCES [GorevKategorileri] ([GorevKategoriId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Gorevler_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE NO ACTION
);
GO


CREATE TABLE [Koordinatorlukler] (
    [KoordinatorlukId] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    [Aciklama] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [TeskilatId] int NOT NULL,
    [IlId] int NULL,
    [BaskanPersonelId] int NULL,
    CONSTRAINT [PK_Koordinatorlukler] PRIMARY KEY ([KoordinatorlukId]),
    CONSTRAINT [FK_Koordinatorlukler_Iller_IlId] FOREIGN KEY ([IlId]) REFERENCES [Iller] ([IlId]),
    CONSTRAINT [FK_Koordinatorlukler_Personeller_BaskanPersonelId] FOREIGN KEY ([BaskanPersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE SET NULL,
    CONSTRAINT [FK_Koordinatorlukler_Teskilatlar_TeskilatId] FOREIGN KEY ([TeskilatId]) REFERENCES [Teskilatlar] ([TeskilatId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelGorevTurleri] (
    [PersonelId] int NOT NULL,
    [GorevTuruId] int NOT NULL,
    CONSTRAINT [PK_PersonelGorevTurleri] PRIMARY KEY ([PersonelId], [GorevTuruId]),
    CONSTRAINT [FK_PersonelGorevTurleri_GorevTurleri_GorevTuruId] FOREIGN KEY ([GorevTuruId]) REFERENCES [GorevTurleri] ([GorevTuruId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelGorevTurleri_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelIsNitelikleri] (
    [PersonelId] int NOT NULL,
    [IsNiteligiId] int NOT NULL,
    CONSTRAINT [PK_PersonelIsNitelikleri] PRIMARY KEY ([PersonelId], [IsNiteligiId]),
    CONSTRAINT [FK_PersonelIsNitelikleri_IsNitelikleri_IsNiteligiId] FOREIGN KEY ([IsNiteligiId]) REFERENCES [IsNitelikleri] ([IsNiteligiId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelIsNitelikleri_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelTeskilatlar] (
    [PersonelId] int NOT NULL,
    [TeskilatId] int NOT NULL,
    CONSTRAINT [PK_PersonelTeskilatlar] PRIMARY KEY ([PersonelId], [TeskilatId]),
    CONSTRAINT [FK_PersonelTeskilatlar_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelTeskilatlar_Teskilatlar_TeskilatId] FOREIGN KEY ([TeskilatId]) REFERENCES [Teskilatlar] ([TeskilatId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelUzmanliklar] (
    [PersonelId] int NOT NULL,
    [UzmanlikId] int NOT NULL,
    CONSTRAINT [PK_PersonelUzmanliklar] PRIMARY KEY ([PersonelId], [UzmanlikId]),
    CONSTRAINT [FK_PersonelUzmanliklar_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelUzmanliklar_Uzmanliklar_UzmanlikId] FOREIGN KEY ([UzmanlikId]) REFERENCES [Uzmanliklar] ([UzmanlikId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelYazilimlar] (
    [PersonelId] int NOT NULL,
    [YazilimId] int NOT NULL,
    CONSTRAINT [PK_PersonelYazilimlar] PRIMARY KEY ([PersonelId], [YazilimId]),
    CONSTRAINT [FK_PersonelYazilimlar_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelYazilimlar_Yazilimlar_YazilimId] FOREIGN KEY ([YazilimId]) REFERENCES [Yazilimlar] ([YazilimId]) ON DELETE CASCADE
);
GO


CREATE TABLE [TopluBildirimler] (
    [Id] int NOT NULL IDENTITY,
    [GonderenId] int NOT NULL,
    [Baslik] nvarchar(200) NOT NULL,
    [Icerik] nvarchar(max) NOT NULL,
    [OlusturmaTarihi] datetime2 NOT NULL,
    [PlanlananZaman] datetime2 NULL,
    [GonderimZamani] datetime2 NULL,
    [Durum] int NOT NULL,
    [HedefKitleJson] nvarchar(max) NULL,
    [RecipientIdsJson] nvarchar(max) NULL,
    CONSTRAINT [PK_TopluBildirimler] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TopluBildirimler_BildirimGonderenler_GonderenId] FOREIGN KEY ([GonderenId]) REFERENCES [BildirimGonderenler] ([Id]) ON DELETE NO ACTION
);
GO


CREATE TABLE [GorevAtamaPersoneller] (
    [GorevId] int NOT NULL,
    [PersonelId] int NOT NULL,
    CONSTRAINT [PK_GorevAtamaPersoneller] PRIMARY KEY ([GorevId], [PersonelId]),
    CONSTRAINT [FK_GorevAtamaPersoneller_Gorevler_GorevId] FOREIGN KEY ([GorevId]) REFERENCES [Gorevler] ([GorevId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GorevAtamaPersoneller_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [GorevAtamaTeskilatlar] (
    [GorevId] int NOT NULL,
    [TeskilatId] int NOT NULL,
    CONSTRAINT [PK_GorevAtamaTeskilatlar] PRIMARY KEY ([GorevId], [TeskilatId]),
    CONSTRAINT [FK_GorevAtamaTeskilatlar_Gorevler_GorevId] FOREIGN KEY ([GorevId]) REFERENCES [Gorevler] ([GorevId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GorevAtamaTeskilatlar_Teskilatlar_TeskilatId] FOREIGN KEY ([TeskilatId]) REFERENCES [Teskilatlar] ([TeskilatId]) ON DELETE CASCADE
);
GO


CREATE TABLE [GorevDurumGecmisleri] (
    [Id] int NOT NULL IDENTITY,
    [GorevId] int NOT NULL,
    [GorevDurumId] int NOT NULL,
    [Aciklama] nvarchar(max) NULL,
    [Tarih] datetime2 NOT NULL,
    [IslemYapanPersonelId] int NULL,
    CONSTRAINT [PK_GorevDurumGecmisleri] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GorevDurumGecmisleri_GorevDurumlari_GorevDurumId] FOREIGN KEY ([GorevDurumId]) REFERENCES [GorevDurumlari] ([GorevDurumId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_GorevDurumGecmisleri_Gorevler_GorevId] FOREIGN KEY ([GorevId]) REFERENCES [Gorevler] ([GorevId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GorevDurumGecmisleri_Personeller_IslemYapanPersonelId] FOREIGN KEY ([IslemYapanPersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE SET NULL
);
GO


CREATE TABLE [GorevAtamaKoordinatorlukler] (
    [GorevId] int NOT NULL,
    [KoordinatorlukId] int NOT NULL,
    CONSTRAINT [PK_GorevAtamaKoordinatorlukler] PRIMARY KEY ([GorevId], [KoordinatorlukId]),
    CONSTRAINT [FK_GorevAtamaKoordinatorlukler_Gorevler_GorevId] FOREIGN KEY ([GorevId]) REFERENCES [Gorevler] ([GorevId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GorevAtamaKoordinatorlukler_Koordinatorlukler_KoordinatorlukId] FOREIGN KEY ([KoordinatorlukId]) REFERENCES [Koordinatorlukler] ([KoordinatorlukId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Komisyonlar] (
    [KomisyonId] int NOT NULL IDENTITY,
    [Ad] nvarchar(150) NOT NULL,
    [Aciklama] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [KoordinatorlukId] int NOT NULL,
    [BaskanPersonelId] int NULL,
    CONSTRAINT [PK_Komisyonlar] PRIMARY KEY ([KomisyonId]),
    CONSTRAINT [FK_Komisyonlar_Koordinatorlukler_KoordinatorlukId] FOREIGN KEY ([KoordinatorlukId]) REFERENCES [Koordinatorlukler] ([KoordinatorlukId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Komisyonlar_Personeller_BaskanPersonelId] FOREIGN KEY ([BaskanPersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE SET NULL
);
GO


CREATE TABLE [PersonelKoordinatorlukler] (
    [PersonelId] int NOT NULL,
    [KoordinatorlukId] int NOT NULL,
    CONSTRAINT [PK_PersonelKoordinatorlukler] PRIMARY KEY ([PersonelId], [KoordinatorlukId]),
    CONSTRAINT [FK_PersonelKoordinatorlukler_Koordinatorlukler_KoordinatorlukId] FOREIGN KEY ([KoordinatorlukId]) REFERENCES [Koordinatorlukler] ([KoordinatorlukId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelKoordinatorlukler_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [Bildirimler] (
    [BildirimId] int NOT NULL IDENTITY,
    [AliciPersonelId] int NOT NULL,
    [GonderenPersonelId] int NULL,
    [BildirimGonderenId] int NULL,
    [TopluBildirimId] int NULL,
    [Baslik] nvarchar(200) NOT NULL,
    [Aciklama] nvarchar(max) NOT NULL,
    [OlusturmaTarihi] datetime2 NOT NULL DEFAULT (GETDATE()),
    [OkunduMu] bit NOT NULL DEFAULT CAST(0 AS bit),
    [OkunmaTarihi] datetime2 NULL,
    [Tip] nvarchar(50) NULL,
    [RefType] nvarchar(50) NULL,
    [RefId] int NULL,
    [Url] nvarchar(300) NULL,
    [SilindiMi] bit NOT NULL,
    CONSTRAINT [PK_Bildirimler] PRIMARY KEY ([BildirimId]),
    CONSTRAINT [FK_Bildirimler_BildirimGonderenler_BildirimGonderenId] FOREIGN KEY ([BildirimGonderenId]) REFERENCES [BildirimGonderenler] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bildirimler_Personeller_AliciPersonelId] FOREIGN KEY ([AliciPersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bildirimler_Personeller_GonderenPersonelId] FOREIGN KEY ([GonderenPersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bildirimler_TopluBildirimler_TopluBildirimId] FOREIGN KEY ([TopluBildirimId]) REFERENCES [TopluBildirimler] ([Id]) ON DELETE SET NULL
);
GO


CREATE TABLE [GorevAtamaKomisyonlar] (
    [GorevId] int NOT NULL,
    [KomisyonId] int NOT NULL,
    CONSTRAINT [PK_GorevAtamaKomisyonlar] PRIMARY KEY ([GorevId], [KomisyonId]),
    CONSTRAINT [FK_GorevAtamaKomisyonlar_Gorevler_GorevId] FOREIGN KEY ([GorevId]) REFERENCES [Gorevler] ([GorevId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GorevAtamaKomisyonlar_Komisyonlar_KomisyonId] FOREIGN KEY ([KomisyonId]) REFERENCES [Komisyonlar] ([KomisyonId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelKomisyonlar] (
    [PersonelId] int NOT NULL,
    [KomisyonId] int NOT NULL,
    CONSTRAINT [PK_PersonelKomisyonlar] PRIMARY KEY ([PersonelId], [KomisyonId]),
    CONSTRAINT [FK_PersonelKomisyonlar_Komisyonlar_KomisyonId] FOREIGN KEY ([KomisyonId]) REFERENCES [Komisyonlar] ([KomisyonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelKomisyonlar_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE
);
GO


CREATE TABLE [PersonelKurumsalRolAtamalari] (
    [Id] int NOT NULL IDENTITY,
    [PersonelId] int NOT NULL,
    [KurumsalRolId] int NOT NULL,
    [TeskilatId] int NULL,
    [KoordinatorlukId] int NULL,
    [KomisyonId] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_PersonelKurumsalRolAtamalari] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PersonelKurumsalRolAtamalari_Komisyonlar_KomisyonId] FOREIGN KEY ([KomisyonId]) REFERENCES [Komisyonlar] ([KomisyonId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonelKurumsalRolAtamalari_Koordinatorlukler_KoordinatorlukId] FOREIGN KEY ([KoordinatorlukId]) REFERENCES [Koordinatorlukler] ([KoordinatorlukId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonelKurumsalRolAtamalari_KurumsalRoller_KurumsalRolId] FOREIGN KEY ([KurumsalRolId]) REFERENCES [KurumsalRoller] ([KurumsalRolId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PersonelKurumsalRolAtamalari_Personeller_PersonelId] FOREIGN KEY ([PersonelId]) REFERENCES [Personeller] ([PersonelId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PersonelKurumsalRolAtamalari_Teskilatlar_TeskilatId] FOREIGN KEY ([TeskilatId]) REFERENCES [Teskilatlar] ([TeskilatId])
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BirimId', N'Ad') AND [object_id] = OBJECT_ID(N'[Birimler]'))
    SET IDENTITY_INSERT [Birimler] ON;
INSERT INTO [Birimler] ([BirimId], [Ad])
VALUES (1, N'Yazılım Birimi'),
(2, N'İçerik Birimi'),
(3, N'Grafik Birimi');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BirimId', N'Ad') AND [object_id] = OBJECT_ID(N'[Birimler]'))
    SET IDENTITY_INSERT [Birimler] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BransId', N'Ad') AND [object_id] = OBJECT_ID(N'[Branslar]'))
    SET IDENTITY_INSERT [Branslar] ON;
INSERT INTO [Branslar] ([BransId], [Ad])
VALUES (1, N'Adalet'),
(2, N'Aile ve Tüketici Hizmetleri'),
(3, N'Almanca'),
(4, N'Arapça'),
(5, N'Ayakkabı ve Saraciye Teknolojisi'),
(6, N'Beden Eğitimi'),
(7, N'Bilgisayar ve Öğretim Teknolojileri'),
(8, N'Bilişim Teknolojileri'),
(9, N'Biyoloji'),
(10, N'Büro Yönetimi / Büro Yönetimi ve Yönetici Asistanlığı'),
(11, N'Coğrafya'),
(12, N'Çocuk Gelişimi ve Eğitimi'),
(13, N'Denizcilik / Gemi Makineleri'),
(14, N'Denizcilik / Gemi Yönetimi'),
(15, N'Din Kültürü ve Ahlâk Bilgisi'),
(16, N'El Sanatları Teknolojisi / El Sanatları'),
(17, N'El Sanatları Teknolojisi / Nakış'),
(18, N'Elektrik-Elektronik Teknolojisi / Elektrik'),
(19, N'Elektrik-Elektronik Teknolojisi / Elektronik'),
(20, N'Endüstriyel Otomasyon Teknolojileri'),
(21, N'Farsça'),
(22, N'Felsefe'),
(23, N'Fen Bilimleri'),
(24, N'Fizik'),
(25, N'Gemi Yapımı / Yat İnşa'),
(26, N'Gıda Teknolojisi'),
(27, N'Giyim Üretim Teknolojisi / Moda Tasarım Teknolojileri'),
(28, N'Görsel Sanatlar'),
(29, N'Grafik ve Fotoğraf / Grafik'),
(30, N'Harita-Tapu-Kadastro / Harita Kadastro'),
(31, N'Hasta ve Yaşlı Hizmetleri'),
(32, N'Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Sağlığı'),
(33, N'Hayvan Sağlığı / Hayvan Yetiştiriciliği ve Sağlığı / Hayvan Yetiştiriciliği'),
(34, N'İlköğretim Matematik'),
(35, N'İmam-Hatip Lisesi Meslek Dersleri'),
(36, N'İngilizce'),
(37, N'İnşaat Teknolojisi / Yapı Dekorasyon'),
(38, N'İnşaat Teknolojisi / Yapı Tasarım'),
(39, N'İtfaiyecilik ve Yangın Güvenliği'),
(40, N'Kimya / Kimya Teknolojisi'),
(41, N'Konaklama ve Seyahat Hizmetleri / Konaklama ve Seyahat'),
(42, N'Kuyumculuk Teknolojisi');
INSERT INTO [Branslar] ([BransId], [Ad])
VALUES (43, N'Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Model'),
(44, N'Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine Ressamlığı'),
(45, N'Makine Teknolojisi / Makine ve Tasarım Teknolojisi / Makine ve Kalıp'),
(46, N'Matbaa / Matbaa Teknolojisi'),
(47, N'Matematik'),
(48, N'Metal Teknolojisi'),
(49, N'Mobilya ve İç Mekan Tasarımı'),
(50, N'Motorlu Araçlar Teknolojisi'),
(51, N'Muhasebe ve Finansman'),
(52, N'Müzik'),
(53, N'Okul Öncesi'),
(54, N'Özel Eğitim'),
(55, N'Pazarlama ve Perakende'),
(56, N'Plastik Teknolojisi'),
(57, N'Raylı Sistemler Teknolojisi / Raylı Sistemler Elektrik-Elektronik'),
(58, N'Rehberlik'),
(59, N'Rusça'),
(60, N'Sağlık / Sağlık Hizmetleri'),
(61, N'Sağlık Bilgisi'),
(62, N'Seramik ve Cam Teknolojisi'),
(63, N'Sınıf Öğretmenliği'),
(64, N'Sosyal Bilgiler'),
(65, N'Tarım Teknolojileri/Tarım'),
(66, N'Tarih'),
(67, N'Teknoloji ve Tasarım'),
(68, N'Tesisat Teknolojisi ve İklimlendirme'),
(69, N'Tiyatro'),
(70, N'Türk Dili ve Edebiyatı'),
(71, N'Türkçe'),
(72, N'Ulaştırma Hizmetleri / Lojistik'),
(73, N'Yaşayan Diller ve Lehçeler (Kürtçe / Kurmançi)'),
(74, N'Yaşayan Diller ve Lehçeler (Kürtçe / Zazaki)'),
(75, N'Yenilenebilir Enerji Teknolojileri'),
(76, N'Yiyecek İçecek Hizmetleri');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BransId', N'Ad') AND [object_id] = OBJECT_ID(N'[Branslar]'))
    SET IDENTITY_INSERT [Branslar] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ad', N'IsActive') AND [object_id] = OBJECT_ID(N'[DaireBaskanliklari]'))
    SET IDENTITY_INSERT [DaireBaskanliklari] ON;
INSERT INTO [DaireBaskanliklari] ([Id], [Ad], [IsActive])
VALUES (1, N'Araştırma Geliştirme ve Projeler Daire Başkanlığı', CAST(0 AS bit)),
(2, N'Eğitim Ortamlarının ve Öğrenme Süreçlerinin Geliştirilmesi Daire Başkanlığı', CAST(0 AS bit)),
(3, N'Eğitim Politikaları Daire Başkanlığı', CAST(0 AS bit)),
(4, N'Erken Çocukluk Eğitimi Daire Başkanlığı', CAST(0 AS bit)),
(5, N'İdari ve Mali İşler Daire Başkanlığı', CAST(0 AS bit)),
(6, N'İzleme ve Değerlendirme Daire Başkanlığı', CAST(0 AS bit)),
(7, N'Kültür, Sanat ve Spor Etkinlikleri Daire Başkanlığı', CAST(0 AS bit)),
(8, N'Öğrenci İşleri Daire Başkanlığı', CAST(0 AS bit)),
(9, N'Programlar ve Öğretim Materyalleri Daire Başkanlığı', CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Ad', N'IsActive') AND [object_id] = OBJECT_ID(N'[DaireBaskanliklari]'))
    SET IDENTITY_INSERT [DaireBaskanliklari] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevDurumId', N'Ad', N'Renk', N'RenkSinifi', N'Sira') AND [object_id] = OBJECT_ID(N'[GorevDurumlari]'))
    SET IDENTITY_INSERT [GorevDurumlari] ON;
INSERT INTO [GorevDurumlari] ([GorevDurumId], [Ad], [Renk], [RenkSinifi], [Sira])
VALUES (1, N'Atanmayı Bekliyor', N'#F59E0B', N'bg-warning', 1),
(2, N'Devam Ediyor', N'#3B82F6', N'bg-primary', 2),
(3, N'Kontrolde', N'#06B6D4', N'bg-info', 3),
(4, N'Tamamlandı', N'#10B981', N'bg-success', 4),
(5, N'İptal', N'#6B7280', N'bg-secondary', 5);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevDurumId', N'Ad', N'Renk', N'RenkSinifi', N'Sira') AND [object_id] = OBJECT_ID(N'[GorevDurumlari]'))
    SET IDENTITY_INSERT [GorevDurumlari] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevKategoriId', N'Aciklama', N'Ad', N'CreatedAt', N'IsActive', N'Renk', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[GorevKategorileri]'))
    SET IDENTITY_INSERT [GorevKategorileri] ON;
INSERT INTO [GorevKategorileri] ([GorevKategoriId], [Aciklama], [Ad], [CreatedAt], [IsActive], [Renk], [UpdatedAt])
VALUES (1, N'Ders kitabı hazırlık işleri', N'Ders Kitapları', '2026-02-13T17:43:02.8334806+03:00', CAST(1 AS bit), N'#3B82F6', NULL),
(2, N'Soru bankası ve etkinlikler', N'Yardımcı Kaynaklar', '2026-02-13T17:43:02.8334814+03:00', CAST(1 AS bit), N'#10B981', NULL),
(3, N'Video ve animasyon işleri', N'Dijital İçerik', '2026-02-13T17:43:02.8334815+03:00', CAST(1 AS bit), N'#F59E0B', NULL),
(4, N'Müfredat çalışmaları', N'Programlar', '2026-02-13T17:43:02.8334816+03:00', CAST(1 AS bit), N'#8B5CF6', NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevKategoriId', N'Aciklama', N'Ad', N'CreatedAt', N'IsActive', N'Renk', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[GorevKategorileri]'))
    SET IDENTITY_INSERT [GorevKategorileri] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevTuruId', N'Ad') AND [object_id] = OBJECT_ID(N'[GorevTurleri]'))
    SET IDENTITY_INSERT [GorevTurleri] ON;
INSERT INTO [GorevTurleri] ([GorevTuruId], [Ad])
VALUES (1, N'Dizgi'),
(2, N'Görsel Üretim'),
(3, N'Çizer'),
(4, N'Çizgi Roman Renklendirme'),
(5, N'Görsel Kontrolü'),
(6, N'Çoklu Ortam Tasarımı'),
(7, N'Video/Kurgu'),
(8, N'Fotoğraf Çekimi'),
(9, N'Açıklama');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevTuruId', N'Ad') AND [object_id] = OBJECT_ID(N'[GorevTurleri]'))
    SET IDENTITY_INSERT [GorevTurleri] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IlId', N'Ad') AND [object_id] = OBJECT_ID(N'[Iller]'))
    SET IDENTITY_INSERT [Iller] ON;
INSERT INTO [Iller] ([IlId], [Ad])
VALUES (1, N'Adana'),
(2, N'Adıyaman'),
(3, N'Afyonkarahisar'),
(4, N'Ağrı'),
(5, N'Amasya'),
(6, N'Ankara'),
(7, N'Antalya'),
(8, N'Artvin'),
(9, N'Aydın'),
(10, N'Balıkesir'),
(11, N'Bilecik'),
(12, N'Bingöl'),
(13, N'Bitlis'),
(14, N'Bolu'),
(15, N'Burdur'),
(16, N'Bursa'),
(17, N'Çanakkale'),
(18, N'Çankırı'),
(19, N'Çorum'),
(20, N'Denizli'),
(21, N'Diyarbakır'),
(22, N'Edirne'),
(23, N'Elazığ'),
(24, N'Erzincan'),
(25, N'Erzurum'),
(26, N'Eskişehir'),
(27, N'Gaziantep'),
(28, N'Giresun'),
(29, N'Gümüşhane'),
(30, N'Hakkari'),
(31, N'Hatay'),
(32, N'Isparta'),
(33, N'Mersin'),
(34, N'İstanbul'),
(35, N'İzmir'),
(36, N'Kars'),
(37, N'Kastamonu'),
(38, N'Kayseri'),
(39, N'Kırklareli'),
(40, N'Kırşehir'),
(41, N'Kocaeli'),
(42, N'Konya');
INSERT INTO [Iller] ([IlId], [Ad])
VALUES (43, N'Kütahya'),
(44, N'Malatya'),
(45, N'Manisa'),
(46, N'Kahramanmaraş'),
(47, N'Mardin'),
(48, N'Muğla'),
(49, N'Muş'),
(50, N'Nevşehir'),
(51, N'Niğde'),
(52, N'Ordu'),
(53, N'Rize'),
(54, N'Sakarya'),
(55, N'Samsun'),
(56, N'Siirt'),
(57, N'Sinop'),
(58, N'Sivas'),
(59, N'Tekirdağ'),
(60, N'Tokat'),
(61, N'Trabzon'),
(62, N'Tunceli'),
(63, N'Şanlıurfa'),
(64, N'Uşak'),
(65, N'Van'),
(66, N'Yozgat'),
(67, N'Zonguldak'),
(68, N'Aksaray'),
(69, N'Bayburt'),
(70, N'Karaman'),
(71, N'Kırıkkale'),
(72, N'Batman'),
(73, N'Şırnak'),
(74, N'Bartın'),
(75, N'Ardahan'),
(76, N'Iğdır'),
(77, N'Yalova'),
(78, N'Karabük'),
(79, N'Kilis'),
(80, N'Osmaniye'),
(81, N'Düzce');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IlId', N'Ad') AND [object_id] = OBJECT_ID(N'[Iller]'))
    SET IDENTITY_INSERT [Iller] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IsNiteligiId', N'Ad') AND [object_id] = OBJECT_ID(N'[IsNitelikleri]'))
    SET IDENTITY_INSERT [IsNitelikleri] ON;
INSERT INTO [IsNitelikleri] ([IsNiteligiId], [Ad])
VALUES (1, N'Ders Kitabı'),
(2, N'Çalışma Kitabı'),
(3, N'Hikâye Kitabı'),
(4, N'Çizgi Roman'),
(5, N'Çoklu Ortam'),
(6, N'MEBİ Mümtaz Şahsiyetler'),
(7, N'Dergi'),
(8, N'E Bülten'),
(9, N'E Dergi'),
(10, N'Öğretim Programı'),
(11, N'Açıklama');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'IsNiteligiId', N'Ad') AND [object_id] = OBJECT_ID(N'[IsNitelikleri]'))
    SET IDENTITY_INSERT [IsNitelikleri] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KurumsalRolId', N'Ad') AND [object_id] = OBJECT_ID(N'[KurumsalRoller]'))
    SET IDENTITY_INSERT [KurumsalRoller] ON;
INSERT INTO [KurumsalRoller] ([KurumsalRolId], [Ad])
VALUES (1, N'Personel'),
(2, N'Komisyon Başkanı'),
(3, N'İl Koordinatörü'),
(4, N'Genel Koordinatör'),
(5, N'Merkez Birim Koordinatörlüğü');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KurumsalRolId', N'Ad') AND [object_id] = OBJECT_ID(N'[KurumsalRoller]'))
    SET IDENTITY_INSERT [KurumsalRoller] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SistemRolId', N'Ad') AND [object_id] = OBJECT_ID(N'[SistemRoller]'))
    SET IDENTITY_INSERT [SistemRoller] ON;
INSERT INTO [SistemRoller] ([SistemRolId], [Ad])
VALUES (1, N'Admin'),
(2, N'Yönetici'),
(3, N'Editör'),
(4, N'Kullanıcı');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SistemRolId', N'Ad') AND [object_id] = OBJECT_ID(N'[SistemRoller]'))
    SET IDENTITY_INSERT [SistemRoller] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UzmanlikId', N'Ad') AND [object_id] = OBJECT_ID(N'[Uzmanliklar]'))
    SET IDENTITY_INSERT [Uzmanliklar] ON;
INSERT INTO [Uzmanliklar] ([UzmanlikId], [Ad])
VALUES (1, N'Bilişim Uzmanı'),
(2, N'Görsel Tasarım Uzmanı'),
(3, N'Yazar'),
(4, N'Dil Uzmanı'),
(5, N'Rehberlik');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UzmanlikId', N'Ad') AND [object_id] = OBJECT_ID(N'[Uzmanliklar]'))
    SET IDENTITY_INSERT [Uzmanliklar] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'YazilimId', N'Ad') AND [object_id] = OBJECT_ID(N'[Yazilimlar]'))
    SET IDENTITY_INSERT [Yazilimlar] ON;
INSERT INTO [Yazilimlar] ([YazilimId], [Ad])
VALUES (1, N'Photoshop'),
(2, N'İllüstrator'),
(3, N'InDesign'),
(4, N'Camtasia'),
(5, N'Premiere'),
(6, N'After Effects'),
(7, N'Cinema 4D'),
(8, N'Blender'),
(9, N'Maya'),
(10, N'Procreate'),
(11, N'Construct'),
(12, N'Articulate'),
(13, N'Unity'),
(14, N'Unreal Engine'),
(15, N'PHP'),
(16, N'Java'),
(17, N'.NET'),
(18, N'Diğer');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'YazilimId', N'Ad') AND [object_id] = OBJECT_ID(N'[Yazilimlar]'))
    SET IDENTITY_INSERT [Yazilimlar] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevId', N'Aciklama', N'Ad', N'BaslangicTarihi', N'BirimId', N'BitisTarihi', N'CreatedAt', N'DurumAciklamasi', N'GorevDurumId', N'IsActive', N'KategoriId', N'PersonelId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Gorevler]'))
    SET IDENTITY_INSERT [Gorevler] ON;
INSERT INTO [Gorevler] ([GorevId], [Aciklama], [Ad], [BaslangicTarihi], [BirimId], [BitisTarihi], [CreatedAt], [DurumAciklamasi], [GorevDurumId], [IsActive], [KategoriId], [PersonelId], [UpdatedAt])
VALUES (1, N'Dizgi taslağını hazırla', N'Matematik 9 Kitap Dizgisi', '2025-11-01T00:00:00.0000000', 3, '2025-11-20T00:00:00.0000000', '2026-02-13T17:43:02.8337882+03:00', NULL, 2, CAST(1 AS bit), 1, 1, NULL),
(2, N'Kapak görseli revizesi', N'Fizik 10 Kapak Tasarımı', '2025-11-05T00:00:00.0000000', 3, '2025-11-08T00:00:00.0000000', '2026-02-13T17:43:02.8337896+03:00', NULL, 3, CAST(1 AS bit), 1, 1, NULL),
(3, N'Yazım hatalarının kontrolü', N'Kimya 11 Yazım Denetimi', '2025-12-01T00:00:00.0000000', 2, NULL, '2026-02-13T17:43:02.8337899+03:00', NULL, 1, CAST(1 AS bit), 1, 1, NULL),
(4, N'Soru girişleri', N'LGS Soru Bankası', '2025-11-15T00:00:00.0000000', 2, '2025-12-15T00:00:00.0000000', '2026-02-13T17:43:02.8337901+03:00', NULL, 2, CAST(1 AS bit), 2, 1, NULL),
(5, N'Baskı öncesi kontrol', N'YKS Deneme Seti', '2025-11-25T00:00:00.0000000', 3, NULL, '2026-02-13T17:43:02.8337904+03:00', NULL, 2, CAST(1 AS bit), 2, 1, NULL),
(6, N'İlkokul seviyesi görselleştirme', N'Etkinlik Yaprakları', '2025-10-20T00:00:00.0000000', 3, '2025-10-25T00:00:00.0000000', '2026-02-13T17:43:02.8337907+03:00', NULL, 4, CAST(1 AS bit), 2, 1, NULL),
(7, N'Ders videoları kurgusu', N'EBA Video Montaj', '2025-12-05T00:00:00.0000000', 1, NULL, '2026-02-13T17:43:02.8337909+03:00', NULL, 1, CAST(1 AS bit), 3, 1, NULL),
(8, N'Karakter çizimleri', N'Animasyon Karakterleri', '2025-11-10T00:00:00.0000000', 3, '2025-12-30T00:00:00.0000000', '2026-02-13T17:43:02.8337911+03:00', NULL, 2, CAST(1 AS bit), 3, 1, NULL),
(9, N'Stüdyo planlaması', N'Seslendirme Kayıtları', '2025-11-01T00:00:00.0000000', 2, '2025-11-02T00:00:00.0000000', '2026-02-13T17:43:02.8337937+03:00', NULL, 4, CAST(1 AS bit), 3, 1, NULL),
(10, N'Talim Terbiye notları', N'Müfredat İncelemesi', '2025-12-10T00:00:00.0000000', 2, NULL, '2026-02-13T17:43:02.8337940+03:00', NULL, 2, CAST(1 AS bit), 4, 1, NULL),
(11, N'Excel tablosu hazırlığı', N'Kazanım Eşleştirme', '2025-12-12T00:00:00.0000000', 2, NULL, '2026-02-13T17:43:02.8337942+03:00', NULL, 1, CAST(1 AS bit), 4, 1, NULL),
(12, N'2. Dönem planlaması', N'Haftalık Plan', '2025-11-28T00:00:00.0000000', 1, '2025-11-30T00:00:00.0000000', '2026-02-13T17:43:02.8337944+03:00', NULL, 4, CAST(1 AS bit), 4, 1, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GorevId', N'Aciklama', N'Ad', N'BaslangicTarihi', N'BirimId', N'BitisTarihi', N'CreatedAt', N'DurumAciklamasi', N'GorevDurumId', N'IsActive', N'KategoriId', N'PersonelId', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Gorevler]'))
    SET IDENTITY_INSERT [Gorevler] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TeskilatId', N'Aciklama', N'Ad', N'CreatedAt', N'DaireBaskanligiId', N'IsActive') AND [object_id] = OBJECT_ID(N'[Teskilatlar]'))
    SET IDENTITY_INSERT [Teskilatlar] ON;
INSERT INTO [Teskilatlar] ([TeskilatId], [Aciklama], [Ad], [CreatedAt], [DaireBaskanligiId], [IsActive])
VALUES (1, NULL, N'Merkez', '2026-02-13T17:43:02.8300019+03:00', 9, CAST(1 AS bit)),
(2, NULL, N'Taşra', '2026-02-13T17:43:02.8300033+03:00', 9, CAST(1 AS bit));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TeskilatId', N'Aciklama', N'Ad', N'CreatedAt', N'DaireBaskanligiId', N'IsActive') AND [object_id] = OBJECT_ID(N'[Teskilatlar]'))
    SET IDENTITY_INSERT [Teskilatlar] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KoordinatorlukId', N'Aciklama', N'Ad', N'BaskanPersonelId', N'CreatedAt', N'IlId', N'IsActive', N'TeskilatId') AND [object_id] = OBJECT_ID(N'[Koordinatorlukler]'))
    SET IDENTITY_INSERT [Koordinatorlukler] ON;
INSERT INTO [Koordinatorlukler] ([KoordinatorlukId], [Aciklama], [Ad], [BaskanPersonelId], [CreatedAt], [IlId], [IsActive], [TeskilatId])
VALUES (2, NULL, N'Mardin İl Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301278+03:00', NULL, CAST(1 AS bit), 2),
(3, NULL, N'İzmir İl Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301279+03:00', NULL, CAST(1 AS bit), 2),
(4, NULL, N'Fen Bilimleri Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301259+03:00', NULL, CAST(1 AS bit), 1),
(5, NULL, N'İngilizce Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301263+03:00', NULL, CAST(1 AS bit), 1),
(6, NULL, N'İlkokul Türkçe Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301265+03:00', NULL, CAST(1 AS bit), 1),
(7, NULL, N'İlkokul Hayat Bilgisi Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301266+03:00', NULL, CAST(1 AS bit), 1),
(8, NULL, N'Ortaokul Matematik Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301267+03:00', NULL, CAST(1 AS bit), 1),
(9, NULL, N'İlkokul Matematik Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301268+03:00', NULL, CAST(1 AS bit), 1),
(10, NULL, N'Ortaokul Türkçe Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301269+03:00', NULL, CAST(1 AS bit), 1),
(11, NULL, N'Sosyal Bilgiler Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301270+03:00', NULL, CAST(1 AS bit), 1),
(12, NULL, N'T.C. İnkılap Tarihi ve Atatürkçülük Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301271+03:00', NULL, CAST(1 AS bit), 1),
(13, NULL, N'Almanca Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301272+03:00', NULL, CAST(1 AS bit), 1),
(14, NULL, N'Görsel Tasarım Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301273+03:00', NULL, CAST(1 AS bit), 1),
(15, NULL, N'Mebi Dijital Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301273+03:00', NULL, CAST(1 AS bit), 1),
(16, NULL, N'Müzik Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301274+03:00', NULL, CAST(1 AS bit), 1),
(17, NULL, N'Uzmanlar Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301275+03:00', NULL, CAST(1 AS bit), 1),
(18, NULL, N'BÖTE Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301276+03:00', NULL, CAST(1 AS bit), 1),
(19, NULL, N'Dil İnceleme Birim Koordinatörlüğü', NULL, '2026-02-13T17:43:02.8301277+03:00', NULL, CAST(1 AS bit), 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KoordinatorlukId', N'Aciklama', N'Ad', N'BaskanPersonelId', N'CreatedAt', N'IlId', N'IsActive', N'TeskilatId') AND [object_id] = OBJECT_ID(N'[Koordinatorlukler]'))
    SET IDENTITY_INSERT [Koordinatorlukler] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KomisyonId', N'Aciklama', N'Ad', N'BaskanPersonelId', N'CreatedAt', N'IsActive', N'KoordinatorlukId') AND [object_id] = OBJECT_ID(N'[Komisyonlar]'))
    SET IDENTITY_INSERT [Komisyonlar] ON;
INSERT INTO [Komisyonlar] ([KomisyonId], [Aciklama], [Ad], [BaskanPersonelId], [CreatedAt], [IsActive], [KoordinatorlukId])
VALUES (4, NULL, N'Türkçe Komisyonu', NULL, '2026-02-13T17:43:02.8302323+03:00', CAST(1 AS bit), 2),
(5, NULL, N'Matematik Komisyonu', NULL, '2026-02-13T17:43:02.8302326+03:00', CAST(1 AS bit), 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'KomisyonId', N'Aciklama', N'Ad', N'BaskanPersonelId', N'CreatedAt', N'IsActive', N'KoordinatorlukId') AND [object_id] = OBJECT_ID(N'[Komisyonlar]'))
    SET IDENTITY_INSERT [Komisyonlar] OFF;
GO


CREATE INDEX [IX_BildirimGonderenler_PersonelId] ON [BildirimGonderenler] ([PersonelId]);
GO


CREATE INDEX [IX_Bildirimler_AliciPersonelId_OkunduMu_OlusturmaTarihi] ON [Bildirimler] ([AliciPersonelId], [OkunduMu], [OlusturmaTarihi]);
GO


CREATE INDEX [IX_Bildirimler_AliciPersonelId_OlusturmaTarihi] ON [Bildirimler] ([AliciPersonelId], [OlusturmaTarihi]);
GO


CREATE INDEX [IX_Bildirimler_BildirimGonderenId] ON [Bildirimler] ([BildirimGonderenId]);
GO


CREATE INDEX [IX_Bildirimler_GonderenPersonelId] ON [Bildirimler] ([GonderenPersonelId]);
GO


CREATE INDEX [IX_Bildirimler_TopluBildirimId] ON [Bildirimler] ([TopluBildirimId]);
GO


CREATE UNIQUE INDEX [IX_Branslar_Ad] ON [Branslar] ([Ad]);
GO


CREATE INDEX [IX_GorevAtamaKomisyonlar_KomisyonId] ON [GorevAtamaKomisyonlar] ([KomisyonId]);
GO


CREATE INDEX [IX_GorevAtamaKoordinatorlukler_KoordinatorlukId] ON [GorevAtamaKoordinatorlukler] ([KoordinatorlukId]);
GO


CREATE INDEX [IX_GorevAtamaPersoneller_PersonelId] ON [GorevAtamaPersoneller] ([PersonelId]);
GO


CREATE INDEX [IX_GorevAtamaTeskilatlar_TeskilatId] ON [GorevAtamaTeskilatlar] ([TeskilatId]);
GO


CREATE INDEX [IX_GorevDurumGecmisleri_GorevDurumId] ON [GorevDurumGecmisleri] ([GorevDurumId]);
GO


CREATE INDEX [IX_GorevDurumGecmisleri_GorevId] ON [GorevDurumGecmisleri] ([GorevId]);
GO


CREATE INDEX [IX_GorevDurumGecmisleri_IslemYapanPersonelId] ON [GorevDurumGecmisleri] ([IslemYapanPersonelId]);
GO


CREATE UNIQUE INDEX [IX_GorevKategorileri_Ad] ON [GorevKategorileri] ([Ad]);
GO


CREATE INDEX [IX_Gorevler_BaslangicTarihi] ON [Gorevler] ([BaslangicTarihi]);
GO


CREATE INDEX [IX_Gorevler_BirimId] ON [Gorevler] ([BirimId]);
GO


CREATE INDEX [IX_Gorevler_GorevDurumId] ON [Gorevler] ([GorevDurumId]);
GO


CREATE INDEX [IX_Gorevler_KategoriId] ON [Gorevler] ([KategoriId]);
GO


CREATE INDEX [IX_Gorevler_PersonelId] ON [Gorevler] ([PersonelId]);
GO


CREATE UNIQUE INDEX [IX_GorevTurleri_Ad] ON [GorevTurleri] ([Ad]);
GO


CREATE UNIQUE INDEX [IX_Iller_Ad] ON [Iller] ([Ad]);
GO


CREATE UNIQUE INDEX [IX_IsNitelikleri_Ad] ON [IsNitelikleri] ([Ad]);
GO


CREATE INDEX [IX_Komisyonlar_BaskanPersonelId] ON [Komisyonlar] ([BaskanPersonelId]);
GO


CREATE INDEX [IX_Komisyonlar_KoordinatorlukId] ON [Komisyonlar] ([KoordinatorlukId]);
GO


CREATE INDEX [IX_Koordinatorlukler_BaskanPersonelId] ON [Koordinatorlukler] ([BaskanPersonelId]);
GO


CREATE INDEX [IX_Koordinatorlukler_IlId] ON [Koordinatorlukler] ([IlId]);
GO


CREATE INDEX [IX_Koordinatorlukler_TeskilatId] ON [Koordinatorlukler] ([TeskilatId]);
GO


CREATE INDEX [IX_PersonelGorevTurleri_GorevTuruId] ON [PersonelGorevTurleri] ([GorevTuruId]);
GO


CREATE INDEX [IX_PersonelIsNitelikleri_IsNiteligiId] ON [PersonelIsNitelikleri] ([IsNiteligiId]);
GO


CREATE INDEX [IX_PersonelKomisyonlar_KomisyonId] ON [PersonelKomisyonlar] ([KomisyonId]);
GO


CREATE INDEX [IX_PersonelKoordinatorlukler_KoordinatorlukId] ON [PersonelKoordinatorlukler] ([KoordinatorlukId]);
GO


CREATE INDEX [IX_PersonelKurumsalRolAtamalari_KomisyonId] ON [PersonelKurumsalRolAtamalari] ([KomisyonId]);
GO


CREATE INDEX [IX_PersonelKurumsalRolAtamalari_KoordinatorlukId] ON [PersonelKurumsalRolAtamalari] ([KoordinatorlukId]);
GO


CREATE INDEX [IX_PersonelKurumsalRolAtamalari_KurumsalRolId] ON [PersonelKurumsalRolAtamalari] ([KurumsalRolId]);
GO


CREATE INDEX [IX_PersonelKurumsalRolAtamalari_PersonelId] ON [PersonelKurumsalRolAtamalari] ([PersonelId]);
GO


CREATE INDEX [IX_PersonelKurumsalRolAtamalari_TeskilatId] ON [PersonelKurumsalRolAtamalari] ([TeskilatId]);
GO


CREATE INDEX [IX_Personeller_BransId] ON [Personeller] ([BransId]);
GO


CREATE UNIQUE INDEX [IX_Personeller_Eposta] ON [Personeller] ([Eposta]);
GO


CREATE INDEX [IX_Personeller_GorevliIlId] ON [Personeller] ([GorevliIlId]);
GO


CREATE INDEX [IX_Personeller_SistemRolId] ON [Personeller] ([SistemRolId]);
GO


CREATE UNIQUE INDEX [IX_Personeller_TcKimlikNo] ON [Personeller] ([TcKimlikNo]);
GO


CREATE INDEX [IX_PersonelTeskilatlar_TeskilatId] ON [PersonelTeskilatlar] ([TeskilatId]);
GO


CREATE INDEX [IX_PersonelUzmanliklar_UzmanlikId] ON [PersonelUzmanliklar] ([UzmanlikId]);
GO


CREATE INDEX [IX_PersonelYazilimlar_YazilimId] ON [PersonelYazilimlar] ([YazilimId]);
GO


CREATE INDEX [IX_Teskilatlar_DaireBaskanligiId] ON [Teskilatlar] ([DaireBaskanligiId]);
GO


CREATE INDEX [IX_TopluBildirimler_GonderenId] ON [TopluBildirimler] ([GonderenId]);
GO


CREATE UNIQUE INDEX [IX_Uzmanliklar_Ad] ON [Uzmanliklar] ([Ad]);
GO


CREATE UNIQUE INDEX [IX_Yazilimlar_Ad] ON [Yazilimlar] ([Ad]);
GO


