$targetPath = 'c:\Users\vedat\source\repos\PersonelTakipSistemi\PersonelTakipSistemi\Views\Shared\_TurkeyMap.cshtml'
$encoding = [System.Text.Encoding]::UTF8
$content = [System.IO.File]::ReadAllText($targetPath, $encoding)

$replacements = @{
    'AdÄ±yaman'      = 'Adıyaman'
    'AÄŸrÄ±'         = 'Ağrı'
    'AydÄ±n'         = 'Aydın'
    'BalÄ±kesir'     = 'Balıkesir'
    'BingÃ¶l'        = 'Bingöl'
    'Ã‡anakkale'     = 'Çanakkale'
    'Ã‡ankÄ±rÄ±'     = 'Çankırı'
    'Ã‡orum'         = 'Çorum'
    'DiyarbakÄ±r'    = 'Diyarbakır'
    'ElazÄ±ÄŸ'       = 'Elazığ'
    'GÃ¼mÃ¼ÅŸhane'   = 'Gümüşhane'
    'HakkÃ¢ri'       = 'Hakkâri'
    'Ä°stanbul'      = 'İstanbul'
    'Ä°zmir'         = 'İzmir'
    'KahramanmaraÅŸ' = 'Kahramanmaraş'
    'KÄ±rklareli'    = 'Kırklareli'
    'KÄ±rÅŸehir'     = 'Kırşehir'
    'KÃ¼tahya'       = 'Kütahya'
    'MuÄŸla'         = 'Muğla'
    'MuÅŸ'           = 'Muş'
    'NevÅŸehir'      = 'Nevşehir'
    'NiÄŸde'         = 'Niğde'
    'ÅžanlÄ±urfa'    = 'Şanlıurfa'
    'TekirdaÄŸ'      = 'Tekirdağ'
    'UÅŸak'          = 'Uşak'
    'ÅžÄ±rnak'       = 'Şırnak'
    'KÄ±rÄ±kkale'    = 'Kırıkkale'
    'KÄ±lÄ±s'        = 'Kilis'
    'DÃ¼zce'         = 'Düzce'
}

foreach ($key in $replacements.Keys) {
    $content = $content.Replace($key, $replacements[$key])
}

[System.IO.File]::WriteAllText($targetPath, $content, $encoding)
