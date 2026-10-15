using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using PEESEE_GAMING.Models;

namespace PEESEE_GAMING
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomerInfo();
                BindCheckout();
            }
        }

        // Lấy thông tin từ tài khoản đang đăng nhập
        private void LoadCustomerInfo()
        {
            StoreUser user = Session["CurrentUser"] as StoreUser;

            if (user == null)
            {
                pnlAccountInfoActions.Visible = false;

                txtFullName.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtAddress.ReadOnly = false;
                return;
            }

            pnlAccountInfoActions.Visible = true;

            txtFullName.Text = user.FullName ?? "";
            txtPhone.Text = user.Phone ?? "";
            txtAddress.Text = user.Address ?? "";

            SetCustomerInfoMode(false);
            lblProfileMessage.Text = "";
        }

        // Chuyển giữa chế độ xem và chế độ chỉnh sửa
        private void SetCustomerInfoMode(bool editing)
        {
            StoreUser user = Session["CurrentUser"] as StoreUser;

            if (user == null)
            {
                pnlAccountInfoActions.Visible = false;
                txtFullName.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtAddress.ReadOnly = false;
                return;
            }

            pnlAccountInfoActions.Visible = true;

            txtFullName.ReadOnly = !editing;
            txtPhone.ReadOnly = !editing;
            txtAddress.ReadOnly = !editing;

            btnEditCustomerInfo.Visible = !editing;
            btnSaveCustomerInfo.Visible = editing;
            btnCancelCustomerInfo.Visible = editing;
        }

        // Bấm nút Sửa thông tin thanh toán
        protected void btnEditCustomerInfo_Click(
            object sender, EventArgs e)
        {
            if (Session["CurrentUser"] == null)
            {
                return;
            }

            SetCustomerInfoMode(true);

            lblProfileMessage.ForeColor =
                System.Drawing.Color.Black;
            lblProfileMessage.Text =
                "Bạn có thể chỉnh sửa thông tin giao hàng.";

            BindCheckout();
        }

        // Bấm nút Lưu thông tin
        protected void btnSaveCustomerInfo_Click(
            object sender, EventArgs e)
        {
            StoreUser currentUser =
                Session["CurrentUser"] as StoreUser;

            if (currentUser == null)
            {
                lblProfileMessage.Text =
                    "Vui lòng đăng nhập để lưu thông tin tài khoản.";
                return;
            }

            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address))
            {
                lblProfileMessage.ForeColor =
                    System.Drawing.Color.Red;
                lblProfileMessage.Text =
                    "Vui lòng nhập đầy đủ họ tên, số điện thoại và địa chỉ.";
                SetCustomerInfoMode(true);
                return;
            }

            bool updated = false;

            // Cập nhật tài khoản trong Application
            Application.Lock();

            try
            {
                var users =
                    Application["Users"] as List<StoreUser>;

                if (users != null)
                {
                    StoreUser storedUser = users.FirstOrDefault(u =>
                        u.Id == currentUser.Id);

                    if (storedUser != null)
                    {
                        storedUser.FullName = fullName;
                        storedUser.Phone = phone;
                        storedUser.Address = address;

                        Session["CurrentUser"] = storedUser;

                        updated = true;
                    }
                }
            }
            finally
            {
                Application.UnLock();
            }

            if (!updated)
            {
                lblProfileMessage.ForeColor =
                    System.Drawing.Color.Red;
                lblProfileMessage.Text =
                    "Không tìm thấy tài khoản. Vui lòng đăng nhập lại.";
                return;
            }

            // Khóa trường thông tin sau khi lưu
            LoadCustomerInfo();

            lblProfileMessage.ForeColor =
                System.Drawing.Color.Green;
            lblProfileMessage.Text =
                "Đã lưu thông tin thanh toán vào tài khoản.";

            BindCheckout();
        }

        // Bấm nút Hủy
        protected void btnCancelCustomerInfo_Click(
            object sender, EventArgs e)
        {
            LoadCustomerInfo();

            lblProfileMessage.ForeColor =
                System.Drawing.Color.Black;
            lblProfileMessage.Text =
                "Đã hủy các thay đổi chưa lưu.";

            BindCheckout();
        }

        // Hiển thị sản phẩm trong giỏ hàng
        private void BindCheckout()
        {
            var products =
                Application["Products"] as List<Product>
                ?? new List<Product>();

            var cart =
                Session["Cart"] as Dictionary<string, int>
                ?? new Dictionary<string, int>();

            var items = products
                .Where(p =>
                    cart.ContainsKey(p.Id) &&
                    cart[p.Id] > 0)
                .Select(p => new CheckoutItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = cart[p.Id],
                    Subtotal = p.Price * cart[p.Id]
                })
                .ToList();

            rptCheckoutItems.DataSource = items;
            rptCheckoutItems.DataBind();

            pnlEmptyCheckout.Visible = items.Count == 0;

            decimal total = items.Sum(item => item.Subtotal);

            lblCheckoutTotal.Text =
                total.ToString(
                    "N0",
                    CultureInfo.GetCultureInfo("vi-VN"))
                + " đ";

            btnPlaceOrder.Enabled = items.Count > 0;
        }

        private class CheckoutItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Subtotal { get; set; }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            // Kiểm tra thông tin người nhận
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address))
            {
                ShowOrderMessage(
                    "Vui lòng nhập đầy đủ họ tên, số điện thoại và địa chỉ giao hàng.");
                return;
            }

            var cart = Session["Cart"] as Dictionary<string, int>;

            if (cart == null || cart.Count == 0 ||
                !cart.Any(item => item.Value > 0))
            {
                ShowOrderMessage("Giỏ hàng đang trống.");
                BindCheckout();
                return;
            }

            string paymentMethod =
                rdoBank.Checked ? "BankTransfer" : "COD";

            string errorMessage = null;
            StoreOrder newOrder = null;

            // Khóa để kiểm tra tồn kho và cập nhật đơn hàng
            // trong cùng một thao tác
            Application.Lock();

            try
            {
                var products =
                    Application["Products"] as List<Product>;

                var orders =
                    Application["Orders"] as List<StoreOrder>;

                if (products == null || orders == null)
                {
                    errorMessage =
                        "Dữ liệu sản phẩm hoặc đơn hàng chưa được khởi tạo.";
                }
                else
                {
                    newOrder = new StoreOrder
                    {
                        Id = "ORD-" +
                             DateTime.Now.ToString("yyyyMMddHHmmss") +
                             "-" +
                             Guid.NewGuid().ToString("N")
                                 .Substring(0, 6).ToUpperInvariant(),

                        CustomerName = fullName,
                        Phone = phone,
                        Address = address,
                        PaymentMethod = paymentMethod,
                        Status = "pending",
                        CreatedAt = DateTime.Now
                    };

                    // Kiểm tra tất cả sản phẩm trước khi trừ kho
                    foreach (var entry in cart)
                    {
                        if (entry.Value <= 0)
                        {
                            errorMessage =
                                "Số lượng sản phẩm trong giỏ hàng không hợp lệ.";
                            break;
                        }

                        Product product = products.FirstOrDefault(
                            p => p.Id == entry.Key);

                        if (product == null)
                        {
                            errorMessage =
                                "Không tìm thấy một sản phẩm trong giỏ hàng.";
                            break;
                        }

                        if (entry.Value > product.Stock)
                        {
                            errorMessage =
                                "Sản phẩm \"" + product.Name +
                                "\" chỉ còn " + product.Stock +
                                " sản phẩm trong kho.";
                            break;
                        }

                        newOrder.Items.Add(new StoreOrderItem
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            Price = product.Price,
                            Quantity = entry.Value,
                            Subtotal = product.Price * entry.Value
                        });

                        newOrder.Total += product.Price * entry.Value;
                    }

                    // Chỉ tạo đơn và trừ kho khi mọi sản phẩm hợp lệ
                    if (errorMessage == null)
                    {
                        foreach (var item in newOrder.Items)
                        {
                            Product product = products.First(
                                p => p.Id == item.ProductId);

                            product.Stock -= item.Quantity;
                        }

                        orders.Add(newOrder);

                        Application["Products"] = products;
                        Application["Orders"] = orders;
                    }
                }
            }
            finally
            {
                Application.UnLock();
            }

            if (errorMessage != null)
            {
                ShowOrderMessage(errorMessage);
                BindCheckout();
                return;
            }

            // Xóa giỏ hàng sau khi đặt thành công
            Session["Cart"] = new Dictionary<string, int>();

            // Cập nhật giao diện rồi thông báo kết quả
            BindCheckout();

            string successMessage =
                "Đặt hàng thành công!" +
                "\\nMã đơn hàng: " + newOrder.Id +
                "\\nTổng tiền: " +
                newOrder.Total.ToString("N0") + " đ";

            string script =
                "alert('" +
                System.Web.HttpUtility.JavaScriptStringEncode(
                    successMessage) +
                "'); window.location.href='" +
                ResolveUrl("~/Default.aspx") + "';";

            ClientScript.RegisterStartupScript(
                GetType(), "OrderSuccess", script, true);
        }

        private void ShowOrderMessage(string message)
        {
            string script =
                "alert('" +
                System.Web.HttpUtility.JavaScriptStringEncode(message) +
                "');";

            ClientScript.RegisterStartupScript(
                GetType(),
                "OrderMessage",
                script,
                true);
        }
    }
}
