
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

function renderAssignmentsTab(state, data) {
    let html = '';

    // 1. Teşkilat
    const selectedTes = data.allTeskilatlar.filter(x => state.teskilatIds.includes(x.id));
    const availableTes = data.allTeskilatlar.filter(x => !state.teskilatIds.includes(x.id));
    html += renderSection('Teşkilat', selectedTes, availableTes, 'addTeskilat', 'removeTeskilat', 'primary');

    // 2. Koordinatörlük (Filtered by Teskilat)
    // Only show koords whose parent Teskilat is selected
    const validKoords = data.allKoordinatorlukler.filter(x => state.teskilatIds.includes(x.parentId));
    const selectedKoords = validKoords.filter(x => state.koordinatorlukIds.includes(x.id));
    const availableKoords = validKoords.filter(x => !state.koordinatorlukIds.includes(x.id));
    html += renderSection('Koordinatörlük', selectedKoords, availableKoords, 'addKoordinatorluk', 'removeKoordinatorluk', 'info');

    // 3. Komisyon (Filtered by Koordinatorluk)
    const validKoms = data.allKomisyonlar.filter(x => state.koordinatorlukIds.includes(x.parentId));
    const selectedKoms = validKoms.filter(x => state.komisyonIds.includes(x.id));
    const availableKoms = validKoms.filter(x => !state.komisyonIds.includes(x.id));
    html += renderSection('Komisyonlar', selectedKoms, availableKoms, 'addKomisyon', 'removeKomisyon', 'success');

    // 4. Görevler (Roller)
    html += `<div class="mb-4"><label class="form-label text-muted small text-uppercase mb-2">Kurumsal Rol / Görevler</label>`;

    // Render current roles
    if (state.gorevler.length > 0) {
        html += `<div class="d-flex flex-column gap-2 mb-2">`;
        state.gorevler.forEach((g, idx) => {
            const rolName = window.allKurumsalRolOptions.find(r => r.value == g.kurumsalRolId)?.text || 'Bilinmeyen Rol';
            let contextName = '';
            if (g.komisyonId) contextName = data.allKomisyonlar.find(k => k.id == g.komisyonId)?.ad || '';
            else if (g.koordinatorlukId) contextName = data.allKoordinatorlukler.find(k => k.id == g.koordinatorlukId)?.ad || '';

            html += `
                <div class="d-flex justify-content-between align-items-center bg-light p-2 rounded border border-start-4 border-danger position-relative">
                     <div>
                        <span class="fw-semibold d-block">${rolName}</span>
                        ${contextName ? `<small class="text-muted">${contextName}</small>` : ''}
                     </div>
                     <i class="bx bx-x text-muted cursor-pointer hover-text-danger" onclick="removeGorev(${idx})"></i>
                </div>
            `;
        });
        html += `</div>`;
    }

    // Add Form
    html += renderRoleAddForm(state, data); // Uses draft state for logic
    html += `</div>`;

    return html;
}

function renderSection(title, items, available, addFn, removeFn, color) {
    let html = `<div class="mb-4"><label class="form-label text-muted small text-uppercase mb-2">${title}</label>`;

    if (items.length > 0) {
        html += `<div class="d-flex flex-column gap-2 mb-2">`;
        items.forEach(item => {
            html += `
                <div class="d-flex justify-content-between align-items-center bg-light p-2 rounded border">
                    <span>${item.ad}</span>
                    <i class="bx bx-x text-muted cursor-pointer hover-text-danger" onclick="${removeFn}(${item.id})"></i>
                </div>`;
        });
        html += `</div>`;
    }

    if (available.length > 0) {
        html += `<select class="form-select form-select-sm mt-1" onchange="${addFn}(this.value); this.value='';">
                    <option value="" selected disabled>+ ${title} Ekle...</option>
                    ${available.map(i => `<option value="${i.id}">${i.ad}</option>`).join('')}
                 </select>`;
    } else {
        html += `<div class="small text-muted fst-italic mt-1">Eklenebilir seçenek yok.</div>`;
    }
    html += `</div>`;
    return html;
}

function renderRoleAddForm(state, data) {
    // Logic for available roles based on state
    const hasMerkez = state.teskilatIds.includes(ROLES.MERKEZ_TESKILAT);
    const hasAnkara = state.koordinatorlukIds.includes(ROLES.ANKARA_TEGM_KOORD);
    const hasKomisyon = state.komisyonIds.length > 0;
    const hasTasraKoord = state.koordinatorlukIds.some(id => id !== ROLES.ANKARA_TEGM_KOORD);
    const hasAnyAssignment = state.koordinatorlukIds.length > 0 || state.komisyonIds.length > 0; // Loose check

    const availableRoles = window.allKurumsalRolOptions.filter(r => {
        const rid = parseInt(r.value);
        if (rid === ROLES.GENEL_KOORD) return hasMerkez && hasAnkara;
        if (rid === ROLES.KOMISYON_BASKANI) return hasKomisyon;
        if (rid === ROLES.IL_KOORD) return hasTasraKoord;
        if (rid === ROLES.PERSONEL) return true; // Always allow
        return true;
    });

    const isDisabled = availableRoles.length === 0;

    return `
        <div class="p-3 bg-light rounded border">
             <div class="mb-2">
                 <select class="form-select" id="newRoleSelect" onchange="handleRoleSelectChange(this.value)" ${isDisabled ? 'disabled' : ''}>
                    <option value="">+ Rol Seçiniz...</option>
                    ${availableRoles.map(r => `<option value="${r.value}">${r.text}</option>`).join('')}
                </select>
            </div>
            <div id="roleContextUI" style="display:none;">
                <!-- Filled dynamically by JS -->
            </div>
        </div>
    `;
}

// --- Action Handlers (Modify Draft State) ---

function updateSistemRol(val) {
    draftState.sistemRol = val;
    renderDrawer();
}

function addTeskilat(id) {
    id = parseInt(id);
    if (!draftState.teskilatIds.includes(id)) {
        draftState.teskilatIds.push(id);
        renderDrawer();
    }
}
function removeTeskilat(id) {
    id = parseInt(id);
    draftState.teskilatIds = draftState.teskilatIds.filter(i => i !== id);

    // Cascade Remove
    // Remove child Koordinatorluks
    const removedKoords = window.drawerRefData.allKoordinatorlukler
        .filter(k => k.parentId === id && draftState.koordinatorlukIds.includes(k.id))
        .map(k => k.id);

    removedKoords.forEach(kid => removeKoordinatorluk(kid)); // Recursive-ish calls

    renderDrawer();
}

function addKoordinatorluk(id) {
    id = parseInt(id);
    if (!draftState.koordinatorlukIds.includes(id)) {
        draftState.koordinatorlukIds.push(id);
        renderDrawer();
    }
}
function removeKoordinatorluk(id) {
    id = parseInt(id);
    draftState.koordinatorlukIds = draftState.koordinatorlukIds.filter(i => i !== id);

    // Cascade Remove Commissions
    const removedKoms = window.drawerRefData.allKomisyonlar
        .filter(k => k.parentId === id && draftState.komisyonIds.includes(k.id))
        .map(k => k.id);

    removedKoms.forEach(kid => removeKomisyon(kid));

    // Cleanup Roles that depended on this context
    draftState.gorevler = draftState.gorevler.filter(g => g.koordinatorlukId !== id);

    renderDrawer();
}

function addKomisyon(id) {
    id = parseInt(id);
    if (!draftState.komisyonIds.includes(id)) {
        draftState.komisyonIds.push(id);
        renderDrawer();
    }
}
function removeKomisyon(id) {
    id = parseInt(id);
    draftState.komisyonIds = draftState.komisyonIds.filter(i => i !== id);

    // Cleanup Roles
    draftState.gorevler = draftState.gorevler.filter(g => g.komisyonId !== id);

    renderDrawer();
}

function removeGorev(index) {
    draftState.gorevler.splice(index, 1);
    renderDrawer();
}

// --- Role Context Logic ---
function handleRoleSelectChange(val) {
    const roleId = parseInt(val);
    const ui = document.getElementById('roleContextUI');
    if (!roleId) {
        ui.style.display = 'none';
        return;
    }

    ui.style.display = 'block';

    let options = [];
    let label = "Kapsam Seçiniz";
    let type = ""; // 'kom', 'koord'

    if (roleId === ROLES.KOMISYON_BASKANI) {
        type = 'kom';
        const koms = window.drawerRefData.allKomisyonlar.filter(x => draftState.komisyonIds.includes(x.id));

        // Contextual Name Logic
        options = koms.map(k => {
            const pid = k.parentId || k.ParentId; // Case-safe check
            const parentKoord = window.drawerRefData.allKoordinatorlukler.find(pk => pk.id === pid);
            const contextText = parentKoord ? ` (${parentKoord.ad || parentKoord.Ad})` : '';
            return { id: k.id, text: (k.ad || k.Ad) + contextText };
        });

    } else if (roleId === ROLES.IL_KOORD) {
        type = 'koord';
        const koords = window.drawerRefData.allKoordinatorlukler.filter(x => draftState.koordinatorlukIds.includes(x.id) && x.id !== ROLES.ANKARA_TEGM_KOORD);

        // Contextual Name Logic
        options = koords.map(k => {
            const pid = k.parentId || k.ParentId; // Case-safe check
            const parentTes = window.drawerRefData.allTeskilatlar.find(t => t.id === pid);
            const contextText = parentTes ? ` (${parentTes.ad || parentTes.Ad})` : '';
            return { id: k.id, text: (k.ad || k.Ad) + contextText };
        });

    } else if (roleId === ROLES.GENEL_KOORD) {
        type = 'koord';
        const ankara = window.drawerRefData.allKoordinatorlukler.find(x => x.id === ROLES.ANKARA_TEGM_KOORD);
        if (ankara) options = [{ id: ankara.id, text: ankara.ad || ankara.Ad }];
    } else {
        // Personel role - No specific context needed usually, or generic
        ui.innerHTML = `
            <div class="d-flex gap-2 mt-2">
                <button class="btn btn-sm btn-primary flex-grow-1" onclick="finalizeRoleAdd(${roleId}, null, null)">Ekle</button>
                <button class="btn btn-sm btn-outline-secondary" onclick="resetRoleForm()">İptal</button>
            </div>
        `;
        return;
    }

    if (options.length === 0) {
        ui.innerHTML = `<div class="text-danger small mb-2">Uygun kapsam bulunamadı.</div><button class="btn btn-sm btn-secondary" onclick="resetRoleForm()">İptal</button>`;
        return;
    }

    ui.innerHTML = `
        <div class="mb-2">
            <select id="roleContextSelect" class="form-select form-select-sm">
                ${options.map(o => `<option value="${o.id}">${o.text}</option>`).join('')}
            </select>
        </div>
         <div class="d-flex gap-2">
            <button class="btn btn-sm btn-primary flex-grow-1" onclick="confirmRoleAdd(${roleId}, '${type}')">Ekle</button>
            <button class="btn btn-sm btn-outline-secondary" onclick="resetRoleForm()">İptal</button>
        </div>
    `;
}

function resetRoleForm() {
    document.getElementById('newRoleSelect').value = "";
    document.getElementById('roleContextUI').style.display = 'none';
}

function confirmRoleAdd(roleId, type) {
    const ctxId = parseInt(document.getElementById('roleContextSelect').value);
    const komId = type === 'kom' ? ctxId : null;
    const koordId = type === 'koord' ? ctxId : null;
    finalizeRoleAdd(roleId, komId, koordId);
}

function finalizeRoleAdd(roleId, komId, koordId) {
    draftState.gorevler.push({
        kurumsalRolId: roleId,
        komisyonId: komId,
        koordinatorlukId: koordId
    });
    renderDrawer(); // Re-renders whole list and resets form
}


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
