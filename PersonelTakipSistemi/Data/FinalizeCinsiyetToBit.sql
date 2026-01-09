-- 0) Verify DB
SELECT DB_NAME() AS CurrentDb;

-- 1) Idempotent Migration
IF COL_LENGTH('dbo.Personeller', 'PersonelCinsiyetBit') IS NULL
BEGIN
    ALTER TABLE dbo.Personeller ADD PersonelCinsiyetBit bit NULL;
    PRINT 'Added temporary column [PersonelCinsiyetBit]';
END

UPDATE dbo.Personeller
SET PersonelCinsiyetBit = CASE WHEN PersonelCinsiyet = 1 THEN 1 ELSE 0 END
WHERE PersonelCinsiyetBit IS NULL;
PRINT 'Data migrated to [PersonelCinsiyetBit]';

IF COL_LENGTH('dbo.Personeller', 'PersonelCinsiyet') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Personeller DROP COLUMN PersonelCinsiyet;
    PRINT 'Dropped old column [PersonelCinsiyet]';
END

EXEC sp_rename 'dbo.Personeller.PersonelCinsiyetBit', 'PersonelCinsiyet', 'COLUMN';
PRINT 'Renamed [PersonelCinsiyetBit] to [PersonelCinsiyet]';

ALTER TABLE dbo.Personeller ALTER COLUMN PersonelCinsiyet bit NOT NULL;
PRINT 'Applied NOT NULL constraint to [PersonelCinsiyet]';

-- Verify
SELECT 
    c.name AS ColumnName, 
    t.name AS DataType, 
    c.is_nullable
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller')
  AND c.name LIKE 'PersonelCinsiyet%';
GO
