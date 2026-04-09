$ErrorActionPreference = 'Stop'

param(
    [string]$ProjectRoot = "C:\Users\vedat\source\repos\PersonelTakipSistemi\PersonelTakipSistemi",
    [string]$ConnectionString = "Server=localhost\MSSQL2022;Database=TegmPersonelTakipDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;",
    [switch]$WhatIf
)

$cp1252 = [System.Text.Encoding]::GetEncoding(1252)
$utf8NoBom = [System.Text.UTF8Encoding]::new($false)
$suspiciousChars = @(
    [char]0x00C2, # Â
    [char]0x00C3, # Ã
    [char]0x00C4, # Ä
    [char]0x00C5, # Å
    [char]0x00E2, # â
    [char]0x0153, # œ
    [char]0x0178, # Ÿ
    [char]0x0192  # ƒ
)

function Get-MojibakeScore {
    param([string]$Value)

    if ([string]::IsNullOrEmpty($Value)) { return 0 }

    $score = 0
    foreach ($char in $suspiciousChars) {
        $score += ([regex]::Matches($Value, [regex]::Escape([string]$char))).Count
    }

    return $score
}

function Repair-MojibakeText {
    param([string]$Value)

    if ([string]::IsNullOrEmpty($Value)) { return $Value }

    $current = $Value
    for ($i = 0; $i -lt 4; $i++) {
        $currentScore = Get-MojibakeScore $current
        if ($currentScore -eq 0) { break }

        try {
            $candidate = [System.Text.Encoding]::UTF8.GetString($cp1252.GetBytes($current))
        }
        catch {
            break
        }

        $candidateScore = Get-MojibakeScore $candidate
        if ($candidateScore -ge $currentScore -or $candidate -eq $current) {
            break
        }

        $current = $candidate
    }

    return $current
}

function Get-PrimaryKeyColumn {
    param(
        [System.Data.SqlClient.SqlConnection]$Connection,
        [string]$SchemaName,
        [string]$TableName
    )

    $sql = @"
SELECT TOP (1) c.name
FROM sys.key_constraints kc
JOIN sys.index_columns ic
  ON ic.object_id = kc.parent_object_id
 AND ic.index_id = kc.unique_index_id
JOIN sys.columns c
  ON c.object_id = ic.object_id
 AND c.column_id = ic.column_id
JOIN sys.tables t
  ON t.object_id = kc.parent_object_id
JOIN sys.schemas s
  ON s.schema_id = t.schema_id
WHERE kc.type = 'PK'
  AND s.name = @schema
  AND t.name = @table
ORDER BY ic.key_ordinal;
"@

    $cmd = $Connection.CreateCommand()
    $cmd.CommandText = $sql
    $null = $cmd.Parameters.Add("@schema", [System.Data.SqlDbType]::NVarChar, 128)
    $cmd.Parameters["@schema"].Value = $SchemaName
    $null = $cmd.Parameters.Add("@table", [System.Data.SqlDbType]::NVarChar, 128)
    $cmd.Parameters["@table"].Value = $TableName
    return $cmd.ExecuteScalar()
}

function Get-StringColumns {
    param([System.Data.SqlClient.SqlConnection]$Connection)

    $sql = @"
SELECT s.name AS SchemaName, t.name AS TableName, c.name AS ColumnName
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
JOIN sys.columns c ON c.object_id = t.object_id
JOIN sys.types ty ON ty.user_type_id = c.user_type_id
WHERE ty.name IN ('varchar','nvarchar','char','nchar','text','ntext')
ORDER BY s.name, t.name, c.column_id;
"@

    $cmd = $Connection.CreateCommand()
    $cmd.CommandText = $sql
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($cmd)
    $table = New-Object System.Data.DataTable
    [void]$adapter.Fill($table)
    return $table
}

function Update-DbMojibake {
    param([string]$ConnStr)

    $connection = New-Object System.Data.SqlClient.SqlConnection($ConnStr)
    $connection.Open()

    try {
        $columns = Get-StringColumns -Connection $connection
        $pkCache = @{}
        $changes = New-Object System.Collections.Generic.List[object]

        foreach ($row in $columns.Rows) {
            $schema = [string]$row.SchemaName
            $table = [string]$row.TableName
            $column = [string]$row.ColumnName
            $tableKey = "$schema.$table"

            if (-not $pkCache.ContainsKey($tableKey)) {
                $pkCache[$tableKey] = Get-PrimaryKeyColumn -Connection $connection -SchemaName $schema -TableName $table
            }

            $pkColumn = $pkCache[$tableKey]
            if ([string]::IsNullOrWhiteSpace($pkColumn)) { continue }

            $scanSql = "SELECT [$pkColumn] AS RowId, CAST([$column] AS nvarchar(max)) AS Value FROM [$schema].[$table] WHERE [$column] IS NOT NULL;"
            $scanCmd = $connection.CreateCommand()
            $scanCmd.CommandText = $scanSql
            $reader = $scanCmd.ExecuteReader()

            try {
                while ($reader.Read()) {
                    $raw = $reader["Value"].ToString()
                    $fixed = Repair-MojibakeText $raw
                    if ($fixed -ne $raw) {
                        $changes.Add([pscustomobject]@{
                            SchemaName = $schema
                            TableName  = $table
                            ColumnName = $column
                            RowId      = $reader["RowId"]
                            Before     = $raw
                            After      = $fixed
                            PkColumn   = $pkColumn
                        })
                    }
                }
            }
            finally {
                $reader.Close()
            }
        }

        foreach ($change in $changes) {
            if (-not $WhatIf) {
                $updateSql = "UPDATE [$($change.SchemaName)].[$($change.TableName)] SET [$($change.ColumnName)] = @value WHERE [$($change.PkColumn)] = @id;"
                $updateCmd = $connection.CreateCommand()
                $updateCmd.CommandText = $updateSql
                $null = $updateCmd.Parameters.Add("@value", [System.Data.SqlDbType]::NVarChar, -1)
                $updateCmd.Parameters["@value"].Value = $change.After
                $null = $updateCmd.Parameters.Add("@id", [System.Data.SqlDbType]::Variant)
                $updateCmd.Parameters["@id"].Value = $change.RowId
                [void]$updateCmd.ExecuteNonQuery()
            }
        }

        return $changes
    }
    finally {
        $connection.Close()
        $connection.Dispose()
    }
}

$extensions = @('*.cs', '*.cshtml', '*.js', '*.json', '*.sql')
$textFiles = Get-ChildItem -Path $ProjectRoot -Recurse -File -Include $extensions |
    Where-Object {
        $_.FullName -notmatch '\\bin\\' -and
        $_.FullName -notmatch '\\obj\\'
    }

$fileChanges = New-Object System.Collections.Generic.List[object]

foreach ($file in $textFiles) {
    $original = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)
    $fixed = Repair-MojibakeText $original
    if ($fixed -ne $original) {
        $fileChanges.Add([pscustomobject]@{
            Path = $file.FullName
        })

        if (-not $WhatIf) {
            [System.IO.File]::WriteAllText($file.FullName, $fixed, $utf8NoBom)
        }
    }
}

$dbChanges = Update-DbMojibake -ConnStr $ConnectionString

"Files changed: $($fileChanges.Count)"
$fileChanges | ForEach-Object { $_.Path }
"DB values changed: $($dbChanges.Count)"
$dbChanges | ForEach-Object {
    "{0}.{1}.{2} [{3}={4}] :: {5} -> {6}" -f $_.SchemaName, $_.TableName, $_.ColumnName, $_.PkColumn, $_.RowId, $_.Before, $_.After
}
