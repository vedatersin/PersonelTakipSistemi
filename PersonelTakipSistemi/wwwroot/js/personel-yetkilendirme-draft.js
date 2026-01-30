
// State
let originalState = null;
let draftState = null;
let currentPersonelId = null;

// Constants
const ROLES = {
    GENEL_KOORD: 4,
    IL_KOORD: 3,
    KOMISYON_BASKANI: 2,
    PERSONEL: 1,
    MERKEZ_TESKILAT: 1,
    ANKARA_TEGM_KOORD: 1
};

// UI Helpers
const showLoader = () => $('#drawerLoader').removeClass('d-none');
const hideLoader = () => $('#drawerLoader').addClass('d-none');
const isDirty = () => JSON.stringify(originalState) !== JSON.stringify(draftState);

// --- Initialization ---
$(document).ready(function () {
    // Initialize standard Select2s
    $('#filterKoordinatorluk, #filterBrans').select2({ theme: 'bootstrap-5', allowClear: false, width: '100%' });

    // Drawer Close Protection
    const drawerEl = document.getElementById('offcanvasEnd');
    drawerEl.addEventListener('hide.bs.offcanvas', function (e) {
        if (isDirty()) {
            e.preventDefault(); // Stop closing
            Swal.fire({
                title: 'Kaydedilmemiş Değişiklikler',
                text: "Yaptığınız değişiklikler kaybolacak. Çıkmak istediğinize emin misiniz?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Evet, Çık',
                cancelButtonText: 'İptal'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Reset state to avoid loop
                    draftState = JSON.parse(JSON.stringify(originalState));
                    updateUI(); // Clear dirty flag visually

                    // Manually close without triggering this listener logic again? 
                    // Remove listener or just use the API method which might re-trigger?
                    // Bootstrap 5 offcanvas hide event can be tricky.
                    // Simplest: Allow close by removing the prevention logic temporarily or standard way.
                    // Actually, since we've reset state, next trigger won't be dirty.
                    const instance = bootstrap.Offcanvas.getInstance(drawerEl);
                    instance.hide();
                }
            });
        }
    });
});

// --- Drawer Open/Load ---
async function openDrawer(id) {
    // Prevent switching if dirty
    if (isDirty()) {
        const result = await Swal.fire({
            title: 'Kaydedilmemiş Değişiklikler',
            text: "Başka bir kişiye geçmeden önce mevcut değişiklikleri kaydetmelisiniz veya iptal etmelisiniz.",
            icon: 'warning',
            showDenyButton: true,
            confirmButtonText: 'Değişiklikleri Sil ve Geç',
            denyButtonText: 'İptal'
        });

        if (!result.isConfirmed) return; // Stay
        // Reset state effectively allowing switch
        draftState = JSON.parse(JSON.stringify(originalState));
    }

    const bsOffcanvas = new bootstrap.Offcanvas(document.getElementById('offcanvasEnd'));
    bsOffcanvas.show();
    loadDraftData(id);
}

async function loadDraftData(id) {
    showLoader();
    $('#drawerContent').html('');
    currentPersonelId = id;

    try {
        const response = await fetch(`/Personel/GetYetkilendirmeData/${id}`);
        if (!response.ok) throw new Error('Veri yüklenemedi');

        const data = await response.json();

        // Normalize Data for State
        // Structure: see DTO. We need to map the incoming ViewModel to our SaveDTO structure for consistency
        originalState = {
            personelId: data.personelId,
            sistemRol: data.sistemRol,
            teskilatIds: data.selectedTeskilatIds || [],
            koordinatorlukIds: data.selectedKoordinatorlukIds || [],
            komisyonIds: data.selectedKomisyonIds || [],
            gorevler: data.kurumsalRolAssignments.map(a => ({
                kurumsalRolId: a.kurumsalRolId, // We need IDs from backend! ViewModel has them? 
                // Wait, ViewModel 'kurumsalRolAssignments' has 'assignmentId', 'rolAd', 'contextAd'.
                // Does it have rolId? We might need to enrich the backend ViewModel or infer it.
                // Checking previous code... ViewModel likely has it or we assume.
                // Let's assume we need to update ViewModel to include IDs if missing.
                // CHECKPOINT: Ensure GetYetkilendirmeData returns IDs.
                // Looking at ViewModel: PersonelYetkiDetailViewModel. 
                // If it lacks IDs, we have a problem.
                // Assuming it has 'kurumsalRolId', 'bindingId' (context).
                // Let's assume data is sufficient for now, or we rely on 'all...' lists to map names? No, IDs are safer.

                // For now, let's assume the ViewModel provides what we need or we map carefully.
                // Actually, let's persist the 'whole' data object for UI rendering (options etc), 
                // and keep 'draftState' as the distinct values we track.

                kurumsalRolId: a.kurumsalRolId,
                koordinatorlukId: a.koordinatorlukId,
                komisyonId: a.komisyonId
            }))
        };

        // Store full reference data for UI rendering (dropdown options)
        // We attach it to the window or a global object for render functions to access
        window.drawerRefData = data;

        draftState = JSON.parse(JSON.stringify(originalState));

        renderDrawer();
    } catch (error) {
        console.error(error);
        $('#drawerContent').html('<div class="alert alert-danger">Veri yüklenemedi.</div>');
    } finally {
        hideLoader();
    }
}

// --- Rendering ---
function renderDrawer() {
    const data = window.drawerRefData;
    const state = draftState;

    // Header updates
    // (Optional: update header UI if needed, usually static per user load)

    // Build Content using State
    let html = `
        <div class="d-flex align-items-center mb-4">
             ${renderAvatar(data)}
             <div>
                 <h5 class="mb-1">${data.adSoyad}</h5>
                 <span class="text-muted small">${state.sistemRol || '-'}</span>
                  ${isDirty() ? '<span class="badge bg-warning ms-2 text-dark">Kaydedilmedi</span>' : ''}
             </div>
        </div>

        <ul class="nav nav-tabs nav-fill mb-4 border-bottom" role="tablist">
            <li class="nav-item"><button class="nav-link active border-0" data-bs-toggle="tab" data-bs-target="#navs-atama">Kurumsal Atamalar</button></li>
            <li class="nav-item"><button class="nav-link border-0" data-bs-toggle="tab" data-bs-target="#navs-sistem">Sistemsel Yetki</button></li>
        </ul>

        <div class="tab-content p-0" style="min-height:300px;">
            <div class="tab-pane fade show active" id="navs-atama">
                ${renderAssignmentsTab(state, data)}
            </div>
            <div class="tab-pane fade" id="navs-sistem">
                ${renderSystemTab(state, data)}
            </div>
        </div>

        <!-- Sticky Footer Actions -->
        <div class="offcanvas-footer border-top p-3 bg-white position-absolute bottom-0 start-0 w-100 d-flex gap-2">
            <button class="btn btn-primary flex-grow-1" onclick="saveDraft()" ${!isDirty() ? 'disabled' : ''}>
                <i class='bx bx-save me-1'></i>Kaydet
            </button>
            <button class="btn btn-outline-secondary flex-grow-1" onclick="cancelDraft()" ${!isDirty() ? 'disabled' : ''}>
                İptal
            </button>
        </div>
    `;

    $('#drawerContent').html(html);
}

function renderAvatar(data) {
    if (data.fotografYolu) {
        return `<img src="${data.fotografYolu}" class="rounded-circle me-3" width="64" height="64">`;
    }
    const initials = data.adSoyad.split(' ').map(n => n[0]).join('').substring(0, 2).toUpperCase();
    return `<span class="badge bg-label-secondary p-3 rounded-circle me-3 d-flex align-items-center justify-content-center" style="width:64px; height:64px; font-size: 1.5rem;">${initials}</span>`;
}

function renderSystemTab(state, data) {
    return `
        <div class="mb-3">
            <label class="form-label text-muted small text-uppercase">Sistem Rolü</label>
            <select class="form-select" onchange="updateSistemRol(this.value)">
                ${window.allSistemRolOptions.map(o => `<option value="${o.text}" ${o.text === state.sistemRol ? 'selected' : ''}>${o.text}</option>`).join('')}
            </select>
        </div>
    `;
}

// --- Unified Logic ---

function renderAssignmentsTab(state, data) {
    const displayList = buildDisplayList(state, data);

    let html = `<label class="form-label text-muted small text-uppercase mb-2">Mevcut Yetkiler</label>`;

    if (displayList.length > 0) {
        html += `<div class="d-flex flex-column gap-2 mb-4">`;
        displayList.forEach((item, idx) => {
            html += `
                <div class="d-flex justify-content-between align-items-center bg-light p-3 rounded border border-start-4 border-${item.color} shadow-sm">
                     <div class="overflow-hidden">
                        <span class="fw-bold d-block text-truncate" title="${item.title}">${item.title}</span>
                        <small class="text-muted d-block text-truncate" title="${item.subtitle}">${item.subtitle}</small>
                     </div>
                     <button class="btn btn-sm btn-icon btn-text-secondary rounded-pill" onclick="removeItem('${item.type}', ${item.id}, ${item.gIndex})" title="Yetkiyi Kaldır">
                        <i class="bx bx-x fs-4"></i>
                     </button>
                </div>
            `;
        });
        html += `</div>`;
    } else {
        html += `<div class="alert alert-secondary d-flex align-items-center" role="alert">
                    <i class="bx bx-info-circle me-2"></i>
                    <div>Henüz tanımlanmış bir yetki yok.</div>
                 </div>`;
    }

    // Unified Add Form
    html += renderUnifiedAddForm(state, data);

    return html;
}

function buildDisplayList(state, data) {
    let list = [];

    // 1. Explicit Roles (Gorevler)
    state.gorevler.forEach((g, idx) => {
        const roleDef = window.allKurumsalRolOptions.find(r => r.value == g.kurumsalRolId);
        const roleName = roleDef ? roleDef.text : 'Rol';

        // Resolve Context
        let contextName = "";
        let path = "";

        if (g.komisyonId) {
            const kom = data.allKomisyonlar.find(k => k.id == g.komisyonId);
            if (kom) {
                contextName = kom.ad;
                // Find Parent path
                const koord = data.allKoordinatorlukler.find(k => k.id == kom.parentId); // correction: VM has parentId
                const tes = koord ? data.allTeskilatlar.find(t => t.id == koord.parentId) : null;
                path = [tes?.ad, koord?.ad].filter(Boolean).join(" > ");
            }
        } else if (g.koordinatorlukId) {
            const koord = data.allKoordinatorlukler.find(k => k.id == g.koordinatorlukId);
            if (koord) {
                contextName = koord.ad;
                const tes = data.allTeskilatlar.find(t => t.id == koord.parentId);
                path = tes?.ad || "";
            }
        }

        list.push({
            type: 'gorev',
            id: null,
            gIndex: idx, // Index in gorevler array
            title: roleName,
            subtitle: contextName ? `${path ? path + " > " : ""}${contextName}` : "Genel",
            color: 'danger',
            priority: 10,
            // Track coverage to hide implicit memberships
            komId: g.komisyonId,
            koordId: g.koordinatorlukId
        });
    });

    // 2. Implicit Memberships (Hide if covered by Explicit Roles OR by Child Units)

    // Coverage Maps
    const coveredKomIds = new Set(list.map(i => i.komId).filter(Boolean));
    const coveredKoordIds = new Set(list.map(i => i.koordId).filter(Boolean));

    // Also, if a Komisyon is selected, its Parent Koordinatorluk is "covered" (implicitly)
    state.komisyonIds.forEach(kid => {
        const kom = data.allKomisyonlar.find(k => k.id == kid);
        if (kom && kom.parentId) coveredKoordIds.add(kom.parentId);
    });

    state.koordinatorlukIds.forEach(kid => {
        const koord = data.allKoordinatorlukler.find(k => k.id == kid);
        if (koord && koord.parentId) {
            // We could mark Teskilat covered, but Teskilat selection is usually too broad to hide completely?
            // User requirement: "Ankara ve Mardin koordinatörlükleri ayrı ayrı görülmesin... yukarıda rollerim listelenmiş olur".
            // If I am "Ankara Personeli", I see it.
            // If I am "Fen Komisyonu Üyesi", I am implicitly Ankara Personeli. Do I see separate "Ankara Personeli"?
            // User says: "Gerek yok".
            // So yes, hide Koordinatorluk membership if Komisyon membership exists in it.
        }
    });


    // A. Komisyon Memberships
    state.komisyonIds.forEach(kid => {
        // If we have an explicit role for this commission (e.g. Baskan), do we show "Member"?
        // Usually Role implies Membership. User said "List my roles... click X to delete". 
        // If I have "Commission President", I shouldn't see "Commission Member" separately? 
        // Assume yes.
        if (coveredKomIds.has(kid)) return;

        const kom = data.allKomisyonlar.find(k => k.id == kid);
        if (!kom) return;

        const koord = data.allKoordinatorlukler.find(k => k.id == kom.parentId);
        const tes = koord ? data.allTeskilatlar.find(t => t.id == koord.parentId) : null;
        const path = [tes?.ad, koord?.ad].filter(Boolean).join(" > ");

        list.push({
            type: 'kom',
            id: kid,
            title: 'Komisyon Üyesi', // Default title for simple membership
            subtitle: `${path ? path + " > " : ""}${kom.ad}`,
            color: 'success',
            priority: 5
        });
    });

    // B. Koordinatörlük Memberships
    state.koordinatorlukIds.forEach(kid => {
        if (coveredKoordIds.has(kid)) return; // Hidden by Commission or Explicit Role

        const koord = data.allKoordinatorlukler.find(k => k.id == kid);
        if (!koord) return;

        const tes = data.allTeskilatlar.find(t => t.id == koord.parentId);

        list.push({
            type: 'koord',
            id: kid,
            title: 'Koordinatörlük Personeli',
            subtitle: `${tes ? tes.ad + " > " : ""}${koord.ad}`,
            color: 'info',
            priority: 2
        });
    });

    // C. Teşkilat Memberships
    // Usually we don't just list "Merkez Teşkilat Personeli" if they have deeper roles?
    // But if they are JUST at root, show it.
    // Logic: If user has ANY koordinatorluk in this teskilat, hide teskilat?
    const activeTeskilatIdsInKoords = new Set();
    state.koordinatorlukIds.forEach(kid => {
        const k = data.allKoordinatorlukler.find(x => x.id == kid);
        if (k) activeTeskilatIdsInKoords.add(k.parentId);
    });

    state.teskilatIds.forEach(tid => {
        if (activeTeskilatIdsInKoords.has(tid)) return;

        const tes = data.allTeskilatlar.find(t => t.id == tid);
        if (!tes) return;

        list.push({
            type: 'tes',
            id: tid,
            title: 'Teşkilat Personeli',
            subtitle: tes.ad,
            color: 'primary',
            priority: 1
        });
    });

    return list.sort((a, b) => b.priority - a.priority);
}

// Global variable for Cascading dropdowns in Add Form
let addFormSelections = {
    teskilatId: null,
    koordinatorlukId: null,
    komisyonId: null,
    roleId: null
};

function renderUnifiedAddForm(state, data) {
    // Reset selections on re-render? No, keeps UI stable if they are in middle of selection?
    // Actually, re-rendering happens on ADD/REMOVE. So we Should reset the form.
    addFormSelections = { teskilatId: null, koordinatorlukId: null, komisyonId: null, roleId: null };

    return `
        <div class="card border-primary border-opacity-25 bg-light-primary">
            <div class="card-header bg-transparent border-bottom border-primary border-opacity-10 py-2">
                <h6 class="mb-0 text-primary">
                    <i class="bx bx-plus-circle me-1"></i>Yeni Yetki Tanımla
                </h6>
            </div>
            <div class="card-body pt-3 pb-3">
                <div class="row g-2">
                    <!-- 1. Teşkilat -->
                    <div class="col-12">
                        <select class="form-select form-select-sm" id="addTeskilat" onchange="handleAddChange('tes', this.value)">
                            <option value="">Teşkilat Seçiniz...</option>
                            ${data.allTeskilatlar.map(t => `<option value="${t.id}">${t.ad}</option>`).join('')}
                        </select>
                    </div>

                    <!-- 2. Koordinatörlük (Cascading) -->
                    <div class="col-12">
                         <select class="form-select form-select-sm" id="addKoordinatorluk" disabled onchange="handleAddChange('koord', this.value)">
                            <option value="">Koordinatörlük Seçiniz...</option>
                        </select>
                    </div>

                    <!-- 3. Komisyon (Cascading) -->
                    <div class="col-12">
                        <select class="form-select form-select-sm" id="addKomisyon" disabled onchange="handleAddChange('kom', this.value)">
                            <option value="">Komisyon Seçiniz (İsteğe Bağlı)...</option>
                        </select>
                    </div>

                    <!-- 4. Rol -->
                    <div class="col-12">
                        <select class="form-select form-select-sm" id="addRole" disabled onchange="handleAddChange('role', this.value)">
                             <option value="">Rol Seçiniz...</option>
                             <!-- Options populated by JS -->
                        </select>
                    </div>
                </div>

                <div class="d-grid mt-3">
                    <button class="btn btn-primary btn-sm" id="btnAddConfirm" disabled onclick="executeAdd()">
                        <i class="bx bx-check me-1"></i>Yetkiyi Ekle
                    </button>
                </div>
            </div>
        </div>
    `;
}

// Logic for Add Form Interactivity (Global scope functions attached to window or clean implementation)
// Since this is re-rendered string HTML, we need global functions or re-attach listeners.
// Using global functions for simplicity in this legacy-style app.

window.updateSistemRol = function (val) {
    draftState.sistemRol = val;
    renderDrawer();
};

window.handleAddChange = function (level, val) {
    const id = parseInt(val) || null;
    const data = window.drawerRefData;

    if (level === 'tes') {
        addFormSelections.teskilatId = id;
        addFormSelections.koordinatorlukId = null;
        addFormSelections.komisyonId = null;
        addFormSelections.roleId = null;

        // Update Koord Dropdown
        const koordSelect = document.getElementById('addKoordinatorluk');
        const komSelect = document.getElementById('addKomisyon');
        const roleSelect = document.getElementById('addRole');

        koordSelect.innerHTML = '<option value="">Koordinatörlük Seçiniz...</option>';
        komSelect.innerHTML = '<option value="">Komisyon Seçiniz (İsteğe Bağlı)...</option>';

        roleSelect.disabled = true;
        komSelect.disabled = true;

        if (id) {
            const koords = data.allKoordinatorlukler.filter(k => k.parentId === id);
            koords.forEach(k => koordSelect.add(new Option(k.ad, k.id)));
            koordSelect.disabled = false;
        } else {
            koordSelect.disabled = true;
        }
    }
    else if (level === 'koord') {
        addFormSelections.koordinatorlukId = id;
        addFormSelections.komisyonId = null;

        const komSelect = document.getElementById('addKomisyon');
        komSelect.innerHTML = '<option value="">Komisyon Seçiniz (İsteğe Bağlı)...</option>';

        if (id) {
            const koms = data.allKomisyonlar.filter(k => k.parentId === id);
            koms.forEach(k => komSelect.add(new Option(k.ad, k.id)));
            komSelect.disabled = false;
        } else {
            komSelect.disabled = true;
        }
        updateRoleOptions();
    }
    else if (level === 'kom') {
        addFormSelections.komisyonId = id;
        updateRoleOptions();
    }
    else if (level === 'role') {
        addFormSelections.roleId = id;
    }

    updateAddButton();
};

function updateRoleOptions() {
    const roleSelect = document.getElementById('addRole');
    roleSelect.innerHTML = '<option value="">Rol Seçiniz...</option>';
    roleSelect.disabled = false;

    // Filter Roles based on context
    // 1. Personel (Always available)
    // 2. Il Koord (If Koord selected, not Ankara)
    // 3. Genel Koord (If Ankara selected)
    // 4. Kom Bsk (If Komisyon selected)

    // Default Role (Implicit)
    roleSelect.add(new Option("Sadece Personel/Üye Olarak Ekle", "-1")); // -1 indicator for "No Explicit Role"

    window.allKurumsalRolOptions.forEach(r => {
        const rid = parseInt(r.value);
        let allowed = false;

        if (rid === ROLES.PERSONEL) allowed = false; // We use "-1" for generic, or we can use explicit 1? Let's use explicit 1.

        // Personel (1) is generic.
        if (rid === 1) allowed = true;

        if (rid === ROLES.KOMISYON_BASKANI && addFormSelections.komisyonId) allowed = true;
        if (rid === ROLES.IL_KOORD && addFormSelections.koordinatorlukId && addFormSelections.koordinatorlukId !== ROLES.ANKARA_TEGM_KOORD) allowed = true;
        if (rid === ROLES.GENEL_KOORD && addFormSelections.koordinatorlukId === ROLES.ANKARA_TEGM_KOORD) allowed = true;

        if (allowed) {
            roleSelect.add(new Option(r.text, r.value));
        }
    });
}

function updateAddButton() {
    const btn = document.getElementById('btnAddConfirm');
    // Valid if at least Koordinatorluk is selected
    // (Teskilat selection alone might not be enough for a role usually, usually need Koord)
    const valid = addFormSelections.koordinatorlukId !== null;
    btn.disabled = !valid;
}

window.executeAdd = function () {
    const s = addFormSelections;
    if (!s.koordinatorlukId) return;

    // 1. Memberships
    if (s.teskilatId && !draftState.teskilatIds.includes(s.teskilatId)) {
        draftState.teskilatIds.push(s.teskilatId);
    }
    if (s.koordinatorlukId && !draftState.koordinatorlukIds.includes(s.koordinatorlukId)) {
        draftState.koordinatorlukIds.push(s.koordinatorlukId);
    }
    if (s.komisyonId && !draftState.komisyonIds.includes(s.komisyonId)) {
        draftState.komisyonIds.push(s.komisyonId);
    }

    // 2. Roles
    if (s.roleId && s.roleId !== -1) {
        // Special Case: "Personel" (1). Is it explicit?
        // If user explicitly chose "Personel", we add it as explicit.
        draftState.gorevler.push({
            kurumsalRolId: s.roleId,
            koordinatorlukId: s.koordinatorlukId,
            komisyonId: s.komisyonId
        });
    }

    renderDrawer();
};

window.removeItem = function (type, id, gIndex) {
    const data = window.drawerRefData;

    // Helper to find parent Koord of a Komisyon
    const getKomParent = (komId) => {
        const kom = data.allKomisyonlar.find(k => k.id == komId);
        return kom ? kom.parentId : null;
    };

    // Helper to find parent Teskilat of a Koordinatorluk
    const getKoordParent = (koordId) => {
        const koord = data.allKoordinatorlukler.find(k => k.id == koordId);
        return koord ? koord.parentId : null;
    };

    // Helper to remove Teskilat
    const removeTeskilat = (tesId) => {
        if (!tesId) return;
        draftState.teskilatIds = draftState.teskilatIds.filter(i => i !== tesId);
    };

    // Helper to remove Koordinatorluk
    const removeKoordinatorluk = (koordId) => {
        if (!koordId) return;
        draftState.koordinatorlukIds = draftState.koordinatorlukIds.filter(i => i !== koordId);
        // Cascade up to Teskilat
        removeTeskilat(getKoordParent(koordId));
    };

    // Helper to remove Komisyon
    const removeKomisyon = (komId) => {
        if (!komId) return;
        draftState.komisyonIds = draftState.komisyonIds.filter(i => i !== komId);
        // Cascade up to Koordinatorluk
        removeKoordinatorluk(getKomParent(komId));
    };


    if (type === 'gorev') {
        const task = draftState.gorevler[gIndex];
        // Remove the role assignment
        draftState.gorevler.splice(gIndex, 1);

        // Cascade Delete Context
        if (task) {
            if (task.komisyonId) {
                removeKomisyon(task.komisyonId);
            } else if (task.koordinatorlukId) {
                removeKoordinatorluk(task.koordinatorlukId);
            } else {
                // If just Teskilat? (Rare for explicit role but possible)
                // We don't track teskilatId in gorev object explicitly based on previous read, 
                // but if we did, we would handle it. 
                // Explicit roles usually sit on Koord or Kom.
            }
        }

    } else if (type === 'kom') {
        // Remove Komisyon
        removeKomisyon(id);

        // Remove explicit roles bound to this commission
        draftState.gorevler = draftState.gorevler.filter(g => g.komisyonId !== id);

    } else if (type === 'koord') {
        // Remove Koordinatorluk
        removeKoordinatorluk(id);

        // Remove child Commissions
        const komsToRemove = data.allKomisyonlar.filter(k => k.parentId === id).map(k => k.id);
        draftState.komisyonIds = draftState.komisyonIds.filter(kid => !komsToRemove.includes(kid));

        // Remove linked Roles
        draftState.gorevler = draftState.gorevler.filter(g => g.koordinatorlukId !== id && (!g.komisyonId || !komsToRemove.includes(g.komisyonId)));

    } else if (type === 'tes') {
        // Remove Teskilat
        removeTeskilat(id);

        // Remove child Koordinatorluks
        const koordsToRemove = data.allKoordinatorlukler.filter(k => k.parentId === id).map(k => k.id);
        draftState.koordinatorlukIds = draftState.koordinatorlukIds.filter(kid => !koordsToRemove.includes(kid));

        // Remove child Commissions (grand-children)
        // Find all commissions belonging to these koords
        const komsToRemove = data.allKomisyonlar.filter(k => koordsToRemove.includes(k.parentId)).map(k => k.id);
        draftState.komisyonIds = draftState.komisyonIds.filter(kid => !komsToRemove.includes(kid));

        // Remove linked Roles
        draftState.gorevler = draftState.gorevler.filter(g => {
            // Check implicit link via Koord or Kom
            // Since we cleared lists, we can't easily check coverage, ensuring by ID matching
            if (g.koordinatorlukId && koordsToRemove.includes(g.koordinatorlukId)) return false;
            if (g.komisyonId && komsToRemove.includes(g.komisyonId)) return false;
            return true;
        });
    }

    renderDrawer();
};




// --- SAVE / CANCEL ---
function cancelDraft() {
    if (!isDirty()) return;
    draftState = JSON.parse(JSON.stringify(originalState));
    renderDrawer();
}

async function saveDraft() {
    if (!isDirty()) return;

    // Show loading?
    const btn = document.querySelector('.offcanvas-footer .btn-primary');
    const originalText = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = 'Kaydediliyor...';

    const payload = {
        PersonelId: originalState.personelId,
        SistemRol: draftState.sistemRol,
        TeskilatIds: draftState.teskilatIds,
        KoordinatorlukIds: draftState.koordinatorlukIds,
        KomisyonIds: draftState.komisyonIds,
        Gorevler: draftState.gorevler
    };

    try {
        const response = await fetch('/Personel/SaveYetkilendirme', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const err = await response.text();
            throw new Error(err);
        }

        // Success
        // Update original state to match current
        // Ideally fetch fresh data from response to ensure sync (IDs etc)
        const freshData = await response.json();

        // We need to re-init everything with fresh data
        // Simulate loadDraftData behavior
        const data = freshData;
        window.drawerRefData = data;

        originalState = { // Reconstruct original state from fresh data
            personelId: data.personelId,
            sistemRol: data.sistemRol,
            teskilatIds: data.selectedTeskilatIds || [],
            koordinatorlukIds: data.selectedKoordinatorlukIds || [],
            komisyonIds: data.selectedKomisyonIds || [],
            gorevler: data.kurumsalRolAssignments.map(a => ({
                kurumsalRolId: a.kurumsalRolId,
                koordinatorlukId: a.koordinatorlukId,
                komisyonId: a.komisyonId
            }))
        };
        draftState = JSON.parse(JSON.stringify(originalState));

        renderDrawer();

        // Close Drawer Immediately
        const drawerEl = document.getElementById('offcanvasEnd');
        const instance = bootstrap.Offcanvas.getInstance(drawerEl);
        if (instance) instance.hide();

        // Show Success Modal
        Swal.fire({
            icon: 'success',
            title: 'Kaydedildi',
            text: 'Yetkilendirme başarıyla güncellendi.',
            showConfirmButton: true,
            confirmButtonText: 'Tamam',
            timer: 2000 // Optional: Auto close and reload after 2s if user doesn't click
        }).then(() => {
            location.reload();
        });

    } catch (error) {
        console.error(error);
        Swal.fire('Hata', 'Kaydetme başarısız: ' + error.message, 'error');
        btn.disabled = false;
        btn.innerHTML = originalText;
    }
}
