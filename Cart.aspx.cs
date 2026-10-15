
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PEESEE_GAMING.Models;
using System.Linq;

namespace PEESEE_GAMING
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            var products = Application["Products"] as List<Product>;
            var cart = Session["Cart"] as Dictionary<string, int>;

            if (products == null)
            {
                products = new List<Product>();
            }

            if (cart == null)
            {
                cart = new Dictionary<string, int>();
            }

            // Kiểm tra sản phẩm và giới hạn số lượng theo tồn kho
            foreach (string id in cart.Keys.ToList())
            {
                Product product = products.FirstOrDefault(p => p.Id == id);

                if (product == null || product.Stock <= 0 || cart[id] <= 0)
                {
                    cart.Remove(id);
                }
                else if (cart[id] > product.Stock)
                {
                    cart[id] = product.Stock;
                }
            }

            Session["Cart"] = cart;

            var cartItems = products
                .Where(p => cart.ContainsKey(p.Id))
                .Select(p => new CartDisplayItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.Image,
                    Price = p.Price,
                    Quantity = cart[p.Id],
                    Subtotal = p.Price * cart[p.Id]
                })
                .ToList();

            rptCart.DataSource = cartItems;
            rptCart.DataBind();

            pnlEmptyCart.Visible = cartItems.Count == 0;

            decimal total = cartItems.Sum(item => item.Subtotal);
            string formattedTotal = total.ToString(
                "N0", CultureInfo.GetCultureInfo("vi-VN"));

            lblSubtotal.Text = formattedTotal + " đ";
            lblOrderTotal.Text = formattedTotal + " đ";
        }

        protected void rptCart_ItemCommand(
            object source, RepeaterCommandEventArgs e)
        {
            string productId = Convert.ToString(e.CommandArgument);

            var products = Application["Products"] as List<Product>;
            var cart = Session["Cart"] as Dictionary<string, int>;

            if (cart == null)
            {
                cart = new Dictionary<string, int>();
            }

            if (e.CommandName == "Remove")
            {
                // Xóa sản phẩm khỏi giỏ
                cart.Remove(productId);
            }
            else if (e.CommandName == "Increase")
            {
                Product product = products?
                    .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    cart.Remove(productId);
                }
                else
                {
                    int quantity = cart.ContainsKey(productId)
                        ? cart[productId]
                        : 0;

                    if (quantity >= product.Stock)
                    {
                        ShowMessage(
                            "Sản phẩm \"" + product.Name +
                            "\" chỉ còn " + product.Stock + " sản phẩm.");
                        return;
                    }

                    cart[productId] = quantity + 1;
                }
            }
            else if (e.CommandName == "Decrease")
            {
                // Không giảm xuống dưới 1, giống chức năng cũ
                if (cart.ContainsKey(productId) && cart[productId] > 1)
                {
                    cart[productId]--;
                }
            }

            Session["Cart"] = cart;
            BindCart();
        }

        private void ShowMessage(string message)
        {
            string safeMessage = message
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", "\\n");

            ClientScript.RegisterStartupScript(
                GetType(),
                "cartMessage",
                "alert('" + safeMessage + "');",
                true);
        }

        private class CartDisplayItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Subtotal { get; set; }
        }
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            var cart = Session["Cart"] as Dictionary<string, int>;

            if (cart == null || !cart.Any(item => item.Value > 0))
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "cartEmpty",
                    "alert('Giỏ hàng đang trống. Vui lòng thêm sản phẩm trước khi thanh toán.');",
                    true);

                BindCart();
                return;
            }

            Response.Redirect("~/Checkout.aspx");
        }
    }
}
