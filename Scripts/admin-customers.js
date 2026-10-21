
document.addEventListener("DOMContentLoaded", function () {
    const customerTableBody = document.getElementById("admin-customer-list");
    const searchInput = document.getElementById("customer-search-input");

    if (!customerTableBody) {
        return;
    }

    function escapeHtml(value) {
        return String(value ?? "").replace(/[&<>"']/g, function (char) {
            const entities = {
                "&": "&amp;",
                "<": "&lt;",
                ">": "&gt;",
                '"': "&quot;",
                "'": "&#39;"
            };

            return entities[char];
        });
    }

    function renderCustomers(keyword) {
        const customers = Array.isArray(window.adminCustomers)
            ? window.adminCustomers
            : [];

        const normalizedKeyword = String(keyword || "")
            .trim()
            .toLowerCase();

        const filteredCustomers = customers.filter(function (customer) {
            const name = String(customer.FullName || "").toLowerCase();
            const email = String(customer.Email || "").toLowerCase();

            return name.includes(normalizedKeyword) ||
                email.includes(normalizedKeyword);
        });

        if (filteredCustomers.length === 0) {
            customerTableBody.innerHTML =
                '<tr><td colspan="6" style="text-align:center;">Không tìm thấy khách hàng.</td></tr>';
            return;
        }

        customerTableBody.innerHTML = filteredCustomers.map(function (customer) {
            return `
                <tr>
                    <td>${escapeHtml(customer.Id)}</td>
                    <td>${escapeHtml(customer.FullName)}</td>
                    <td>${escapeHtml(customer.Email)}</td>
                    <td>${escapeHtml(customer.Role)}</td>
                    <td>${escapeHtml(customer.CreatedAt || "Chưa có dữ liệu")}</td>
                    <td>
                        <button type="button"
                                class="btn-toggle-lock ${customer.IsLocked ? "is-locked" : "is-active"}"
                                data-user-id="${escapeHtml(customer.Id)}">
                            ${customer.IsLocked ? "Mở khóa" : "Khóa"}
                        </button>
                    </td>
                </tr>
            `;
        }).join("");
    }

    renderCustomers("");


    if (searchInput) {
        searchInput.addEventListener("input", function () {
            renderCustomers(searchInput.value);
        });
    }

    // Xử lý nút Khóa / Mở khóa tài khoản
    customerTableBody.addEventListener("click", function (event) {
        const button = event.target.closest(".btn-toggle-lock");

        if (!button) {
            return;
        }

        const customerId = button.getAttribute("data-user-id");
        const hiddenField = document.getElementById("hfCustomerId");
        const submitButton = document.getElementById("btnToggleCustomerLock");

        if (!customerId || !hiddenField || !submitButton) {
            return;
        }

        if (!confirm("Bạn có chắc chắn muốn " +
            button.textContent.trim().toLowerCase() +
            " tài khoản này không?")) {
            return;
        }

        hiddenField.value = customerId;
        submitButton.click();
    });
});
