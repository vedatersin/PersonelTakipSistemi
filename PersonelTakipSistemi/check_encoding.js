const fs = require('fs');
const c = fs.readFileSync('Views/Personel/Ekle.cshtml', 'utf-8');

// Find patterns that look like remaining mojibake
// ş should be: c5 9f in UTF-8
// In Windows-1252 double-encoding: c5 -> Å (U+00C5, UTF-8: c3 85)
//                                   9f -> ÿ (U+0178 in Win1252? No, 0x9f = Ÿ in Win1252)
// Let me check what we have near "ifre" (from Şifre)
let idx = c.indexOf('ifre');
if (idx > 0) {
    console.log('Before "ifre":', JSON.stringify(c.substring(idx - 5, idx + 10)));
    let charBefore = c.charCodeAt(idx - 1);
    let charBefore2 = c.charCodeAt(idx - 2);
    console.log('Char at idx-1:', charBefore.toString(16), String.fromCharCode(charBefore));
    console.log('Char at idx-2:', charBefore2.toString(16), String.fromCharCode(charBefore2));
}

// Find what replacement pattern is used for ş
// Look for the actual bytes
const buf = fs.readFileSync('Views/Personel/Ekle.cshtml');
let searchStr = Buffer.from('ifre', 'utf-8');
let byteIdx = buf.indexOf(searchStr);
while (byteIdx > 0) {
    console.log('Found "ifre" at byte', byteIdx);
    console.log('  Preceding bytes:', buf[byteIdx - 4].toString(16), buf[byteIdx - 3].toString(16), buf[byteIdx - 2].toString(16), buf[byteIdx - 1].toString(16));
    byteIdx = buf.indexOf(searchStr, byteIdx + 1);
    break; // just first one
}

// Check for Å (c3 85) which is what double-encoded ş first byte looks like
let aIdx = buf.indexOf(Buffer.from([0xc3, 0x85]));
console.log('Found Å (c3 85) at:', aIdx);
if (aIdx > -1) {
    console.log('  Next byte:', buf[aIdx + 2].toString(16));
}

// Try specific: the Windows-1252 double-encoding of ş is Å followed by Ÿ
// Å = c3 85, Ÿ = c5 b8 (no...) 
// Actually, 0x9f in Win-1252 = Ÿ (U+0178). 
// So ş (c5 9f) double-encoded via Win1252 = Å + Ÿ = c3 85 + c5 b8? No...
// Let me just look at what's actually in the file now

// The script already ran and undid one level. So now anything still mojibake 
// is because the byte value > 127 AND < 256 in position was from Win1252 range 80-9f
// which maps to different Unicode codepoints than Latin-1

// Let me restore from backup and try a Win1252-aware approach
console.log('\n--- Checking what remains ---');
const remaining = c.match(/[\u0152\u0153\u0160\u0161\u0178\u017D\u017E\u0192\u02C6\u02DC\u2013\u2014\u2018\u2019\u201A\u201C\u201D\u201E\u2020\u2021\u2022\u2026\u2030\u2039\u203A\u20AC\u2122]/g);
if (remaining) {
    console.log('Win1252 artifacts found:', [...new Set(remaining)].join(', '));
} else {
    console.log('No Win1252 artifacts found');
}

// Let's check what character is used before "ifre" and "le" where ş and ğ should be
const lines = c.split('\n');
for (let i = 0; i < lines.length; i++) {
    if (lines[i].includes('ifre') && !lines[i].includes('//')) {
        console.log(`Line ${i + 1}: ${lines[i].trim().substring(0, 80)}`);
        break;
    }
}
for (let i = 0; i < lines.length; i++) {
    if (lines[i].includes('ifreler') || lines[i].includes('ireler')) {
        if (i < 100) continue; // skip style sections
        console.log(`ğ check Line ${i + 1}: ${lines[i].trim().substring(0, 80)}`);
        break;
    }
}
