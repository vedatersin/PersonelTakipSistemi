-- 1. Personel Tablosu Kolon Kontrolü
SELECT 
    c.name AS ColumnName,
    t.name AS DataType,
    c.max_length,
    c.is_nullable
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller');

-- 2. FK Kontrolü
SELECT 
    fk.name AS FK_Name,
    OBJECT_NAME(fk.parent_object_id) AS TableName,
    c.name AS ColumnName,
    OBJECT_NAME(fk.referenced_object_id) AS ReferencedTable,
    rc.name AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
INNER JOIN sys.columns rc ON fkc.referenced_object_id = rc.object_id AND fkc.referenced_column_id = rc.column_id
WHERE fk.parent_object_id = OBJECT_ID('dbo.Personeller');

-- 3 & 4. Kayıt Sayıları
SELECT 'Branslar' AS TableName, COUNT(*) AS Count FROM dbo.Branslar
UNION ALL
SELECT 'Iller' AS TableName, COUNT(*) AS Count FROM dbo.Iller;

-- 5. Telefon Tipi Kontrolü (Ayrıca ilk sorguda da görülecek)
SELECT c.name, t.name, c.max_length 
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller') AND c.name = 'Telefon';
