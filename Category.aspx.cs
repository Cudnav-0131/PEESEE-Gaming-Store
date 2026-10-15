using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PEESEE_GAMING.Models;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;

namespace PEESEE_GAMING
{
    public partial class Category : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryProducts();
            }
        }

        private void BindCategoryProducts()
        {
            var allProducts = Application["Products"] as List<Product>;

            if (allProducts == null)
            {
                allProducts = new List<Product>();
            }

            string categoryKey = Request.QueryString["category"];

            if (categoryKey == null)
            {
                categoryKey = "";
            }

            categoryKey = categoryKey.Trim();

            string categoryTitle = GetCategoryTitle(categoryKey);

            List<Product> filteredProducts;

            if (string.IsNullOrWhiteSpace(categoryKey))
            {
                filteredProducts = allProducts;
                categoryTitle = "Tất cả sản phẩm";
            }
            else
            {
                filteredProducts = allProducts
                    .Where(p => string.Equals(
                        p.Category,
                        categoryKey,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            lblCategoryTitle.Text = categoryTitle;
            lblProductsTitle.Text = categoryTitle;
            lblCategoryCount.Text =
                "Có " + filteredProducts.Count + " sản phẩm";

            rptCategoryProducts.DataSource = filteredProducts;
            rptCategoryProducts.DataBind();
        }
        protected void rptCategoryProducts_ItemDataBound(
            object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            Product product = e.Item.DataItem as Product;

            HtmlGenericControl card =
                e.Item.FindControl("productCard") as HtmlGenericControl;

            if (product == null || card == null)
            {
                return;
            }

            var specifications = product.Specifications
                ?? new Dictionary<string, string>();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            card.Attributes["data-specs"] =
                serializer.Serialize(specifications);
        }
        private string GetCategoryTitle(string category)
        {
            switch (category.ToLowerInvariant())
            {
                case "laptop":
                    return "Laptop";

                case "laptop-gaming":
                    return "Laptop Gaming";

                case "pc-ps":
                    return "PC & PlayStation";

                case "components":
                    return "Linh kiện máy tính";

                case "case":
                    return "Case máy tính";

                case "speaker-microphone":
                    return "Loa & Microphone";

                case "monitor":
                    return "Màn hình";

                case "keyboard":
                    return "Bàn phím";

                case "mouse-mousepad":
                    return "Chuột & Bàn di chuột";

                case "headphone":
                    return "Tai nghe";

                case "chair-desk":
                    return "Ghế & Bàn Gaming";

                default:
                    return "Sản phẩm";
            }
        }

        protected void rptCategoryProducts_ItemCommand(
            object source,
            RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "AddToCart")
            {
                return;
            }

            string productId = Convert.ToString(e.CommandArgument);

            var products = Application["Products"] as List<Product>;

            if (products == null)
            {
                return;
            }

            Product product = products.FirstOrDefault(
                p => p.Id == productId);

            if (product == null || product.Stock <= 0)
            {
                return;
            }

            var cart = Session["Cart"] as Dictionary<string, int>;

            if (cart == null)
            {
                cart = new Dictionary<string, int>();
                Session["Cart"] = cart;
            }

            int quantity;
            cart.TryGetValue(productId, out quantity);

            if (quantity < product.Stock)
            {
                cart[productId] = quantity + 1;
            }
        }
    }
}
