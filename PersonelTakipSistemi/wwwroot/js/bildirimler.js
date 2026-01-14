
// Global variables
let selectedNotificationId = null;

$(document).ready(function () {
    // Check URL for selectedId
    const urlParams = new URLSearchParams(window.location.search);
    const paramId = urlParams.get('selectedId');

    if ($('#notification-list').length) {
        if (paramId) {
            selectedNotificationId = paramId;
        }

        loadInbox();

        // Search input listener
        $('#search-input').on('keyup', function () {
            var value = $(this).val().toLowerCase();
            $("#notification-list a").filter(function () {
                var text = $(this).text().toLowerCase();
                $(this).toggle(text.indexOf(value) > -1)
            });
        });
    }
});

function loadInbox() {
    // 1. STATE: LOADING
    // Always render loading on left
    $('#notification-list').html(document.getElementById('loading-left-template').innerHTML);

    // Render loading on right ONLY if we don't have a content to show (e.g. refreshing) OR if we want to blocking-load
    // For smoother UX, if we already have a selection, maybe we don't wipe right panel?
    // User requested strict Loading state -> "Loading UI (sol + sağ)" when isLoading=true.
    // So let's be strict.
    $('#notification-detail-container').html(document.getElementById('loading-right-template').innerHTML);

    $.ajax({
        url: '/Bildirimler/GetData',
        type: 'GET',
        data: { selectedId: selectedNotificationId },
        success: function (response) {

            // 2. STATE: EMPTY (Normal Success but 0 items)
            if (!response.inbox || response.inbox.length === 0) {
                $('#notification-list').html(document.getElementById('empty-left-template').innerHTML);
                $('#notification-detail-container').html(document.getElementById('empty-right-template').innerHTML);
                return; // Stop processing
            }

            // 3. STATE: NORMAL (Items exist)
            renderList(response.inbox);

            if (response.selectedNotification) {
                // Case: Item selected
                renderDetail(response.selectedNotification);
                // Scroll to item
                setTimeout(() => {
                    const item = document.getElementById(`notif-item-${response.selectedNotification.bildirimId}`);
                    if (item) item.scrollIntoView({ behavior: 'smooth', block: 'center' });
                }, 100);
            } else {
                // Case: No item selected (Placeholder)
                // NOTE: User said "Bir bildirim seçin" must ONLY appear when there ARE notifications.
                // We are in the notifications.length > 0 block, so this is correct.
                $('#notification-detail-container').html(document.getElementById('select-notification-template').innerHTML);
            }
        },
        error: function () {
            // 4. STATE: ERROR
            console.error("Bildirimler yüklenemedi.");
            $('#notification-list').html(document.getElementById('error-left-template').innerHTML);
            $('#notification-detail-container').html(document.getElementById('error-right-template').innerHTML);
        }
    });
}

function renderList(inbox) {
    const container = $('#notification-list');
    container.empty();
    const template = document.getElementById('notification-item-template').innerHTML;

    inbox.forEach(function (n) {
        const initials = getInitials(n.gonderenAdSoyad);
        const dateStr = formatDate(n.olusturmaTarihi);
        let unreadClass = n.okunduMu ? 'd-none' : '';
        let bgClass = n.okunduMu ? 'read' : 'unread'; // Using CSS classes
        if (selectedNotificationId && n.bildirimId == selectedNotificationId) {
            bgClass += ' active-item';
            unreadClass = 'd-none'; // Hide dot if active (assumed read)
        }

        let html = template
            .replace(/{id}/g, n.bildirimId)
            .replace('{initials}', initials)
            .replace('{sender}', `${n.gonderenAdSoyad} <span class="fw-normal text-muted" style="font-size:0.8em">(${n.gonderenKurumsalRolOzet})</span>`)
            .replace('{date}', dateStr)
            .replace('{title}', n.baslik)
            .replace('{desc}', n.aciklama)
            .replace('{unreadClass}', unreadClass);

        const $el = $(html);
        $el.addClass(bgClass);
        if (!n.okunduMu) {
            // Specific styles if needed, handled by css class
        }

        container.append($el);
    });
}

function renderDetail(n) {
    const container = $('#notification-detail-container');
    const template = document.getElementById('notification-detail-template').innerHTML;

    const initials = getInitials(n.gonderenAdSoyad);
    const dateStr = new Date(n.olusturmaTarihi).toLocaleString('tr-TR', { day: 'numeric', month: 'long', year: 'numeric', hour: '2-digit', minute: '2-digit' });
    const linkClass = n.url ? '' : 'd-none';

    let html = template
        .replace('{initials}', initials)
        .replace('{sender}', n.gonderenAdSoyad)
        .replace('{role}', n.gonderenKurumsalRolOzet)
        .replace('{fullDate}', dateStr)
        .replace('{title}', n.baslik)
        .replace('{body}', n.aciklama)
        .replace('{link}', n.url || '#')
        .replace('{linkClass}', linkClass);

    container.html(html);
}

function selectNotification(el, id) {
    // UI Update
    $('.list-group-item').removeClass('active-item');
    $(el).addClass('active-item');
    $(el).find('.unread-indicator').addClass('d-none'); // Hide dot
    $(el).removeClass('unread').addClass('read');

    selectedNotificationId = id;

    // Update URL
    const url = new URL(window.location);
    url.searchParams.set('selectedId', id);
    window.history.pushState({}, '', url);

    // Fetch Detail
    // We can fetch just detail or use stored data if we had it. Use endpoint for simplicity and mark read.
    $.ajax({
        url: '/Bildirimler/GetData', // Logic handles mark read
        data: { selectedId: id },
        success: function (response) {
            if (response.selectedNotification) {
                renderDetail(response.selectedNotification);
                loadTopbarCount(); // Refresh badge
            }
        }
    });
}

// Helpers
function getInitials(name) {
    if (!name) return "S";
    var parts = name.split(' ');
    var initials = parts[0].substring(0, 1).toUpperCase();
    if (parts.length > 1) {
        initials += parts[parts.length - 1].substring(0, 1).toUpperCase();
    }
    return initials;
}

function formatDate(dateString) {
    const d = new Date(dateString);
    const now = new Date();
    const diff = now - d;

    // If today: HH:mm
    if (d.toDateString() === now.toDateString()) {
        return d.toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' });
    }
    return d.toLocaleDateString('tr-TR', { day: 'numeric', month: 'short' });
}

// Topbar Logic (Global)
function loadTopbarCount() {
    $.get('/Bildirimler/Topbar', function (data) {
        const badge = $('#notificationBadge');
        if (data.count > 0) {
            badge.removeClass('bg-secondary').addClass('bg-danger');
            badge.text(data.count > 99 ? '99+' : data.count).show();
        } else {
            // User requested: 0 count, gray background, text '0'
            badge.removeClass('bg-danger').addClass('bg-secondary');
            badge.text('0').show();
        }
        renderDropdown(data.top);
    }).fail(function () {
        console.error("Bildirimler/Topbar failed.");
        // Fallback: Show 0 gray
        const badge = $('#notificationBadge');
        badge.removeClass('bg-danger').addClass('bg-secondary');
        badge.text('0').show();
    });
}

function renderDropdown(list) {
    const container = $('#notificationList'); // In _Header
    if (!container.length) return;

    container.empty();
    if (list.length === 0) {
        container.html('<li class="list-group-item text-center small text-muted">Yeni bildirim yok</li>');
        return;
    }

    // Similar to list but mini
    list.forEach(n => {
        const bg = n.okunduMu ? '' : 'bg-light'; // highlight unread
        const html = `
             <li class="list-group-item list-group-item-action dropdown-notifications-item ${bg}" onclick="window.location.href='/Bildirimler?selectedId=${n.bildirimId}'">
                <div class="d-flex">
                  <div class="flex-shrink-0 me-3">
                    <div class="avatar">
                      <span class="avatar-initial rounded-circle bg-label-primary">${getInitials(n.gonderenAdSoyad)}</span>
                    </div>
                  </div>
                  <div class="flex-grow-1">
                    <h6 class="mb-1">${n.baslik}</h6>
                    <small class="text-muted">${n.gonderenAdSoyad}</small>
                    <small class="text-muted d-block" style="font-size:0.7em">${formatDate(n.olusturmaTarihi)}</small>
                  </div>
                </div>
              </li>
        `;
        container.append(html);
    });
}

function markAllRead() {
    $.post('/Bildirimler/MarkAllRead', function () {
        loadTopbarCount();
        if (typeof loadInbox === 'function') loadInbox(); // if on page
    });
}

// Init topbar
$(function () {
    loadTopbarCount();
    // Optional: Poll every 30s
    setInterval(loadTopbarCount, 30000);
});
