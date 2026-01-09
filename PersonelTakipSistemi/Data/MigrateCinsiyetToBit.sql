-- 1. Check for invalid values (Just for verification, manual check in log)
SELECT PersonelCinsiyet, COUNT(*) AS Cnt
FROM dbo.Personeller
GROUP BY PersonelCinsiyet;
GO

-- 2. Normalize Data (Ensure only 0 or 1 exists)
UPDATE dbo.Personeller
SET PersonelCinsiyet = CASE WHEN PersonelCinsiyet = 1 THEN 1 ELSE 0 END
WHERE PersonelCinsiyet NOT IN (0,1);
GO

-- 3. Add new BIT column
ALTER TABLE dbo.Personeller ADD PersonelCinsiyetBit bit NULL;
GO

-- 4. Migrate data (1 -> 1, 0 -> 0)
UPDATE dbo.Personeller
SET PersonelCinsiyetBit = CAST(PersonelCinsiyet AS bit);
GO

-- 5. Drop old column
ALTER TABLE dbo.Personeller DROP COLUMN PersonelCinsiyet;
GO

-- 6. Rename new column to old name
EXEC sp_rename 'dbo.Personeller.PersonelCinsiyetBit', 'PersonelCinsiyet', 'COLUMN';
GO

-- 7. Set Not Null constraint
ALTER TABLE dbo.Personeller ALTER COLUMN PersonelCinsiyet bit NOT NULL;
GO

-- 8. Verify
SELECT TOP 20 PersonelId, PersonelCinsiyet
FROM dbo.Personeller
ORDER BY PersonelId DESC;
GO
