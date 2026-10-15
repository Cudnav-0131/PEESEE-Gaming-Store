
<%@ Page Title="Chi tiết sản phẩm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="PEESEE_GAMING.ProductDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="product-detail-page">
        <div class="product-detail-container">

            <!-- Thông tin sản phẩm -->
            <asp:Panel ID="pnlProduct" runat="server" Visible="false">

                <section class="product-detail-main">

                    <!-- Ảnh sản phẩm -->
                    <div class="product-detail-image">
                        <asp:Image ID="imgProduct"
                            runat="server"
                            AlternateText="Ảnh sản phẩm"
                            CssClass="product-detail-image-main" />
                    </div>

                    <!-- Thông tin -->
                    <div class="product-detail-info">

                        <p class="product-detail-brand">
                            <asp:Label ID="lblBrand" runat="server" />
                        </p>

                        <h1>
                            <asp:Label ID="lblProductName" runat="server" />
                        </h1>

                        <div class="product-detail-rating">
                            <span>
                                ⭐ <asp:Label ID="lblRating" runat="server" />
                            </span>

                            <span>
                                Đã bán <asp:Label ID="lblSold" runat="server" />
                            </span>
                        </div>

                        <!-- Giá bán -->
                        <div class="product-detail-price">

                            <strong>
                                <asp:Label ID="lblPrice" runat="server" />
                            </strong>

                            <asp:Label ID="lblOldPrice"
                                runat="server"
                                CssClass="product-detail-old-price"
                                Visible="false" />

                            <asp:Label ID="lblDiscount"
                                runat="server"
                                CssClass="product-detail-discount"
                                Visible="false" />

                        </div>

                        <!-- Tồn kho -->
                        <asp:Label ID="lblStock"
                            runat="server"
                            CssClass="product-detail-stock"
                            Text="Còn hàng" />
                        <asp:Label ID="lblProductMessage"
                            runat="server"
                            CssClass="product-detail-message"
                            EnableViewState="false" />

                        <!-- Số lượng -->
                        <div class="product-detail-quantity">

                            <span>Số lượng</span>

                            <div class="quantity-control">

                                <asp:Button ID="btnDecreaseQuantity"
                                    runat="server"
                                    Text="−"
                                    CssClass="quantity-decrease"
                                    OnClick="btnDecreaseQuantity_Click"
                                    CausesValidation="false" />

                                <asp:TextBox ID="txtQuantity"
                                    runat="server"
                                    Text="1"
                                    TextMode="Number"
                                    CssClass="detail-quantity-input" />

                                <asp:Button ID="btnIncreaseQuantity"
                                    runat="server"
                                    Text="+"
                                    CssClass="quantity-increase"
                                    OnClick="btnIncreaseQuantity_Click"
                                    CausesValidation="false" />

                            </div>

                        </div>

                        <!-- Nút mua hàng -->
                        <div class="product-detail-actions">

                            <asp:Button ID="btnAddToCart"
                                runat="server"
                                Text="Thêm vào giỏ"
                                CssClass="add-detail-cart-btn"
                                OnClick="btnAddToCart_Click"
                                CausesValidation="false" />

                            <asp:Button ID="btnBuyNow"
                                runat="server"
                                Text="Mua ngay"
                                CssClass="buy-now-btn"
                                OnClick="btnBuyNow_Click"
                                CausesValidation="false" />

                        </div>

                    </div>
                </section>

                <!-- Mô tả -->
                <section class="product-detail-section">

                    <h2>Mô tả sản phẩm</h2>

                    <div id="product-detail-description">
                        <asp:Literal ID="litDescription"
                            runat="server"
                            Mode="Encode" />
                    </div>

                </section>

                <!-- Thông số kỹ thuật -->
                <section class="product-detail-section">

                    <h2>Thông số kỹ thuật</h2>

                    <div class="product-specifications">

                        <asp:Repeater ID="rptSpecifications" runat="server">
                            <ItemTemplate>

                                <div class="specification-row">

                                    <span class="spec-key">
                                        <%#: Eval("Key") %>
                                    </span>

                                    <span class="spec-value">
                                        <%#: Eval("Value") %>
                                    </span>

                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Label ID="lblNoSpecifications"
                            runat="server"
                            Text="Chưa có thông số kỹ thuật."
                            Visible="false" />

                    </div>

                </section>

            </asp:Panel>

            <!-- Không tìm thấy sản phẩm -->
            <asp:Panel ID="pnlProductNotFound"
                runat="server"
                Visible="false"
                CssClass="product-detail-not-found">

                <h2>Không tìm thấy sản phẩm</h2>

                <p>Sản phẩm bạn đang xem không tồn tại hoặc đã bị xóa.</p>

                <a href="<%= ResolveUrl("~/Default.aspx") %>">
                    Quay lại trang chủ
                </a>

            </asp:Panel>

        </div>
    </main>

</asp:Content>
