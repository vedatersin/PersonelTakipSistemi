-- Check if column is BIT, if so, convert to TINYINT
IF EXISTS (
    SELECT * 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.Personeller') 
    AND name = 'PersonelCinsiyet' 
    AND system_type_id = 104 -- 104 is bit
)
BEGIN
    ALTER TABLE dbo.Personeller ADD PersonelCinsiyetByte tinyint NULL;
    EXEC('UPDATE dbo.Personeller SET PersonelCinsiyetByte = CAST(PersonelCinsiyet AS tinyint)');
    ALTER TABLE dbo.Personeller DROP COLUMN PersonelCinsiyet;
    EXEC sp_rename 'dbo.Personeller.PersonelCinsiyetByte', 'PersonelCinsiyet', 'COLUMN';
    ALTER TABLE dbo.Personeller ALTER COLUMN PersonelCinsiyet tinyint NOT NULL;
    PRINT 'Converted PersonelCinsiyet from BIT to TINYINT';
END
ELSE
BEGIN
    PRINT 'PersonelCinsiyet is already not BIT (likely tinyint). No action taken.';
END
GO
