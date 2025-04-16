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
            string password = txtPassword.Text.Trim(); // Cần mã hóa trong thực tế
            string email = txtEmail.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            try
            {
                if (userBLL.RegisterUser(username, password, email, fullName, phone, address))
                {
                    lblMessage.Text = "Đăng ký thành công! Vui lòng <a href='Login.aspx'>đăng nhập</a>.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Đăng ký thất bại. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}