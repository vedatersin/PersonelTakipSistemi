param(
    [string]$Root = ".\PersonelTakipSistemi"
)

$ErrorActionPreference = "Stop"
$utf8Strict = New-Object System.Text.UTF8Encoding($false, $true)
$utf8Bom = New-Object System.Text.UTF8Encoding($true)
$cp1254 = [System.Text.Encoding]::GetEncoding(1254)
$cp1252 = [System.Text.Encoding]::GetEncoding(1252)

$include = @('*.cs', '*.cshtml', '*.js', '*.json', '*.css', '*.xml', '*.config', '*.md')
$files = @()
foreach ($pattern in $include) {
    $files += Get-ChildItem -Path $Root -Recurse -File -Filter $pattern
}
$files = $files | Where-Object {
    $_.FullName -notmatch '\\bin\\|\\obj\\|\\wwwroot\\lib\\|\\wwwroot\\sevilay-tema\\assets\\vendor\\'
} | Sort-Object FullName -Unique

foreach ($file in $files) {
    $bytes = [System.IO.File]::ReadAllBytes($file.FullName)

    $text = $null
    try {
        $text = $utf8Strict.GetString($bytes)
    }
    catch {
        try {
            $text = $cp1254.GetString($bytes)
        }
        catch {
            $text = $cp1252.GetString($bytes)
        }
    }

    while ($text.Length -gt 0 -and $text[0] -eq [char]0xFEFF) {
        $text = $text.Substring(1)
    }

    [System.IO.File]::WriteAllText($file.FullName, $text, $utf8Bom)
}

Write-Host "Encoding normalization completed."
