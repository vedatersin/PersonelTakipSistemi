using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class FixChars {
    static void Main() {
        string path = @"c:\Users\vedat\source\repos\PersonelTakipSistemi\PersonelTakipSistemi\Views\Shared\_TurkeyMap.cshtml";
        string content = File.ReadAllText(path, Encoding.UTF8);

        var map = new Dictionary<string, string> {
            { "AdÄ±yaman", "Adıyaman" },
            { "AÄŸrÄ±", "Ağrı" },
            { "AydÄ±n", "Aydın" },
            { "BalÄ±kesir", "Balıkesir" },
            { "BingÃ¶l", "Bingöl" },
            { "Ã‡anakkale", "Çanakkale" },
            { "Ã‡ankÄ±rÄ±", "Çankırı" },
            { "Ã‡orum", "Çorum" },
            { "DiyarbakÄ±r", "Diyarbakır" },
            { "ElazÄ±ÄŸ", "Elazığ" },
            { "GÃ¼mÃ¼ÅŸhane", "Gümüşhane" },
            { "HakkÃ¢ri", "Hakkari" },
            { "Ä°stanbul", "İstanbul" },
            { "Ä°zmir", "İzmir" },
            { "KahramanmaraÅŸ", "Kahramanmaraş" },
            { "KÄ±rklareli", "Kırklareli" },
            { "KÄ±rÅŸehir", "Kırşehir" },
            { "KÃ¼tahya", "Kütahya" },
            { "MuÄŸla", "Muğla" },
            { "MuÅŸ", "Muş" },
            { "NevÅŸehir", "Nevşehir" },
            { "NiÄŸde", "Niğde" },
            { "ÅžanlÄ±urfa", "Şanlıurfa" },
            { "TekirdaÄŸ", "Tekirdağ" },
            { "UÅŸak", "Uşak" },
            { "ÅžÄ±rnak", "Şırnak" },
            { "KÄ±rÄ±kkale", "Kırıkkale" },
            { "KÄ±lÄ±s", "Kilis" },
            { "DÃ¼zce", "Düzce" }
        };

        foreach (var kvp in map) {
            content = content.Replace(kvp.Key, kvp.Value);
        }

        File.WriteAllText(path, content, new UTF8Encoding(false)); // Write without BOM
    }
}
