
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using PEESEE_GAMING.Models;

namespace PEESEE_GAMING
{
    public partial class ProductDetail : Page
    {
        private CultureInfo VietnameseCulture
        {
            get { return CultureInfo.GetCultureInfo("vi-VN"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Product product = FindProduct();

                if (product == null)
                {
                    ShowProductNotFound();
                    return;
                }

                BindProduct(product);
            }
        }

        // Lấy sản phẩm từ mã id trên URL.
        private Product FindProduct()
        {
            string productId = Request.QueryString["id"];

            if (string.IsNullOrWhiteSpace(productId))
            {
                return null;
            }

            List<Product> products =
                Application["Products"] as List<Product>;

            if (products == null)
            {
                return null;
            }

            return products.FirstOrDefault(p =>
                string.Equals(
                    Convert.ToString(p.Id),
                    productId,
                    StringComparison.OrdinalIgnoreCase));
        }

        // Hiển thị thông tin sản phẩm.
        private void BindProduct(Product product)
        {
            pnlProduct.Visible = true;
            pnlProductNotFound.Visible = false;

            imgProduct.AlternateText = product.Name ?? "Sản phẩm";

            if (string.IsNullOrWhiteSpace(product.Image))
            {
                imgProduct.ImageUrl =
                    ResolveUrl("~/images/logo-peesee.png");
            }
            else if (product.Image.StartsWith("~/"))
            {
                imgProduct.ImageUrl = ResolveUrl(product.Image);
            }
            else
            {
                imgProduct.ImageUrl = ResolveUrl(
                    "~/" + product.Image.TrimStart('/'));
            }

            lblBrand.Text = product.Brand ?? "";
            lblProductName.Text = product.Name ?? "Sản phẩm";

            lblRating.Text =
                product.Rating.ToString("N1", VietnameseCulture);

            lblSold.Text =
                product.Sold.ToString("N0", VietnameseCulture);

            lblPrice.Text =
                product.Price.ToString("N0", VietnameseCulture) + "đ";

            // Chỉ hiện giá cũ và phần trăm giảm khi có giảm giá.
            if (product.OldPrice > product.Price &&
                product.OldPrice > 0)
            {
                lblOldPrice.Text =
                    product.OldPrice.ToString(
                        "N0", VietnameseCulture) + "đ";

                lblOldPrice.Visible = true;

                decimal discountPercent = product.Discount;

                if (discountPercent <= 0)
                {
                    discountPercent = Math.Round(
                        (product.OldPrice - product.Price)
                        * 100m / product.OldPrice);
                }

                lblDiscount.Text =
                    "-" + discountPercent.ToString(
                        "0", VietnameseCulture) + "%";

                lblDiscount.Visible = true;
            }
            else
            {
                lblOldPrice.Visible = false;
                lblDiscount.Visible = false;
            }

            // Hiển thị mô tả dưới dạng văn bản an toàn.
            litDescription.Text =
                string.IsNullOrWhiteSpace(product.Description)
                ? "Chưa có mô tả sản phẩm."
                : product.Description;

            // Hiển thị thông số kỹ thuật.
            if (product.Specifications != null &&
                product.Specifications.Count > 0)
            {
                rptSpecifications.DataSource =
                    product.Specifications;

                rptSpecifications.DataBind();

                lblNoSpecifications.Visible = false;
            }
            else
            {
                rptSpecifications.DataSource = null;
                rptSpecifications.DataBind();

                lblNoSpecifications.Visible = true;
            }

            // Trạng thái tồn kho.
            int stock = product.Stock;

            if (stock <= 0)
            {
                lblStock.Text = "Hết hàng";
                lblStock.CssClass =
                    "product-detail-stock out-of-stock";

                txtQuantity.Text = "0";
                txtQuantity.Enabled = false;

                btnDecreaseQuantity.Enabled = false;
                btnIncreaseQuantity.Enabled = false;
                btnAddToCart.Enabled = false;
                btnBuyNow.Enabled = false;
            }
            else if (stock <= 5)
            {
                lblStock.Text =
                    "Chỉ còn " + stock + " sản phẩm";

                lblStock.CssClass =
                    "product-detail-stock low-stock";

                txtQuantity.Text = "1";
            }
            else
            {
                lblStock.Text = "Còn hàng";

                lblStock.CssClass = "product-detail-stock";
                txtQuantity.Text = "1";
            }
        }

        private void ShowProductNotFound()
        {
            pnlProduct.Visible = false;
            pnlProductNotFound.Visible = true;
        }

        private int GetRequestedQuantity()
        {
            int quantity;

            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                return 0;
            }

            return quantity;
        }

        // Kiểm tra số lượng trước khi cập nhật giỏ hàng.
        private bool TryAddToCart(
            Product product,
            out string message)
        {
            message = "";

            int quantity = GetRequestedQuantity();

            if (quantity < 1)
            {
                message = "Vui lòng nhập số lượng hợp lệ.";
                return false;
            }

            if (product.Stock <= 0)
            {
                message = "Sản phẩm hiện đã hết hàng.";
                return false;
            }

            if (quantity > product.Stock)
            {
                message =
                    "Số lượng vượt quá tồn kho hiện tại ("
                    + product.Stock + " sản phẩm).";

                return false;
            }

            Dictionary<string, int> cart =
                Session["Cart"] as Dictionary<string, int>;

            if (cart == null)
            {
                cart = new Dictionary<string, int>();
            }

            string productId = Convert.ToString(product.Id);

            int existingQuantity = 0;
            cart.TryGetValue(productId, out existingQuantity);

            if (existingQuantity + quantity > product.Stock)
            {
                message =
                    "Giỏ hàng đã có " + existingQuantity
                    + " sản phẩm này. Tồn kho chỉ còn "
                    + product.Stock + " sản phẩm.";

                return false;
            }

            cart[productId] = existingQuantity + quantity;

            Session["Cart"] = cart;

            message = "Đã thêm sản phẩm vào giỏ hàng.";
            return true;
        }

        protected void btnDecreaseQuantity_Click(
            object sender, EventArgs e)
        {
            Product product = FindProduct();

            if (product == null)
            {
                ShowProductNotFound();
                return;
            }

            int quantity = GetRequestedQuantity();

            if (quantity < 2)
            {
                quantity = 1;
            }
            else
            {
                quantity--;
            }

            txtQuantity.Text = quantity.ToString();
            lblProductMessage.Text = "";
        }

        protected void btnIncreaseQuantity_Click(
            object sender, EventArgs e)
        {
            Product product = FindProduct();

            if (product == null)
            {
                ShowProductNotFound();
                return;
            }

            int quantity = GetRequestedQuantity();

            if (product.Stock <= 0)
            {
                txtQuantity.Text = "0";
                lblProductMessage.Text =
                    "Sản phẩm hiện đã hết hàng.";

                return;
            }

            if (quantity < 1)
            {
                quantity = 1;
            }

            if (quantity < product.Stock)
            {
                quantity++;
                txtQuantity.Text = quantity.ToString();
                lblProductMessage.Text = "";
            }
            else
            {
                txtQuantity.Text = product.Stock.ToString();

                lblProductMessage.Text =
                    "Bạn đã chọn số lượng tối đa trong kho.";
            }
        }

        protected void btnAddToCart_Click(
            object sender, EventArgs e)
        {
            Product product = FindProduct();

            if (product == null)
            {
                ShowProductNotFound();
                return;
            }

            string message;

            TryAddToCart(product, out message);

            lblProductMessage.Text = message;
        }

        protected void btnBuyNow_Click(
            object sender, EventArgs e)
        {
            Product product = FindProduct();

            if (product == null)
            {
                ShowProductNotFound();
                return;
            }

            string message;

            bool success = TryAddToCart(product, out message);

            if (!success)
            {
                lblProductMessage.Text = message;
                return;
            }

            Response.Redirect(
                ResolveUrl("~/Checkout.aspx"),
                false);

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
