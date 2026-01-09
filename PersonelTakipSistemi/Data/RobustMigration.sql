BEGIN TRY
    BEGIN TRAN;

    -- 1) Bit kolon yoksa ekle
    IF COL_LENGTH('dbo.Personeller', 'PersonelCinsiyetBit') IS NULL
    BEGIN
        ALTER TABLE dbo.Personeller ADD PersonelCinsiyetBit bit NULL;
    END

    -- 2) Bit kolonunu doldur
    UPDATE dbo.Personeller
    SET PersonelCinsiyetBit = CASE WHEN PersonelCinsiyet = 1 THEN 1 ELSE 0 END
    WHERE PersonelCinsiyetBit IS NULL;

    -- 3) Eğer PersonelCinsiyet üzerinde constraint/index varsa kaldır
    DECLARE @sql NVARCHAR(MAX) = N'';

    -- Default/Check vb constraint’leri drop et
    SELECT @sql = @sql + N'ALTER TABLE dbo.Personeller DROP CONSTRAINT [' + con.name + N'];' + CHAR(10)
    FROM sys.objects con
    JOIN sys.sysconstraints sc ON con.object_id = sc.constid
    JOIN sys.columns col ON sc.colid = col.column_id AND sc.id = col.object_id
    WHERE col.object_id = OBJECT_ID('dbo.Personeller')
      AND col.name = 'PersonelCinsiyet';

    -- Indexleri drop et
    SELECT @sql = @sql + N'DROP INDEX [' + i.name + N'] ON dbo.Personeller;' + CHAR(10)
    FROM sys.indexes i
    JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
    JOIN sys.columns c ON c.object_id = i.object_id AND c.column_id = ic.column_id
    WHERE i.object_id = OBJECT_ID('dbo.Personeller')
      AND c.name = 'PersonelCinsiyet'
      AND i.name IS NOT NULL;

    IF (@sql <> N'') EXEC sp_executesql @sql;

    -- 4) Eski tinyint kolonunu sil
    IF COL_LENGTH('dbo.Personeller', 'PersonelCinsiyet') IS NOT NULL
    BEGIN
        ALTER TABLE dbo.Personeller DROP COLUMN PersonelCinsiyet;
    END

    -- 5) Bit kolonunu asıl isme çevir
    EXEC sp_rename 'dbo.Personeller.PersonelCinsiyetBit', 'PersonelCinsiyet', 'COLUMN';

    -- 6) NOT NULL yap
    ALTER TABLE dbo.Personeller ALTER COLUMN PersonelCinsiyet bit NOT NULL;

    COMMIT TRAN;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;

    SELECT 
      ERROR_NUMBER() AS ErrorNumber,
      ERROR_MESSAGE() AS ErrorMessage,
      ERROR_LINE() AS ErrorLine;
END CATCH

-- D) KANIT
SELECT c.name, t.name AS DataType, c.is_nullable
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller')
  AND c.name LIKE 'PersonelCinsiyet%';
