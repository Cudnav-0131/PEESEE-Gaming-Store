<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PEESEE_GAMING.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="login-page">
        <div class="login-card">

            <h1>Đăng nhập</h1>

            <p class="login-description">
                Đăng nhập vào tài khoản PEESEE để tiếp tục mua sắm.
            </p>

            <div class="login-field">
                <asp:Label ID="lblEmail" runat="server"
                    AssociatedControlID="txtEmail"
                    Text="Email" />

                <asp:TextBox ID="txtEmail" runat="server"
                    TextMode="Email"
                    MaxLength="150"
                    CssClass="login-input"
                    placeholder="Nhập địa chỉ email" />
            </div>

            <div class="login-field">
                <asp:Label ID="lblPassword" runat="server"
                    AssociatedControlID="txtPassword"
                    Text="Mật khẩu" />

                <asp:TextBox ID="txtPassword" runat="server"
                    TextMode="Password"
                    MaxLength="100"
                    CssClass="login-input"
                    placeholder="Nhập mật khẩu" />
            </div>

            <asp:Label ID="lblMessage" runat="server"
                CssClass="login-message" />

            <asp:Button ID="btnLogin" runat="server"
                Text="Đăng nhập"
                CssClass="login-btn"
                OnClick="btnLogin_Click"
                CausesValidation="false" />
            
            <p class="forgot-password-wrap">
                <a href="#" id="forgot-password-link">Quên mật khẩu?</a>
            </p>

            <div id="forgot-password-panel"
                class="forgot-password-panel hidden">

                <h2>Đặt lại mật khẩu</h2>

                <div class="login-field">
                    <asp:Label ID="lblResetEmail" runat="server"
                        AssociatedControlID="txtResetEmail"
                        Text="Email đã đăng ký" />

                    <asp:TextBox ID="txtResetEmail" runat="server"
                        TextMode="Email"
                        MaxLength="150"
                        CssClass="login-input"
                        placeholder="Nhập email tài khoản" />
                </div>

                <div class="login-field">
                    <asp:Label ID="lblResetPassword" runat="server"
                        AssociatedControlID="txtResetPassword"
                        Text="Mật khẩu mới" />

                    <asp:TextBox ID="txtResetPassword" runat="server"
                        TextMode="Password"
                        MaxLength="100"
                        CssClass="login-input"
                        placeholder="Nhập mật khẩu mới" />
                </div>

                <div class="login-field">
                    <asp:Label ID="lblResetConfirmPassword" runat="server"
                        AssociatedControlID="txtResetConfirmPassword"
                        Text="Xác nhận mật khẩu mới" />

                    <asp:TextBox ID="txtResetConfirmPassword" runat="server"
                        TextMode="Password"
                        MaxLength="100"
                        CssClass="login-input"
                        placeholder="Nhập lại mật khẩu mới" />
                </div>

                <asp:Button ID="btnResetPassword" runat="server"
                    Text="Đặt lại mật khẩu"
                    CssClass="login-btn reset-password-btn"
                    OnClick="btnResetPassword_Click"
                    CausesValidation="false" />
            </div>

            <p class="login-switch">
                Chưa có tài khoản?
                <a href="<%= ResolveUrl("~/Register.aspx") %>">
                    Đăng ký ngay
                </a>
            </p>

        </div>
    </main>

    <style>
    .login-page {
        min-height: 70vh;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 40px 20px;
        box-sizing: border-box;
    }

    .login-card {
        width: 100%;
        max-width: 440px;
        padding: 32px 28px;
        box-sizing: border-box;
        background: #fff;
        border: 1px solid #eee;
        border-radius: 16px;
        box-shadow: 0 8px 25px rgba(0, 0, 0, .06);
    }

    .login-card h1 {
        margin: 0 0 10px;
        text-align: center;
        font-size: 30px;
        color: #111827;
    }

    .login-description {
        margin: 0 0 26px;
        text-align: center;
        color: #777;
        font-size: 13px;
        line-height: 1.5;
    }

    .login-field {
        display: flex;
        flex-direction: column;
        gap: 8px;
        margin-bottom: 18px;
    }

    .login-field label {
        font-size: 14px;
        font-weight: 600;
        color: #374151;
    }

    .login-field input.login-input {
        display: block;
        width: 100%;
        max-width: none;
        height: 44px;
        padding: 10px 12px;
        box-sizing: border-box;
        border: 1px solid #d1d5db;
        border-radius: 8px;
        background: #fff;
        font-size: 14px;
        outline: none;
    }

    .login-field input.login-input:focus {
        border-color: #1e88e5;
        box-shadow: 0 0 0 3px rgba(30, 136, 229, .08);
    }

    .login-message {
        display: block;
        margin: 8px 0;
        font-size: 13px;
        color: #d32f2f;
    }

    /* Chỉ áp dụng cho nút bên trong form đăng nhập */
    .login-card .login-btn {
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

    .login-card .login-btn:hover {
        background: #1976d2;
    }

    .login-switch {
        margin: 22px 0 0;
        text-align: center;
        font-size: 14px;
        color: #4b5563;
    }

    .login-switch a {
        color: #1e88e5;
        font-weight: 600;
        text-decoration: none;
    }

    .login-switch a:hover {
        text-decoration: underline;
    }

    @media (max-width: 500px) {
        .login-card {
            padding: 24px 18px;
        }
    }
    
    .forgot-password-wrap {
        margin: 14px 0;
        text-align: right;
    }

    .forgot-password-wrap a {
        color: #1e88e5;
        font-size: 14px;
        font-weight: 600;
        text-decoration: none;
    }

    .forgot-password-wrap a:hover {
        text-decoration: underline;
    }

    .forgot-password-panel {
        margin-top: 18px;
        padding: 18px;
        border: 1px solid #e5e7eb;
        border-radius: 12px;
        background: #f9fafb;
    }

    .forgot-password-panel h2 {
        margin: 0 0 18px;
        font-size: 18px;
        color: #111827;
    }

    .forgot-password-panel.hidden {
        display: none;
    }

    .reset-password-btn {
        background: #10b981;
    }

    .reset-password-btn:hover {
        background: #059669;
    }
    </style>
    <script>
    document.addEventListener("DOMContentLoaded", function () {
        const link = document.getElementById("forgot-password-link");
        const panel = document.getElementById("forgot-password-panel");

        if (link && panel) {
            link.addEventListener("click", function (event) {
                event.preventDefault();
                panel.classList.toggle("hidden");
            });
        }
    });
    </script>

</asp:Content>
