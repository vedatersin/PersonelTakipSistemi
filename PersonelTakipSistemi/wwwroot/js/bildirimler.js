
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
    $.ajax({
        url: '/Bildirimler/GetData',
        type: 'GET',
        data: { selectedId: selectedNotificationId },
        success: function (response) {
            renderList(response.inbox);

            if (response.selectedNotification) {
                renderDetail(response.selectedNotification);
                // Scroll to item
                setTimeout(() => {
                    const item = document.getElementById(`notif-item-${response.selectedNotification.bildirimId}`);
                    if (item) item.scrollIntoView({ behavior: 'smooth', block: 'center' });
                }, 100);
            } else if (response.inbox.length > 0 && !selectedNotificationId) {
                // Optional: Select first item automatically? 
                // User requirement: "Sayfa açılınca ilgili bildirim otomatik seçilir... Yoksa placeholder"
                // So if no param, placeholder is fine.
            } else if (response.inbox.length === 0) {
                $('#notification-list').html('<div class="text-center p-4 text-muted">Henüz bildirim yok.</div>');
            }
        },
        error: function () {
            console.error("Bildirimler yüklenemedi.");
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
        if (data.count > 0) {
            $('#notificationBadge').text(data.count > 99 ? '99+' : data.count).show();
        } else {
            $('#notificationBadge').hide();
        }
        renderDropdown(data.top);
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
