-- Clean up the artifact column from the failed/aborted migration
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Personeller') AND name = 'PersonelCinsiyetBit')
BEGIN
    ALTER TABLE dbo.Personeller DROP COLUMN PersonelCinsiyetBit;
    PRINT 'Dropped artifact column PersonelCinsiyetBit';
END

-- Ensure PersonelCinsiyet is tinyint (just in case)
-- (The image shows it is, but this is a safety check)
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Personeller') AND name = 'PersonelCinsiyet' AND system_type_id = 48) -- 48 is tinyint
BEGIN
    PRINT 'Confirmed PersonelCinsiyet is tinyint.';
END
ELSE
BEGIN
    PRINT 'WARNING: PersonelCinsiyet is NOT tinyint. Please check manually.';
END
GO
