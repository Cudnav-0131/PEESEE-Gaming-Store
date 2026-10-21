<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="PEESEE_GAMING.Admin.Admin" %>

<!DOCTYPE html>
<html lang="vi">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Quản trị - PEESEE</title>

    <link rel="stylesheet" href="<%= ResolveUrl("~/Content/Site.css") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Content/admin.css") %>" />
</head>

<body>
    <form id="formAdmin" runat="server" enctype="multipart/form-data">
        <asp:HiddenField
            ID="hfCustomerId"
            runat="server"
            ClientIDMode="Static" />

        <asp:Button
            ID="btnToggleCustomerLock"
            runat="server"
            Text="Cập nhật trạng thái"
            OnClick="btnToggleCustomerLock_Click"
            Style="display:none;" />
        <div class="admin-layout">

            <!-- SIDEBAR -->
            <aside class="admin-sidebar">

                <div class="admin-logo">
                    <img src="../images/logo-peesee.png" alt="PEESEE">
                </div>

                <nav class="admin-nav">

                    <a href="#dashboard" class="admin-nav-item active" data-section="dashboard">
                        <img src="../images/dashboard.png" alt="Dashboard">
                        <span>Dashboard</span>
                    </a>

                    <a href="#products" class="admin-nav-item" data-section="products">
                        <img src="../images/products.png" alt="Sản phẩm">
                        <span>Sản phẩm</span>
                    </a>

                    <a href="#categories" class="admin-nav-item" data-section="categories">
                        <img src="../images/categories.png" alt="Danh mục">
                        <span>Danh mục</span>
                    </a>

                    <a href="#orders" class="admin-nav-item" data-section="orders">
                        <img src="../images/orders.png" alt="Đơn hàng">
                        <span>Đơn hàng</span>
                    </a>

                    <a href="#customers" class="admin-nav-item" data-section="customers">
                        <img src="../images/customers.png" alt="Khách hàng">
                        <span>Khách hàng</span>
                    </a>
                    <a href="#statistics" class="admin-nav-item" data-section="statistics">
                        <img src="../images/analytics.png" alt="Thống kê">
                        <span>Thống kê</span>
                    </a>

                    <a href="#" class="admin-nav-item">
                        <img src="../images/settings.png" alt="Cài đặt">
                        <span>Cài đặt</span>
                    </a>

                </nav>

                <div class="admin-account" id="admin-account">

                    <button type="button" id="admin-account-btn" class="admin-account-btn">

                        <img src="../images/logo-user.png" alt="Tài khoản">

                        <div class="admin-account-info">
                            <strong id="admin-username">Admin</strong>
                            <span>Quản trị viên</span>
                        </div>

                        <span class="admin-account-arrow">⌃</span>

                    </button>

                    <div id="admin-account-menu" class="admin-account-menu hidden">

                        <button type="button" id="admin-logout-btn">
                            Đăng xuất
                        </button>

                    </div>

                </div>

            </aside>

            <!-- PHẦN NỘI DUNG -->
            <div class="admin-main">

                <!-- HEADER ADMIN -->
                <header class="admin-header">

                    <div class="admin-search">
                        <span>⌕</span>
                        <input type="text" id="admin-search-input" placeholder="Tìm kiếm...">
                    </div>

                    <div class="admin-header-right">

                        <button type="button" class="admin-notification">
                            <img src="../images/bell.png" alt="Thông báo">
                        </button>

                    </div>

                </header>

                <main class="admin-page">
                    <div class="admin-container">
                        <section class="admin-dashboard admin-section" id="dashboard">

                            <div class="dashboard-card">
                                <div class="dashboard-card-info">
                                    <span>Tổng sản phẩm</span>
                                    <strong>
                                        <asp:Label ID="lblTotalProducts"
                                            runat="server" Text="0" />
                                    </strong>
                                </div>
                            </div>

                            <div class="dashboard-card">
                                <div class="dashboard-card-info">
                                    <span>Tổng đơn hàng</span>
                                    <strong>
                                        <asp:Label ID="lblTotalOrders"
                                            runat="server" Text="0" />
                                    </strong>
                                </div>
                            </div>

                            <div class="dashboard-card">
                                <div class="dashboard-card-info">
                                    <span>Đơn hoàn thành</span>
                                    <strong>
                                        <asp:Label ID="lblCompletedOrders"
                                            runat="server" Text="0" />
                                    </strong>
                                </div>
                            </div>

                            <div class="dashboard-card">
                                <div class="dashboard-card-info">
                                    <span>Doanh thu</span>
                                    <strong>
                                        <asp:Label ID="lblTotalRevenue"
                                            runat="server" Text="0đ" />
                                    </strong>
                                </div>
                            </div>

                        </section>
                        <section class="dashboard-chart-card admin-section" id="revenue-section">

                            <div class="dashboard-chart-header">

                                <div>
                                    <h2>Doanh thu</h2>
                                    <p>Thống kê doanh thu theo các đơn hàng đã hoàn thành.</p>
                                </div>

                                <select id="revenue-period">
                                    <option value="all">Tất cả</option>
                                    <option value="month">Theo tháng</option>
                                    <option value="week">Theo tuần</option>
                                </select>

                            </div>

                            <div class="revenue-chart-box">
                                <div class="revenue-chart" id="revenue-chart">
                                    <div class="chart-placeholder">
                                        Biểu đồ doanh thu
                                    </div>
                                </div>

                                <asp:HiddenField
                                    ID="hfRevenueOrdersJson"
                                    runat="server"
                                    ClientIDMode="Static" />
                            </div>

                        </section>
                        <section class="dashboard-recent-orders admin-section" id="recent-orders">

                            <div class="dashboard-recent-header">
                                <div>
                                    <h2>Đơn hàng gần đây</h2>
                                    <p>Những đơn hàng mới nhất của cửa hàng.</p>
                                </div>

                                <a href="#orders" class="view-all-orders" data-section="orders">
                                    Xem tất cả
                                </a>
                            </div>

                            <div class="recent-orders-table-wrapper">

                                <table class="recent-orders-table">

                                    <thead>
                                        <tr>
                                            <th>Mã đơn</th>
                                            <th>Khách hàng</th>
                                            <th>Tổng tiền</th>
                                            <th>Trạng thái</th>
                                        </tr>
                                    </thead>
                                    
                                    <tbody>
                                        <asp:Repeater ID="rptRecentOrders" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#: Eval("Id") %></td>
                                                    <td><%#: Eval("CustomerName") %></td>
                                                    <td><%#: Eval("Total", "{0:N0}") %>đ</td>
                                                    <td><%#: Eval("Status") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Literal ID="litRecentOrdersEmpty" runat="server" />
                                    </tbody>
                                    
                                </table>

                            </div>

                        </section>
                        <section class="admin-products admin-section hidden" id="products">
                            <div class="admin-section-header">
                                <div>
                                    <h2>Quản lý sản phẩm</h2>
                                    <p>Danh sách sản phẩm hiện có trong cửa hàng.</p>
                                </div>

                                <button type="button" class="admin-add-btn" id="open-add-product-btn">
                                    + Thêm sản phẩm
                                </button>
                            </div>

                            <div class="admin-table-wrapper" >
                                <table class="admin-table">
                                    <thead>
                                        <tr>
                                            <th>Mã SP</th>
                                            <th>Tên sản phẩm</th>
                                            <th>Danh mục</th>
                                            <th>Thương hiệu</th>
                                            <th>Giá</th>
                                            <th>Tồn kho</th>
                                            <th>Thao tác</th>
                                        </tr>
                                    </thead>
                                    
                                    <tbody>
                                        <asp:Repeater ID="rptAdminProducts" runat="server"
                                            OnItemCommand="rptAdminProducts_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#: Eval("Id") %></td>

                                                    <td><%#: Eval("Name") %></td>

                                                    <td><%#: Eval("Category") %></td>

                                                    <td><%#: Eval("Brand") %></td>

                                                    <td>
                                                        <%#: Eval("Price", "{0:N0}") %>đ
                                                    </td>

                                                    <td><%#: Eval("Stock") %></td>
                                                    
                                                    <td>
                                                        <asp:Button
                                                            ID="btnEditProduct"
                                                            runat="server"
                                                            Text="Sửa"
                                                            CssClass="admin-edit-btn"
                                                            CommandName="EditProduct"
                                                            CommandArgument='<%# Eval("Id") %>'
                                                            CausesValidation="false" />

                                                        <asp:Button
                                                            ID="btnDeleteProduct"
                                                            runat="server"
                                                            Text="Xóa"
                                                            CssClass="admin-delete-btn"
                                                            CommandName="DeleteProduct"
                                                            CommandArgument='<%# Eval("Id") %>'
                                                            CausesValidation="false"
                                                            OnClientClick="return confirm('Bạn có chắc muốn xóa sản phẩm này không?');" />
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>

                                </table>
                            </div>
                            
                            <div class="product-list-footer">

                                <asp:Label ID="lblProductListCount"
                                    runat="server"
                                    CssClass="product-list-count"
                                    Text="Hiển thị 8 sản phẩm" />

                                <asp:Button ID="btnToggleProducts"
                                    runat="server"
                                    Text="Xem thêm"
                                    CssClass="toggle-products-btn"
                                    OnClick="btnToggleProducts_Click"
                                    CausesValidation="false" />

                            </div>

                        </section>
                        <!-- POPUP THÊM SẢN PHẨM -->
                        <div id="add-product-modal" class="add-product-modal hidden">

                            <div class="add-product-overlay"></div>

                            <div class="add-product-box">

                                <div class="add-product-header">
                                    <h2>Thêm sản phẩm</h2>

                                    <button type="button" id="close-add-product" class="add-product-close">
                                        ×
                                    </button>
                                </div>

                                <div id="add-product-form">

                                    
                                    <div class="add-product-grid">

                                        <div class="form-group">
                                            <label for="txtAddProductId">Mã sản phẩm</label>
                                            <asp:TextBox ID="txtAddProductId"
                                                runat="server"
                                                ClientIDMode="Static" />
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAddProductName">Tên sản phẩm</label>
                                            <asp:TextBox ID="txtAddProductName"
                                                runat="server"
                                                ClientIDMode="Static" />
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAddProductBrand">Thương hiệu</label>
                                            <asp:TextBox ID="txtAddProductBrand"
                                                runat="server"
                                                ClientIDMode="Static" />
                                        </div>

                                        <div class="form-group">
                                            <label for="ddlAddProductCategory">Danh mục</label>
                                            
                                            <asp:DropDownList ID="ddlAddProductCategory"
                                                runat="server"
                                                ClientIDMode="Static">
                                                <asp:ListItem Value="">-- Chọn danh mục --</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="form-group">
                                            <label for="txtAddProductPrice">Giá bán</label>
                                            <asp:TextBox ID="txtAddProductPrice"
                                                runat="server"
                                                ClientIDMode="Static"
                                                TextMode="Number" />
                                        </div>

                                        <div class="form-group">
                                            <label for="txtAddProductStock">Tồn kho</label>
                                            <asp:TextBox ID="txtAddProductStock"
                                                runat="server"
                                                ClientIDMode="Static"
                                                TextMode="Number" />
                                        </div>

                                        <div class="form-group">
                                            <label for="fuAddProductImage">Ảnh sản phẩm</label>
                                            <asp:FileUpload ID="fuAddProductImage"
                                                runat="server"
                                                ClientIDMode="Static"
                                                accept="image/*" />

                                            <asp:Image ID="imgAddProductPreview"
                                                runat="server"
                                                ClientIDMode="Static"
                                                AlternateText="Ảnh xem trước"
                                                CssClass="product-image-preview hidden" />
                                        </div>

                                    </div>

                                    <div class="add-product-actions">

                                        <button type="button" id="cancel-add-product-btn" class="admin-cancel-btn">
                                            Hủy
                                        </button>

                                        <asp:Button
                                            ID="btnSaveAddProduct"
                                            runat="server"
                                            Text="Lưu sản phẩm"
                                            CssClass="admin-save-btn"
                                            OnClick="btnSaveAddProduct_Click"
                                            CausesValidation="false" />

                                    </div>

                                </div>

                            </div>
                        </div>
                        <section class="admin-orders admin-section hidden" id="orders">

                            <div class="admin-section-header">
                                <div>
                                    <h2>Quản lý đơn hàng</h2>
                                    <p>Danh sách các đơn hàng khách hàng đã đặt.</p>
                                </div>
                            </div>

                            <div class="admin-table-wrapper">
                                <table class="admin-table">
                                    <thead>
                                        <tr>
                                            <th>Mã đơn</th>
                                            <th>Khách hàng</th>
                                            <th>Số điện thoại</th>
                                            <th>Số sản phẩm</th>
                                            <th>Tổng tiền</th>
                                            <th>Thanh toán</th>
                                            <th>Trạng thái</th>
                                            <th>Thao tác</th>
                                        </tr>
                                    </thead>
                                    
                                    <tbody>
                                        <asp:Repeater ID="rptAdminOrders" runat="server"
                                            OnItemDataBound="rptAdminOrders_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%#: Eval("Id") %>

                                                        <asp:HiddenField
                                                            ID="hfAdminOrderId"
                                                            runat="server"
                                                            Value='<%# Eval("Id") %>' />
                                                    </td>

                                                    <td><%#: Eval("CustomerName") %></td>

                                                    <td><%#: Eval("Phone") %></td>

                                                    <td>
                                                        <%#: GetOrderQuantity(Eval("Items")) %>
                                                    </td>

                                                    <td>
                                                        <%#: Eval("Total", "{0:N0}") %>đ
                                                    </td>

                                                    <td><%#: Eval("PaymentMethod") %></td>

                                                    <td>
                                                        <asp:DropDownList
                                                            ID="ddlOrderStatus"
                                                            runat="server"
                                                            CssClass="order-status-select"
                                                            AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">

                                                            <asp:ListItem Value="pending">Chờ xử lý</asp:ListItem>
                                                            <asp:ListItem Value="processing">Đang xử lý</asp:ListItem>
                                                            <asp:ListItem Value="shipping">Đang giao</asp:ListItem>
                                                            <asp:ListItem Value="completed">Hoàn thành</asp:ListItem>
                                                            <asp:ListItem Value="cancelled">Đã hủy</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>

                                                    <td>—</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>

                                </table>
                            </div>

                        </section>
                        <section class="admin-customers admin-section hidden" id="customers">

                            <div class="admin-section-header">

                                <div>
                                    <h2>Quản lý tài khoản</h2>
                                    <p>Danh sách tài khoản người dùng trong hệ thống.</p>
                                </div>

                                <div class="customer-search-box">
                                    <input type="text" id="customer-search-input" placeholder="Tìm theo tên hoặc email...">
                                </div>

                            </div>

                            <div class="admin-table-wrapper">
                                <table class="admin-table">

                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Tên đăng nhập</th>
                                            <th>Email</th>
                                            <th>Vai trò</th>
                                            <th>Ngày tạo</th>
                                            <th>Thao tác</th>
                                        </tr>
                                    </thead>

                                    <tbody id="admin-customer-list">
                                    </tbody>

                                </table>
                            </div>

                        </section>
                        <section class="admin-categories admin-section hidden" id="categories">

                            <div class="admin-section-header">
                                <div>
                                    <h2>Quản lý danh mục</h2>
                                    <p>Danh sách danh mục sản phẩm trong cửa hàng.</p>
                                </div>
                                                                
                                <button type="button" class="admin-add-btn" id="btnOpenCategoryModal">
                                    + Thêm danh mục
                                </button>

                            </div>

                            <div class="admin-table-wrapper">
                                <table class="admin-table">
                                    <thead>
                                        <tr>
                                            <th>Danh mục</th>
                                            <th>Số sản phẩm</th>
                                            <th>Trạng thái</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <asp:Repeater ID="rptAdminCategories" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#: Eval("Name") %></td>
                                                    <td><%#: Eval("ProductCount") %></td>
                                                    <td>
                                                        <span class="status-badge status-active">
                                                            Đang hoạt động
                                                        </span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div class="admin-modal-overlay" id="categoryModal">
                                <div class="admin-modal">
                                    <div class="admin-modal-header">
                                        <h3>Thêm danh mục mới</h3>
                                        <button type="button" class="admin-modal-close"
                                                id="btnCloseCategoryModal">&times;</button>
                                    </div>

                                    <div class="admin-modal-body">
                                        <label for="txtNewCategory">Tên danh mục</label>
                                        <input type="text" id="txtNewCategory"
                                            name="txtNewCategory"
                                            placeholder="Nhập tên danh mục..."
                                            maxlength="100" />
                                        <p id="categoryModalError" class="admin-form-error"></p>
                                    </div>

                                    <div class="admin-modal-footer">
                                        <button type="button" class="admin-modal-cancel"
                                                id="btnCancelCategoryModal">Hủy</button>
                                        <asp:Button
                                            ID="btnSaveCategory"
                                            runat="server"
                                            Text="Lưu danh mục"
                                            CssClass="admin-add-btn"
                                            OnClick="btnAddCategory_Click"
                                            CausesValidation="false"
                                            UseSubmitBehavior="true" />
                                    </div>
                                </div>
                            </div>
                        </section>
                        
                        

                        <section class="admin-statistics admin-section hidden" id="statistics">

                            <div class="admin-section-header">
                                <div>
                                    <h2>Thống kê</h2>
                                    <p>Tổng hợp tình hình hoạt động của cửa hàng.</p>
                                </div>
                            </div>

                            <div class="statistics-cards">

                                <div class="statistics-card">
                                    <span>Tổng doanh thu</span>
                                    <strong id="statistics-revenue">0đ</strong>
                                </div>

                                <div class="statistics-card">
                                    <span>Đơn hoàn thành</span>
                                    <strong id="statistics-completed">0</strong>
                                </div>

                                <div class="statistics-card">
                                    <span>Đơn đã hủy</span>
                                    <strong id="statistics-cancelled">0</strong>
                                </div>

                                <div class="statistics-card">
                                    <span>Giá trị đơn trung bình</span>
                                    <strong id="statistics-average">0đ</strong>
                                </div>

                            </div>

                            <div class="statistics-panels">

                                <div class="statistics-panel">
                                    <h3>Sản phẩm bán chạy</h3>

                                    <div class="statistics-table-wrapper">
                                        <table class="statistics-table">
                                            <thead>
                                                <tr>
                                                    <th>Sản phẩm</th>
                                                    <th>Số lượng bán</th>
                                                </tr>
                                            </thead>

                                            <tbody id="best-selling-products">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="statistics-panel">
                                    <h3>Trạng thái đơn hàng</h3>

                                    <div id="order-status-statistics">
                                    </div>
                                </div>

                            </div>

                        </section>

                    </div>
                </main>
                <!-- POPUP CHI TIẾT ĐƠN HÀNG -->
                <div id="order-detail-modal" class="order-detail-modal hidden">

                    <div class="order-detail-overlay"></div>

                    <div class="order-detail-box">

                        <div class="order-detail-header">
                            <h2>Chi tiết đơn hàng</h2>

                            <button type="button" id="close-order-detail" class="order-detail-close">
                                ×
                            </button>
                        </div>

                        <div id="order-detail-content">
                            <!-- Nội dung đơn hàng sẽ được JS đưa vào đây -->
                        </div>

                        <div class="order-detail-footer">
                            <button type="button" id="close-order-detail-btn" class="admin-cancel-btn">
                                Đóng
                            </button>
                        </div>

                    </div>
                </div>
                <!-- POPUP SỬA SẢN PHẨM -->
                <div id="edit-product-modal" class="edit-product-modal hidden">

                    <div class="edit-product-overlay"></div>

                    <div class="edit-product-box">

                        <div class="edit-product-header">
                            <h2>Sửa sản phẩm</h2>

                            <button type="button" id="close-edit-product" class="edit-product-close">
                                ×
                            </button>
                        </div>

                        <div id="edit-product-form">

                            
                            <div class="edit-product-grid">

                                <div class="form-group">
                                    <label for="txtEditProductId">Mã sản phẩm</label>
                                    <asp:TextBox ID="txtEditProductId"
                                        runat="server"
                                        ClientIDMode="Static"
                                        ReadOnly="true" />
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProductName">Tên sản phẩm</label>
                                    <asp:TextBox ID="txtEditProductName"
                                        runat="server"
                                        ClientIDMode="Static" />
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProductBrand">Thương hiệu</label>
                                    <asp:TextBox ID="txtEditProductBrand"
                                        runat="server"
                                        ClientIDMode="Static" />
                                </div>

                                <div class="form-group">
                                    <label for="ddlEditProductCategory">Danh mục</label>
                                    <asp:DropDownList ID="ddlEditProductCategory"
                                        runat="server"
                                        ClientIDMode="Static">
                                        <asp:ListItem Value="laptop">Laptop</asp:ListItem>
                                        <asp:ListItem Value="laptop-gaming">Laptop Gaming</asp:ListItem>
                                        <asp:ListItem Value="pc-ps">PC / PS</asp:ListItem>
                                        <asp:ListItem Value="components">Linh kiện</asp:ListItem>
                                        <asp:ListItem Value="case">Case</asp:ListItem>
                                        <asp:ListItem Value="speaker-microphone">Loa / Micro</asp:ListItem>
                                        <asp:ListItem Value="monitor">Màn hình</asp:ListItem>
                                        <asp:ListItem Value="keyboard">Bàn phím</asp:ListItem>
                                        <asp:ListItem Value="mouse-mousepad">Chuột / Lót chuột</asp:ListItem>
                                        <asp:ListItem Value="headphone">Tai nghe</asp:ListItem>
                                        <asp:ListItem Value="chair-desk">Ghế / Bàn</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProductPrice">Giá</label>
                                    <asp:TextBox ID="txtEditProductPrice"
                                        runat="server"
                                        ClientIDMode="Static"
                                        TextMode="Number" />
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProductOldPrice">Giá cũ</label>
                                    <asp:TextBox ID="txtEditProductOldPrice"
                                        runat="server"
                                        ClientIDMode="Static"
                                        TextMode="Number" />
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProductStock">Tồn kho</label>
                                    <asp:TextBox ID="txtEditProductStock"
                                        runat="server"
                                        ClientIDMode="Static"
                                        TextMode="Number" />
                                </div>

                                <div class="form-group">
                                    <label for="fuEditProductImage">Ảnh sản phẩm</label>

                                    <asp:Image ID="imgEditProductPreview"
                                        runat="server"
                                        ClientIDMode="Static"
                                        AlternateText="Ảnh sản phẩm"
                                        CssClass="product-image-preview hidden" />

                                    <asp:FileUpload ID="fuEditProductImage"
                                        runat="server"
                                        ClientIDMode="Static" />

                                    <asp:HiddenField ID="hfEditProductImage"
                                        runat="server"
                                        ClientIDMode="Static" />
                                </div>

                                <div class="form-group edit-product-full">
                                    <label for="txtEditProductDescription">Mô tả</label>
                                    <asp:TextBox ID="txtEditProductDescription"
                                        runat="server"
                                        ClientIDMode="Static"
                                        TextMode="MultiLine" />
                                </div>

                            </div>

                            <div class="edit-product-actions">

                                <button type="button" id="cancel-edit-product" class="admin-cancel-btn">
                                    Hủy
                                </button>

                                <asp:Button
                                    ID="btnSaveEditProduct"
                                    runat="server"
                                    Text="Lưu thay đổi"
                                    CssClass="admin-save-btn"
                                    OnClick="btnSaveEditProduct_Click"
                                    CausesValidation="false" />

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="<%= ResolveUrl("~/Scripts/admin-navigation.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/admin-revenue-chart.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/admin-customers.js") %>"></script>
</body>

</html>
