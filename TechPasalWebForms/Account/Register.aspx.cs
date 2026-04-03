using System;
using TechPasalWebForms.Data;

namespace TechPasalWebForms.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var repo = new UserRepository();
            if (repo.EmailExists(txtEmail.Text.Trim()))
            {
                lblError.Text = "Email already registered.";
                lblError.Visible = true;
                return;
            }
            try
            {
                repo.Register(txtUsername.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text);
                lblSuccess.Text = "Account created successfully! <a href='Login.aspx'>Login now</a>";
                lblSuccess.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Registration failed: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}
