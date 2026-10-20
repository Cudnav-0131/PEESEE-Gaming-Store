using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.UI;
using PEESEE_GAMING.Models;
using PEESEE_GAMING.Models;

namespace PEESEE_GAMING
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Vui lòng nhập email và mật khẩu.");
                return;
            }

            var users = Application["Users"] as List<StoreUser>;

            if (users == null)
            {
                ShowMessage("Chưa có tài khoản nào. Vui lòng đăng ký trước.");
                return;
            }

            StoreUser user = users.FirstOrDefault(u =>
                string.Equals(
                    u.Email,
                    email,
                    StringComparison.OrdinalIgnoreCase));

            if (user == null ||
                !VerifyPassword(password, user.PasswordHash))
            {
                ShowMessage("Email hoặc mật khẩu không chính xác.");
                return;
            }


            Session["CurrentUser"] = user;

            // Phân biệt tài khoản Admin và tài khoản thông thường.
            if (string.Equals(
                user.Role,
                "admin",
                StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("~/Admin/Admin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Người dùng thông thường chuyển về trang chủ.
            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;

        }

        private bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            try
            {
                string[] parts = storedHash.Split('.');

                if (parts.Length != 2)
                {
                    return false;
                }

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expectedHash = Convert.FromBase64String(parts[1]);

                using (var deriveBytes =
                    new Rfc2898DeriveBytes(password, salt, 100000))
                {
                    byte[] actualHash = deriveBytes.GetBytes(32);

                    if (actualHash.Length != expectedHash.Length)
                    {
                        return false;
                    }

                    int difference = 0;

                    for (int i = 0; i < actualHash.Length; i++)
                    {
                        difference |= actualHash[i] ^ expectedHash[i];
                    }

                    return difference == 0;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private void ShowMessage(string message)
        {
            lblMessage.ForeColor =
                System.Drawing.Color.Red;

            lblMessage.Text = message;
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtResetEmail.Text.Trim();
            string newPassword = txtResetPassword.Text;
            string confirmPassword = txtResetConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                ShowMessage("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (newPassword.Length < 8)
            {
                ShowMessage("Mật khẩu mới phải có ít nhất 8 ký tự.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowMessage("Mật khẩu xác nhận không khớp.");
                return;
            }

            var users = Application["Users"] as List<StoreUser>;

            if (users == null)
            {
                ShowMessage("Danh sách tài khoản chưa được khởi tạo.");
                return;
            }

            StoreUser user = users.FirstOrDefault(u =>
                string.Equals(
                    u.Email,
                    email,
                    StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                ShowMessage("Không tìm thấy tài khoản với email đã nhập.");
                return;
            }

            user.PasswordHash = HashPassword(newPassword);

            Application["Users"] = users;

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text =
                "Đặt lại mật khẩu thành công. Vui lòng đăng nhập lại.";

            txtResetEmail.Text = "";
            txtResetPassword.Text = "";
            txtResetConfirmPassword.Text = "";
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var deriveBytes =
                new Rfc2898DeriveBytes(password, salt, 100000))
            {
                byte[] hash = deriveBytes.GetBytes(32);

                return Convert.ToBase64String(salt) + "." +
                       Convert.ToBase64String(hash);
            }
        }

    }
}
