using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.User
{
    public partial class Login : System.Web.UI.Page
    {
        private UserBLL userBLL = new UserBLL();

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim(); // Trong thực tế, cần mã hóa

            DataTable dt = userBLL.GetUserByCredentials(username, password);
            if (dt.Rows.Count > 0)
            {
                Session["UserId"] = dt.Rows[0]["user_id"];
                Session["Username"] = username;
                Session["Role"] = dt.Rows[0]["role"].ToString();
                if (Session["Role"].ToString() == "admin")
                    Response.Redirect("../Admin/ProductManager.aspx");
                else
                    Response.Redirect("../Pages/Home.aspx");
            }
            else
            {
                lblMessage.Text = "Sai tên đăng nhập hoặc mật khẩu!";
            }
        }
    }
}