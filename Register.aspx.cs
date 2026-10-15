
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web.UI;
using PEESEE_GAMING.Models;

namespace PEESEE_GAMING
{
    public partial class Register : Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                ShowMessage("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra email
            try
            {
                var mail = new MailAddress(email);

                if (mail.Address != email)
                {
                    ShowMessage("Địa chỉ email không hợp lệ.");
                    return;
                }
            }
            catch
            {
                ShowMessage("Địa chỉ email không hợp lệ.");
                return;
            }

            // Kiểm tra mật khẩu
            if (password.Length < 8)
            {
                ShowMessage("Mật khẩu phải có ít nhất 8 ký tự.");
                return;
            }

            if (password != confirmPassword)
            {
                ShowMessage("Mật khẩu xác nhận không khớp.");
                return;
            }

            // Lấy danh sách tài khoản
            var users = Application["Users"] as List<StoreUser>;

            if (users == null)
            {
                users = new List<StoreUser>();
                Application["Users"] = users;
            }

            // Kiểm tra tên đăng nhập hoặc email đã tồn tại
            if (users.Any(u =>
                string.Equals(u.Email, email,
                    StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("Email này đã được sử dụng.");
                return;
            }

            // Tạo tài khoản mới
            var newUser = new StoreUser
            {
                Id = "USER-" + Guid.NewGuid().ToString("N"),
                FullName = fullName,
                Email = email,
                Phone = phone,
                Address = address,
                PasswordHash = HashPassword(password),
                Role = "user"
            };

            users.Add(newUser);
            Application["Users"] = users;

            lblMessage.ForeColor =
                System.Drawing.Color.Green;
            lblMessage.Text =
                "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
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

        private void ShowMessage(string message)
        {
            lblMessage.ForeColor =
                System.Drawing.Color.Red;
            lblMessage.Text = message;
        }
    }
}
