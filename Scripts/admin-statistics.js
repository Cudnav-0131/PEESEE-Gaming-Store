(function () {
    "use strict";

    function initStatistics() {
        // Lấy dữ liệu JSON từ HiddenField của ASP.NET
        var jsonField = document.getElementById("hfRevenueOrdersJson");

        if (!jsonField) {
            console.error("Không tìm thấy hfRevenueOrdersJson.");
            return;
        }

        var orders = [];

        try {
            orders = JSON.parse(jsonField.value || "[]");
        } catch (error) {
            console.error("Không đọc được dữ liệu thống kê:", error);
            return;
        }

        // Chuẩn hóa trạng thái đơn hàng
        function normalizeStatus(status) {
            return String(status || "")
                .trim()
                .toLowerCase()
                .normalize("NFD")
                .replace(/[\u0300-\u036f]/g, "");
        }

        function isCompleted(status) {
            var value = normalizeStatus(status);
            return value === "completed" ||
                value === "complete" ||
                value === "delivered" ||
                value === "done" ||
                value === "hoan thanh" ||
                value === "da giao";
        }

        function isCancelled(status) {
            var value = normalizeStatus(status);
            return value === "cancelled" ||
                value === "canceled" ||
                value === "cancel" ||
                value === "da huy" ||
                value === "huy";
        }

        function formatMoney(amount) {
            return Number(amount || 0).toLocaleString("vi-VN") + "đ";
        }

        function setText(id, value) {
            var element = document.getElementById(id);
            if (element) {
                element.textContent = value;
            }
        }

        // Chỉ tính doanh thu từ đơn hàng hoàn thành
        var completedOrders = orders.filter(function (order) {
            return isCompleted(order.status);
        });

        var cancelledOrders = orders.filter(function (order) {
            return isCancelled(order.status);
        });

        var revenue = completedOrders.reduce(function (sum, order) {
            return sum + Number(order.total || 0);
        }, 0);

        var average = completedOrders.length > 0
            ? revenue / completedOrders.length
            : 0;

        setText("statistics-revenue", formatMoney(revenue));
        setText("statistics-completed", completedOrders.length);
        setText("statistics-cancelled", cancelledOrders.length);
        setText("statistics-average", formatMoney(average));

        console.log("Dữ liệu đơn hàng:", orders);
        console.log("Đơn hàng hoàn thành:", completedOrders);
        console.log(
            "Chi tiết sản phẩm của đơn hoàn thành:",
            completedOrders.map(function (order) {
                return {
                    status: order.status,
                    items: order.items
                };
            })
        );

        // Tổng hợp số lượng bán theo sản phẩm
        var products = {};

        completedOrders.forEach(function (order) {
            (order.items || []).forEach(function (item) {
                var name = item.productName || "Sản phẩm không tên";
                var key = item.productId || name;

                if (!products[key]) {
                    products[key] = {
                        name: name,
                        quantity: 0
                    };
                }

                products[key].quantity += Number(item.quantity || 0);
            });
        });

        var bestSelling = Object.keys(products).map(function (key) {
            return products[key];
        }).sort(function (a, b) {
            return b.quantity - a.quantity;
        });

        var productTable = document.getElementById("best-selling-products");

        if (productTable) {
            productTable.replaceChildren();

            if (bestSelling.length === 0) {
                var emptyRow = document.createElement("tr");
                var emptyCell = document.createElement("td");

                emptyCell.colSpan = 2;
                emptyCell.textContent = "Chưa có dữ liệu sản phẩm bán.";
                emptyRow.appendChild(emptyCell);
                productTable.appendChild(emptyRow);
            } else {
                bestSelling.slice(0, 5).forEach(function (product) {
                    var row = document.createElement("tr");
                    var nameCell = document.createElement("td");
                    var quantityCell = document.createElement("td");

                    nameCell.textContent = product.name;
                    quantityCell.textContent = product.quantity;

                    row.appendChild(nameCell);
                    row.appendChild(quantityCell);
                    productTable.appendChild(row);
                });
            }
        }

        // Thống kê số đơn theo trạng thái
        var statusCounts = {};

        orders.forEach(function (order) {
            var status = String(order.status || "Chưa xác định").trim();

            if (!status) {
                status = "Chưa xác định";
            }

            statusCounts[status] = (statusCounts[status] || 0) + 1;
        });

        var statusContainer = document.getElementById("order-status-statistics");

        if (statusContainer) {
            statusContainer.replaceChildren();

            var statuses = Object.keys(statusCounts);

            if (statuses.length === 0) {
                statusContainer.textContent = "Chưa có dữ liệu đơn hàng.";
            } else {
                statuses.forEach(function (status) {
                    var row = document.createElement("div");
                    row.className = "statistics-status-row";

                    var name = document.createElement("span");
                    name.textContent = status;

                    var count = document.createElement("strong");
                    count.textContent = statusCounts[status];

                    row.appendChild(name);
                    row.appendChild(count);
                    statusContainer.appendChild(row);
                });
            }
        }
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initStatistics);
    } else {
        initStatistics();
    }
})();
