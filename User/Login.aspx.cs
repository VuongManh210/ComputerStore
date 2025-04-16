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
            string password = txtPassword.Text.Trim();

            try
            {
                DataTable dt = userBLL.GetUserByCredentials(username, password);
                if (dt.Rows.Count > 0)
                {
                    Session["UserId"] = dt.Rows[0]["user_id"].ToString();
                    Session["Role"] = dt.Rows[0]["role"].ToString();
                    Response.Redirect("~/Pages/Home.aspx");
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}