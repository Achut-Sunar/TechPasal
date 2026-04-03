using System;
using System.Web;
using System.Web.Security;

namespace TechPasalWebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e) { }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                try
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (ticket != null && !ticket.Expired)
                    {
                        // UserData = "UserId|Role"
                        var parts = ticket.UserData.Split('|');
                        string role = parts.Length > 1 ? parts[1] : "Customer";
                        var roles = new string[] { role };
                        var principal = new System.Security.Principal.GenericPrincipal(
                            new FormsIdentity(ticket), roles);
                        Context.User = principal;
                    }
                }
                catch { }
            }
        }
    }
}
