using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PEESEE_GAMING
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            var cart = Session["Cart"] as Dictionary<string, int>;

            int totalQuantity = 0;

            if (cart != null)
            {
                foreach (int quantity in cart.Values)
                {
                    totalQuantity += quantity;
                }
            }

            lblCartCount.Text = totalQuantity.ToString();
            // Kiểm tra tài khoản đang đăng nhập
            var currentUser =
                Session["CurrentUser"] as PEESEE_GAMING.Models.StoreUser;

            phGuestLogin.Visible = currentUser == null;
            phUserAccount.Visible = currentUser != null;
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Chỉ đăng xuất tài khoản, không xóa giỏ hàng
            Session.Remove("CurrentUser");

            Response.Redirect("~/Default.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}