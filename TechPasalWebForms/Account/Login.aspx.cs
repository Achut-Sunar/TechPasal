using System;
using System.Web.Security;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "logout")
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var repo = new UserRepository();
            var user = repo.GetByEmail(txtEmail.Text.Trim());
            if (user == null || !repo.ValidatePassword(txtPassword.Text, user.PasswordHash))
            {
                lblError.Text = "Invalid email or password.";
                lblError.Visible = true;
                return;
            }

            // UserData format: "UserId|Role"
            string userData = user.UserId + "|" + user.Role;
            var ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now,
                DateTime.Now.AddMinutes(60), false, userData);
            var encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection
            };
            Response.Cookies.Add(cookie);

            // Validate ReturnUrl to prevent open redirect attacks
            string returnUrl = Request.QueryString["ReturnUrl"];
            if (!IsLocalUrl(returnUrl))
                returnUrl = "~/Default.aspx";
            Response.Redirect(returnUrl);
        }

        private static bool IsLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            return url.StartsWith("/", StringComparison.Ordinal)
                && !url.StartsWith("//", StringComparison.Ordinal)
                && !url.StartsWith("/\\", StringComparison.Ordinal);
        }
    }
}
