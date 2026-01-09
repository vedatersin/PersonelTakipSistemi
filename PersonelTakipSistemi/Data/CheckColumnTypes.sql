SELECT 
  DB_NAME() AS CurrentDb,
  c.name AS ColumnName,
  t.name AS DataType,
  c.is_nullable
FROM sys.columns c
JOIN sys.types t ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('dbo.Personeller')
  AND c.name IN ('PersonelCinsiyet','AktifMi');
GO
