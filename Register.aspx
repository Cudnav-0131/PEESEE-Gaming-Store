<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PEESEE_GAMING.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="auth-page">
        <div class="auth-card">

            <h1>Đăng ký tài khoản</h1>

            <p class="auth-description">
                Tạo tài khoản PEESEE để mua sắm dễ dàng hơn.
            </p>
            <div class="auth-fields-grid">
                <div class="form-group">
                    <asp:Label ID="lblFullName" runat="server"
                        AssociatedControlID="txtFullName"
                        Text="Họ và tên" />

                    <asp:TextBox ID="txtFullName" runat="server"
                        CssClass="auth-input"
                        MaxLength="100"
                        placeholder="Nhập họ và tên" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server"
                        AssociatedControlID="txtEmail"
                        Text="Email" />

                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="auth-input"
                        TextMode="Email"
                        MaxLength="150"
                        placeholder="Nhập địa chỉ email" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblPhone" runat="server"
                        AssociatedControlID="txtPhone"
                        Text="Số điện thoại" />

                    <asp:TextBox ID="txtPhone" runat="server"
                        CssClass="auth-input"
                        TextMode="Phone"
                        MaxLength="15"
                        placeholder="Nhập số điện thoại" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblAddress" runat="server"
                        AssociatedControlID="txtAddress"
                        Text="Địa chỉ giao hàng" />

                    <asp:TextBox ID="txtAddress" runat="server"
                        CssClass="auth-input"
                        MaxLength="250"
                        placeholder="Nhập địa chỉ giao hàng" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server"
                        AssociatedControlID="txtPassword"
                        Text="Mật khẩu" />

                    <asp:TextBox ID="txtPassword" runat="server"
                        CssClass="auth-input"
                        TextMode="Password"
                        MaxLength="100"
                        placeholder="Nhập mật khẩu" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblConfirmPassword" runat="server"
                        AssociatedControlID="txtConfirmPassword"
                        Text="Xác nhận mật khẩu" />

                    <asp:TextBox ID="txtConfirmPassword" runat="server"
                        CssClass="auth-input"
                        TextMode="Password"
                        MaxLength="100"
                        placeholder="Nhập lại mật khẩu" />
                </div>
            </div>

            <asp:Label ID="lblMessage" runat="server"
                CssClass="auth-message" />

            <asp:Button ID="btnRegister" runat="server"
                Text="Đăng ký"
                CssClass="auth-btn"
                OnClick="btnRegister_Click"
                CausesValidation="false" />

            <p class="auth-switch">
                Đã có tài khoản?
                <a href="<%= ResolveUrl("~/Login.aspx") %>">
                    Đăng nhập
                </a>
            </p>

        </div>
    </main>

    <style>
    .auth-page {
        min-height: 70vh;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 40px 20px;
        box-sizing: border-box;
    }

    .auth-card {
        width: 100%;
        max-width: 760px;
        padding: 32px;
        box-sizing: border-box;
        background: #fff;
        border: 1px solid #eee;
        border-radius: 16px;
        box-shadow: 0 8px 25px rgba(0, 0, 0, .06);
    }
    .auth-fields-grid {
        display: grid;
        grid-template-columns: repeat(2, minmax(0, 1fr));
        column-gap: 24px;
        row-gap: 4px;
    }

    .auth-card .form-group {
        min-width: 0;
        display: flex;
        flex-direction: column;
        gap: 8px;
        margin-bottom: 16px;
    }

    .auth-card .form-group input.auth-input {
        width: 100%;
        max-width: none;
        box-sizing: border-box;
    }

    .auth-message {
        display: block;
        margin-top: 12px;
    }

    @media (max-width: 600px) {
        .auth-fields-grid {
            grid-template-columns: 1fr;
        }

        .auth-card {
            padding: 24px 18px;
        }
    }
    .auth-card h1 {
        margin: 0 0 10px;
        text-align: center;
        font-size: 28px;
        color: #111827;
    }

    .auth-description {
        margin: 0 0 24px;
        text-align: center;
        color: #777;
        font-size: 13px;
    }

    .auth-card .form-group {
        display: flex;
        flex-direction: column;
        gap: 8px;
        margin-bottom: 16px;
    }

    .auth-card .form-group label {
        font-size: 14px;
        font-weight: 600;
        color: #374151;
    }

    .auth-card .form-group input.auth-input {
        display: block;
        width: 100%;
        max-width: none;
        height: 42px;
        padding: 10px 12px;
        box-sizing: border-box;
        border: 1px solid #d1d5db;
        border-radius: 8px;
        background: #fff;
        font-size: 14px;
        outline: none;
    }

    .auth-card .form-group input.auth-input:focus {
        border-color: #1e88e5;
        box-shadow: 0 0 0 3px rgba(30, 136, 229, .08);
    }

    .auth-btn {
        display: block;
        width: 100%;
        max-width: none;
        height: 44px;
        margin-top: 12px;
        padding: 0 16px;
        border: none;
        border-radius: 8px;
        background: #1e88e5;
        color: #fff;
        font-size: 15px;
        font-weight: 700;
        cursor: pointer;
    }

    .auth-btn:hover {
        background: #1976d2;
    }

    .auth-message {
        display: block;
        margin-top: 12px;
        color: #d32f2f;
        font-size: 13px;
    }

    .auth-switch {
        margin: 20px 0 0;
        text-align: center;
        font-size: 14px;
        color: #4b5563;
    }

    .auth-switch a {
        color: #1e88e5;
        font-weight: 600;
        text-decoration: none;
    }

    .auth-switch a:hover {
        text-decoration: underline;
    }

    @media (max-width: 500px) {
        .auth-card {
            padding: 24px 18px;
        }
    }
    </style>

</asp:Content>
