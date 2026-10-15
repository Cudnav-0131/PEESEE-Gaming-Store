<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="PEESEE_GAMING.Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"
     OnItemCommand="rptCategoryProducts_ItemCommand" >
    <main class="category-page">

        <!-- TIÊU ĐỀ DANH MỤC -->
        <div class="category-header">
            <h1>
                <asp:Label ID="lblCategoryTitle"
                    runat="server" Text="Sản phẩm" />
            </h1>

            <p>
                <asp:Label ID="lblCategoryCount"
                    runat="server" Text="Đang tải sản phẩm..." />
            </p>
        </div>

        <div class="category-container">

            <!-- BỘ LỌC -->
            <aside class="category-filter">

                <div class="filter-header">
                    <div class="filter-title">
                        <img src="<%= ResolveUrl("~/images/filter.png") %>"
                             alt="Bộ lọc" />
                        <span>Bộ lọc tìm kiếm</span>
                    </div>
                </div>

                <!-- KHOẢNG GIÁ -->
                <section class="filter-section">
                    <div class="filter-section-title">
                        <span>Khoảng giá</span>
                        <button type="button" class="filter-toggle">⌃</button>
                    </div>

                    <div class="filter-options">
                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="all" checked />
                            <span>Tất cả</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="0-999999" />
                            <span>Dưới 1 triệu</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="1000000-2999999" />
                            <span>Từ 1 đến dưới 3 triệu</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="3000000-4999999" />
                            <span>Từ 3 đến dưới 5 triệu</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="5000000-9999999" />
                            <span>Từ 5 đến dưới 10 triệu</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="10000000-19999999" />
                            <span>Từ 10 đến dưới 20 triệu</span>
                        </label>

                        <label class="filter-option">
                            <input type="checkbox" name="price-range"
                                   value="20000000-35000000" />
                            <span>Từ 20 đến 35 triệu</span>
                        </label>
                    </div>

                    <div class="custom-price-title">
                        Hoặc nhập khoảng giá của bạn:
                    </div>

                    <div class="custom-price">
                        <input type="number" id="min-price"
                               placeholder="Giá thấp nhất" min="0" />

                        <span>~</span>

                        <input type="number" id="max-price"
                               placeholder="Giá cao nhất" min="0" />
                    </div>

                    <div class="price-range">
                        <input type="range" id="price-min-range"
                               min="0" max="35000000"
                               value="0" step="100000" />

                        <input type="range" id="price-max-range"
                               min="0" max="35000000"
                               value="35000000" step="100000" />
                    </div>
                </section>

                <!-- BỘ LỌC ĐẶC THÙ TỪNG DANH MỤC -->
                <div id="category-dynamic-filters">
                    <!-- JavaScript sẽ tạo bộ lọc phù hợp
                         với thông số sản phẩm của danh mục -->
                </div>

            </aside>

            <!-- DANH SÁCH SẢN PHẨM -->
            <section class="category-products">

                <div class="category-products-header">
                    <h2>
                        <asp:Label ID="lblProductsTitle"
                            runat="server" Text="Sản phẩm" />
                    </h2>

                    <select id="category-sort">
                        <option value="default">Sắp xếp mặc định</option>
                        <option value="price-asc">Giá thấp đến cao</option>
                        <option value="price-desc">Giá cao đến thấp</option>
                        <option value="name-asc">Tên A → Z</option>
                    </select>
                </div>

                <div id="category-product-list" class="category-product-list">

                    <asp:Repeater ID="rptCategoryProducts" runat="server"
                        OnItemCommand="rptCategoryProducts_ItemCommand"
                        OnItemDataBound="rptCategoryProducts_ItemDataBound" >
                        <ItemTemplate>
                            <article id="productCard" runat="server"
                                class="category-product-card"
                                data-price='<%# Eval("Price", "{0:0}") %>'
                                data-type='<%# Eval("Type") %>'
                                data-brand='<%# Eval("Brand") %>'>
 
                                <a href='<%# ResolveUrl("~/ProductDetail.aspx?id=" + Eval("Id")) %>'
                                   class="category-product-detail-link">
                                    <asp:Image ID="imgCategoryProduct"
                                        runat="server"
                                        ImageUrl='<%# ResolveUrl("~/" + Eval("Image")) %>'
                                        AlternateText="Ảnh sản phẩm"
                                        CssClass="category-product-image" />
                                </a>

                                <h3 class="category-product-name">
                                    <a href='<%# ResolveUrl("~/ProductDetail.aspx?id=" + Eval("Id")) %>'
                                       class="category-product-detail-link">
                                        <%#: Eval("Name") %>
                                    </a>
                                </h3>

                                <p class="category-product-price">
                                    <%#: Eval("Price", "{0:N0}") %>đ

                                    <span class="category-product-old-price">
                                        <%#: Eval("OldPrice", "{0:N0}") %>đ
                                    </span>
                                </p>

                                <p class="category-product-rating">
                                    ⭐ <%#: Eval("Rating", "{0:N1}") %>
                                    | Đã bán <%#: Eval("Sold") %>
                                </p>

                                <p class="category-product-stock">
                                    Tồn kho: <%#: Eval("Stock") %>
                                </p>

                                <asp:Button ID="btnAddCategoryCart"
                                    runat="server"
                                    Text="Thêm vào giỏ"
                                    CommandName="AddToCart"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="category-add-btn"
                                    CausesValidation="false" />

                            </article>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>

                <div id="category-no-result"
                     class="category-no-result hidden">
                    Không tìm thấy sản phẩm phù hợp.
                </div>

            </section>

        </div>
    </main>
    <script src="<%= ResolveUrl("~/Scripts/category-page.js") %>"></script>
</asp:Content>
