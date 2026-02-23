const fs = require('fs');
const path = 'Views/Personel/Ekle.cshtml';

// Read file as raw buffer
const buf = fs.readFileSync(path);

// Known double-encoded UTF-8 via Windows-1252 patterns for Turkish chars:
// Format: [double-encoded bytes] -> [correct UTF-8 bytes]
const replacements = [
    // ö (U+00F6) = c3 b6 -> double: c3 83 c2 b6 (Ã + ¶)
    { from: [0xc3, 0x83, 0xc2, 0xb6], to: [0xc3, 0xb6] },
    // ü (U+00FC) = c3 bc -> double: c3 83 c2 bc (Ã + ¼)
    { from: [0xc3, 0x83, 0xc2, 0xbc], to: [0xc3, 0xbc] },
    // ç (U+00E7) = c3 a7 -> double: c3 83 c2 a7 (Ã + §)
    { from: [0xc3, 0x83, 0xc2, 0xa7], to: [0xc3, 0xa7] },
    // ı (U+0131) = c4 b1 -> double: c3 84 c2 b1 (Ä + ±)
    { from: [0xc3, 0x84, 0xc2, 0xb1], to: [0xc4, 0xb1] },
    // İ (U+0130) = c4 b0 -> double: c3 84 c2 b0 (Ä + °)
    { from: [0xc3, 0x84, 0xc2, 0xb0], to: [0xc4, 0xb0] },
    // Ö (U+00D6) = c3 96 -> double: c3 83 c2 96 (Ã + – but 96 in win1252 = –)
    // Actually 0x96 in Win1252 = – (U+2013), so double encode = c3 83 + e2 80 93
    // Let's check: Ö = c3 96. Reading c3 as Latin-1/Win1252: Ã (U+00C3). 96 as Win1252: – (U+2013)
    // UTF-8 of Ã = c3 83. UTF-8 of – = e2 80 93
    { from: [0xc3, 0x83, 0xe2, 0x80, 0x93], to: [0xc3, 0x96] },
    // Ü (U+00DC) = c3 9c -> double: c3 83 + œ(U+0153, win1252 0x9c)
    // UTF-8 of œ = c5 93
    { from: [0xc3, 0x83, 0xc5, 0x93], to: [0xc3, 0x9c] },
    // Ç (U+00C7) = c3 87 -> double: c3 83 + ‡(U+2021, win1252 0x87)
    // UTF-8 of ‡ = e2 80 a1
    { from: [0xc3, 0x83, 0xe2, 0x80, 0xa1], to: [0xc3, 0x87] },
    // ş (U+015F) = c5 9f -> double: c3 85 + Ÿ(U+0178, win1252 0x9f)
    // Å = c3 85. Ÿ = c5 b8
    { from: [0xc3, 0x85, 0xc5, 0xb8], to: [0xc5, 0x9f] },
    // Ş (U+015E) = c5 9e -> double: c3 85 + ž(U+017E, win1252 0x9e)
    // ž = c5 be
    { from: [0xc3, 0x85, 0xc5, 0xbe], to: [0xc5, 0x9e] },
    // ğ (U+011F) = c4 9f -> double: c3 84 + Ÿ(U+0178, win1252 0x9f)
    // c3 84 + c5 b8
    { from: [0xc3, 0x84, 0xc5, 0xb8], to: [0xc4, 0x9f] },
    // Ğ (U+011E) = c4 9e -> double: c3 84 + ž(U+017E, win1252 0x9e)
    // c3 84 + c5 be
    { from: [0xc3, 0x84, 0xc5, 0xbe], to: [0xc4, 0x9e] },
    // â (U+00E2) = c3 a2 -> double: c3 83 c2 a2
    { from: [0xc3, 0x83, 0xc2, 0xa2], to: [0xc3, 0xa2] },
    // é (U+00E9) = c3 a9 -> double: c3 83 c2 a9
    { from: [0xc3, 0x83, 0xc2, 0xa9], to: [0xc3, 0xa9] },
];

let result = Buffer.from(buf);
let totalReplacements = 0;

for (const r of replacements) {
    const fromBuf = Buffer.from(r.from);
    const toBuf = Buffer.from(r.to);
    let count = 0;

    // Find and replace all occurrences
    let newParts = [];
    let lastEnd = 0;
    let searchStart = 0;

    while (searchStart <= result.length - fromBuf.length) {
        let idx = result.indexOf(fromBuf, searchStart);
        if (idx === -1) break;

        newParts.push(result.slice(lastEnd, idx));
        newParts.push(toBuf);
        lastEnd = idx + fromBuf.length;
        searchStart = lastEnd;
        count++;
    }

    if (count > 0) {
        newParts.push(result.slice(lastEnd));
        result = Buffer.concat(newParts);
        console.log(`Replaced ${count}x: [${r.from.map(b => b.toString(16)).join(' ')}] -> [${r.to.map(b => b.toString(16)).join(' ')}]`);
        totalReplacements += count;
    }
}

console.log(`\nTotal replacements: ${totalReplacements}`);

// Verify
const content = result.toString('utf-8');
console.log('\n=== Verification ===');
const chars = ['ö', 'ü', 'ş', 'ç', 'İ', 'ğ', 'ı', 'Ş', 'Ğ', 'Ö'];
chars.forEach(c => console.log(`  ${c}: ${content.includes(c) ? 'OK' : 'MISSING'}`));

const words = ['Görevli', 'İlçe', 'Seçiniz', 'Kişisel', 'Doğum'];
words.forEach(w => console.log(`  ${w}: ${content.includes(w) ? 'OK' : 'MISSING'}`));

// Write
fs.writeFileSync(path, result);
console.log('\nFile written. Original size:', buf.length, '-> New size:', result.length);
