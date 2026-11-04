using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PEESEE_GAMING.Models;
using System.Linq;

namespace PEESEE_GAMING
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var products = Application["Products"] as List<Product>;

                if (products == null)
                {
                    rptProducts.DataSource = new List<Product>();
                    rptProducts.DataBind();
                    return;
                }

                // Lọc theo danh mục
                string category = Request.QueryString["category"];

                if (!string.IsNullOrWhiteSpace(category))
                {
                    products = products
                        .Where(p => string.Equals(
                            p.Category,
                            category,
                            StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                // Tìm kiếm sản phẩm
                string keyword = Request.QueryString["search"];

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    products = products.Where(p =>
                        (!string.IsNullOrEmpty(p.Name) &&
                         p.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        ||
                        (!string.IsNullOrEmpty(p.Brand) &&
                         p.Brand.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        ||
                        (!string.IsNullOrEmpty(p.Category) &&
                         p.Category.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    ).ToList();
                }

                rptProducts.DataSource = products;
                rptProducts.DataBind();
            }
        }

        protected void rptProducts_ItemCommand(
            object source,
            RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "AddToCart")
                return;

            string productId = Convert.ToString(e.CommandArgument);

            if (string.IsNullOrWhiteSpace(productId))
                return;

            string errorMessage = null;

            Application.Lock();

            try
            {
                var products =
                    Application["Products"] as List<Product>;

                if (products == null)
                {
                    errorMessage = "Không tìm thấy dữ liệu sản phẩm.";
                }
                else
                {
                    Product product = products.FirstOrDefault(p =>
                        string.Equals(
                            p.Id,
                            productId,
                            StringComparison.OrdinalIgnoreCase));

                    if (product == null)
                    {
                        errorMessage = "Sản phẩm không tồn tại.";
                    }
                    else
                    {
                        var cart =
                            Session["Cart"] as Dictionary<string, int>
                            ?? new Dictionary<string, int>();

                        // Tìm sản phẩm đã có trong giỏ hàng
                        string cartKey = cart.Keys.FirstOrDefault(k =>
                            string.Equals(
                                k,
                                productId,
                                StringComparison.OrdinalIgnoreCase));

                        int currentQuantity = cartKey == null
                            ? 0
                            : cart[cartKey];

                        // Kiểm tra tồn kho trước khi thêm
                        if (product.Stock <= 0)
                        {
                            errorMessage = "Sản phẩm đã hết hàng.";
                        }
                        else if (currentQuantity >= product.Stock)
                        {
                            errorMessage =
                                "Sản phẩm \"" + product.Name +
                                "\" chỉ còn " + product.Stock +
                                " sản phẩm trong kho. Bạn không thể thêm thêm.";
                        }
                        else
                        {
                            if (cartKey == null)
                            {
                                cart[productId] = 1;
                            }
                            else
                            {
                                cart[cartKey]++;
                            }

                            Session["Cart"] = cart;
                        }
                    }
                }
            }
            finally
            {
                Application.UnLock();
            }

            // Thông báo nếu không thể thêm sản phẩm
            if (errorMessage != null)
            {
                string script =
                    "alert('" +
                    System.Web.HttpUtility.JavaScriptStringEncode(
                        errorMessage) +
                    "');";

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "AddToCartError",
                    script,
                    true);
            }
        } 
    }
}