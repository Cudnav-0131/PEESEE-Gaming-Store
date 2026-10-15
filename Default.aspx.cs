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
            if (e.CommandName == "AddToCart")
            {
                string productId = Convert.ToString(e.CommandArgument);

                var cart = Session["Cart"] as Dictionary<string, int>;

                if (cart == null)
                {
                    cart = new Dictionary<string, int>();
                }

                if (cart.ContainsKey(productId))
                {
                    cart[productId]++;
                }
                else
                {
                    cart[productId] = 1;
                }

                Session["Cart"] = cart;
            }
        }
    }
}