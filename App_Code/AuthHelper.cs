using System;
using System.Web;

namespace ComputerStore
{
    public static class AuthHelper
    {
        public static bool IsAuthenticated()
        {
            return HttpContext.Current.Session != null && HttpContext.Current.Session["UserId"] != null;
        }

        public static bool IsAdmin()
        {
            if (!IsAuthenticated())
            {
                return false;
            }
            string role = HttpContext.Current.Session["Role"] != null ? HttpContext.Current.Session["Role"].ToString() : string.Empty;
            return role == "admin";
        }

        public static bool IsUser()
        {
            if (!IsAuthenticated())
            {
                return false;
            }
            string role = HttpContext.Current.Session["Role"] != null ? HttpContext.Current.Session["Role"].ToString() : string.Empty;
            return role == "user";
        }

        public static bool IsShopowner()
        {
            if (!IsAuthenticated())
            {
                return false;
            }
            string role = HttpContext.Current.Session["Role"] != null ? HttpContext.Current.Session["Role"].ToString() : string.Empty;
            return role == "shopowner";
        }

        public static int GetUserId()
        {
            if (!IsAuthenticated())
            {
                throw new UnauthorizedAccessException("Chưa đăng nhập.");
            }
            string userId = HttpContext.Current.Session["UserId"].ToString();
            int result;
            if (!int.TryParse(userId, out result))
            {
                throw new InvalidOperationException("ID người dùng không hợp lệ.");
            }
            return result;
        }

        public static void RequireLogin(HttpResponse response)
        {
            if (!IsAuthenticated())
            {
                response.Redirect("~/User/Login.aspx");
            }
        }

        public static void RequireAdmin(HttpResponse response)
        {
            if (!IsAdmin())
            {
                response.Redirect("~/User/Login.aspx");
            }
        }

        public static void RequireShopowner(HttpResponse response)
        {
            if (!IsShopowner())
            {
                response.Redirect("~/User/Login.aspx");
            }
        }

        public static void RequireUserOrShopowner(HttpResponse response)
        {
            if (!IsUser() && !IsShopowner())
            {
                response.Redirect("~/User/Login.aspx");
            }
        }
    }
}