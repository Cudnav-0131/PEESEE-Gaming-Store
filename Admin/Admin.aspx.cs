using PEESEE_GAMING.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Serialization;

namespace PEESEE_GAMING.Admin
{
    public partial class Admin : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra tài khoản đã đăng nhập.
            StoreUser currentUser =
                Session["CurrentUser"] as StoreUser;

            if (currentUser == null)
            {
                Response.Redirect(
                    ResolveUrl("~/Login.aspx"),
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Chỉ cho phép tài khoản có vai trò admin.
            if (!string.Equals(
                currentUser.Role,
                "admin",
                StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect(
                    ResolveUrl("~/Default.aspx"),
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            BindRevenueOrdersJson();

            // Tài khoản hợp lệ mới được tải dữ liệu Dashboard.
            if (!IsPostBack)
            {
                BindDashboard();
                BindAdminProducts();
                BindAdminOrders();
                BindRecentOrders();
                BindAdminCategories();
                BindAddProductCategories();
                BindAdminCustomers();
            }
        }


        private void BindDashboard()
        {
            // Lấy dữ liệu sản phẩm trong ứng dụng.
            List<Product> products =
                Application["Products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }

            // Lấy dữ liệu đơn hàng trong ứng dụng.
            List<StoreOrder> orders =
                Application["Orders"] as List<StoreOrder>;

            if (orders == null)
            {
                orders = new List<StoreOrder>();
            }

            // Chỉ tính những đơn đã hoàn thành.
            List<StoreOrder> completedOrders = orders
                .Where(o => IsCompletedStatus(o.Status))
                .ToList();

            // Tổng sản phẩm.
            lblTotalProducts.Text =
                products.Count.ToString("N0");

            // Tổng đơn hàng.
            lblTotalOrders.Text =
                orders.Count.ToString("N0");

            // Số đơn hoàn thành.
            lblCompletedOrders.Text =
                completedOrders.Count.ToString("N0");

            // Doanh thu từ các đơn hoàn thành.
            decimal totalRevenue = completedOrders.Sum(
                o => o.Total
            );

            lblTotalRevenue.Text =
                totalRevenue.ToString(
                    "N0",
                    CultureInfo.GetCultureInfo("vi-VN")
                ) + "đ";
        }

        private bool IsCompletedStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return false;
            }

            string normalizedStatus = status.Trim();

            return string.Equals(
                       normalizedStatus,
                       "completed",
                       StringComparison.OrdinalIgnoreCase)
                   || string.Equals(
                       normalizedStatus,
                       "Hoàn thành",
                       StringComparison.OrdinalIgnoreCase)
                   || string.Equals(
                       normalizedStatus,
                       "Đã hoàn thành",
                       StringComparison.OrdinalIgnoreCase);
        }


        private void BindAdminProducts()
        {
            List<Product> products =
                Application["Products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }

            // Mặc định chỉ hiển thị 8 sản phẩm.
            List<Product> visibleProducts = ShowAllProducts
                ? products
                : products.Take(8).ToList();

            rptAdminProducts.DataSource = visibleProducts;
            rptAdminProducts.DataBind();

            // Cập nhật số lượng đang hiển thị.
            lblProductListCount.Text = string.Format(
                "Hiển thị {0} / {1} sản phẩm",
                visibleProducts.Count,
                products.Count
            );

            // Chỉ hiện nút khi có hơn 8 sản phẩm.
            btnToggleProducts.Visible = products.Count > 8;

            btnToggleProducts.Text = ShowAllProducts
                ? "Thu gọn"
                : "Xem thêm";
        }

        protected void rptAdminProducts_ItemCommand(
            object source,
            RepeaterCommandEventArgs e)
        {
            string productId = Convert.ToString(e.CommandArgument);

            List<Product> products =
                Application["Products"] as List<Product>;

            if (products == null)
            {
                return;
            }

            Product product = products.FirstOrDefault(p =>
                string.Equals(
                    Convert.ToString(p.Id),
                    productId,
                    StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "ProductNotFound",
                    "alert('Không tìm thấy sản phẩm.');",
                    true);
                return;
            }

            // MỞ POPUP SỬA SẢN PHẨM
            if (e.CommandName == "EditProduct")
            {
                txtEditProductId.Text = Convert.ToString(product.Id);
                txtEditProductName.Text = product.Name ?? "";
                txtEditProductBrand.Text = product.Brand ?? "";

                ddlEditProductCategory.SelectedValue = product.Category;

                txtEditProductPrice.Text =
                    product.Price.ToString(
                        System.Globalization.CultureInfo.InvariantCulture);

                txtEditProductOldPrice.Text =
                    product.OldPrice.ToString(
                        System.Globalization.CultureInfo.InvariantCulture);

                txtEditProductStock.Text =
                    product.Stock.ToString();

                txtEditProductDescription.Text =
                    product.Description ?? "";

                hfEditProductImage.Value = product.Image ?? "";

                if (!string.IsNullOrWhiteSpace(product.Image))
                {
                    string imagePath = product.Image;

                    if (imagePath.StartsWith("~/"))
                    {
                        imgEditProductPreview.ImageUrl =
                            ResolveUrl(imagePath);
                    }
                    else
                    {
                        imgEditProductPreview.ImageUrl =
                            ResolveUrl("~/" + imagePath.TrimStart('/'));
                    }

                    imgEditProductPreview.CssClass =
                        "product-image-preview";
                }
                else
                {
                    imgEditProductPreview.CssClass =
                        "product-image-preview hidden";
                }

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "OpenEditProductModal",
                    "document.getElementById('edit-product-modal').classList.remove('hidden');",
                    true);

                return;
            }

            // XÓA SẢN PHẨM
            if (e.CommandName == "DeleteProduct")
            {
                Application.Lock();

                try
                {
                    products.Remove(product);
                    Application["Products"] = products;
                }
                finally
                {
                    Application.UnLock();
                }

                BindAdminProducts();
                BindDashboard();

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "DeleteProductSuccess",
                    "alert('Đã xóa sản phẩm thành công.');",
                    true);
            }
        }

        protected void btnSaveEditProduct_Click(object sender, EventArgs e)
        {
            string productId = txtEditProductId.Text.Trim();
            string name = txtEditProductName.Text.Trim();
            string brand = txtEditProductBrand.Text.Trim();
            string category = ddlEditProductCategory.SelectedValue;
            string description = txtEditProductDescription.Text.Trim();

            decimal price;
            decimal oldPrice;
            int stock;

            if (string.IsNullOrWhiteSpace(productId) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(brand))
            {
                ShowAdminAlert("Vui lòng nhập đầy đủ mã, tên và thương hiệu.");
                return;
            }

            if (!decimal.TryParse(
                    txtEditProductPrice.Text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out price) || price < 0)
            {
                ShowAdminAlert("Giá sản phẩm không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(
                    txtEditProductOldPrice.Text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out oldPrice) || oldPrice < 0)
            {
                ShowAdminAlert("Giá cũ không hợp lệ.");
                return;
            }

            if (!int.TryParse(txtEditProductStock.Text, out stock) ||
                stock < 0)
            {
                ShowAdminAlert("Tồn kho phải là số nguyên không âm.");
                return;
            }

            if (oldPrice < price)
            {
                ShowAdminAlert("Giá cũ không được nhỏ hơn giá bán.");
                return;
            }

            string imagePath = hfEditProductImage.Value;

            // Nếu chọn ảnh mới, kiểm tra trước khi lưu.
            if (fuEditProductImage.HasFile)
            {
                string extension = Path.GetExtension(
                    fuEditProductImage.FileName).ToLowerInvariant();

                string[] allowedExtensions =
                {
            ".jpg", ".jpeg", ".png", ".webp"
        };

                if (!allowedExtensions.Contains(extension))
                {
                    ShowAdminAlert("Chỉ chấp nhận ảnh JPG, JPEG, PNG hoặc WEBP.");
                    return;
                }

                if (fuEditProductImage.PostedFile.ContentLength > 5 * 1024 * 1024)
                {
                    ShowAdminAlert("Ảnh không được vượt quá 5 MB.");
                    return;
                }

                string folderPath = Server.MapPath("~/images/products/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName =
                    "admin-" + Guid.NewGuid().ToString("N") + extension;

                string fullPath = Path.Combine(folderPath, fileName);

                fuEditProductImage.SaveAs(fullPath);

                imagePath = "images/products/" + fileName;
            }

            Application.Lock();

            try
            {
                List<Product> products =
                    Application["Products"] as List<Product>;

                if (products == null)
                {
                    ShowAdminAlert("Không thể lấy danh sách sản phẩm.");
                    return;
                }

                Product product = products.FirstOrDefault(p =>
                    string.Equals(
                        Convert.ToString(p.Id),
                        productId,
                        StringComparison.OrdinalIgnoreCase));

                if (product == null)
                {
                    ShowAdminAlert("Không tìm thấy sản phẩm cần sửa.");
                    return;
                }

                product.Name = name;
                product.Brand = brand;
                product.Category = category;
                product.Price = price;
                product.OldPrice = oldPrice;
                product.Stock = stock;
                product.Description = description;
                product.Image = imagePath;

                // Cập nhật phần trăm giảm giá.
                product.Discount = oldPrice > 0 && oldPrice > price
                    ? (int)Math.Round((oldPrice - price) * 100m / oldPrice)
                    : 0;

                Application["Products"] = products;
            }
            finally
            {
                Application.UnLock();
            }

            BindAdminProducts();
            BindDashboard();

            ShowAdminAlert("Đã cập nhật sản phẩm thành công.");
        }

        private void ShowAdminAlert(string message)
        {
            string safeMessage =
                new JavaScriptSerializer().Serialize(message);

            ClientScript.RegisterStartupScript(
                GetType(),
                Guid.NewGuid().ToString("N"),
                "alert(" + safeMessage + ");",
                true);
        }

        protected void btnSaveAddProduct_Click(object sender, EventArgs e)
        {
            string id = txtAddProductId.Text.Trim();
            string name = txtAddProductName.Text.Trim();
            string brand = txtAddProductBrand.Text.Trim();
            string category = ddlAddProductCategory.SelectedValue;

            decimal price;
            int stock;

            // Kiểm tra thông tin bắt buộc.
            if (string.IsNullOrWhiteSpace(id) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(brand) ||
                string.IsNullOrWhiteSpace(category))
            {
                ShowAddProductMessage(
                    "Vui lòng nhập đầy đủ mã, tên, thương hiệu và danh mục.",
                    true);
                return;
            }

            // Kiểm tra giá.
            if (!decimal.TryParse(
                    txtAddProductPrice.Text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out price) || price < 0)
            {
                ShowAddProductMessage("Giá sản phẩm không hợp lệ.", true);
                return;
            }

            // Kiểm tra tồn kho.
            if (!int.TryParse(txtAddProductStock.Text, out stock) || stock < 0)
            {
                ShowAddProductMessage(
                    "Tồn kho phải là số nguyên không âm.",
                    true);
                return;
            }

            // Kiểm tra ảnh nếu người dùng chọn ảnh.
            if (fuAddProductImage.HasFile)
            {
                string extension = Path.GetExtension(
                    fuAddProductImage.FileName).ToLowerInvariant();

                string[] allowedExtensions =
                {
                    ".jpg", ".jpeg", ".png", ".webp"
                };

                if (!allowedExtensions.Contains(extension))
                {
                    ShowAddProductMessage(
                        "Chỉ chấp nhận ảnh JPG, JPEG, PNG hoặc WEBP.",
                        true);
                    return;
                }

                if (fuAddProductImage.PostedFile.ContentLength > 5 * 1024 * 1024)
                {
                    ShowAddProductMessage(
                        "Ảnh không được vượt quá 5 MB.",
                        true);
                    return;
                }
            }

            Application.Lock();

            try
            {
                List<Product> products =
                    Application["Products"] as List<Product>;

                if (products == null)
                {
                    ShowAddProductMessage(
                        "Không lấy được danh sách sản phẩm.",
                        true);
                    return;
                }

                // Không cho phép trùng mã sản phẩm.
                bool duplicate = products.Any(p =>
                    string.Equals(
                        Convert.ToString(p.Id),
                        id,
                        StringComparison.OrdinalIgnoreCase));

                if (duplicate)
                {
                    ShowAddProductMessage(
                        "Mã sản phẩm đã tồn tại. Vui lòng nhập mã khác.",
                        true);
                    return;
                }

                // Lưu ảnh vào thư mục products.
                string imagePath = "images/logo-peesee.png";

                if (fuAddProductImage.HasFile)
                {
                    string folderPath =
                        Server.MapPath("~/images/products/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName =
                        "admin-" + Guid.NewGuid().ToString("N")
                        + Path.GetExtension(
                            fuAddProductImage.FileName).ToLowerInvariant();

                    string fullPath = Path.Combine(folderPath, fileName);

                    fuAddProductImage.SaveAs(fullPath);

                    imagePath = "images/products/" + fileName;
                }

                // Tạo sản phẩm mới.
                Product newProduct = new Product
                {
                    Id = id,
                    Name = name,
                    Brand = brand,
                    Category = category,
                    Series = "",
                    Type = "",
                    Price = price,
                    OldPrice = price,
                    Discount = 0,
                    Image = imagePath,
                    Rating = 0,
                    Sold = 0,
                    Stock = stock,
                    Description = "",
                    Specifications = new Dictionary<string, string>()
                };

                products.Add(newProduct);
                Application["Products"] = products;
            }
            catch (Exception)
            {
                ShowAddProductMessage(
                    "Không thể lưu sản phẩm. Vui lòng kiểm tra quyền ghi thư mục ảnh.",
                    true);
                return;
            }
            finally
            {
                Application.UnLock();
            }

            // Cập nhật bảng và Dashboard.
            BindAdminProducts();
            BindAdminCategories();
            BindDashboard();

            // Xóa nội dung form sau khi lưu thành công.
            txtAddProductId.Text = "";
            txtAddProductName.Text = "";
            txtAddProductBrand.Text = "";
            txtAddProductPrice.Text = "";
            txtAddProductStock.Text = "";
            ddlAddProductCategory.SelectedIndex = 0;

            ShowAddProductMessage("Đã thêm sản phẩm thành công.", false);
        }

        private void ShowAddProductMessage(string message, bool reopenModal)
        {
            string safeMessage =
                new System.Web.Script.Serialization.JavaScriptSerializer()
                    .Serialize(message);

            string script = "alert(" + safeMessage + ");";

            if (reopenModal)
            {
                script +=
                    "var modal = document.getElementById('add-product-modal');" +
                    "if (modal) modal.classList.remove('hidden');";
            }

            ClientScript.RegisterStartupScript(
                GetType(),
                "AddProductMessage" + Guid.NewGuid().ToString("N"),
                script,
                true);
        }
        private bool ShowAllProducts
        {
            get
            {
                object value = ViewState["ShowAllProducts"];
                return value != null && (bool)value;
            }
            set
            {
                ViewState["ShowAllProducts"] = value;
            }
        }
        protected void btnToggleProducts_Click(object sender, EventArgs e)
        {
            ShowAllProducts = !ShowAllProducts;

            BindAdminProducts();
        }

        private void BindAdminOrders()
        {
            List<StoreOrder> orders =
                Application["Orders"] as List<StoreOrder>;

            if (orders == null)
            {
                orders = new List<StoreOrder>();
            }

            rptAdminOrders.DataSource = orders
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            rptAdminOrders.DataBind();
        }

        protected int GetOrderQuantity(object items)
        {
            List<StoreOrderItem> orderItems =
                items as List<StoreOrderItem>;

            if (orderItems == null)
            {
                return 0;
            }

            return orderItems.Sum(item => item.Quantity);
        }
        private void BindRecentOrders()
        {
            List<StoreOrder> orders =
                Application["Orders"] as List<StoreOrder>;

            if (orders == null)
            {
                orders = new List<StoreOrder>();
            }

            List<StoreOrder> recentOrders = orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .ToList();

            rptRecentOrders.DataSource = recentOrders;
            rptRecentOrders.DataBind();

            if (recentOrders.Count == 0)
            {
                litRecentOrdersEmpty.Text =
                    "<tr><td colspan='4'>Chưa có đơn hàng.</td></tr>";
            }
            else
            {
                litRecentOrdersEmpty.Text = "";
            }
        }

        protected void rptAdminOrders_ItemDataBound(
            object sender,
            RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            StoreOrder order = e.Item.DataItem as StoreOrder;

            DropDownList ddl =
                e.Item.FindControl("ddlOrderStatus") as DropDownList;

            if (order == null || ddl == null)
            {
                return;
            }

            string status = (order.Status ?? "pending")
                .Trim()
                .ToLowerInvariant();

            // Chuẩn hóa những trạng thái được lưu bằng tiếng Việt.
            switch (status)
            {
                case "chờ xử lý":
                    status = "pending";
                    break;

                case "đang xử lý":
                    status = "processing";
                    break;

                case "đang giao":
                    status = "shipping";
                    break;

                case "hoàn thành":
                case "đã hoàn thành":
                    status = "completed";
                    break;

                case "đã hủy":
                    status = "cancelled";
                    break;
            }

            ListItem selectedItem = ddl.Items.FindByValue(status);

            if (selectedItem != null)
            {
                ddl.ClearSelection();
                selectedItem.Selected = true;
            }
        }

        protected void ddlOrderStatus_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            if (ddl == null)
            {
                return;
            }

            RepeaterItem item = ddl.NamingContainer as RepeaterItem;

            if (item == null)
            {
                return;
            }

            HiddenField hfOrderId =
                item.FindControl("hfAdminOrderId") as HiddenField;

            if (hfOrderId == null ||
                string.IsNullOrWhiteSpace(hfOrderId.Value))
            {
                return;
            }

            string orderId = hfOrderId.Value;
            string newStatus = ddl.SelectedValue;

            // Chỉ chấp nhận những trạng thái hợp lệ.
            string[] allowedStatuses =
            {
                "pending",
                "processing",
                "shipping",
                "completed",
                "cancelled"
            };

            if (!allowedStatuses.Contains(newStatus))
            {
                return;
            }

            bool updated = false;

            Application.Lock();

            try
            {
                List<StoreOrder> orders =
                    Application["Orders"] as List<StoreOrder>;

                if (orders != null)
                {
                    StoreOrder order = orders.FirstOrDefault(o =>
                        string.Equals(
                            Convert.ToString(o.Id),
                            orderId,
                            StringComparison.OrdinalIgnoreCase));

                    if (order != null)
                    {
                        order.Status = newStatus;
                        Application["Orders"] = orders;
                        updated = true;
                    }
                }
            }
            finally
            {
                Application.UnLock();
            }

            if (updated)
            {
                BindAdminOrders();
                BindDashboard();
                BindRecentOrders();

                // Cập nhật dữ liệu cho biểu đồ doanh thu
                BindRevenueOrdersJson();

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "UpdateOrderStatus" + Guid.NewGuid().ToString("N"),
                    "alert('Đã cập nhật trạng thái đơn hàng thành công.');",
                    true);
            }
            else
            {
                BindAdminOrders();

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "UpdateOrderStatusError" + Guid.NewGuid().ToString("N"),
                    "alert('Không tìm thấy đơn hàng cần cập nhật.');",
                    true);
            }
        }

        private void BindRevenueOrdersJson()
        {
            List<StoreOrder> orders =
                Application["Orders"] as List<StoreOrder>;

            if (orders == null)
            {
                orders = new List<StoreOrder>();
            }

            // Chỉ truyền những dữ liệu cần cho biểu đồ.
            var chartOrders = orders.Select(o => new
            {
                createdAt = o.CreatedAt.ToString(
                    "o",
                    CultureInfo.InvariantCulture),

                total = o.Total,
                status = o.Status ?? ""
            }).ToList();

            JavaScriptSerializer serializer =
                new JavaScriptSerializer();

            hfRevenueOrdersJson.Value =
                serializer.Serialize(chartOrders);
        }


        private void BindAdminCategories()
        {
            var categories = Application["Categories"] as List<string>;
            var products = Application["Products"]
                as List<PEESEE_GAMING.Models.Product>;

            if (categories == null)
            {
                return;
            }

            var categoryData = categories
                .Select(category => new
                {
                    Name = GetCategoryDisplayName(category),
                    ProductCount = products == null
                        ? 0
                        : products.Count(p =>
                            string.Equals(
                                p.Category?.Trim(),
                                category.Trim(),
                                StringComparison.OrdinalIgnoreCase
                            ))
                })
                .OrderBy(c => c.Name)
                .ToList();

            rptAdminCategories.DataSource = categoryData;
            rptAdminCategories.DataBind();
        }
        private string GetCategoryDisplayName(string category)
        {
            switch (category.Trim().ToLower())
            {
                case "laptop":
                    return "Laptop";

                case "laptop-gaming":
                    return "Laptop Gaming";

                case "pc-ps":
                    return "PC & PS";

                case "components":
                    return "Linh kiện máy tính";

                case "case":
                    return "Vỏ case";

                case "speaker-microphone":
                    return "Loa & Micro";

                case "monitor":
                    return "Màn hình";

                case "keyboard":
                    return "Bàn phím";

                case "mouse-mousepad":
                    return "Chuột & Lót chuột";

                case "headphone":
                    return "Tai nghe";

                case "chair-desk":
                    return "Ghế & Bàn Gaming";

                default:
                    return category;
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = Request.Form["txtNewCategory"]?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "categoryEmpty",
                    "alert('Vui lòng nhập tên danh mục!');",
                    true
                );
                return;
            }

            var products = Application["Products"]
                as List<PEESEE_GAMING.Models.Product>;

            if (products == null)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "categoryError",
                    "alert('Không thể tải danh sách sản phẩm!');",
                    true
                );
                return;
            }

            bool exists = products.Any(p =>
                !string.IsNullOrWhiteSpace(p.Category) &&
                string.Equals(
                    GetCategoryDisplayName(p.Category).Trim(),
                    categoryName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (exists)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "categoryExists",
                    "alert('Danh mục này đã tồn tại!');",
                    true
                );
                return;
            }

            lock (Application)
            {
                var savedCategories = Application["Categories"] as List<string>;

                if (savedCategories == null)
                {
                    savedCategories = new List<string>();
                    Application["Categories"] = savedCategories;
                }

                bool alreadyExists = savedCategories.Any(c =>
                    string.Equals(
                        GetCategoryDisplayName(c).Trim(),
                        categoryName,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

                if (alreadyExists)
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "categoryExistsAgain",
                        "alert('Danh mục này đã tồn tại!');",
                        true
                    );
                    return;
                }

                savedCategories.Add(categoryName);
            }

            BindAdminCategories();
            BindAddProductCategories();

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "categoryAdded",
                "alert('Đã thêm danh mục thành công!');" +
                "document.getElementById('categoryModal').classList.remove('show');" +
                "document.getElementById('txtNewCategory').value = '';",
                true
            );
        }

        private void BindAddProductCategories()
        {
            var categories = Application["Categories"] as List<string>;

            ddlAddProductCategory.Items.Clear();

            ddlAddProductCategory.Items.Add(
                new ListItem("-- Chọn danh mục --", "")
            );

            if (categories == null)
            {
                return;
            }

            foreach (string category in categories)
            {
                if (string.IsNullOrWhiteSpace(category))
                {
                    continue;
                }

                string categoryName = GetCategoryDisplayName(category);

                ddlAddProductCategory.Items.Add(
                    new ListItem(categoryName, category)
                );
            }
        }

        private void BindAdminCustomers()
        {
            var users = Application["Users"] as List<StoreUser>;

            if (users == null)
            {
                users = new List<StoreUser>();
            }

            var customers = users
                .Where(u => u != null &&
                            string.Equals(
                                u.Role,
                                "user",
                                StringComparison.OrdinalIgnoreCase))
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Role,
                    u.IsLocked,
                    CreatedAt = u.CreatedAt.ToString("dd/MM/yyyy HH:mm")
                })
                .ToList();

            System.Web.Script.Serialization.JavaScriptSerializer serializer =
                new System.Web.Script.Serialization.JavaScriptSerializer();

            string json = serializer.Serialize(customers);

            string script =
                "window.adminCustomers = " + json + ";";

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "loadAdminCustomers",
                script,
                true);
        }

        protected void btnToggleCustomerLock_Click(object sender, EventArgs e)
        {
            string customerId = hfCustomerId.Value;

            if (string.IsNullOrWhiteSpace(customerId))
            {
                return;
            }

            var users = Application["Users"] as List<StoreUser>;

            if (users == null)
            {
                return;
            }

            var customer = users.FirstOrDefault(u =>
                u != null &&
                string.Equals(
                    u.Id,
                    customerId,
                    StringComparison.Ordinal));

            // Chỉ cho phép khóa/mở khóa tài khoản khách hàng.
            if (customer == null ||
                !string.Equals(
                    customer.Role,
                    "user",
                    StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            customer.IsLocked = !customer.IsLocked;
            Application["Users"] = users;

            BindAdminCustomers();
        }
    }
}
