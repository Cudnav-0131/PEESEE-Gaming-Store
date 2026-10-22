document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("order-detail-modal");
    const overlay = modal
        ? modal.querySelector(".order-detail-overlay")
        : null;

    const closeButton = document.getElementById("close-order-detail");
    const closeButtonFooter = document.getElementById("close-order-detail-btn");

    if (!modal) {
        console.warn("Không tìm thấy order-detail-modal.");
        return;
    }

    function closeModal() {
        modal.classList.add("hidden");
    }

    function openModal() {
        modal.classList.remove("hidden");
    }

    if (closeButton) {
        closeButton.addEventListener("click", closeModal);
    }

    if (closeButtonFooter) {
        closeButtonFooter.addEventListener("click", closeModal);
    }

    if (overlay) {
        overlay.addEventListener("click", closeModal);
    }

    document.addEventListener("keydown", function (event) {
        if (event.key === "Escape") {
            closeModal();
        }
    });

    // Cho phép các đoạn JavaScript khác mở modal khi cần.
    window.openOrderDetailModal = openModal;
    window.closeOrderDetailModal = closeModal;
});
