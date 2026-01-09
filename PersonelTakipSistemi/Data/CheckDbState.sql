-- A) Prove Active DB
SELECT @@SERVERNAME AS ServerName, DB_NAME() AS CurrentDb;
SELECT name FROM sys.databases WHERE name LIKE '%Tegm%';

-- B1) Check Columns
SELECT c.name, t.name AS DataType, c.is_nullable
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller')
  AND c.name LIKE 'PersonelCinsiyet%';

-- B2) Check Constraints
SELECT 
    con.name AS ConstraintName,
    con.type_desc AS ConstraintType
FROM sys.objects con
JOIN sys.sysconstraints sc ON con.object_id = sc.constid
JOIN sys.columns col ON sc.colid = col.column_id AND sc.id = col.object_id
WHERE col.object_id = OBJECT_ID('dbo.Personeller')
  AND col.name = 'PersonelCinsiyet';

-- Check Indexes
SELECT i.name AS IndexName, i.type_desc
FROM sys.indexes i
JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns c ON c.object_id = i.object_id AND c.column_id = ic.column_id
WHERE i.object_id = OBJECT_ID('dbo.Personeller')
  AND c.name = 'PersonelCinsiyet';
