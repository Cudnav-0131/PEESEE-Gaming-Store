
document.addEventListener("DOMContentLoaded", function () {
    const productList = document.getElementById("category-product-list");
    const noResult = document.getElementById("category-no-result");
    const dynamicFilters = document.getElementById("category-dynamic-filters");

    const minPriceInput = document.getElementById("min-price");
    const maxPriceInput = document.getElementById("max-price");
    const minRange = document.getElementById("price-min-range");
    const maxRange = document.getElementById("price-max-range");
    const sortSelect = document.getElementById("category-sort");

    const priceCheckboxes = Array.from(
        document.querySelectorAll('input[name="price-range"]')
    );

    if (!productList) return;

    const cards = Array.from(
        productList.querySelectorAll(".category-product-card")
    );

    // Tên hiển thị thân thiện cho các thông số sản phẩm.
    const SPEC_LABELS = {
        cpu: "CPU",
        ram: "RAM",
        storage: "Dung lượng lưu trữ",
        gpu: "Card đồ họa",
        screen: "Kích thước màn hình",
        resolution: "Độ phân giải",
        refreshrate: "Tần số quét",
        panel: "Loại tấm nền",
        socket: "Socket",
        cores: "Số nhân",
        threads: "Số luồng",
        boost: "Xung nhịp",
        chipset: "Chipset",
        formfactor: "Kích thước / Form Factor",
        vram: "VRAM",
        interface: "Giao tiếp",
        boostclock: "Xung nhịp tăng tốc",
        material: "Chất liệu",
        fan: "Quạt đi kèm",
        connection: "Kết nối",
        power: "Công suất",
        channels: "Số kênh",
        pickup: "Hướng thu âm",
        switch: "Loại switch",
        layout: "Layout bàn phím",
        sensor: "Cảm biến",
        dpi: "DPI",
        buttons: "Số nút",
        weight: "Trọng lượng",
        size: "Kích thước",
        surface: "Bề mặt",
        thickness: "Độ dày",
        driver: "Driver",
        microphone: "Microphone",
        surround: "Âm thanh vòm",
        recline: "Độ ngả",
        load: "Tải trọng",
        width: "Chiều rộng",
        height: "Chiều cao",
        type: "Loại"
    };

    function normalizeKey(key) {
        return String(key).toLowerCase().replace(/[\s_-]/g, "");
    }

    function getSpecLabel(key) {
        const normalized = normalizeKey(key);

        if (SPEC_LABELS[normalized]) {
            return SPEC_LABELS[normalized];
        }

        return String(key)
            .replace(/[_-]/g, " ")
            .replace(/\b\w/g, function (letter) {
                return letter.toUpperCase();
            });
    }

    // Đọc thông số JSON được tạo từ Category.aspx.cs.
    cards.forEach(function (card, index) {
        card.dataset.originalIndex = String(index);
        card._specifications = {};

        try {
            const rawSpecs = card.dataset.specs || "{}";
            const parsedSpecs = JSON.parse(rawSpecs);

            Object.keys(parsedSpecs).forEach(function (key) {
                const value = parsedSpecs[key];

                if (value !== null && value !== undefined &&
                    String(value).trim() !== "") {
                    card._specifications[normalizeKey(key)] =
                        String(value).trim();
                }
            });
        } catch (error) {
            console.error(
                "Không đọc được thông số của sản phẩm:",
                card,
                error
            );
        }
    });

    function getCardPrice(card) {
        return Number(card.dataset.price || 0);
    }

    function getCardFacetValue(card, key) {
        if (key === "brand") {
            return (card.dataset.brand || "").trim();
        }

        if (key === "productType") {
            return (card.dataset.type || "").trim();
        }

        if (key.indexOf("spec:") === 0) {
            const specKey = key.substring(5);
            return card._specifications[specKey] || "";
        }

        return "";
    }

    // Tạo một nhóm bộ lọc nếu có ít nhất hai giá trị khác nhau.
    function createFacet(key, labelText, valueGetter) {
        if (!dynamicFilters) return;

        const values = Array.from(
            new Set(
                cards.map(function (card) {
                    return valueGetter(card);
                }).filter(function (value) {
                    return value !== "";
                })
            )
        );

        if (values.length < 2) return;

        values.sort(function (a, b) {
            return a.localeCompare(b, "vi", {
                numeric: true,
                sensitivity: "base"
            });
        });

        const section = document.createElement("section");
        section.className = "filter-section dynamic-filter-section";

        const heading = document.createElement("div");
        heading.className = "filter-section-title";

        const title = document.createElement("span");
        title.textContent = labelText;

        const toggle = document.createElement("button");
        toggle.type = "button";
        toggle.className = "filter-toggle";
        toggle.textContent = "⌃";
        toggle.setAttribute("aria-label", "Thu gọn bộ lọc " + labelText);

        heading.appendChild(title);
        heading.appendChild(toggle);

        const options = document.createElement("div");
        options.className = "filter-options";

        values.forEach(function (value) {
            const optionLabel = document.createElement("label");
            optionLabel.className = "filter-option";

            const checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.dataset.facetKey = key;
            checkbox.dataset.facetValue = value;

            const text = document.createElement("span");
            text.textContent = value;

            optionLabel.appendChild(checkbox);
            optionLabel.appendChild(text);
            options.appendChild(optionLabel);
        });

        toggle.addEventListener("click", function () {
            const isCollapsed = options.hidden;
            options.hidden = !isCollapsed;
            toggle.textContent = isCollapsed ? "⌃" : "⌄";
        });

        section.appendChild(heading);
        section.appendChild(options);
        dynamicFilters.appendChild(section);
    }

    // Các bộ lọc chung chỉ xuất hiện khi danh mục có nhiều giá trị.
    createFacet("brand", "Thương hiệu", function (card) {
        return getCardFacetValue(card, "brand");
    });

    createFacet("productType", "Loại sản phẩm", function (card) {
        return getCardFacetValue(card, "productType");
    });

    // Tự tìm tất cả trường thông số đang có trong danh mục.
    const specKeys = new Set();

    cards.forEach(function (card) {
        Object.keys(card._specifications).forEach(function (key) {
            specKeys.add(key);
        });
    });

    Array.from(specKeys).sort().forEach(function (key) {
        createFacet(
            "spec:" + key,
            getSpecLabel(key),
            function (card) {
                return getCardFacetValue(card, "spec:" + key);
            }
        );
    });

    // Lấy các khoảng giá đang được chọn.
    function getSelectedPriceRanges() {
        return priceCheckboxes
            .filter(function (checkbox) {
                return checkbox.checked && checkbox.value !== "all";
            })
            .map(function (checkbox) {
                const parts = checkbox.value.split("-");

                return {
                    min: Number(parts[0]),
                    max: Number(parts[1])
                };
            });
    }

    function getCustomMinPrice() {
        if (minPriceInput.value !== "") {
            return Number(minPriceInput.value);
        }

        return Number(minRange.value);
    }

    function getCustomMaxPrice() {
        if (maxPriceInput.value !== "") {
            return Number(maxPriceInput.value);
        }

        return Number(maxRange.value);
    }

    // Thu thập các checkbox thông số đang được chọn.
    function getSelectedFacets() {
        const selected = new Map();

        if (!dynamicFilters) return selected;

        dynamicFilters.querySelectorAll(
            'input[data-facet-key]:checked'
        ).forEach(function (checkbox) {
            const key = checkbox.dataset.facetKey;
            const value = checkbox.dataset.facetValue;

            if (!selected.has(key)) {
                selected.set(key, new Set());
            }

            selected.get(key).add(value);
        });

        return selected;
    }

    // Áp dụng đồng thời bộ lọc giá và thông số.
    function applyFilters() {
        const selectedRanges = getSelectedPriceRanges();
        const minPrice = getCustomMinPrice();
        const maxPrice = getCustomMaxPrice();
        const selectedFacets = getSelectedFacets();

        let visibleCount = 0;

        cards.forEach(function (card) {
            const price = getCardPrice(card);

            const matchesRange =
                selectedRanges.length === 0 ||
                selectedRanges.some(function (range) {
                    return price >= range.min && price <= range.max;
                });

            const matchesCustomPrice =
                price >= minPrice && price <= maxPrice;

            let matchesFacets = true;

            selectedFacets.forEach(function (selectedValues, key) {
                const cardValue = getCardFacetValue(card, key);

                if (!selectedValues.has(cardValue)) {
                    matchesFacets = false;
                }
            });

            const matches =
                matchesRange && matchesCustomPrice && matchesFacets;

            card.style.display = matches ? "" : "none";

            if (matches) visibleCount++;
        });

        if (noResult) {
            noResult.classList.toggle("hidden", visibleCount !== 0);
        }

        const countText = document.querySelector(".category-header p");

        if (countText) {
            countText.textContent =
                "Hiển thị " + visibleCount + " / " +
                cards.length + " sản phẩm";
        }
    }

    // Sắp xếp danh sách.
    function sortProducts() {
        const sortValue = sortSelect ? sortSelect.value : "default";
        const sortedCards = cards.slice();

        sortedCards.sort(function (a, b) {
            if (sortValue === "price-asc") {
                return getCardPrice(a) - getCardPrice(b);
            }

            if (sortValue === "price-desc") {
                return getCardPrice(b) - getCardPrice(a);
            }

            if (sortValue === "name-asc") {
                const nameA = a.querySelector(".category-product-name");
                const nameB = b.querySelector(".category-product-name");

                const textA = nameA ? nameA.textContent.trim() : "";
                const textB = nameB ? nameB.textContent.trim() : "";

                return textA.localeCompare(textB, "vi");
            }

            return Number(a.dataset.originalIndex) -
                Number(b.dataset.originalIndex);
        });

        sortedCards.forEach(function (card) {
            productList.appendChild(card);
        });

        applyFilters();
    }

    // Chỉ chọn một hoặc nhiều khoảng giá, hoặc chọn Tất cả.
    priceCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener("change", function () {
            if (checkbox.value === "all" && checkbox.checked) {
                priceCheckboxes.forEach(function (item) {
                    if (item.value !== "all") {
                        item.checked = false;
                    }
                });
            } else if (checkbox.value !== "all" && checkbox.checked) {
                const allCheckbox = priceCheckboxes.find(function (item) {
                    return item.value === "all";
                });

                if (allCheckbox) allCheckbox.checked = false;
            }

            const hasSelectedRange = priceCheckboxes.some(function (item) {
                return item.checked && item.value !== "all";
            });

            if (!hasSelectedRange) {
                const allCheckbox = priceCheckboxes.find(function (item) {
                    return item.value === "all";
                });

                if (allCheckbox) allCheckbox.checked = true;
            }

            applyFilters();
        });
    });

    // Đồng bộ thanh trượt và ô nhập giá.
    function syncPriceRanges(changedRange) {
        let min = Number(minRange.value);
        let max = Number(maxRange.value);

        if (min > max) {
            if (changedRange === minRange) {
                max = min;
                maxRange.value = String(max);
            } else {
                min = max;
                minRange.value = String(min);
            }
        }

        minPriceInput.value = String(min);
        maxPriceInput.value = String(max);

        applyFilters();
    }

    minRange.addEventListener("input", function () {
        syncPriceRanges(minRange);
    });

    maxRange.addEventListener("input", function () {
        syncPriceRanges(maxRange);
    });

    minPriceInput.addEventListener("input", function () {
        const value = Number(minPriceInput.value);

        if (minPriceInput.value === "") {
            applyFilters();
            return;
        }

        if (value < Number(minRange.min) ||
            value > Number(maxRange.max)) {
            return;
        }

        minRange.value = String(value);

        if (value > Number(maxRange.value)) {
            maxRange.value = String(value);
            maxPriceInput.value = String(value);
        }

        applyFilters();
    });

    maxPriceInput.addEventListener("input", function () {
        const value = Number(maxPriceInput.value);

        if (maxPriceInput.value === "") {
            applyFilters();
            return;
        }

        if (value < Number(minRange.min) ||
            value > Number(maxRange.max)) {
            return;
        }

        maxRange.value = String(value);

        if (value < Number(minRange.value)) {
            minRange.value = String(value);
            minPriceInput.value = String(value);
        }

        applyFilters();
    });

    if (dynamicFilters) {
        dynamicFilters.addEventListener("change", applyFilters);
    }

    if (sortSelect) {
        sortSelect.addEventListener("change", sortProducts);
    }

    // Khởi tạo lần đầu.
    sortProducts();
});
