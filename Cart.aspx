<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="PEESEE_GAMING.Cart" %>
<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main class="cart-page">
        <div class="cart-container">

            <!-- Bên trái: danh sách sản phẩm -->
            <section class="cart-content">

                <h1>Giỏ hàng</h1>

                <p class="cart-subtitle">
                    Các sản phẩm bạn đã thêm vào giỏ hàng
                </p>

                <div class="cart-table-header">
                    <span>Sản phẩm</span>
                    <span>Đơn giá</span>
                    <span>Số lượng</span>
                    <span>Thành tiền</span>
                </div>

                <asp:Panel ID="pnlEmptyCart" runat="server"
                    CssClass="cart-empty">
                    <p>Giỏ hàng đang trống.</p>
                </asp:Panel>

                <asp:Repeater ID="rptCart" runat="server"
                    OnItemCommand="rptCart_ItemCommand">
                    <ItemTemplate>
                        <div class="cart-item">

                            <div class="cart-product">
                                <asp:Image ID="imgCartProduct" runat="server"
                                    ImageUrl='<%# ResolveUrl("~/" + Eval("Image")) %>'
                                    AlternateText="Ảnh sản phẩm" />

                                <div class="cart-product-info">
                                    <h3><%#: Eval("Name") %></h3>

                                    <asp:Button ID="btnRemove" runat="server"
                                        Text="Xóa"
                                        CommandName="Remove"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="cart-remove-btn"
                                        CausesValidation="false" />
                                </div>
                            </div>

                            <div class="cart-price">
                                <%#: Eval("Price", "{0:N0}") %> đ
                            </div>

                            <div class="quantity-control">
                                <asp:Button ID="btnDecrease" runat="server"
                                    Text="-"
                                    CommandName="Decrease"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="quantity-btn"
                                    CausesValidation="false" />

                                <span><%#: Eval("Quantity") %></span>

                                <asp:Button ID="btnIncrease" runat="server"
                                    Text="+"
                                    CommandName="Increase"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="quantity-btn"
                                    CausesValidation="false" />
                            </div>

                            <div class="cart-item-total">
                                <%#: Eval("Subtotal", "{0:N0}") %> đ
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <a href="<%= ResolveUrl("~/Default.aspx") %>"
                    class="continue-shopping">
                    ← Tiếp tục mua hàng
                </a>

            </section>

            <!-- Bên phải: tóm tắt đơn hàng -->
            <aside class="order-summary">

                <h2>Tóm tắt đơn hàng</h2>

                <div class="summary-row">
                    <span>Tạm tính</span>
                    <asp:Label ID="lblSubtotal" runat="server" Text="0 đ" />
                </div>

                <div class="summary-row">
                    <span>Phí vận chuyển</span>
                    <span>Miễn phí</span>
                </div>

                <div class="summary-row">
                    <span>Thuế</span>
                    <span>0 đ</span>
                </div>

                <hr />

                <div class="summary-total">
                    <span>Tổng cộng</span>
                    <strong>
                        <asp:Label ID="lblOrderTotal" runat="server" Text="0 đ" />
                    </strong>
                </div>

                <asp:Button ID="btnCheckout" runat="server"
                    Text="Tiến hành thanh toán"
                    CssClass="checkout-btn"
                    CausesValidation="false"
                    OnClick="btnCheckout_Click"/>

            </aside>

        </div>
    </main>

    <style>
    .cart-page {
        max-width: 1200px;
        margin: 40px auto;
        padding: 0 30px;
        box-sizing: border-box;
    }

    .cart-container {
        display: grid;
        grid-template-columns: minmax(0, 1fr) 320px;
        gap: 25px;
        align-items: start;
    }

    .cart-content h1 {
        margin: 0 0 5px;
        font-size: 30px;
        font-weight: 500;
    }

    .cart-subtitle {
        margin: 0 0 25px;
        color: #777;
        font-size: 13px;
    }

    .cart-table-header,
    .cart-item {
        display: grid;
        grid-template-columns: minmax(0, 1fr) 110px 120px 110px;
        gap: 15px;
        align-items: center;
    }

    .cart-table-header {
        padding: 14px 15px;
        background: #f5f6f7;
        color: #555;
        font-size: 12px;
        font-weight: 600;
    }

    .cart-item {
        padding: 15px;
        background: #fff;
        border-bottom: 1px solid #eee;
    }

    .cart-product {
        display: flex;
        align-items: center;
        gap: 15px;
        min-width: 0;
    }

    .cart-product img {
        width: 70px;
        height: 70px;
        object-fit: contain;
        flex-shrink: 0;
    }

    .cart-product-info {
        min-width: 0;
    }

    .cart-product-info h3 {
        margin: 0 0 8px;
        font-size: 16px;
        font-weight: 600;
        color: #222;
    }

    .cart-product-info input.cart-remove-btn {
        display: block;
        width: auto;
        max-width: none;
        height: auto;
        padding: 0;
        border: none;
        background: none;
        color: #888;
        font-size: 13px;
        font-weight: normal;
        cursor: pointer;
    }

    .cart-product-info input.cart-remove-btn:hover {
        background: none;
        color: #e53935;
    }

    .cart-price,
    .cart-item-total {
        font-size: 14px;
        color: #333;
    }

    .cart-item-total {
        font-weight: 500;
    }

    .quantity-control {
        display: flex;
        align-items: center;
        gap: 0;
    }

    .quantity-control input.quantity-btn {
        display: block;
        width: 28px;
        max-width: none;
        height: 28px;
        padding: 0;
        border: 1px solid #ddd;
        border-radius: 0;
        background: #fff;
        color: #333;
        font-size: 13px;
        cursor: pointer;
    }

    .quantity-control input.quantity-btn:hover {
        background: #f5f5f5;
    }

    .quantity-control span {
        width: 30px;
        height: 28px;
        display: flex;
        align-items: center;
        justify-content: center;
        box-sizing: border-box;
        border-top: 1px solid #ddd;
        border-bottom: 1px solid #ddd;
        font-size: 13px;
    }

    .cart-empty {
        padding: 4px 15px;
        color: #666;
        font-size: 14px;
    }

    .order-summary {
        padding: 20px;
        box-sizing: border-box;
        background: #f8f9fa;
        border: 1px solid #e5e5e5;
        border-radius: 7px;
    }

    .order-summary h2 {
        margin: 0 0 20px;
        font-size: 18px;
        color: #333;
    }

    .summary-row {
        display: flex;
        justify-content: space-between;
        gap: 12px;
        margin-bottom: 15px;
        font-size: 13px;
        color: #666;
    }

    .order-summary hr {
        border: none;
        border-top: 1px solid #ddd;
        margin: 20px 0;
    }

    .summary-total {
        display: flex;
        justify-content: space-between;
        align-items: center;
        gap: 12px;
        margin-bottom: 20px;
        font-size: 14px;
    }

    .summary-total strong {
        font-size: 18px;
        color: #222;
    }

    .checkout-btn {
        display: block;
        width: 100%;
        max-width: none;
        height: 42px;
        padding: 0;
        border: none;
        border-radius: 4px;
        background: #1e88e5;
        color: white;
        font-size: 14px;
        font-weight: 600;
        cursor: pointer;
    }

    .checkout-btn:hover {
        background: #1976d2;
    }

    .continue-shopping {
        display: inline-block;
        margin-top: 20px;
        color: #d69a24;
        text-decoration: none;
        font-size: 13px;
    }

    .continue-shopping:hover {
        text-decoration: underline;
    }

    @media (max-width: 900px) {
        .cart-container {
            grid-template-columns: 1fr;
        }

        .order-summary {
            width: 100%;
        }
    }

    @media (max-width: 600px) {
        .cart-page {
            padding: 0 12px;
        }

        .cart-table-header,
        .cart-item {
            grid-template-columns: minmax(0, 1fr) 75px 85px 85px;
            gap: 8px;
            padding: 10px 6px;
        }

        .cart-table-header {
            font-size: 10px;
        }

        .cart-product {
            gap: 6px;
        }

        .cart-product img {
            width: 45px;
            height: 45px;
        }

        .cart-product-info h3 {
            font-size: 12px;
            overflow-wrap: anywhere;
        }

        .cart-price,
        .cart-item-total {
            font-size: 11px;
        }
    }
    </style>
</asp:Content>
