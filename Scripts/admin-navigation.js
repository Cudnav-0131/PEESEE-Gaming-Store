document.addEventListener("DOMContentLoaded", function () {
    const navItems = document.querySelectorAll(
        ".admin-nav-item[data-section]"
    );

    const sections = document.querySelectorAll(".admin-section");

    function showSection(sectionId) {
        sections.forEach(function (section) {
            section.classList.add("hidden");
        });

        if (sectionId === "dashboard") {
            ["dashboard", "revenue-section", "recent-orders"]
                .forEach(function (id) {
                    const section = document.getElementById(id);

                    if (section) {
                        section.classList.remove("hidden");
                    }
                });
        } else {
            const target = document.getElementById(sectionId);

            if (target && target.classList.contains("admin-section")) {
                target.classList.remove("hidden");
            } else {
                showSection("dashboard");
                return;
            }
        }

        navItems.forEach(function (item) {
            item.classList.toggle(
                "active",
                item.dataset.section === sectionId
            );
        });

        history.replaceState(null, "", "#" + sectionId);
    }

    navItems.forEach(function (item) {
        item.addEventListener("click", function (event) {
            event.preventDefault();
            showSection(this.dataset.section);
        });
    });

    const viewAllOrdersLink = document.querySelector(
        ".view-all-orders[data-section='orders']"
    );

    if (viewAllOrdersLink) {
        viewAllOrdersLink.addEventListener("click", function (event) {
            event.preventDefault();
            showSection("orders");
        });
    }

    const requestedSection = window.location.hash.substring(1);

    const validSection = Array.from(navItems).some(function (item) {
        return item.dataset.section === requestedSection;
    });

    showSection(validSection ? requestedSection : "dashboard");
});

document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("edit-product-modal");
    const closeButton = document.getElementById("close-edit-product");
    const cancelButton = document.getElementById("cancel-edit-product");
    const overlay = document.querySelector(".edit-product-overlay");

    function closeEditProductModal() {
        if (modal) {
            modal.classList.add("hidden");
        }
    }

    // Nút X
    if (closeButton) {
        closeButton.addEventListener(
            "click",
            closeEditProductModal
        );
    }

    // Nút Hủy
    if (cancelButton) {
        cancelButton.addEventListener(
            "click",
            closeEditProductModal
        );
    }

    // Nhấn vùng nền tối để đóng popup
    if (overlay) {
        overlay.addEventListener(
            "click",
            closeEditProductModal
        );
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const openButton = document.getElementById("open-add-product-btn");
    const modal = document.getElementById("add-product-modal");
    const closeButton = document.getElementById("close-add-product");
    const cancelButton = document.getElementById("cancel-add-product-btn");
    const overlay = document.querySelector(".add-product-overlay");

    // Mở popup Thêm sản phẩm
    if (openButton && modal) {
        openButton.addEventListener("click", function () {
            modal.classList.remove("hidden");
        });
    }

    // Đóng popup
    function closeAddProductModal() {
        if (modal) {
            modal.classList.add("hidden");
        }
    }

    if (closeButton) {
        closeButton.addEventListener("click", closeAddProductModal);
    }

    if (cancelButton) {
        cancelButton.addEventListener("click", closeAddProductModal);
    }

    if (overlay) {
        overlay.addEventListener("click", closeAddProductModal);
    }
});
