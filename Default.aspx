<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PEESEE_GAMING._Default" %>

<asp:Content ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <!-- BANNER PEESEE GAMING STORE -->
    <div class="peesee-hero-banner">
        <a href="#featured-products" aria-label="Khám phá sản phẩm PEESEE Gaming Store">
            <img
                src="<%= ResolveUrl("~/Images/peesee-banner.png") %>"
                alt="PEESEE GAMING STORE - Play, Upgrade, Win" />
        </a>
    </div>

    <div id="featured-products" class="featured-products-title">
        <h2>Sản phẩm nổi bật</h2>
        <p>Khám phá các thiết bị gaming dành cho bạn</p>
    </div>

    <div class="product-list">

        <asp:Repeater ID="rptProducts" runat="server"
            OnItemCommand="rptProducts_ItemCommand">
            <ItemTemplate>
                <div class="product-card">

                    <a href='<%# ResolveUrl("~/ProductDetail.aspx?id=" + Eval("Id")) %>'>
                        <asp:Image
                            ID="imgProduct"
                            runat="server"
                            ImageUrl='<%# ResolveUrl("~/" + Eval("Image")) %>'
                            AlternateText="Ảnh sản phẩm"
                            CssClass="product-image" />
                    </a>

                    <p class="product-brand">
                        <%#: Eval("Brand") %>
                    </p>

                    <h3 class="product-name">
                        <a href='<%# ResolveUrl("~/ProductDetail.aspx?id=" + Eval("Id")) %>'
                           class="product-detail-link">
                            <%#: Eval("Name") %>
                        </a>
                    </h3>

                    <p class="product-category">
                        <%#: Eval("Category") %>
                    </p>

                    <div class="product-pricing">
                        <span class="product-price">
                            <%#: Eval("Price", "{0:N0}") %> đ
                        </span>

                        <span class="product-old-price">
                            <%#: Eval("OldPrice", "{0:N0}") %> đ
                        </span>
                    </div>

                    <p class="product-rating">
                        ⭐ <%#: Eval("Rating", "{0:N1}") %>
                        | Đã bán <%#: Eval("Sold") %>
                    </p>

                    <p class="product-stock">
                        Tồn kho: <%#: Eval("Stock") %>
                    </p>
                    <asp:Button
                        ID="btnAddToCart"
                        runat="server"
                        Text="Thêm vào giỏ hàng"
                        CommandName="AddToCart"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn-add-cart" />

                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

    <style>
        .product-list {
            display: grid;
            grid-template-columns: repeat(4, minmax(0, 1fr));
            gap: 18px;
            margin-top: 24px;
        }

        .product-card {
            min-width: 0;
            padding: 16px;
            box-sizing: border-box;
            background: #fff;
            border: 1px solid #e5e7eb;
            border-radius: 10px;
            transition: box-shadow 0.2s, transform 0.2s;
        }

        .product-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.08);
        }

        .product-image {
            display: block;
            width: 100%;
            height: 200px;
            object-fit: contain;
            margin-bottom: 12px;
        }

        .product-brand {
            margin: 0 0 6px;
            color: #777;
            font-size: 13px;
        }

        .product-card h3 {
            min-height: 44px;
            margin: 0 0 8px;
            font-size: 16px;
            line-height: 1.4;
        }

        .product-category {
            color: #666;
            font-size: 13px;
        }

        .product-pricing {
            display: flex;
            flex-direction: column;
            gap: 4px;
        }

        .product-price {
            color: #d90000;
            font-size: 19px;
            font-weight: bold;
        }

        .product-old-price {
            color: #999;
            font-size: 13px;
            text-decoration: line-through;
        }

        .product-rating,
        .product-stock {
            font-size: 13px;
            color: #666;
        }

        @media (max-width: 1100px) {
            .product-list {
                grid-template-columns: repeat(2, minmax(0, 1fr));
            }
        }

        @media (max-width: 600px) {
            .product-list {
                grid-template-columns: 1fr;
            }
        }
        /* NÚT THÊM VÀO GIỎ - GIAO DIỆN CŨ */
        .product-card input.btn-add-cart {
            display: block;
            width: 100%;
            max-width: none;
            height: 38px;
            padding: 0;
            margin-top: 0;
            box-sizing: border-box;

            border: none;
            border-radius: 6px;

            background: #1e88e5;
            color: white;

            font-size: 14px;
            font-weight: 600;

            cursor: pointer;
            transition: background 0.2s;
        }

        .product-card input.btn-add-cart:hover {
            background: #1976d2;
        }

        .product-card input.btn-add-cart:disabled {
            background: #ccc;
            color: #666;
            cursor: not-allowed;
        }
        /* ===== PEESEE GAMING STORE BANNER ===== */

        .peesee-hero-banner {
            width: 100%;
            aspect-ratio: 16 / 6.2;
            overflow: hidden;
            border-radius: 14px;
            margin-bottom: 35px;
            background: #020817;
            box-shadow: 0 8px 25px rgba(0, 120, 255, 0.15);
        }

        .peesee-hero-banner a {
            display: block;
            width: 100%;
            height: 100%;
        }

        .peesee-hero-banner img {
            display: block;
            width: 100%;
            height: 100%;
            object-fit: cover;
            object-position: center;
            transition: transform 0.4s ease;
        }

        .peesee-hero-banner:hover img {
            transform: scale(1.02);
        }

        /* Tiêu đề sản phẩm */
        .featured-products-title {
            margin-bottom: 20px;
        }

        .featured-products-title h2 {
            margin: 0 0 8px;
            color: #172554;
            font-size: 26px;
            font-weight: 700;
        }

        .featured-products-title p {
            margin: 0;
            color: #64748b;
            font-size: 14px;
        }

        /* Responsive điện thoại */
        @media (max-width: 600px) {
            .peesee-hero-banner {
                aspect-ratio: 16 / 9;
                border-radius: 8px;
                margin-bottom: 24px;
            }

            .featured-products-title h2 {
                font-size: 21px;
            }
        }
    </style>

</asp:Content>