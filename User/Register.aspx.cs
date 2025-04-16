using System;
using ComputerStore.BLL;

namespace ComputerStore.User
{
    public partial class Register : System.Web.UI.Page
    {
        private UserBLL userBLL = new UserBLL();

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            try
            {
                bool result = userBLL.RegisterUser(username, password, email, fullName, phone, address);
                if (result)
                {
                    lblMessage.Text = "Đăng ký thành công! Vui lòng <a href='Login.aspx'>đăng nhập</a>.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Đăng ký thất bại. Vui lòng thử lại.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }
    }
}