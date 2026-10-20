(function () {
    "use strict";

    // Lấy dữ liệu đơn hàng do C# truyền xuống HiddenField
    function getOrders() {
        const hiddenField = document.getElementById("hfRevenueOrdersJson");

        if (!hiddenField || !hiddenField.value) {
            return [];
        }

        try {
            const orders = JSON.parse(hiddenField.value);
            return Array.isArray(orders) ? orders : [];
        } catch (error) {
            console.error("Không đọc được dữ liệu đơn hàng:", error);
            return [];
        }
    }

    // Chỉ lấy những đơn hàng đã hoàn thành
    function getCompletedOrders() {
        return getOrders().filter(function (order) {
            return String(order.status || "").toLowerCase() === "completed";
        }).filter(function (order) {
            const date = new Date(order.createdAt);
            const total = Number(order.total);

            return !isNaN(date.getTime()) && Number.isFinite(total);
        });
    }

    // Định dạng tiền Việt Nam
    function formatMoney(value) {
        return new Intl.NumberFormat("vi-VN", {
            maximumFractionDigits: 0
        }).format(value) + " ₫";
    }

    // Tạo dữ liệu theo khoảng thời gian đang chọn
    function buildChartData(orders, period) {
        const data = new Map();
        const now = new Date();

        if (period === "week") {
            // Tính ngày thứ Hai của tuần hiện tại
            const monday = new Date(
                now.getFullYear(),
                now.getMonth(),
                now.getDate()
            );

            const day = monday.getDay();
            const daysSinceMonday = (day + 6) % 7;
            monday.setDate(monday.getDate() - daysSinceMonday);

            const labels = [
                "Thứ 2", "Thứ 3", "Thứ 4",
                "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật"
            ];

            const keys = [];

            for (let i = 0; i < 7; i++) {
                const date = new Date(monday);
                date.setDate(monday.getDate() + i);

                const key = [
                    date.getFullYear(),
                    String(date.getMonth() + 1).padStart(2, "0"),
                    String(date.getDate()).padStart(2, "0")
                ].join("-");

                keys.push(key);
                data.set(key, 0);
            }

            orders.forEach(function (order) {
                const date = new Date(order.createdAt);
                const key = [
                    date.getFullYear(),
                    String(date.getMonth() + 1).padStart(2, "0"),
                    String(date.getDate()).padStart(2, "0")
                ].join("-");

                if (data.has(key)) {
                    data.set(key, data.get(key) + Number(order.total));
                }
            });

            return {
                labels: labels,
                values: keys.map(function (key) {
                    return data.get(key);
                })
            };
        }

        if (period === "month") {
            // Doanh thu từng tháng trong năm hiện tại
            const year = now.getFullYear();

            for (let month = 0; month < 12; month++) {
                const key = year + "-" + String(month + 1).padStart(2, "0");
                data.set(key, 0);
            }

            orders.forEach(function (order) {
                const date = new Date(order.createdAt);

                if (date.getFullYear() === year) {
                    const key = year + "-" +
                        String(date.getMonth() + 1).padStart(2, "0");

                    data.set(key, data.get(key) + Number(order.total));
                }
            });

            return {
                labels: [
                    "T1", "T2", "T3", "T4", "T5", "T6",
                    "T7", "T8", "T9", "T10", "T11", "T12"
                ],
                values: Array.from(data.values())
            };
        }

        // Tất cả: tổng doanh thu theo từng tháng và năm
        orders.forEach(function (order) {
            const date = new Date(order.createdAt);
            const key = date.getFullYear() + "-" +
                String(date.getMonth() + 1).padStart(2, "0");

            data.set(
                key,
                (data.get(key) || 0) + Number(order.total)
            );
        });

        const keys = Array.from(data.keys()).sort();

        return {
            labels: keys.map(function (key) {
                const parts = key.split("-");
                return parts[1] + "/" + parts[0];
            }),
            values: keys.map(function (key) {
                return data.get(key);
            })
        };
    }

    // Vẽ biểu đồ bằng Canvas
    function drawChart(container, labels, values) {
        let canvas = container.querySelector("canvas");

        if (!canvas) {
            container.innerHTML = "";
            canvas = document.createElement("canvas");
            canvas.id = "revenue-chart-canvas";
            canvas.style.display = "block";
            canvas.style.width = "100%";
            canvas.style.height = "280px";
            container.appendChild(canvas);
        }

        container.style.minHeight = "280px";

        const width = Math.max(container.clientWidth, 320);
        const height = 280;
        const ratio = window.devicePixelRatio || 1;

        canvas.width = width * ratio;
        canvas.height = height * ratio;

        const context = canvas.getContext("2d");

        context.setTransform(ratio, 0, 0, ratio, 0, 0);
        context.clearRect(0, 0, width, height);

        if (values.length === 0) {
            context.fillStyle = "#777";
            context.font = "14px Arial";
            context.textAlign = "center";
            context.fillText(
                "Chưa có đơn hàng hoàn thành",
                width / 2,
                height / 2
            );
            return;
        }

        const padding = {
            top: 24,
            right: 18,
            bottom: 48,
            left: 72
        };

        const chartWidth = Math.max(
            1,
            width - padding.left - padding.right
        );

        const chartHeight = height - padding.top - padding.bottom;

        const maxValue = Math.max.apply(null, values);

        let scaleMax = 1;

        if (maxValue > 0) {
            const rawStep = (maxValue * 1.15) / 4;
            const magnitude = Math.pow(
                10,
                Math.floor(Math.log10(rawStep))
            );

            const normalized = rawStep / magnitude;

            const niceFactors = [
                1, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10
            ];

            let factor = niceFactors.find(function (number) {
                return number >= normalized;
            });

            if (!factor) {
                factor = 10;
            }

            const step = factor * magnitude;

            // Trục dọc luôn chia thành 4 khoảng bằng nhau
            scaleMax = step * 4;
        }

        // Các đường lưới và nhãn giá trị
        context.font = "11px Arial";
        context.textAlign = "right";
        context.textBaseline = "middle";

        for (let i = 0; i <= 4; i++) {
            const y = padding.top + chartHeight * i / 4;
            const value = scaleMax * (4 - i) / 4;

            context.beginPath();
            context.moveTo(padding.left, y);
            context.lineTo(width - padding.right, y);
            context.strokeStyle = "#e5e7eb";
            context.lineWidth = 1;
            context.stroke();

            context.fillStyle = "#6b7280";
            context.fillText(
                formatMoney(value),
                padding.left - 8,
                y
            );
        }

        const slotWidth = chartWidth / values.length;
        const barWidth = Math.min(slotWidth * 0.62, 42);

        values.forEach(function (value, index) {
            const barHeight = Math.max(
                0,
                chartHeight * value / scaleMax
            );

            const x = padding.left +
                index * slotWidth +
                (slotWidth - barWidth) / 2;

            const y = padding.top + chartHeight - barHeight;

            context.fillStyle = "#4f46e5";

            if (barHeight > 0) {
                context.fillRect(x, y, barWidth, barHeight);
            }

            context.fillStyle = "#6b7280";
            context.font = "11px Arial";
            context.textAlign = "center";
            context.textBaseline = "top";

            // Rút gọn nhãn khi có nhiều cột
            const label = labels.length > 12
                ? labels[index].slice(0, 5)
                : labels[index];

            context.fillText(
                label,
                padding.left + index * slotWidth + slotWidth / 2,
                padding.top + chartHeight + 10
            );
        });
    }

    // Hàm dùng chung để cập nhật biểu đồ
    window.updateRevenueChart = function () {
        const container = document.getElementById("revenue-chart");

        if (!container) {
            return;
        }

        const periodSelect = document.getElementById("revenue-period");
        const period = periodSelect ? periodSelect.value : "all";

        const orders = getCompletedOrders();
        const chartData = buildChartData(orders, period);

        drawChart(container, chartData.labels, chartData.values);
    };

    // Khởi tạo biểu đồ và sự kiện lọc
    function initializeChart() {
        const periodSelect = document.getElementById("revenue-period");

        if (periodSelect && !periodSelect.dataset.chartBound) {
            periodSelect.addEventListener("change", function () {
                window.updateRevenueChart();
            });

            periodSelect.dataset.chartBound = "true";
        }

        window.updateRevenueChart();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initializeChart);
    } else {
        initializeChart();
    }

    // Vẽ lại khi thay đổi kích thước cửa sổ
    window.addEventListener("resize", function () {
        window.updateRevenueChart();
    });
})();