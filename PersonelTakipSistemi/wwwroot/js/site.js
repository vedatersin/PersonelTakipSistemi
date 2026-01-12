// Notification Logic

function markAsRead(element) {
    // 1. Mark item as read styling
    element.classList.add('marked-as-read');

    // Find the read indicator dot inside and remove it or change color
    var dot = element.querySelector('.dropdown-notifications-read span');
    if (dot) {
        dot.classList.remove('badge-dot');
        dot.classList.add('text-muted');
    }

    // 2. Decrement badge count
    updateBadgeCount(-1);
}

function updateBadgeCount(change) {
    var badge = document.getElementById('notificationBadge');
    if (badge) {
        var currentCount = parseInt(badge.innerText);
        var newCount = currentCount + change;
        if (newCount < 0) newCount = 0;

        badge.innerText = newCount;

        // 3. Update Badge Color
        if (newCount === 0) {
            badge.classList.remove('bg-danger');
            badge.classList.add('bg-secondary');
        } else {
            badge.classList.remove('bg-secondary');
            badge.classList.add('bg-danger');
        }
    }
}

function deleteNotification(event, id) {
    event.stopPropagation(); // Prevent triggering the item click (markAsRead)
    var item = document.getElementById(id);
    if (item) {
        // If it wasn't read yet, decrement count
        if (!item.classList.contains('marked-as-read')) {
            updateBadgeCount(-1);
        }
        item.remove();
    }
}

function markAllRead() {
    // Select all unread items
    var unreadItems = document.querySelectorAll('.dropdown-notifications-item:not(.marked-as-read)');
    unreadItems.forEach(function (item) {
        markAsRead(item);
    });
}
