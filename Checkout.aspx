<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="PEESEE_GAMING.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="checkout-page">

        <h1>Thanh toán</h1>

        <div class="checkout-container">

            <!-- Thông tin giao hàng -->
            <section class="shipping-info">

                <h2>Thông tin giao hàng</h2>

                <asp:Panel ID="pnlAccountInfoActions" runat="server"
                    Visible="false" CssClass="checkout-profile-actions">

                    <p class="checkout-profile-note">
                        Thông tin giao hàng được lấy từ tài khoản đã đăng nhập.
                    </p>

                    <asp:Button ID="btnEditCustomerInfo" runat="server"
                        Text="Sửa thông tin thanh toán"
                        CssClass="checkout-profile-btn"
                        OnClick="btnEditCustomerInfo_Click" />

                    <asp:Button ID="btnSaveCustomerInfo" runat="server"
                        Text="Lưu thông tin"
                        CssClass="checkout-profile-btn"
                        Visible="false"
                        OnClick="btnSaveCustomerInfo_Click" />

                    <asp:Button ID="btnCancelCustomerInfo" runat="server"
                        Text="Hủy"
                        CssClass="checkout-profile-btn checkout-cancel-btn"
                        Visible="false"
                        OnClick="btnCancelCustomerInfo_Click" />

                    <asp:Label ID="lblProfileMessage" runat="server"
                        CssClass="checkout-profile-message" />

                </asp:Panel>

                <div class="form-group">
                    <asp:Label ID="lblFullName" runat="server"
                        AssociatedControlID="txtFullName"
                        Text="Họ và tên" />

                    <asp:TextBox ID="txtFullName" runat="server"
                        MaxLength="100"
                        placeholder="Nhập họ và tên" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblPhone" runat="server"
                        AssociatedControlID="txtPhone"
                        Text="Số điện thoại" />

                    <asp:TextBox ID="txtPhone" runat="server"
                        TextMode="Phone"
                        MaxLength="15"
                        placeholder="Nhập số điện thoại" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblAddress" runat="server"
                        AssociatedControlID="txtAddress"
                        Text="Địa chỉ giao hàng" />

                    <asp:TextBox ID="txtAddress" runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        MaxLength="250"
                        placeholder="Nhập địa chỉ nhận hàng" />
                </div>

                <h2>Phương thức thanh toán</h2>

                <div class="payment-option">
                    <asp:RadioButton ID="rdoCOD" runat="server"
                        GroupName="PaymentMethod"
                        Text="Thanh toán khi nhận hàng"
                        Checked="true" />
                </div>

                <div class="payment-option">
                    <asp:RadioButton ID="rdoBank" runat="server"
                        GroupName="PaymentMethod"
                        Text="Chuyển khoản ngân hàng" />
                </div>

                <div id="bank-transfer-info"
                    class="bank-transfer-info hidden">

                    <h3>Thông tin chuyển khoản</h3>

                    <p class="bank-transfer-note">
                        Vui lòng chuyển khoản đúng số tiền đơn hàng
                        trước khi hoàn tất.
                    </p>

                    <div class="bank-info-row">
                        <span>Ngân hàng</span>
                        <strong>CHƯA CẤU HÌNH</strong>
                    </div>

                    <div class="bank-info-row">
                        <span>Số tài khoản</span>
                        <strong>CHƯA CẤU HÌNH</strong>
                    </div>

                    <div class="bank-info-row">
                        <span>Chủ tài khoản</span>
                        <strong>CHƯA CẤU HÌNH</strong>
                    </div>

                    <div class="bank-info-row">
                        <span>Nội dung chuyển khoản</span>
                        <strong>PEESEE + MÃ ĐƠN</strong>
                    </div>

                    <div class="bank-transfer-qr">
                        <div class="qr-placeholder">QR CODE</div>
                    </div>

                </div>

            </section>

            <!-- Tóm tắt đơn hàng -->
            <aside class="checkout-summary">

                <h2>Đơn hàng của bạn</h2>

                <asp:Panel ID="pnlEmptyCheckout" runat="server"
                    Visible="false">
                    <p>Giỏ hàng đang trống.</p>
                    <a href="<%= ResolveUrl("~/Default.aspx") %>">
                        Tiếp tục mua hàng
                    </a>
                </asp:Panel>
                <asp:Repeater ID="rptCheckoutItems" runat="server">
                    <ItemTemplate>
                        <div class="checkout-item">

                            <div>
                                <h3><%#: Eval("Name") %></h3>
                                <p>Số lượng: <%#: Eval("Quantity") %></p>
                            </div>

                            <strong>
                                <%#: Eval("Subtotal", "{0:N0}") %>đ
                            </strong>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <hr />

                <div class="checkout-total">
                    <span>Tổng thanh toán</span>

                    <strong>
                        <asp:Label ID="lblCheckoutTotal"
                            runat="server" Text="0đ" />
                    </strong>
                </div>

                <asp:Button ID="btnPlaceOrder" runat="server"
                    Text="Đặt hàng"
                    CssClass="place-order-btn"
                    CausesValidation="false"
                    OnClick="btnPlaceOrder_Click" />

            </aside>

        </div>

    </main>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const cod = document.getElementById("<%= rdoCOD.ClientID %>");
            const bank = document.getElementById("<%= rdoBank.ClientID %>");
            const bankInfo = document.getElementById("bank-transfer-info");

            function updatePaymentMethod() {
                bankInfo.classList.toggle("hidden", !bank.checked);
            }

            cod.addEventListener("change", updatePaymentMethod);
            bank.addEventListener("change", updatePaymentMethod);

            updatePaymentMethod();
        });
    </script>
    <style>
        /* =========================
           TRANG THANH TOÁN
        ========================= */

        .checkout-page {
            max-width: 1200px;
            margin: 40px auto;
            padding: 0 30px;
        }

        .checkout-page>h1 {
            margin: 0 0 30px;
            font-size: 30px;
            font-weight: 500;
        }


        /* =========================
           BỐ CỤC CHÍNH
        ========================= */

        .checkout-container {
            display: grid;
            grid-template-columns: 1fr 360px;
            gap: 30px;
            align-items: start;
        }


        /* =========================
           THÔNG TIN GIAO HÀNG
        ========================= */

        .shipping-info {
            padding: 25px;

            background: #ffffff;
            border: 1px solid #e5e5e5;
            border-radius: 8px;
        }

        .shipping-info h2 {
            margin: 0 0 20px;

            font-size: 19px;
            font-weight: 600;
            color: #222;
        }

        .shipping-info h2:not(:first-child) {
            margin-top: 30px;
        }

        /* FORM THÔNG TIN GIAO HÀNG */

        .form-group {
            width: 100%;
            margin-bottom: 18px;
            box-sizing: border-box;
        }

        .form-group label {
            display: block;
            margin-bottom: 7px;
            font-size: 13px;
            font-weight: 600;
            color: #444;
        }

        .form-group input,
        .form-group textarea {
            display: block;
            width: 100%;
            max-width: none;
            box-sizing: border-box;

            padding: 11px 12px;

            border: 1px solid #ddd;
            border-radius: 5px;
            background: #fff;

            font-family: inherit;
            font-size: 14px;
            outline: none;
        }

        .form-group input {
            height: 42px;
        }

        .form-group textarea {
            min-height: 90px;
            resize: vertical;
        }

        .form-group input:focus,
        .form-group textarea:focus {
            border-color: #1e88e5;
        }

        /* =========================
           PHƯƠNG THỨC THANH TOÁN
        ========================= */

        .payment-option {
            display: flex;
            align-items: center;
            gap: 10px;

            padding: 14px;

            margin-bottom: 10px;

            border: 1px solid #e5e5e5;
            border-radius: 5px;

            cursor: pointer;

            font-size: 14px;
        }

        .payment-option:hover {
            background: #f8f9fa;
        }

        .payment-option input {
            width: 16px;
            height: 16px;
        }


        /* =========================
           TÓM TẮT ĐƠN HÀNG
        ========================= */

        .checkout-summary {
            padding: 22px;

            background: #f8f9fa;

            border: 1px solid #e5e5e5;
            border-radius: 8px;
        }

        .checkout-summary h2 {
            margin: 0 0 20px;

            font-size: 19px;
            font-weight: 600;
            color: #222;
        }


        /* Sản phẩm */

        .checkout-item {
            display: flex;
            justify-content: space-between;
            gap: 15px;

            padding: 14px 0;

            border-bottom: 1px solid #e5e5e5;
        }

        .checkout-item h3 {
            margin: 0 0 6px;

            font-size: 14px;
            font-weight: 600;
            color: #333;
        }

        .checkout-item p {
            margin: 0;

            font-size: 12px;
            color: #777;
        }

        .checkout-item strong {
            white-space: nowrap;

            font-size: 14px;
            color: #333;
        }


        /* =========================
           TỔNG TIỀN
        ========================= */

        .checkout-summary hr {
            border: none;
            border-top: 1px solid #ddd;

            margin: 20px 0;
        }

        .checkout-total {
            display: flex;
            justify-content: space-between;
            align-items: center;

            margin-bottom: 20px;

            font-size: 14px;
        }

        .checkout-total strong {
            font-size: 20px;
            color: #e53935;
        }


        /* =========================
           NÚT ĐẶT HÀNG
        ========================= */

        .place-order-btn {
            width: 100%;
            height: 45px;

            border: none;
            border-radius: 5px;

            background: #1e88e5;
            color: white;

            font-size: 15px;
            font-weight: 600;

            cursor: pointer;
        }

        .place-order-btn:hover {
            background: #1976d2;
        }


        /* =========================
           RESPONSIVE
        ========================= */

        @media (max-width: 800px) {

            .checkout-container {
                grid-template-columns: 1fr;
            }

            .checkout-summary {
                order: -1;
            }

        }

        /* =========================
           BANK TRANSFER
        ========================= */

        .bank-transfer-info {
            margin-top: 16px;
            padding: 18px;

            background: #f8fafc;
            border: 1px solid #e5e7eb;
            border-radius: 8px;
        }

        .bank-transfer-info.hidden {
            display: none;
        }

        .bank-transfer-info h3 {
            margin: 0 0 8px;
            font-size: 16px;
            color: #222;
        }

        .bank-transfer-note {
            margin: 0 0 16px;
            font-size: 13px;
            line-height: 1.5;
            color: #666;
        }

        .bank-info-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 20px;

            padding: 10px 0;
            border-bottom: 1px solid #e5e7eb;

            font-size: 13px;
        }

        .bank-info-row:last-of-type {
            border-bottom: none;
        }

        .bank-info-row span {
            color: #666;
        }

        .bank-info-row strong {
            color: #222;
            text-align: right;
        }

        .bank-transfer-qr {
            display: flex;
            justify-content: center;

            margin-top: 18px;
        }

        .qr-placeholder {
            width: 150px;
            height: 150px;

            display: flex;
            align-items: center;
            justify-content: center;

            border: 1px dashed #cbd5e1;
            border-radius: 8px;

            background: #fff;

            color: #94a3b8;
            font-size: 13px;
        }

        @media (max-width: 600px) {
            .bank-info-row {
                flex-direction: column;
                align-items: flex-start;
                gap: 4px;
            }

            .bank-info-row strong {
                text-align: left;
            }
        }

        .checkout-profile-actions {
            margin-bottom: 24px;
            padding: 16px;
            background: #f8fafc;
            border: 1px solid #e5e7eb;
            border-radius: 8px;
        }

        .checkout-profile-note {
            margin: 0 0 14px;
            color: #666;
            font-size: 13px;
            line-height: 1.5;
        }

        .checkout-profile-btn {
            display: inline-block;
            width: auto;
            max-width: 100%;
            height: 38px;
            margin: 4px 6px 4px 0;
            padding: 0 14px;
            border: none;
            border-radius: 5px;
            background: #1e88e5;
            color: #fff;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
        }

        .checkout-profile-btn:hover {
            background: #1976d2;
        }

        .checkout-profile-btn.checkout-cancel-btn {
            background: #6b7280;
        }

        .checkout-profile-message {
            display: block;
            margin-top: 10px;
            font-size: 13px;
        }
    </style>

</asp:Content>
