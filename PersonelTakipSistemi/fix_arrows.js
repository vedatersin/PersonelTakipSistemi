const fs = require('fs');
const path = 'Views/Personel/Ekle.cshtml';

let content = fs.readFileSync(path, 'utf-8');

// Track replacements
let count = 0;

// Pattern 1: () => { ... } (no params arrow with block body)
// Replace: () => { with function() {
content = content.replace(/\(\)\s*=>\s*\{/g, function (match) {
    count++;
    return 'function() {';
});

// Pattern 2: () => "string" or () => expression (no params arrow with expression body)
// Replace: () => expr with function() { return expr; }
// This is tricky - need to handle inline returns like: noResults: () => "text"
content = content.replace(/\(\)\s*=>\s*(".*?")/g, function (match, expr) {
    count++;
    return 'function() { return ' + expr + '; }';
});

// Pattern 3: (params) => { (arrow with params and block body)
// e.g., (btnId, currentTabId, checkBoxName) => {
content = content.replace(/\(([^)]+)\)\s*=>\s*\{/g, function (match, params) {
    // Skip if it looks like a Razor/C# expression (contains @ or asp-)
    if (params.includes('@') || params.includes('asp-')) return match;
    count++;
    return 'function(' + params + ') {';
});

// Pattern 4: singleParam => { (single param arrow without parens)
// e.g., g => { or item => {
content = content.replace(/\b([a-zA-Z_$][a-zA-Z0-9_$]*)\s*=>\s*\{/g, function (match, param) {
    // Skip common non-arrow patterns 
    // Check if it's inside a script section contextually
    count++;
    return 'function(' + param + ') {';
});

// Pattern 5: singleParam => expression (single param arrow with expression body)
// e.g., g => g.Name
content = content.replace(/\b([a-zA-Z_$][a-zA-Z0-9_$]*)\s*=>\s*([^{;\n][^;\n]*)/g, function (match, param, expr) {
    // Be careful not to match C# lambda expressions in Razor blocks
    // Only match if not inside @{ } or @foreach etc.
    // This is risky - skip this pattern as it could match Razor C# code
    return match;
});

fs.writeFileSync(path, content);
console.log('Replacements made:', count);

// Verify no arrow functions remain in JS context
const remaining = content.match(/=>\s/g);
console.log('Remaining => occurrences:', remaining ? remaining.length : 0);

// Show remaining locations
if (remaining) {
    const re2 = /=>\s/g;
    let m2;
    while (m2 = re2.exec(content)) {
        const line = content.substring(0, m2.index).split('\n').length;
        const ctx = content.substring(m2.index - 15, m2.index + 5).replace(/\n/g, '\\n').replace(/\r/g, '');
        console.log('  Line', line, ':', ctx);
    }
}
